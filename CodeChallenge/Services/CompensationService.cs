using System;
using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Services;

public class CompensationService : ICompensationService
{
    private readonly ICompensationRepository _compensationRepository;
    private readonly ILogger<CompensationService> _logger;

    public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository)
    {
        _compensationRepository = compensationRepository;
        _logger = logger;
    }
    
    public Compensation Create(Compensation compensation)
    {
        if(compensation != null)
        {
            _compensationRepository.Add(compensation);
            _compensationRepository.SaveAsync().Wait();
            return compensation;
        }

        return null;
    }
    public Compensation GetByEmployeeId(string employeeId)
    {
        if(!String.IsNullOrEmpty(employeeId))
        {
            return _compensationRepository.GetByEmployeeId(employeeId);
        }

        return null;
    }
}