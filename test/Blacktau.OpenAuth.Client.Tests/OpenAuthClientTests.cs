namespace Blacktau.OpenAuth.Client.Tests
{
    using System;

    using Blacktau.OpenAuth.Client;
    using Blacktau.OpenAuth.Client.Interfaces;
    using Blacktau.OpenAuth.Client.VersionOneA;

    using NSubstitute;

    using Xunit;

    public class OpenAuthClientTests
    {
        private const string ApplicationKey = "NotARealApplicationKey";

        private const string PostUrl = "https://api.fakeresouce.com/v2/blog/post";

        private const string GetUrl = "https://api.fakeresouce.com/v2/blog/entries";

        private const string BodyParameterOneName = "BodyParameterOneName";

        private const string BodyParameterOneValue = "BodyParameterOneValue";

        private const string BodyParameterTwoName = "BodyParameterTwoName";

        private const string BodyParameterTwoValue = "BodyParameterTwoValue";

        private const string QueryParameterOneName = "QueryParameterOneName";

        private const string QueryParameterOneValue = "QueryParameterOneValue";

        private const string QueryParameterTwoName = "QueryParameterTwoName";

        private const string QueryParameterTwoValue = "QueryParameterTwoValue";

        private const string ApplicationSecret = "NotARealApplicationSecret";

        private const string AccessToken = "NotARealAccessToken";

        private const string AccessTokenSecret = "NotARealAccessTokenSecret";

        #region [ HELPERS ]

        private static IApplicationCredentials CreateApplicationCredentials()
        {
            var credentials = Substitute.For<IApplicationCredentials>();

            credentials.ApplicationKey.Returns(ApplicationKey);
            credentials.ApplicationSecret.Returns(ApplicationSecret);

            return credentials;
        }

        private static IAuthorizationHeaderGenerator CreateAuthorizationHeaderGenerator()
        {
            return new AuthorizationHeaderGenerator(new AuthorizationParametersGenerator(new NonceFactory(), new TimeStampFactory(new SystemClock())), new AuthorizationSigner());
        }

        private static IAuthorizationInformation CreateAuthorizationInformation()
        {
            var information = Substitute.For<IAuthorizationInformation>();
            information.AccessToken.Returns(AccessToken);
            information.AccessTokenSecret.Returns(AccessTokenSecret);
            return information;
        }

        private static IHttpClientFactory CreateHttpClientFactory(IHttpClient httpClient = null)
        {
            var factory = Substitute.For<IHttpClientFactory>();
            httpClient = httpClient ?? CreateHttpClient();

            factory.CreateHttpClient(null).ReturnsForAnyArgs(httpClient);

            return factory;
        }

        private static IHttpClient CreateHttpClient()
        {
            var httpClient = Substitute.For<IHttpClient>();

            var httpRequestHeaders = Substitute.For<IHttpRequestHeaders>();

            httpClient.DefaultRequestHeaders.Returns(httpRequestHeaders);
            return httpClient;
        }

        private static OpenAuthClient CreateOpenAuthClientForPost()
        {
            return CreateOpenAuthClient(PostUrl, HttpMethod.Post);
        }

        private static OpenAuthClient CreateOpenAuthClient(string url, HttpMethod method)
        {
            var applicationCredentials = CreateApplicationCredentials();
            var authorizationHeaderGenerator = CreateAuthorizationHeaderGenerator();
            var authorizationInformation = CreateAuthorizationInformation();
            var httpClientFactory = CreateHttpClientFactory();

            return new OpenAuthClient(url, method, applicationCredentials, authorizationHeaderGenerator, authorizationInformation, httpClientFactory);
        }

        private static OpenAuthClient CreateOpenAuthClientForGet()
        {
            return CreateOpenAuthClient(GetUrl, HttpMethod.Get);
        }
        #endregion

        public class TheConstructor
        {
            [Fact]
            public void GivenNullApplicationCredentialsRaisesException()
            {
                var authorizationHeaderGenerator = CreateAuthorizationHeaderGenerator();
                var authorizationInformation = CreateAuthorizationInformation();
                var httpClientFactory = CreateHttpClientFactory();

                var exception = Record.Exception(() => new OpenAuthClient(GetUrl, HttpMethod.Get, null, authorizationHeaderGenerator, authorizationInformation, httpClientFactory));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("applicationCredentials", exception.Message);
            }

            [Fact]
            public void GivenNullAuthorizationHeaderGeneratorRaisesException()
            {
                var applicationCredentials = Substitute.For<IApplicationCredentials>();
                var authorizationInformation = Substitute.For<IAuthorizationInformation>();
                var httpClientFactory = CreateHttpClientFactory();

                var exception = Record.Exception(() => new OpenAuthClient(PostUrl, HttpMethod.Get, applicationCredentials, null, authorizationInformation, httpClientFactory));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("authHeaderGenerator", exception.Message);
            }

            [Fact]
            public void GivenNullHttpClientFactoryRaisesException()
            {
                var applicationCredentials = Substitute.For<IApplicationCredentials>();
                var authorizationHeaderGenerator = Substitute.For<IAuthorizationHeaderGenerator>();
                var authorizationInformation = Substitute.For<IAuthorizationInformation>();

                var exception = Record.Exception(() => new OpenAuthClient(GetUrl, HttpMethod.Get, applicationCredentials, authorizationHeaderGenerator, authorizationInformation, null));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("httpClientFactory", exception.Message);
            }

            [Fact]
            public void GivenNullUrlRaisesException()
            {
                var applicationCredentials = Substitute.For<IApplicationCredentials>();
                var authorizationHeaderGenerator = Substitute.For<IAuthorizationHeaderGenerator>();
                var authorizationInformation = Substitute.For<IAuthorizationInformation>();
                var httpClientFactory = CreateHttpClientFactory();

                var exception = Record.Exception(() => new OpenAuthClient(null, HttpMethod.Get, applicationCredentials, authorizationHeaderGenerator, authorizationInformation, httpClientFactory));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("url", exception.Message);
            }

            [Fact]
            public void GivenValidParametersConstructs()
            {
                var applicationCredentials = Substitute.For<IApplicationCredentials>();
                var authorizationHeaderGenerator = Substitute.For<IAuthorizationHeaderGenerator>();
                var authorizationInformation = Substitute.For<IAuthorizationInformation>();
                var httpClientFactory = CreateHttpClientFactory();

                var client = new OpenAuthClient(PostUrl, HttpMethod.Get, applicationCredentials, authorizationHeaderGenerator, authorizationInformation, httpClientFactory);

                Assert.NotNull(client);
            }
        }

        public class AddQueryParameterMethod
        {
            [Fact]
            public void GivenEmptyNameThrowsException()
            {
                var client = CreateOpenAuthClientForGet();

                var exception = Record.Exception(() => client.AddQueryParameter(string.Empty, QueryParameterOneValue));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentException>(exception);
                Assert.Contains("name", exception.Message);
            }

            [Fact]
            public void GivenNullNameThrowsException()
            {
                var client = CreateOpenAuthClientForGet();

                var exception = Record.Exception(() => client.AddQueryParameter(null, QueryParameterOneValue));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("name", exception.Message);
            }

            [Fact]
            public void GivenNullValueAddsQueryParameter()
            {
                var client = CreateOpenAuthClientForGet();

                client.AddQueryParameter(QueryParameterOneName, null);

                Assert.NotEmpty(client.QueryParameters);
                Assert.Contains(QueryParameterOneName, client.QueryParameters.Keys);
                Assert.Null(client.QueryParameters[QueryParameterOneName]);
            }

            [Fact]
            public void GivenValidParametersAddsQueryParameter()
            {
                var client = CreateOpenAuthClientForGet();

                client.AddQueryParameter(QueryParameterOneName, QueryParameterOneValue);

                Assert.NotEmpty(client.QueryParameters);
                Assert.Contains(QueryParameterOneName, client.QueryParameters.Keys);
                Assert.Equal(QueryParameterOneValue, client.QueryParameters[QueryParameterOneName]);
            }

            [Fact]
            public void GivenMultipleValidParametersAddsQueryParameters()
            {
                var client = CreateOpenAuthClientForGet();

                client.AddQueryParameter(QueryParameterOneName, QueryParameterOneValue);
                client.AddQueryParameter(QueryParameterTwoName, QueryParameterTwoValue);

                Assert.NotEmpty(client.QueryParameters);
                Assert.Equal(2, client.QueryParameters.Count);

                Assert.Contains(QueryParameterOneName, client.QueryParameters.Keys);
                Assert.Contains(QueryParameterTwoName, client.QueryParameters.Keys);

                Assert.Equal(QueryParameterOneValue, client.QueryParameters[QueryParameterOneName]);
                Assert.Equal(QueryParameterTwoValue, client.QueryParameters[QueryParameterTwoName]);
            }
        }

        public class AddBodyParameterMethod
        {
            [Fact]
            public void GivenEmptyNameThrowsException()
            {
                var client = CreateOpenAuthClientForPost();

                var exception = Record.Exception(() => client.AddBodyParameter(string.Empty, BodyParameterOneValue));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentException>(exception);
                Assert.Contains("name", exception.Message);
            }

            [Fact]
            public void GivenNullNameThrowsException()
            {
                var client = CreateOpenAuthClientForPost();

                var exception = Record.Exception(() => client.AddBodyParameter(null, BodyParameterOneValue));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("name", exception.Message);
            }

            [Fact]
            public void GivenNullValueAddsBodyParameter()
            {
                var client = CreateOpenAuthClientForPost();

                client.AddBodyParameter(BodyParameterOneName, null);

                Assert.NotEmpty(client.BodyParameters);
                Assert.Contains(BodyParameterOneName, client.BodyParameters.Keys);
                Assert.Null(client.BodyParameters[BodyParameterOneName]);
            }

            [Fact]
            public void GivenValidParametersAddsBodyParameter()
            {
                var client = CreateOpenAuthClientForPost();

                client.AddBodyParameter(BodyParameterOneName, BodyParameterOneValue);

                Assert.NotEmpty(client.BodyParameters);
                Assert.Contains(BodyParameterOneName, client.BodyParameters.Keys);
                Assert.Equal(BodyParameterOneValue, client.BodyParameters[BodyParameterOneName]);
            }

            [Fact]
            public void GivenMultipleValidParametersAddsBodyParameters()
            {
                var client = CreateOpenAuthClientForPost();

                client.AddBodyParameter(BodyParameterOneName, BodyParameterOneValue);
                client.AddBodyParameter(BodyParameterTwoName, BodyParameterTwoValue);

                Assert.NotEmpty(client.BodyParameters);
                Assert.Equal(2, client.BodyParameters.Count);

                Assert.Contains(BodyParameterOneName, client.BodyParameters.Keys);
                Assert.Contains(BodyParameterTwoName, client.BodyParameters.Keys);

                Assert.Equal(BodyParameterOneValue, client.BodyParameters[BodyParameterOneName]);
                Assert.Equal(BodyParameterTwoValue, client.BodyParameters[BodyParameterTwoName]);
            }
        }
    }
}