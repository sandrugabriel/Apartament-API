using System.Net;
using System.Text;
using ApartamentAPI.Dto;
using ApartamentAPI.Models;
using Newtonsoft.Json;
using Tests.Apartaments.Helpers;
using Tests.Apartaments.Infrastucture;

namespace Tests.Apartaments.IntegrationTests;

public class IntegrationTest : IClassFixture<ApiWebApplicationFactory>
{
    
        private readonly HttpClient _client;

        public IntegrationTest(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllApartaments_ApartamentsFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createApartamentRequest = TestApartamentFactory.CreateApartament(1);
            var content = new StringContent(JsonConvert.SerializeObject(createApartamentRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/ControllerApartament/createApartament", content);

            var response = await _client.GetAsync("/api/v1/ControllerApartament/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetApartamentById_ApartamentFound_ReturnsOkStatusCode()
        {
            var createApartamentRequest = new CreateRequest
            { Room = 3, Address = "ASasdadd", Price = 1000};
            var content = new StringContent(JsonConvert.SerializeObject(createApartamentRequest), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/ControllerApartament/createApartament", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Apartament>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result.Room, createApartamentRequest.Room);
        }

        [Fact]
        public async Task GetApartamentById_ApartamentNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/Apartament/findById?id=9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode()
        {
            var request = "/api/v1/ControllerApartament/createApartament";
            var createApartamentRequest = new CreateRequest
                { Room = 3, Address = "ASasdadd", Price = 1000};
            var content = new StringContent(JsonConvert.SerializeObject(createApartamentRequest), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Apartament>(responseString);

            Assert.NotNull(result);
            Assert.Equal(createApartamentRequest.Room, result.Room);
        }

        [Fact]
        public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode()
        {
            var request = "/api/v1/ControllerApartament/createApartament";
            var createApartament = new CreateRequest
                { Room = 3, Address = "ASasdadd", Price = 1000};
            var content = new StringContent(JsonConvert.SerializeObject(createApartament), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Apartament>(responseString);

            request = $"/api/v1/ControllerApartament/updateApartament?id={result.Id}";
            var updateApartament = new UpdateRequest { Address = "12test" };
            content = new StringContent(JsonConvert.SerializeObject(updateApartament), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);
            var responceStringUp = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Apartament>(responceStringUp);


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result1.Address, updateApartament.Address);
        }

        [Fact]
        public async Task Put_Update_InvalidApartamentPrice_ReturnsBadRequestStatusCode()
        {
            var request = "/api/v1/ControllerApartament/createApartament";
            var createApartament = new CreateRequest
                { Room = 3, Address = "ASasdadd", Price = 1000};
            var content = new StringContent(JsonConvert.SerializeObject(createApartament), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Apartament>(responseString);

            request = $"/api/v1/ControllerApartament/updateApartament?id={result.Id}";
            var updateApartament = new UpdateRequest { Price = 0 };
            content = new StringContent(JsonConvert.SerializeObject(updateApartament), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);
            var responceStringUp = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Apartament>(responseString);


            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEqual(result1.Address, updateApartament.Address);
        }

        [Fact]
        public async Task Put_Update_ApartamentDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerApartament/updateApartament";
            var updateApartament = new UpdateRequest { Address = "asd" };
            var content = new StringContent(JsonConvert.SerializeObject(updateApartament), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Delete_ApartamentExists_ReturnsDeletedApartament()
        {
            var request = "/api/v1/ControllerApartament/createApartament";
            var createApartament = new CreateRequest
                { Room = 3, Address = "ASasdadd", Price = 1000};
            var content = new StringContent(JsonConvert.SerializeObject(createApartament), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Apartament>(responseString)!;

            request = $"/api/v1/ControllerApartament/deleteApartament?id={result.Id}";

            response = await _client.DeleteAsync(request);
            var responceString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Apartament>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result1.Room, createApartament.Room);
        }

        [Fact]
        public async Task Delete_Delete_ApartamentDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerApartament/deleteApartament?id=7";

            var response = await _client.DeleteAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

}
