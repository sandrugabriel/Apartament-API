using ApartamentAPI.Models;
using ApartamentAPI.Dto;

namespace ApartamentAPI.Service.interfaces
{
    public interface IApartamentCommandService
    {

        Task<Apartament> CreateApartament(CreateRequest request);

        Task<Apartament> UpdateApartament(int id,UpdateRequest request);

        Task<Apartament> DeleteApartament(int id);

    }
}
