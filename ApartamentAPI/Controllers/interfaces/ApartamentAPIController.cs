using ApartamentAPI.Dto;
using ApartamentAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace ApartamentAPI.Controllers.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ApartamentAPIController : ControllerBase
    {

        [HttpGet("all")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<Apartament>))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<List<Apartament>>> GetAll();

        [HttpGet("findByPrice")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<Apartament>))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<List<Apartament>>> GetAllByPrice([FromQuery]int price);

        [HttpGet("findById")]
        [ProducesResponseType(statusCode:200,type: typeof(Apartament))]
        [ProducesResponseType(statusCode:400,type:typeof(String))]
        public abstract Task<ActionResult<Apartament>> GetById(int id);

        [HttpPost("createApartament")]
        [ProducesResponseType(statusCode: 201, type: typeof(Apartament))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Apartament>> CreateApartament(CreateRequest request);

        [HttpPut("updateApartament")]
        [ProducesResponseType(statusCode:200,type:typeof(Apartament))]
        [ProducesResponseType(statusCode:400,type:typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Apartament>> UpdateApartament(int id,UpdateRequest request);

        [HttpDelete("deleteApartament")]
        [ProducesResponseType(statusCode:200,type:typeof(Apartament))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Apartament>> DeleteApartament(int id);

    }
}
