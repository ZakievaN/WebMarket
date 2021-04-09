using WebMarket.ViewModels;

namespace WebMarket.Infrastructure.Services.Interfaces
{
    public interface ICartServices
    {
        void Add(int id);

        void Decrement(int id);

        void Remove(int id);

        void Clear();

        CartViewModel GetViewModel();
    }
}
