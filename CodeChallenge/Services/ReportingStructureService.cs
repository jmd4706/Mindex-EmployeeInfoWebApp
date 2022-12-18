using System;
using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services;

public class ReportingStructureService : IReportingStructureService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ILogger<ReportingStructureService> _logger;

    public ReportingStructureService(ILogger<ReportingStructureService> logger, IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
        _logger = logger;
    }
    
    public ReportingStructure Create(string employeeId)
    {
        if(!string.IsNullOrEmpty(employeeId))
        {
            var employee = _employeeRepository.GetById(employeeId);
            var numOfReports = GetNumOfReports(employee);
            return new ReportingStructure()
            {
                Employee = employee,
                NumberOfReports = numOfReports
            };
        }

        return null;
        
    }

    private int GetNumOfReports(Employee employee)      // recursive helper method
    {
        var directReports = employee.DirectReports;
        if (directReports == null) return 0;
        var numOfReports = directReports.Count;
        foreach (var directReport in directReports)
        {
            numOfReports += GetNumOfReports(directReport);
        }

        return numOfReports;
    }
}