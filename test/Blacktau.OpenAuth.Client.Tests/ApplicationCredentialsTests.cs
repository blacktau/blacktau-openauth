namespace Blacktau.OpenAuth.Client.Tests
{
    using System;

    using Xunit;

    public class ApplicationCredentialsTests
    {
        public class TheConstrctor
        {
            [Fact]
            public void Constructs()
            {
                var applicationCredentials = new ApplicationCredentials();

                Assert.NotNull(applicationCredentials);
            }
        }

        public class TheApplicationKeyProperty
        {
            [Fact]
            public void IsDefaultNull()
            {
                var applicationCredentials = new ApplicationCredentials();
                
                Assert.Null(applicationCredentials.ApplicationKey);
            }


            [Fact]
            public void CanBeSet()
            {
                string appKey = Guid.NewGuid().ToString();

                var applicationCredentials = new ApplicationCredentials();

                Assert.Null(applicationCredentials.ApplicationKey);

                applicationCredentials.ApplicationKey = appKey;

                Assert.NotNull(applicationCredentials.ApplicationKey);
            }

            [Fact]
            public void WhenSetReturnsTheSameValue()
            {
                string appKey = Guid.NewGuid().ToString();

                var applicationCredentials = new ApplicationCredentials();

                Assert.Null(applicationCredentials.ApplicationKey);

                applicationCredentials.ApplicationKey = appKey;

                Assert.Equal(appKey, applicationCredentials.ApplicationKey);
            }
        }

        public class TheApplicationSecretProperty
        {
            [Fact]
            public void IsDefaultNull()
            {
                var applicationCredentials = new ApplicationCredentials();

                Assert.Null(applicationCredentials.ApplicationSecret);
            }


            [Fact]
            public void CanBeSet()
            {
                string appKey = Guid.NewGuid().ToString();

                var applicationCredentials = new ApplicationCredentials();

                Assert.Null(applicationCredentials.ApplicationSecret);

                applicationCredentials.ApplicationSecret = appKey;

                Assert.NotNull(applicationCredentials.ApplicationSecret);
            }

            [Fact]
            public void WhenSetReturnsTheSameValue()
            {
                string appKey = Guid.NewGuid().ToString();

                var applicationCredentials = new ApplicationCredentials();

                Assert.Null(applicationCredentials.ApplicationSecret);

                applicationCredentials.ApplicationSecret = appKey;

                Assert.Equal(appKey, applicationCredentials.ApplicationSecret);
            }
        }
    }
}
