using System.Threading.Tasks;

namespace WebApi.Services
{
    internal class CalculatorService : ICalculatorService
    {
        public Task<int> AddNumberAsync(int a, int b)
        {
            return Task.FromResult(a + b);
        }
    }
}
