using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services;

public class RandomService : IRandomService
{
    private Random random;

    public RandomService()
    {
        random = new Random(Guid.NewGuid().GetHashCode());
    }

    public async Task<int> GetRandom()
    {
        return random.Next(100);
    }
}
