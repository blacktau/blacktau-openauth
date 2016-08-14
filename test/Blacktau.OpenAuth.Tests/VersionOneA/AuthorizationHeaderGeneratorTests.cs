namespace Blacktau.OpenAuth.Tests.VersionOneA
{
    using System;
    using System.Collections.Generic;

    using Blacktau.OpenAuth.Interfaces;
    using Blacktau.OpenAuth.Interfaces.VersionOneA;
    using Blacktau.OpenAuth.VersionOneA;

    using NSubstitute;

    using Xunit;

    public class AuthorizationHeaderGeneratorTests
    {
        public class TheConstructor
        {
            [Fact]
            public void Constructs()
            {
                var parametersGenerator = Substitute.For<IAuthorizationParametersGenerator>();
                var signer = Substitute.For<IAuthorizationSigner>();

                var authorizationHeaderGenerator = new AuthorizationHeaderGenerator(parametersGenerator, signer);

                Assert.NotNull(authorizationHeaderGenerator);
            }

            [Fact]
            public void GivenNullAuthorizationParametersGeneratorThrowsException()
            {
                var signer = Substitute.For<IAuthorizationSigner>();

                var exception = Record.Exception(() => new AuthorizationHeaderGenerator(null, signer));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("parametersGenerator", exception.Message);
            }

            [Fact]
            public void GivenNullAuthorizationSignerThrowsException()
            {
                var parametersGenerator = Substitute.For<IAuthorizationParametersGenerator>();

                var exception = Record.Exception(() => new AuthorizationHeaderGenerator(parametersGenerator, null));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("authorizationSigner", exception.Message);
            }
        }

        public class TheGenerateHeaderValueMethod
        {
            public const string GetResult =  "oauth_consumer_key=\"ApplicationKey_5nagEQ0k6fiBQc8eMYAh\",oauth_nonce=\"vQxdgbUKRgvwXwkGihwtSpFlPiZmonhMs\",oauth_signature=\"iFfKurpTB%2BuQv%2FUcRu0pHhzhr8E%3D\",oauth_signature_method=\"HMAC-SHA1\",oauth_timestamp=\"1471127777\",oauth_token=\"AccessToken_HcSg3eHyNLo5zkfbuOrW\",oauth_version=\"1.0\"";

            public const string PostResult = "oauth_consumer_key=\"ApplicationKey_5nagEQ0k6fiBQc8eMYAh\",oauth_nonce=\"vQxdgbUKRgvwXwkGihwtSpFlPiZmonhMs\",oauth_signature=\"meM3UHFGifwIGvlZtscpGs6uoxE%3D\",oauth_signature_method=\"HMAC-SHA1\",oauth_timestamp=\"1471127777\",oauth_token=\"AccessToken_HcSg3eHyNLo5zkfbuOrW\",oauth_version=\"1.0\"";

            private const string ApplicationKey = "ApplicationKey_5nagEQ0k6fiBQc8eMYAh";

            public const string Nonce = "vQxdgbUKRgvwXwkGihwtSpFlPiZmonhMs";

            public const string SignatureMethod = "HMAC-SHA1";

            public const string TimeStamp = "1471127777";

            private const string Version = "1.0";

            private const string AccessToken = "AccessToken_HcSg3eHyNLo5zkfbuOrW";

            private const string Signature = "945qIT%2Bj%2FvKCXqr%2FL%2BOefR3%2FULI%3D";

            private static IDictionary<string, string> CreateBodyParameters()
            {
                var bodyParameters = new Dictionary<string, string>();

                bodyParameters.Add("Body1Name", "Body1Value");
                bodyParameters.Add("Body2Name", "Body2Value");
                bodyParameters.Add("Body3Name", "Body3Value");
                bodyParameters.Add("Body4Name", "Body4Value");

                return bodyParameters;
            }

            private static IDictionary<string, string> CreateQueryParameters()
            {
                var queryParameters = new Dictionary<string, string>();

                queryParameters.Add("Query1Name", "Query1Value");
                queryParameters.Add("Query2Name", "Query2Value");
                queryParameters.Add("Query3Name", "Query3Value");
                queryParameters.Add("Query4Name", "Query4Value");

                return queryParameters;
            }

            private static IApplicationCredentials CreateApplicationCredentials()
            {
                var credentials = Substitute.For<IApplicationCredentials>();

                credentials.ApplicationKey.Returns(ApplicationKey);
                credentials.ApplicationSecret.Returns("ApplicationSecret_Avrxtc8Wr5u84qwi1jLP");

                return credentials;
            }

            private static IAuthorizationInformation CreateAuthorizationInformation()
            {
                var information = Substitute.For<IAuthorizationInformation>();

                information.AccessToken.Returns(AccessToken);
                information.AccessTokenSecret.Returns("AccessTokenSecret_OWqCTqiAhEs9paf0ufjX");

                return information;
            }

            private static IDictionary<string, string> CreateStandardParameterSet()
            {
                var result = new Dictionary<string, string>
                {
                    {AuthorizationFieldNames.ConsumerKey, ApplicationKey},
                    {AuthorizationFieldNames.Nonce, Nonce},
                    {AuthorizationFieldNames.SignatureMethod, SignatureMethod},
                    {AuthorizationFieldNames.TimeStamp, TimeStamp},
                    {AuthorizationFieldNames.Version, Version}
                };
                return result;
            }

            private static AuthorizationHeaderGenerator CreateAuthorizationHeaderGenerator()
            {
                var parametersGenerator = CreateAuthorizationParametersGenerator();
                var signer = new AuthorizationSigner();

                var authorizationHeaderGenerator = new AuthorizationHeaderGenerator(parametersGenerator, signer);

                return authorizationHeaderGenerator;
            }

            private static IAuthorizationSigner CreateAuthorizationSigner()
            {
                var signer = Substitute.For<IAuthorizationSigner>();
                signer.GetSignature(null, null, null, null, null).ReturnsForAnyArgs(Signature);
                return signer;
            }

            private static IAuthorizationParametersGenerator CreateAuthorizationParametersGenerator()
            {
                var standardParameters = CreateStandardParameterSet();

                var authorizationParameters = CreateStandardParameterSet();
                authorizationParameters.Add(AuthorizationFieldNames.Token, AccessToken);

                var parametersGenerator = Substitute.For<IAuthorizationParametersGenerator>();
                parametersGenerator.CreateStandardParameterSet(null).ReturnsForAnyArgs(standardParameters);
                parametersGenerator.GetAuthorizationParameters(null, null).ReturnsForAnyArgs(authorizationParameters);

                return parametersGenerator;
            }

            [Fact]
            public void GivenNullApplicationCredentialsThrowsException()
            {
                var authorizationHeaderGenerator = CreateAuthorizationHeaderGenerator();

                IApplicationCredentials applicationCredentials = null;
                var queryParameters = CreateQueryParameters();
                var bodyParameters = CreateBodyParameters();
                var authorizationInformation = CreateAuthorizationInformation();
                var method = HttpMethod.Get;
                var url = "http://www.google.com";

                var exception = Record.Exception(() => authorizationHeaderGenerator.GenerateHeaderValue(applicationCredentials, queryParameters, bodyParameters, authorizationInformation, method, url));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("applicationCredentials", exception.Message);
            }

            [Fact]
            public void GivenNullAuthorizationInformationThrowsException()
            {
                var authorizationHeaderGenerator = CreateAuthorizationHeaderGenerator();

                var applicationCredentials = CreateApplicationCredentials();
                var queryParameters = Substitute.For<IDictionary<string, string>>();
                var bodyParameters = CreateBodyParameters();
                IAuthorizationInformation authorizationInformation = null;
                var method = HttpMethod.Get;
                var url = "http://www.google.com";

                var exception = Record.Exception(() => authorizationHeaderGenerator.GenerateHeaderValue(applicationCredentials, queryParameters, bodyParameters, authorizationInformation, method, url));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("authorizationInformation", exception.Message);
            }

            [Fact]
            public void GivenNullBodyParametersThrowsException()
            {
                var authorizationHeaderGenerator = CreateAuthorizationHeaderGenerator();

                var applicationCredentials = CreateApplicationCredentials();
                var queryParameters = CreateQueryParameters();
                IDictionary<string, string> bodyParameters = null;
                var authorizationInformation = CreateAuthorizationInformation();
                var method = HttpMethod.Get;
                var url = "http://www.google.com";

                var exception = Record.Exception(() => authorizationHeaderGenerator.GenerateHeaderValue(applicationCredentials, queryParameters, bodyParameters, authorizationInformation, method, url));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("bodyParameters", exception.Message);
            }

            [Fact]
            public void GivenNullQueryParametersThrowsException()
            {
                var authorizationHeaderGenerator = CreateAuthorizationHeaderGenerator();

                var applicationCredentials = CreateApplicationCredentials();
                IDictionary<string, string> queryParameters = null;
                var bodyParameters = CreateBodyParameters();
                var authorizationInformation = CreateAuthorizationInformation();
                var method = HttpMethod.Get;
                var url = "http://www.google.com";

                var exception = Record.Exception(() => authorizationHeaderGenerator.GenerateHeaderValue(applicationCredentials, queryParameters, bodyParameters, authorizationInformation, method, url));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("queryParameters", exception.Message);
            }

            [Fact]
            public void GivenNullUrlThrowsException()
            {
                var authorizationHeaderGenerator = CreateAuthorizationHeaderGenerator();

                var applicationCredentials = CreateApplicationCredentials();
                var queryParameters = CreateQueryParameters();
                var bodyParameters = CreateBodyParameters();
                var authorizationInformation = CreateAuthorizationInformation();
                var method = HttpMethod.Get;
                string url = null;

                var exception = Record.Exception(() => authorizationHeaderGenerator.GenerateHeaderValue(applicationCredentials, queryParameters, bodyParameters, authorizationInformation, method, url));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("url", exception.Message);
            }

            [Fact]
            public void GivenValidInputsGeneratesCorrectHeader()
            {
                var authorizationHeaderGenerator = CreateAuthorizationHeaderGenerator();

                var applicationCredentials = CreateApplicationCredentials();
                var queryParameters = CreateQueryParameters();
                var bodyParameters = CreateBodyParameters();
                var authorizationInformation = CreateAuthorizationInformation();
                var method = HttpMethod.Get;
                string url = "http://www.google.com/";

                var result = authorizationHeaderGenerator.GenerateHeaderValue(applicationCredentials, queryParameters, bodyParameters, authorizationInformation, method, url);

                Assert.NotNull(result);
                Assert.False(string.IsNullOrWhiteSpace(result));
                Assert.Equal(GetResult, result);
            }

            [Fact]
            public void GivenValidInputsAndPostMethodIncludeBodyParametersGeneratesCorrectHeader()
            {
                var authorizationHeaderGenerator = CreateAuthorizationHeaderGenerator();

                var applicationCredentials = CreateApplicationCredentials();
                var queryParameters = CreateQueryParameters();
                var bodyParameters = CreateBodyParameters();
                var authorizationInformation = CreateAuthorizationInformation();
                var method = HttpMethod.Post;

                string url = "http://www.google.com/";

                var result = authorizationHeaderGenerator.GenerateHeaderValue(applicationCredentials, queryParameters, bodyParameters, authorizationInformation, method, url);

                Assert.NotNull(result);
                Assert.False(string.IsNullOrWhiteSpace(result));
                Assert.Equal(PostResult, result);
            }

        }
    }
} ;