using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;

namespace CodeChallenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetById(string id)
        {
            var employee = _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
            if (employee != null)
            {
                _employeeContext.Entry(employee).Collection(e => e.DirectReports).Load();
                var dirReports = new List<Employee>();

                foreach (var e in employee.DirectReports)
                {
                    dirReports.Add(GetById(e.EmployeeId));      // recursively builds the directReports map for this employee
                }
                if (dirReports.Count == 0) dirReports = null;

                employee.DirectReports = dirReports;
            }
            return employee;
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
