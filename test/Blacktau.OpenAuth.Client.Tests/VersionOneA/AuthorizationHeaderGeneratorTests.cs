namespace Blacktau.OpenAuth.Tests.VersionOneA
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Blacktau.OpenAuth.Client;
    using Blacktau.OpenAuth.Client.Interfaces;
    using Blacktau.OpenAuth.Client.Interfaces.VersionOneA;
    using Blacktau.OpenAuth.Client.VersionOneA;

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
            private const string GetResult =
                "oauth_consumer_key=\"ApplicationKey_5nagEQ0k6fiBQc8eMYAh\",oauth_nonce=\"vQxdgbUKRgvwXwkGihwtSpFlPiZmonhMs\",oauth_signature=\"iFfKurpTB%2BuQv%2FUcRu0pHhzhr8E%3D\",oauth_signature_method=\"HMAC-SHA1\",oauth_timestamp=\"1471127777\",oauth_token=\"AccessToken_HcSg3eHyNLo5zkfbuOrW\",oauth_version=\"1.0\"";

            private const string PostResult =
                "oauth_consumer_key=\"ApplicationKey_5nagEQ0k6fiBQc8eMYAh\",oauth_nonce=\"vQxdgbUKRgvwXwkGihwtSpFlPiZmonhMs\",oauth_signature=\"meM3UHFGifwIGvlZtscpGs6uoxE%3D\",oauth_signature_method=\"HMAC-SHA1\",oauth_timestamp=\"1471127777\",oauth_token=\"AccessToken_HcSg3eHyNLo5zkfbuOrW\",oauth_version=\"1.0\"";

            private const string ApplicationKey = "ApplicationKey_5nagEQ0k6fiBQc8eMYAh";

            private const string Nonce = "vQxdgbUKRgvwXwkGihwtSpFlPiZmonhMs";

            private const string SignatureMethod = "HMAC-SHA1";

            private const string TimeStamp = "1471127777";

            private const string Version = "1.0";

            private const string AccessToken = "AccessToken_HcSg3eHyNLo5zkfbuOrW";

            private const string PostSignature = "meM3UHFGifwIGvlZtscpGs6uoxE=";

            private const string GetSignature = "iFfKurpTB+uQv/UcRu0pHhzhr8E=";
            
            private const string ApplicationSecret = "ApplicationSecret_Avrxtc8Wr5u84qwi1jLP";

            private const string AccessTokenSecret = "AccessTokenSecret_OWqCTqiAhEs9paf0ufjX";

            private const string Url = "https://www.google.com";

            private static IApplicationCredentials CreateApplicationCredentials()
            {
                var credentials = Substitute.For<IApplicationCredentials>();

                credentials.ApplicationKey.Returns(ApplicationKey);
                credentials.ApplicationSecret.Returns(ApplicationSecret);

                return credentials;
            }

            private static IAuthorizationInformation CreateAuthorizationInformation()
            {
                var information = Substitute.For<IAuthorizationInformation>();

                information.AccessToken.Returns(AccessToken);
                information.AccessTokenSecret.Returns(AccessTokenSecret);

                return information;
            }

            private static IOpenAuthClient CreateOpenAuthClient(IReadOnlyDictionary<string, string> queryParameters = null, IReadOnlyDictionary<string, string> bodyParameters = null, HttpMethod? method = null, string url = null)
            {
                queryParameters = queryParameters ?? new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
                bodyParameters = bodyParameters ?? new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());

                method = method ?? HttpMethod.Get;
                url = url ?? Url;

                var openAuthClient = Substitute.For<IOpenAuthClient>();

                openAuthClient.QueryParameters.Returns(queryParameters);
                openAuthClient.BodyParameters.Returns(bodyParameters);
                openAuthClient.Method.Returns(method.Value);
                openAuthClient.Url.Returns(url);
                
                return openAuthClient;
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

            private static AuthorizationHeaderGenerator CreateAuthorizationHeaderGenerator(IAuthorizationSigner signer = null)
            {
                var parametersGenerator = CreateAuthorizationParametersGenerator();
                signer = signer ?? CreateAuthorizationSigner();

                var authorizationHeaderGenerator = new AuthorizationHeaderGenerator(parametersGenerator, signer);

                return authorizationHeaderGenerator;
            }

            private static IAuthorizationSigner CreateAuthorizationSigner()
            {
                var signer = Substitute.For<IAuthorizationSigner>();

                signer.GetSignature(ApplicationSecret, AccessTokenSecret, Url, "POST", Arg.Any<IEnumerable<KeyValuePair<string, string>>[]>()).Returns(PostSignature);
                signer.GetSignature(ApplicationSecret, AccessTokenSecret, Url, "GET", Arg.Any<IEnumerable<KeyValuePair<string, string>>[]>()).Returns(GetSignature);

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
                var openAuthClient = CreateOpenAuthClient();

                var authorizationInformation = CreateAuthorizationInformation();

                var exception = Record.Exception(() => authorizationHeaderGenerator.GenerateHeaderValue(null, authorizationInformation, openAuthClient));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("applicationCredentials", exception.Message);
            }

            [Fact]
            public void GivenNullAuthorizationInformationThrowsException()
            {
                var authorizationHeaderGenerator = CreateAuthorizationHeaderGenerator();

                var openAuthClient = CreateOpenAuthClient();

                var applicationCredentials = CreateApplicationCredentials();
                
                var exception = Record.Exception(() => authorizationHeaderGenerator.GenerateHeaderValue(applicationCredentials, null, openAuthClient));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("authorizationInformation", exception.Message);
            }

            [Fact]
            public void GivenNullOpenAuthClientInformationThrowsException()
            {
                var authorizationHeaderGenerator = CreateAuthorizationHeaderGenerator();

                var applicationCredentials = CreateApplicationCredentials();
                var authorizationInformation = CreateAuthorizationInformation();

                var exception = Record.Exception(() => authorizationHeaderGenerator.GenerateHeaderValue(applicationCredentials, authorizationInformation, null));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("openAuthClient", exception.Message);
            }

            [Fact]
            public void GivenValidInputsAndPostMethodIncludeBodyParametersGeneratesCorrectHeader()
            {
                var signer = CreateAuthorizationSigner();
                var authorizationHeaderGenerator = CreateAuthorizationHeaderGenerator(signer);

                var openAuthClient = CreateOpenAuthClient(method: HttpMethod.Post);
                var applicationCredentials = CreateApplicationCredentials();
                var authorizationInformation = CreateAuthorizationInformation();

                var result = authorizationHeaderGenerator.GenerateHeaderValue(applicationCredentials, authorizationInformation, openAuthClient);


                signer.Received().GetSignature(ApplicationSecret, AccessTokenSecret, Url, "POST", Arg.Any<IEnumerable<KeyValuePair<string, string>>[]>());
                Assert.NotNull(result);
                Assert.False(string.IsNullOrWhiteSpace(result.Parameter));
                Assert.Equal(PostResult, result.Parameter);
            }

            [Fact]
            public void GivenValidInputsGeneratesHeader()
            {
                var signer = CreateAuthorizationSigner();
                var authorizationHeaderGenerator = CreateAuthorizationHeaderGenerator(signer);

                var openAuthClient = CreateOpenAuthClient();
                var applicationCredentials = CreateApplicationCredentials();
                var authorizationInformation = CreateAuthorizationInformation();

                var result = authorizationHeaderGenerator.GenerateHeaderValue(applicationCredentials, authorizationInformation, openAuthClient);

                signer.Received().GetSignature(ApplicationSecret, AccessTokenSecret, Url, "GET", Arg.Any<IEnumerable<KeyValuePair<string, string>>[]>());

                Assert.NotNull(result);
                Assert.False(string.IsNullOrWhiteSpace(result.Parameter));
                Assert.Equal(GetResult, result.Parameter);
                Assert.Equal("OAuth", result.Scheme);
            }
        }
    }
}