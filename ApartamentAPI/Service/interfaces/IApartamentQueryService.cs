using ApartamentAPI.Models;

namespace ApartamentAPI.Service.interfaces
{
    public interface IApartamentQueryService
    {

        Task<List<Apartament>> GetAll();

        Task<Apartament> GetById(int id);

        Task<List<Apartament>> GetAllByPrice(int price);

    }
}
