using ApartamentAPI.Controllers.interfaces;
using ApartamentAPI.Dto;
using ApartamentAPI.Exceptions;
using ApartamentAPI.Models;
using ApartamentAPI.Repository.interfaces;
using ApartamentAPI.Service.interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApartamentAPI.Controllers
{

    public class ControllerApartament : ApartamentAPIController
    {

        private IApartamentQueryService _queryService;
        private IApartamentCommandService _commandService;

        public ControllerApartament(IApartamentQueryService queryService,IApartamentCommandService commandService)
        {
            _queryService = queryService;
            _commandService = commandService;
        }

        public override async Task<ActionResult<List<Apartament>>> GetAll()
        {
            try
            {
                var apartaments = await _queryService.GetAll();

                return Ok(apartaments);

            }
            catch (ItemsDoNotExists ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<List<Apartament>>> GetAllByPrice([FromQuery] int price)
        {
            try
            {
                var apartaments = await _queryService.GetAllByPrice(price);
                return Ok(apartaments);
            }
            catch (ItemsDoNotExists ex)
            {
                return NotFound(ex.Message);
            }

        }

        public override async Task<ActionResult<Apartament>> GetById(int id)
        {

            try
            {
                var apartament = await _queryService.GetById(id);
                return Ok(apartament);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }

        public override async Task<ActionResult<Apartament>> CreateApartament(CreateRequest request)
        {
            try
            {
                var apartament = await _commandService.CreateApartament(request);
                return Ok(apartament);
            }
            catch (InvalidPrice ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<Apartament>> UpdateApartament(int id, UpdateRequest request)
        {
            try
            {
                var apartament = await _commandService.UpdateApartament(id,request);
                return Ok(apartament);
            }
            catch(InvalidPrice ex)
            {
                return BadRequest(ex.Message);
            }
            catch(ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<Apartament>> DeleteApartament(int id)
        {
            try
            {
                var apartament = await _commandService.DeleteApartament(id);
                return Ok(apartament);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }



        /*private readonly ILogger<ControllerApartament> _logger;

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
 */

    }
}
