namespace Blacktau.OpenAuth.Tests.VersionOneA
{
    using System;
    using System.Globalization;

    using Blacktau.OpenAuth.Interfaces;
    using Blacktau.OpenAuth.Interfaces.VersionOneA;
    using Blacktau.OpenAuth.VersionOneA;

    using NSubstitute;

    using Xunit;

    public class AuthorizationParametersGeneratorTests
    {
        private const string Nonce = "Nonce_dVAfwvkuhxunRqqMegJKXMGANNustyNDWBJs";

        private const long Ticks = 635965000000000000;

        private const string ApplicationKey = "NotAReal_ApplicationKey";

        private const string ApplicationSecret = "NotAReal_ApplicationSecret";

        private const string HmacSha1 = "HMAC-SHA1";

        private const string AccessToken = "accessToken";

        private const string VersionOnePointZero = "1.0";

        private static INonceFactory CreateMockNonceFactory(string nonce)
        {
            var nonceFactory = Substitute.For<INonceFactory>();
            nonceFactory.GenerateNonce().Returns(nonce);
            return nonceFactory;
        }

        private static ITimeStampFactory CreateMockTimeStampFactory(TimeSpan timeSpanFromEpoch)
        {
            var timeStampFactory = Substitute.For<ITimeStampFactory>();

            timeStampFactory.GetTimeSpanFromEpoch().Returns(timeSpanFromEpoch);

            return timeStampFactory;
        }

        private static IApplicationCredentials CreateMockApplicationCredentials()
        {
            var credentials = Substitute.For<IApplicationCredentials>();

            credentials.ApplicationKey.Returns(ApplicationKey);
            credentials.ApplicationSecret.Returns(ApplicationSecret);
            return credentials;
        }

        public class TheConstructor
        {
            [Fact]
            public void GivenNullNonceFactoryThrowsArguementNullException()
            {
                var timestampFactoryMock = Substitute.For<ITimeStampFactory>();

                var exception = Record.Exception(() => new AuthorizationParametersGenerator(null, timestampFactoryMock));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("nonceFactory", exception.Message);
            }

            [Fact]
            public void GivenNullTimeStampFactoryThrowsArguementNullException()
            {
                var nonceFactoryMock = Substitute.For<INonceFactory>();

                var exception = Record.Exception(() => new AuthorizationParametersGenerator(nonceFactoryMock, null));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("timeStampFactory", exception.Message);
            }

            [Fact]
            public void GivenValidInputConstucts()
            {
                var nonceFactoryMock = Substitute.For<INonceFactory>();
                var timestampFactoryMock = Substitute.For<ITimeStampFactory>();

                var generator = new AuthorizationParametersGenerator(nonceFactoryMock, timestampFactoryMock);

                Assert.NotNull(generator);
            }
        }

        public class TheCreateStandardParameterSetMethod
        {
            [Fact]
            public void GivenNullApplicationCredentialsThrowsException()
            {
                var nonceFactory = CreateMockNonceFactory(Nonce);
                var timeStampFactory = CreateMockTimeStampFactory(new TimeSpan(Ticks));

                var generator = new AuthorizationParametersGenerator(nonceFactory, timeStampFactory);


                var exception = Record.Exception(() => generator.CreateStandardParameterSet(null));
                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("credentials", exception.Message);
            }

            [Fact]
            public void GivenValidApplicationCredentialsCreatesStandardParameters()
            {
                var nonceFactory = CreateMockNonceFactory(Nonce);
                var timeStampFactory = CreateMockTimeStampFactory(new TimeSpan(Ticks));
                var applicationCredentials = CreateMockApplicationCredentials();
                var generator = new AuthorizationParametersGenerator(nonceFactory, timeStampFactory);

                var standardParameters = generator.CreateStandardParameterSet(applicationCredentials);

                Assert.NotNull(standardParameters);
            }

            [Fact]
            public void StandardParametersContainsConsumerKey()
            {
                var nonceFactory = CreateMockNonceFactory(Nonce);
                var timeStampFactory = CreateMockTimeStampFactory(new TimeSpan(Ticks));
                var applicationCredentials = CreateMockApplicationCredentials();
                var generator = new AuthorizationParametersGenerator(nonceFactory, timeStampFactory);

                var standardParameters = generator.CreateStandardParameterSet(applicationCredentials);

                Assert.True(standardParameters.ContainsKey(AuthorizationFieldNames.ConsumerKey));
            }

            [Fact]
            public void ConsumerKeyIsApplicationKey()
            {
                var nonceFactory = CreateMockNonceFactory(Nonce);
                var timeStampFactory = CreateMockTimeStampFactory(new TimeSpan(Ticks));
                var applicationCredentials = CreateMockApplicationCredentials();
                var generator = new AuthorizationParametersGenerator(nonceFactory, timeStampFactory);

                var standardParameters = generator.CreateStandardParameterSet(applicationCredentials);

                Assert.Equal(ApplicationKey, standardParameters[AuthorizationFieldNames.ConsumerKey]);
            }

