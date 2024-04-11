using ApartamentAPI.Constants;
using ApartamentAPI.Controllers;
using ApartamentAPI.Controllers.interfaces;
using ApartamentAPI.Dto;
using ApartamentAPI.Exceptions;
using ApartamentAPI.Models;
using ApartamentAPI.Service.interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Apartaments.Helpers;

namespace Tests.Apartaments.UnitTests
{
    public class TestController
    {

        private readonly Mock<IApartamentCommandService> _mockCommandService;
        private readonly Mock<IApartamentQueryService> _mockQueryService;
        private readonly ApartamentAPIController apartamentApiController;

        public TestController()
        {
            _mockCommandService = new Mock<IApartamentCommandService>();
            _mockQueryService = new Mock<IApartamentQueryService>();

            apartamentApiController = new ControllerApartament(_mockQueryService.Object, _mockCommandService.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mockQueryService.Setup(repo => repo.GetAll()).ThrowsAsync(new ItemsDoNotExists(Constants.ItemsDoNotExist));

            var restult = await apartamentApiController.GetAll();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(restult.Result);

            Assert.Equal(Constants.ItemsDoNotExist, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task GetAll_ValidData()
        {
            var apartaments = TestApartamentFactory.CreateApartaments(5);
            _mockQueryService.Setup(repo => repo.GetAll()).ReturnsAsync(apartaments);

            var result = await apartamentApiController.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allapartaments = Assert.IsType<List<Apartament>>(okResult.Value);

            Assert.Equal(4, allapartaments.Count);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact]
        public async Task Create_InvalidPrice()
        {

            var createRequest = new CreateRequest
            {
                Price = 0,
                Room = 2,
                Address = "test"
            };

            _mockCommandService.Setup(repo => repo.CreateApartament(It.IsAny<CreateRequest>())).
                ThrowsAsync(new InvalidPrice(Constants.InvalidPrice));

            var result = await apartamentApiController.CreateApartament(createRequest);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(400, badRequest.StatusCode);
            Assert.Equal(Constants.InvalidPrice, badRequest.Value);

        }

        [Fact]
        public async Task Create_ValidData()
        {
            var createRequest = new CreateRequest
            {

                Price = 100000,
                Room = 2,
                Address = "test"
            };

            var apartament = TestApartamentFactory.CreateApartament(1);
            apartament.Price = createRequest.Price;
            apartament.Room = createRequest.Room;
            apartament.Address = createRequest.Address;

            _mockCommandService.Setup(repo => repo.CreateApartament(It.IsAny<CreateRequest>())).ReturnsAsync(apartament);

            var result = await apartamentApiController.CreateApartament(createRequest);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, apartament);

        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var update = new UpdateRequest
            {
               Price = 100000
            };

            _mockCommandService.Setup(repo => repo.UpdateApartament(1, update)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await apartamentApiController.UpdateApartament(1, update);

            var ntFound = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(ntFound.StatusCode, 404);
            Assert.Equal(Constants.ItemDoesNotExist, ntFound.Value);

        }
        [Fact]
        public async Task Update_ValidData()
        {
            var update = new UpdateRequest
            {
              Price = 100000
            };

            var apartament = TestApartamentFactory.CreateApartament(1);

            _mockCommandService.Setup(repo => repo.UpdateApartament(It.IsAny<int>(), It.IsAny<UpdateRequest>())).ReturnsAsync(apartament);

            var result = await apartamentApiController.UpdateApartament(1, update);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, apartament);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mockCommandService.Setup(repo => repo.DeleteApartament(1)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await apartamentApiController.DeleteApartament(1);

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notFound.StatusCode, 404);
            Assert.Equal(notFound.Value, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {

            var apartament = TestApartamentFactory.CreateApartament(1);

            _mockCommandService.Setup(repo => repo.DeleteApartament(It.IsAny<int>())).ReturnsAsync(apartament);

            var result = await apartamentApiController.DeleteApartament(1);

            var okresult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okresult.StatusCode);
            Assert.Equal(okresult.Value, apartament);

        }
    }
}
