using ApartamentAPI.Data;
using ApartamentAPI.Dto;
using ApartamentAPI.Models;
using ApartamentAPI.Repository.interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;

namespace ApartamentAPI.Repository
{
    public class RepositoryApartament : IRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public RepositoryApartament(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Apartament>> GetAllAsync()
        {
            return await _context.Apartaments.ToListAsync();
        }

        public async Task<Apartament> GetById(int id)
        {
            List<Apartament> apartaments = await _context.Apartaments.ToListAsync();

            for(int i=0; i < apartaments.Count; i++)
            {
                if (apartaments[i].Id == id) return apartaments[i];
            }
            return null;
        }

        public async Task<List<Apartament>> GetByPrice(int price)
        {
            List<Apartament> apartaments = await _context.Apartaments.ToListAsync();

            List<Apartament> nou = new List<Apartament>();
            for(int i=0;i<apartaments.Count;i++)
            {
                if (apartaments[i].Price == price) nou.Add(apartaments[i]);
            }

            return nou;
        }


        public async Task<Apartament> Create(CreateRequest request)
        {

            var apart = _mapper.Map<Apartament>(request);

            _context.Apartaments.Add(apart);

            await _context.SaveChangesAsync();

            return apart;

        }

        public async Task<Apartament> Update(int id, UpdateRequest request)
        {

            var apart = await _context.Apartaments.FindAsync(id);

            apart.Price = request.Price ?? apart.Price;
            apart.Address = request.Address ?? apart.Address;
            apart.Room = request.Room ?? apart.Room;

            _context.Apartaments.Update(apart);

            await _context.SaveChangesAsync();

            return apart;

        }

        public async Task<Apartament> DeleteById(int id)
        {
            var apart = await _context.Apartaments.FindAsync(id);

            _context.Apartaments.Remove(apart);

            await _context.SaveChangesAsync();

            return apart;
        }



    }
}
