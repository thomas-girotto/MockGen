using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MockGen.Sample.Tests
{
    public class ProtectedMethod
    {
        [Fact]
        public async Task Can_mock_protected_method()
        {
            // Given
            var mockHttpHandler = MockG.Generate<CustomHttpHandler>().New();
            mockHttpHandler
                .SendAsync(Arg<HttpRequestMessage>.Any, Arg<CancellationToken>.Any)
                .Returns(new HttpResponseMessage(HttpStatusCode.OK));

            var httpClient = new HttpClient(mockHttpHandler.Build())
            {
                BaseAddress = new Uri("http://whatever:123")
            };

            // When
            var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, "foo"));

            // Then
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
