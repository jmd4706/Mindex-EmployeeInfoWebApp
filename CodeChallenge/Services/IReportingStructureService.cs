using CodeChallenge.Models;

namespace CodeChallenge.Services;

public interface IReportingStructureService
{
    ReportingStructure Create(string employeeId);
}