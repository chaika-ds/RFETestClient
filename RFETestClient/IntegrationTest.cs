using RFETestClient.Enums;
using RFETestClient.Fakes;
using RFETestClient.Models;
using System.Net;

namespace RFETestClient
{
    public class IntegrationTest
    {
        private Uri uri;
        public IntegrationTest()
        {
            uri = new Uri("https://localhost:7047/");
        }

        [Theory]
        [InlineData("some value to be compared", "some value to be compared", "eyJpbnB1dCI6InRlc3RWYWx1ZSJ9", 0)]
        [InlineData("some value to be compared1", "some value to be compared", "eyJpbnB1dCI6InRlc3RWYWx1ZSJ8", 1)]
        [InlineData("some value to be compare1", "some value to be compared", "eyJpbnB1dCI6InRlc3RWYWx1ZSJ7", 2)]
        public async void TestRfe(string leftInput, string rightInput, string idTest, int resultType)
        {
            //Arrange
            var directions = Enum.GetNames(typeof(DirectionType)).ToList();
            using HttpClient client = new();
            client.AdjustHttpClient(uri);

            //Act
            var statusPost = await Task.WhenAll(directions.Select(async direction =>
            {
                if (direction == "left")
                    return await TestClient.CreateItemAsync(client, new InputModel { Input = leftInput }, idTest, direction);

                return await TestClient.CreateItemAsync(client, new InputModel { Input = rightInput }, idTest, direction);

            }).ToArray());

            var result = await TestClient.GetItemAsync(client, idTest);

            //Assert
           statusPost.ToList().ForEach(status=> Assert.Equal(HttpStatusCode.Created, status));

            switch (resultType)
            {
                case 0: Assert.Equal( "inputs were equal", result);
                    break;
                case 1:
                    Assert.Equal("inputs are of different size", result);
                    break;
                case 2:
                    Assert.Contains(leftInput.Length.ToString(), result);
                    Assert.Contains(rightInput.Length.ToString(), result);
                    break;
            }
        }
    }
}