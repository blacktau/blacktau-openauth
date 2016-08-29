namespace Blacktau.OpenAuth.Tests
{
    using System;
    using System.Net.Http;

    using Xunit;

    public class HttpClientTests
    {
        public class TheCreateHttpClientMethod
        {
            [Fact]
            public void GivenValidHttpMessageHandlerConstructsHttpClient()
            {
                var httpClientFactory = new HttpClientFactory();
                var httpMessageHandler = new HttpClientHandler();

                var httpClient = httpClientFactory.CreateHttpClient(httpMessageHandler);

                Assert.NotNull(httpClient);
            }

            [Fact]
            public void GivenNullHttpMessageHandlerThrowsException()
            {
                var httpClientFactory = new HttpClientFactory();

                var exception = Record.Exception(() => httpClientFactory.CreateHttpClient(null));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("handler", exception.Message);
            }
        }
    }
}