            [Fact]
            public void StandardParametersContainsNonce()
            {
                var nonceFactory = CreateMockNonceFactory(Nonce);
                var timeStampFactory = CreateMockTimeStampFactory(new TimeSpan(Ticks));
                var applicationCredentials = CreateMockApplicationCredentials();
                var generator = new AuthorizationParametersGenerator(nonceFactory, timeStampFactory);

                var standardParameters = generator.CreateStandardParameterSet(applicationCredentials);

                Assert.True(standardParameters.ContainsKey(AuthorizationFieldNames.Nonce));
            }

            [Fact]
            public void NonceInResultIsNonce()
            {
                var nonceFactory = CreateMockNonceFactory(Nonce);
                var timeStampFactory = CreateMockTimeStampFactory(new TimeSpan(Ticks));
                var applicationCredentials = CreateMockApplicationCredentials();

                var generator = new AuthorizationParametersGenerator(nonceFactory, timeStampFactory);

                var standardParameters = generator.CreateStandardParameterSet(applicationCredentials);

                Assert.Equal(Nonce, standardParameters[AuthorizationFieldNames.Nonce]);
            }
            
            [Fact]
            public void StandardParametersContainsSignatureMethod()
            {
                var nonceFactory = CreateMockNonceFactory(Nonce);
                var timeStampFactory = CreateMockTimeStampFactory(new TimeSpan(Ticks));
                var applicationCredentials = CreateMockApplicationCredentials();
                var generator = new AuthorizationParametersGenerator(nonceFactory, timeStampFactory);

                var standardParameters = generator.CreateStandardParameterSet(applicationCredentials);

                Assert.True(standardParameters.ContainsKey(AuthorizationFieldNames.SignatureMethod));
            }

            [Fact]
            public void SignatureMethodIsHmacsha1()
            {
                var nonceFactory = CreateMockNonceFactory(Nonce);
                var timeStampFactory = CreateMockTimeStampFactory(new TimeSpan(Ticks));
                var applicationCredentials = CreateMockApplicationCredentials();
                var generator = new AuthorizationParametersGenerator(nonceFactory, timeStampFactory);

                var standardParameters = generator.CreateStandardParameterSet(applicationCredentials);

                Assert.Equal(HmacSha1, standardParameters[AuthorizationFieldNames.SignatureMethod]);
            }

            [Fact]
            public void StandardParametersContainsTimestamp()
            {
                var nonceFactory = CreateMockNonceFactory(Nonce);
                var timeStampFactory = CreateMockTimeStampFactory(new TimeSpan(Ticks));
                var applicationCredentials = CreateMockApplicationCredentials();
                var generator = new AuthorizationParametersGenerator(nonceFactory, timeStampFactory);

                var standardParameters = generator.CreateStandardParameterSet(applicationCredentials);

                Assert.True(standardParameters.ContainsKey(AuthorizationFieldNames.TimeStamp));
            }

            [Fact]
            public void TimestampIsTimestamp()
            {
                var nonceFactory = CreateMockNonceFactory(Nonce);
                var timestamp = new TimeSpan(Ticks);
                var timeStampFactory = CreateMockTimeStampFactory(timestamp);
                var applicationCredentials = CreateMockApplicationCredentials();
                var generator = new AuthorizationParametersGenerator(nonceFactory, timeStampFactory);

                var standardParameters = generator.CreateStandardParameterSet(applicationCredentials);

                Assert.Equal(timestamp.TotalSeconds.ToString(CultureInfo.InvariantCulture), standardParameters[AuthorizationFieldNames.TimeStamp]);
            }


            [Fact]
            public void StandardParametersContainsVersion()
            {
                var nonceFactory = CreateMockNonceFactory(Nonce);
                var timeStampFactory = CreateMockTimeStampFactory(new TimeSpan(Ticks));
                var applicationCredentials = CreateMockApplicationCredentials();
                var generator = new AuthorizationParametersGenerator(nonceFactory, timeStampFactory);

                var standardParameters = generator.CreateStandardParameterSet(applicationCredentials);

                Assert.True(standardParameters.ContainsKey(AuthorizationFieldNames.Version));
            }

            [Fact]
            public void VersionIsOnePointZero()
            {
                var nonceFactory = CreateMockNonceFactory(Nonce);
                var timestamp = new TimeSpan(Ticks);
                var timeStampFactory = CreateMockTimeStampFactory(timestamp);
                var applicationCredentials = CreateMockApplicationCredentials();
                var generator = new AuthorizationParametersGenerator(nonceFactory, timeStampFactory);

                var standardParameters = generator.CreateStandardParameterSet(applicationCredentials);

                Assert.Equal(standardParameters[AuthorizationFieldNames.Version], VersionOnePointZero);
            }

        }

