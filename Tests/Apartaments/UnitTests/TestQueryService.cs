using ApartamentAPI.Constants;
using ApartamentAPI.Exceptions;
using ApartamentAPI.Models;
using ApartamentAPI.Repository.interfaces;
using ApartamentAPI.Service;
using ApartamentAPI.Service.interfaces;
using Moq;
using System.Net;
using Tests.Apartaments.Helpers;

namespace Tests.Apartaments.UnitTests
{
    public class TestQueryService
    {
        private readonly Mock<IRepository> _mock;
        private readonly IApartamentQueryService _service;

        public TestQueryService()
        {
            _mock = new Mock<IRepository>();
            _service = new ApartamentQueryService(_mock.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Apartament>());

            var exception = await Assert.ThrowsAsync<ItemsDoNotExists>(() => _service.GetAll());

            Assert.Equal(exception.Message,Constants.ItemsDoNotExist);
        }

        [Fact]
        public async Task GetAll_ValidData()
        {
            var apartaments = TestApartamentFactory.CreateApartaments(5);

            _mock.Setup(repo=>repo.GetAllAsync()).ReturnsAsync(apartaments);

            var result = await _service.GetAll();

            Assert.NotNull(result);
            Assert.Equal(apartaments,result);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {
            _mock.Setup(repo=>repo.GetById(1)).ReturnsAsync((Apartament)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(()=>_service.GetById(1));

            Assert.Equal(exception.Message,Constants.ItemDoesNotExist);
        }

        [Fact]
        public async Task GetById_ValidData()
        {
            var apartament = TestApartamentFactory.CreateApartament(1);
            _mock.Setup(repo=>repo.GetById(1)).ReturnsAsync(apartament);

            var result = await _service.GetById(1);

            Assert.NotNull(result);
            Assert.Equal(apartament, result);

        }

        [Fact]
        public async Task GetByPrice_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByPrice(1000)).ReturnsAsync(new List<Apartament>());
            var exception = await Assert.ThrowsAsync<ItemsDoNotExists>(() => _service.GetAllByPrice(1000));

            Assert.Equal(Constants.ItemsDoNotExist, exception.Message);
        }

        [Fact]
        public async Task GetByPrice_ValidData()
        {
            var apartament = TestApartamentFactory.CreateApartaments(10);
            _mock.Setup(repo => repo.GetByPrice((10 + 5) * 1000)).ReturnsAsync(apartament);
            var result = await _service.GetAllByPrice((10 + 5) * 1000);

            Assert.NotNull(result);
            Assert.Equal(apartament, result);

        }


    }
}