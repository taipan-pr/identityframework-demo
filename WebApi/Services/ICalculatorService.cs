using System.Threading.Tasks;

namespace WebApi.Services
{
    public interface ICalculatorService
    {
        Task<int> AddNumberAsync(int a, int b);
    }
}
