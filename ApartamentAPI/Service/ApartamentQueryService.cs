using ApartamentAPI.Models;
using ApartamentAPI.Repository.interfaces;
using ApartamentAPI.Service.interfaces;
using ApartamentAPI.Exceptions;

namespace ApartamentAPI.Service
{
    public class ApartamentQueryService : IApartamentQueryService
    {

        private IRepository _repository;

        public ApartamentQueryService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Apartament>> GetAll()
        {
           var apartaments = await _repository.GetAllAsync();

            if(apartaments.Count() == 0)
            {
                throw new ItemsDoNotExists(Constants.Constants.ItemsDoNotExist);
            }

            return (List<Apartament>)apartaments;
        }

        public async Task<List<Apartament>> GetAllByPrice(int price)
        {
            var apartaments = await _repository.GetByPrice(price);

            if(apartaments.Count == 0)
            {
                throw new ItemsDoNotExists(Constants.Constants.ItemsDoNotExist);
            }

            return apartaments;

        }

        public async Task<Apartament> GetById(int id)
        {
            var apartament = await _repository.GetById(id);

            if(apartament == null)
            {
                throw new ItemsDoNotExists(Constants.Constants.ItemDoesNotExist);
            }

            return apartament;
        }
    }
}
