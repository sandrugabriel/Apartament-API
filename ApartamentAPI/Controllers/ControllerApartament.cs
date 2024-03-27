using ApartamentAPI.Dto;
using ApartamentAPI.Models;
using ApartamentAPI.Repository.interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApartamentAPI.Controllers
{
    [ApiController]
    [Route("api/v1/apartament")]
    public class ControllerApartament : ControllerBase
    {

        private readonly ILogger<ControllerApartament> _logger;

        private IRepository _repository;

        public ControllerApartament(ILogger<ControllerApartament> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("/all")]
        public async Task<ActionResult<IEnumerable<Apartament>>> GetAll()
        {
            var apartaments = await _repository.GetAllAsync();
            return Ok(apartaments);
        }

        [HttpGet("/findByPriceQuery")]
        public async Task<ActionResult<List<Apartament>>> GetByPrice([FromQuery]int price)
        {
            var apartaments = await _repository.GetByPrice(price);
            return Ok(apartaments);
        }

        [HttpGet("/findByIdRoute/{id}")]
        public async Task<ActionResult<Apartament>> GetById([FromRoute]int id)
        {
            var apartament = await _repository.GetById(id);
            return Ok(apartament);
        }


        [HttpPost("/create")]
        public async Task<ActionResult<Apartament>> Create([FromBody] CreateRequest request)
        {
            var apartament = await _repository.Create(request);
            return Ok(apartament);

        }

        [HttpPut("/update")]
        public async Task<ActionResult<Apartament>> Update([FromQuery] int id, [FromBody] UpdateRequest request)
        {
            var apartament = await _repository.Update(id, request);
            return Ok(apartament);
        }

        [HttpDelete("/deleteById")]
        public async Task<ActionResult<Apartament>> DeleteCarById([FromQuery] int id)
        {
            var apartament = await _repository.DeleteById(id);
            return Ok(apartament);
        }


    }
}
