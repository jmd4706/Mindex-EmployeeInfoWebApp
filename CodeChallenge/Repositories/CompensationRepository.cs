using System;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly CompensationContext _compensationContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, EmployeeContext employeeContext, CompensationContext compensationContext)
        {
            _employeeContext = employeeContext;
            _compensationContext = compensationContext;
            _logger = logger;
        }
        
        public Compensation Add(Compensation compensation)
        {
            compensation.CompensationId = Guid.NewGuid().ToString();
            _compensationContext.Compensations.Add(compensation);
            return compensation;
        }
        
        public Compensation GetByEmployeeId(string employeeId)
        {
            var compensation = _compensationContext.Compensations.SingleOrDefault(e => e.Employee.EmployeeId == employeeId);
            if(compensation != null) _compensationContext.Entry(compensation).Reference(e => e.Employee).Load();
            return compensation;
        }

        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }
    }
}
