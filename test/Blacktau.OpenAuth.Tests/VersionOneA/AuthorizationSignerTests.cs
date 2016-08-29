namespace Blacktau.OpenAuth.Tests.VersionOneA
{
    using System;
    using System.Collections.Generic;

    using Blacktau.OpenAuth.VersionOneA;

    using NSubstitute;

    using Xunit;

    public class AuthorizationSignerTests
    {
        private const string ApplicationKey = "9PuDTZCFGp4MTUUExK5SIQLUEH7Ttm9xyhBYVIgI";

        private const string Nonce = "3ELd7B";

        private const string SignatureMethod = "HMAC-SHA1";

        private const string Version = "1.0";

        private const string Timestamp = "1468427734";

        private const string ApplicationSecret = "K67tQEKyn2V8R72bvazcodblq0MF545i4Z48cM2M";

        private const string AccessTokenSecret = "301fKUCpOlNYmNmVBtO5TqwfP4Rvo3klPSK4U5vg";

        private const string AuthorizationToken = "b8p4xixWKKEkQFrhWzI73KctuX56xA2bErYmcmQb";

        private const string ExpectedSignatureWithAccessTokenSecret = "vhuI3fKVVyEn7F/3vsc1XJ3hceo=";

        private const string ExpectedSignatureWithOutAccessTokenSecret = "Boi9Auqo323J74NWOaRrOzp4a/4=";

        private const string ExpectedSignatureEmptyParameters = "eU39nvvzN8OiFy1mtc4D0HKs4Rs=";
        
        private const string Url = "http://photos.example.net/photos";

        private const string Method = "GET";

        private static Dictionary<string, string> CreateParameters()
        {
            var result = new Dictionary<string, string>
            {
                {AuthorizationFieldNames.ConsumerKey, ApplicationKey},
                {AuthorizationFieldNames.Nonce, Nonce},
                {AuthorizationFieldNames.SignatureMethod, SignatureMethod},
                {AuthorizationFieldNames.TimeStamp, Timestamp},
                {AuthorizationFieldNames.Token, AuthorizationToken},
                {AuthorizationFieldNames.Version, Version}
            };

            return result;
        }

        private static IEnumerable<KeyValuePair<string, string>> CreateDummyParameters()
        {
            return Substitute.For<IEnumerable<KeyValuePair<string, string>>>();
        }

        public class TheConstructor
        {
            [Fact]
            public void Constructs()
            {
                var authorizationSigner = new AuthorizationSigner();

                Assert.NotNull(authorizationSigner);
            }
        }

        public class TheGetSignatureMethod
        {
            [Fact]
            public void GivenValidInputsGeneratesCorrectSignature()
            {
                var authorizationSigner = new AuthorizationSigner();
                var parameters = CreateParameters();

                var signature = authorizationSigner.GetSignature(ApplicationSecret, AccessTokenSecret, Url, Method, parameters);

                Assert.Equal(ExpectedSignatureWithAccessTokenSecret, signature);
            }

            [Fact]
            public void GivenMultipleParametersGeneratesCorrectSignature()
            {
                var authorizationSigner = new AuthorizationSigner();
                var parameters = CreateParameters();
                var parameters2 = CreateDummyParameters();
                var parameters3 = CreateDummyParameters();

                var signature = authorizationSigner.GetSignature(ApplicationSecret, AccessTokenSecret, Url, Method, parameters, parameters2, parameters3);

                Assert.Equal(ExpectedSignatureWithAccessTokenSecret, signature);
            }

            [Fact]
            public void GivenEmptyParametersGeneratesCorrectSignature()
            {
                var authorizationSigner = new AuthorizationSigner();
                var parameters = CreateDummyParameters();

                var signature = authorizationSigner.GetSignature(ApplicationSecret, AccessTokenSecret, Url, Method, parameters);

                Assert.Equal(ExpectedSignatureEmptyParameters, signature);
            }
            
            [Fact]
            public void GivenNullParametersThrowsException()
            {
                var authorizationSigner = new AuthorizationSigner();

                var exception = Record.Exception(() => authorizationSigner.GetSignature(ApplicationSecret, AccessTokenSecret, Url, Method, null));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("parameters", exception.Message);
            }

            [Fact]
            public void GivenNullApplicationSecretThrowsException()
            {
                var authorizationSigner = new AuthorizationSigner();
                var parameters = CreateParameters();

                var exception = Record.Exception(() => authorizationSigner.GetSignature(null, AccessTokenSecret, Url, Method, parameters));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("applicationSecret", exception.Message);
            }

            [Fact]
            public void GivenNullUrlThrowsException()
            {
                var authorizationSigner = new AuthorizationSigner();
                var parameters = CreateParameters();

                var exception = Record.Exception(() => authorizationSigner.GetSignature(ApplicationSecret, AccessTokenSecret, null, Method, parameters));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("url", exception.Message);
            }

            [Fact]
            public void GivenNullMethodThrowsException()
            {
                var authorizationSigner = new AuthorizationSigner();
                var parameters = CreateParameters();

                var exception = Record.Exception(() => authorizationSigner.GetSignature(ApplicationSecret, AccessTokenSecret, Url, null, parameters));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("method", exception.Message);
            }

            [Fact]
            public void GivenNullAccessTokenSecretGeneratesSignature()
            {
                var authorizationSigner = new AuthorizationSigner();
                var parameters = CreateParameters();

                var signature = authorizationSigner.GetSignature(ApplicationSecret, null, Url, Method, parameters);

                Assert.Equal(ExpectedSignatureWithOutAccessTokenSecret, signature);
            }

        }
    }
}