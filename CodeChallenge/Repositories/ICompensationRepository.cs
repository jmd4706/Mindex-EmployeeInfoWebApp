using System.Threading.Tasks;
using CodeChallenge.Models;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        Compensation GetByEmployeeId(string employeeId);
        Compensation Add(Compensation compensation);
        Task SaveAsync();
    }
}
