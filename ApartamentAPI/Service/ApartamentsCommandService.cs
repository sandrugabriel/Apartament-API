using ApartamentAPI.Dto;
using ApartamentAPI.Models;
using ApartamentAPI.Repository.interfaces;
using ApartamentAPI.Service.interfaces;
using ApartamentAPI.Exceptions;

namespace ApartamentAPI.Service
{
    public class ApartamentsCommandService : IApartamentCommandService
    {

        private IRepository _repository;

        public ApartamentsCommandService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Apartament> CreateApartament(CreateRequest request)
        {

            if (request.Price <= 500)
            {
                throw new InvalidPrice(Constants.Constants.InvalidPrice);
            }

            var apartamet = await _repository.Create(request);

            return apartamet;
        }

        public async Task<Apartament> UpdateApartament(int id, UpdateRequest request)
        {

            var apartament = await _repository.GetById(id);
            if (apartament == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }


            if (apartament.Price <= 500)
            {
                throw new InvalidPrice(Constants.Constants.InvalidPrice);
            }
            apartament = await _repository.Update(id, request);
            return apartament;
        }

        public async Task<Apartament> DeleteApartament(int id)
        {

            var apartament = await _repository.GetById(id);
            if (apartament == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }
            apartament = await _repository.DeleteById(id);
            return apartament;
        }

        
    }
}