        public class TheGetAuthorizationParametersMethod
        {
            [Fact]
            public void GivenEmptyAccessTokenThrowsException()
            {
                var nonceFactory = CreateMockNonceFactory(Nonce);
                var timeStampFactory = CreateMockTimeStampFactory(new TimeSpan(Ticks));
                var applicationCredentials = CreateMockApplicationCredentials();
                var generator = new AuthorizationParametersGenerator(nonceFactory, timeStampFactory);

                var exception = Record.Exception(() => generator.GetAuthorizationParameters(applicationCredentials, string.Empty));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentException>(exception);
                Assert.Contains(AccessToken, exception.Message);
            }

            [Fact]
            public void GivenNullAccessTokenThrowsException()
            {
                var nonceFactory = CreateMockNonceFactory(Nonce);
                var timeStampFactory = CreateMockTimeStampFactory(new TimeSpan(Ticks));
                var applicationCredentials = CreateMockApplicationCredentials();
                var generator = new AuthorizationParametersGenerator(nonceFactory, timeStampFactory);

                var exception = Record.Exception(() => generator.GetAuthorizationParameters(applicationCredentials, null));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains(AccessToken, exception.Message);
            }

            [Fact]
            public void GivenNullApplicationCredentialsThrowsException()
            {
                var nonceFactory = CreateMockNonceFactory(Nonce);
                var timeStampFactory = CreateMockTimeStampFactory(new TimeSpan(Ticks));
                var generator = new AuthorizationParametersGenerator(nonceFactory, timeStampFactory);

                var exception = Record.Exception(() => generator.GetAuthorizationParameters(null, AccessToken));

                Assert.NotNull(exception);
                Assert.IsType<ArgumentNullException>(exception);
                Assert.Contains("credentials", exception.Message);
            }

            [Fact]
            public void GivenValidInputResultContainsStandardParameters()
            {
                var nonceFactory = CreateMockNonceFactory(Nonce);
                var timestamp = new TimeSpan(Ticks);
                var timeStampFactory = CreateMockTimeStampFactory(timestamp);
                var applicationCredentials = CreateMockApplicationCredentials();
                var generator = new AuthorizationParametersGenerator(nonceFactory, timeStampFactory);

                var authorizationParameters = generator.GetAuthorizationParameters(applicationCredentials, AccessToken);

                Assert.True(authorizationParameters.ContainsKey(AuthorizationFieldNames.ConsumerKey));
                Assert.True(authorizationParameters.ContainsKey(AuthorizationFieldNames.Nonce));
                Assert.True(authorizationParameters.ContainsKey(AuthorizationFieldNames.SignatureMethod));
                Assert.True(authorizationParameters.ContainsKey(AuthorizationFieldNames.TimeStamp));
                Assert.True(authorizationParameters.ContainsKey(AuthorizationFieldNames.Version));

                Assert.Equal(ApplicationKey, authorizationParameters[AuthorizationFieldNames.ConsumerKey]);
                Assert.Equal(Nonce, authorizationParameters[AuthorizationFieldNames.Nonce]);
                Assert.Equal(HmacSha1, authorizationParameters[AuthorizationFieldNames.SignatureMethod]);
                Assert.Equal(timestamp.TotalSeconds.ToString(CultureInfo.InvariantCulture), authorizationParameters[AuthorizationFieldNames.TimeStamp]);
                Assert.Equal(VersionOnePointZero, authorizationParameters[AuthorizationFieldNames.Version]);
            }

            [Fact]
            public void GivenValidInputResultContainsToken()
            {
                var nonceFactory = CreateMockNonceFactory(Nonce);
                var timestamp = new TimeSpan(Ticks);
                var timeStampFactory = CreateMockTimeStampFactory(timestamp);
                var applicationCredentials = CreateMockApplicationCredentials();
                var generator = new AuthorizationParametersGenerator(nonceFactory, timeStampFactory);

                var authorizationParameters = generator.GetAuthorizationParameters(applicationCredentials, AccessToken);

                Assert.True(authorizationParameters.ContainsKey(AuthorizationFieldNames.Token));
            }

            [Fact]
            public void TokenIsAccessToken()
            {
                var nonceFactory = CreateMockNonceFactory(Nonce);
                var timestamp = new TimeSpan(Ticks);
                var timeStampFactory = CreateMockTimeStampFactory(timestamp);
                var applicationCredentials = CreateMockApplicationCredentials();
                var generator = new AuthorizationParametersGenerator(nonceFactory, timeStampFactory);

                var authorizationParameters = generator.GetAuthorizationParameters(applicationCredentials, AccessToken);

                Assert.Equal(AccessToken, authorizationParameters[AuthorizationFieldNames.Token]);
            }

        }
    }
}