using ApartamentAPI.Dto;
using ApartamentAPI.Models;
using System;

namespace ApartamentAPI.Repository.interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<Apartament>> GetAllAsync();

        Task<List<Apartament>> GetByPrice(int name);

        Task<Apartament> GetById(int id);


        Task<Apartament> Create(CreateRequest request);

        Task<Apartament> Update(int id, UpdateRequest request);

        Task<Apartament> DeleteById(int id);

    }
}
