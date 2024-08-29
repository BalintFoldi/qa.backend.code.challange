using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Betsson.OnlineWallets.Services
{

    public class OnlineWalletApiTests
    {
        private const string BaseUrl = "http://localhost:8080/onlinewallet";

        [Fact]
        public async void Test_GetBalance_ShouldReturnSuccess()
        {
            // Arrange
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("balance", Method.Get);

            // Act
            RestResponse response = await client.ExecuteAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);
        }

        [Fact]
        public async void Test_Deposit_ShouldReturnSuccess()
        {
            // Arrange
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("deposit", Method.Post);
            request.AddJsonBody(new { amount = 100 }); // Example payload

            // Act
            RestResponse response = await client.ExecuteAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);

        }
    }
}

