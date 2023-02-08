using RFETestClient.Models;
using System.Net;
using System.Net.Http.Json;


namespace RFETestClient
{    
    internal static class TestClient
    {
        internal static async Task<HttpStatusCode> CreateItemAsync(HttpClient client, InputModel item, string id, string direction)
        {            var response = await client.PostAsJsonAsync($"v1/diff/{id}/{direction}", item);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.StatusCode;
        }

        internal static async Task<string> GetItemAsync(HttpClient client, string id)
        {
            var response = await client.GetStringAsync($"v1/diff/{id}");

            return response.ToString();
        }

    }
}
