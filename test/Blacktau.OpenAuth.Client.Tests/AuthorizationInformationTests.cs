namespace Blacktau.OpenAuth.Client.Tests
{
    using System;

    using Xunit;

    public class AuthorizationInformationTests
    {
        public class TheAccessTokenProperty
        {
            [Fact]
            public void CanBeSet()
            {
                string accessToken = Guid.NewGuid().ToString();

                var authorizationInformation = new AuthorizationInformation();

                Assert.Null(authorizationInformation.AccessToken);

                authorizationInformation.AccessToken = accessToken;

                Assert.NotNull(authorizationInformation.AccessToken);
            }

            [Fact]
            public void IsDefaultNull()
            {
                var authorizationInformation = new AuthorizationInformation();

                Assert.Null(authorizationInformation.AccessToken);
            }

            [Fact]
            public void WhenSetReturnsTheSameValue()
            {
                string accessToken = Guid.NewGuid().ToString();

                var authorizationInformation = new AuthorizationInformation();

                Assert.Null(authorizationInformation.AccessToken);

                authorizationInformation.AccessToken = accessToken;

                Assert.Equal(accessToken, authorizationInformation.AccessToken);
            }
        }

        public class TheAccessTokenSecretProperty
        {
            [Fact]
            public void CanBeSet()
            {
                string accessTokenSecret = Guid.NewGuid().ToString();

                var authorizationInformation = new AuthorizationInformation();

                Assert.Null(authorizationInformation.AccessTokenSecret);

                authorizationInformation.AccessTokenSecret = accessTokenSecret;

                Assert.NotNull(authorizationInformation.AccessTokenSecret);
            }

            [Fact]
            public void IsDefaultNull()
            {
                var authorizationInformation = new AuthorizationInformation();

                Assert.Null(authorizationInformation.AccessTokenSecret);
            }

            [Fact]
            public void WhenSetReturnsTheSameValue()
            {
                string accessTokenSecret = Guid.NewGuid().ToString();

                var authorizationInformation = new AuthorizationInformation();

                Assert.Null(authorizationInformation.AccessTokenSecret);

                authorizationInformation.AccessTokenSecret = accessTokenSecret;

                Assert.Equal(accessTokenSecret, authorizationInformation.AccessTokenSecret);
            }
        }

        public class TheConstrctor
        {
            [Fact]
            public void Constructs()
            {
                var authorizationInformation = new AuthorizationInformation();

                Assert.NotNull(authorizationInformation);
            }
        }

        public class TheExpiresProperty
        {
            private static readonly Random RandomGenerator = new Random();

            [Fact]
            public void CanBeSet()
            {
                DateTime? expires = GetRandomDateTime();

                var authorizationInformation = new AuthorizationInformation();

                Assert.Null(authorizationInformation.Expires);

                authorizationInformation.Expires = expires;

                Assert.NotNull(authorizationInformation.Expires);
            }

            [Fact]
            public void IsDefaultNull()
            {
                var authorizationInformation = new AuthorizationInformation();

                Assert.Null(authorizationInformation.Expires);
            }

            [Fact]
            public void WhenSetReturnsTheSameValue()
            {
                DateTime? expires = GetRandomDateTime();

                var authorizationInformation = new AuthorizationInformation();

                Assert.Null(authorizationInformation.Expires);

                authorizationInformation.Expires = expires;

                Assert.Equal(expires, authorizationInformation.Expires);
            }

            [Fact]
            public void CanBeSetToNull()
            {
                DateTime? expires = null;

                var authorizationInformation = new AuthorizationInformation();

                Assert.Null(authorizationInformation.Expires);

                authorizationInformation.Expires = expires;

                Assert.Equal(expires, authorizationInformation.Expires);
            }

            private static DateTime GetRandomDateTime()
            {
                DateTime start = new DateTime(1900, 1, 1); 

                long randomTicks = GetRandomInt(1469337836) * GetRandomInt(int.MaxValue);
                
                return start.AddTicks(randomTicks);
            }

            private static long GetRandomInt(int max)
            {
                return RandomGenerator.Next(0, max);
            }
        }

        public class TheRefreshTokenProperty
        {
            [Fact]
            public void CanBeSet()
            {
                string refreshToken = Guid.NewGuid().ToString();

                var authorizationInformation = new AuthorizationInformation();

                Assert.Null(authorizationInformation.RefreshToken);

                authorizationInformation.RefreshToken = refreshToken;

                Assert.NotNull(authorizationInformation.RefreshToken);
            }

            [Fact]
            public void IsDefaultNull()
            {
                var authorizationInformation = new AuthorizationInformation();

                Assert.Null(authorizationInformation.RefreshToken);
            }

            [Fact]
            public void WhenSetReturnsTheSameValue()
            {
                string refreshToken = Guid.NewGuid().ToString();

                var authorizationInformation = new AuthorizationInformation();

                Assert.Null(authorizationInformation.RefreshToken);

                authorizationInformation.RefreshToken = refreshToken;

                Assert.Equal(refreshToken, authorizationInformation.RefreshToken);
            }
        }
    }
}