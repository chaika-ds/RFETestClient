using System.Net.Http.Headers;

namespace RFETestClient.Fakes
{
    internal static class HttpClientExtensions
    {
        internal static void AdjustHttpClient(this HttpClient client, Uri uri)
        {
            client.BaseAddress = uri;
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

    }
}
