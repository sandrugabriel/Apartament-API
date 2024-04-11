using ApartamentAPI.Constants;
using ApartamentAPI.Dto;
using ApartamentAPI.Exceptions;
using ApartamentAPI.Models;
using ApartamentAPI.Repository.interfaces;
using ApartamentAPI.Service;
using ApartamentAPI.Service.interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Apartaments.Helpers;

namespace Tests.Apartaments.UnitTests
{
    public class TestCommandService
    {

        private readonly Mock<IRepository> _mock;
        private readonly IApartamentCommandService _commandService;

        public TestCommandService()
        {
            _mock = new Mock<IRepository>();
            _commandService = new ApartamentsCommandService(_mock.Object);
        }

        [Fact]
        public async Task Create_InvalidPrice()
        {
            var createRequest = new CreateRequest
            {
                Address = "test",
                Price = 0,
                Room = 2,
            };

            _mock.Setup(repo => repo.Create(createRequest)).ReturnsAsync((Apartament)null);
            Exception exception = await Assert.ThrowsAsync<InvalidPrice>(() => _commandService.CreateApartament(createRequest));

            Assert.Equal(Constants.InvalidPrice, exception.Message);
        }

        [Fact]
        public async Task Create_ValidData()
        {
            var createRequest = new CreateRequest
            {
                Address = "test",
                Price = 100000,
                Room = 2,
            };

            var apartament = TestApartamentFactory.CreateApartament(50);
            apartament.Price = createRequest.Price;

            _mock.Setup(repo => repo.Create(It.IsAny<CreateRequest>())).ReturnsAsync(apartament);

            var result = await _commandService.CreateApartament(createRequest);

            Assert.NotNull(result);
            Assert.Equal(result.Price, createRequest.Price);
        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var updateRequest = new UpdateRequest
            {
                Price = 100000
            };

            _mock.Setup(repo => repo.GetById(50)).ReturnsAsync((Apartament)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _commandService.UpdateApartament(50, updateRequest));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task Update_InvalidPrice()
        {
            var updateRequest = new UpdateRequest
            {
                Price = 0
            };
            var apartament = TestApartamentFactory.CreateApartament(50);
            apartament.Price = updateRequest.Price.Value;
            _mock.Setup(repo => repo.GetById(50)).ReturnsAsync(apartament);

            var exception = await Assert.ThrowsAsync<InvalidPrice>(() => _commandService.UpdateApartament(50, updateRequest));

            Assert.Equal(Constants.InvalidPrice, exception.Message);
        }

        [Fact]
        public async Task Update_ValidData()
        {
            var updateREquest = new UpdateRequest
            {
               Price = 1000000
            };

            var apartament = TestApartamentFactory.CreateApartament(1);
            apartament.Price = updateREquest.Price.Value;

            _mock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(apartament);
            _mock.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<UpdateRequest>())).ReturnsAsync(apartament);

            var result = await _commandService.UpdateApartament(1, updateREquest);

            Assert.NotNull(result);
            Assert.Equal(apartament, result);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.DeleteById(It.IsAny<int>())).ReturnsAsync((Apartament)null);

            var exception = await Assert.ThrowsAnyAsync<ItemDoesNotExist>(() => _commandService.DeleteApartament(1));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var apartament = TestApartamentFactory.CreateApartament(1);

            _mock.Setup(repo=>repo.GetById(It.IsAny<int>())).ReturnsAsync(apartament);

            var result = await _commandService.DeleteApartament(1);

            Assert.NotNull(result);
            Assert.Equal(apartament,result);
        }

    }
}
