namespace Blacktau.OpenAuth.Tests.VersionOneA
{
    using System.Collections.Generic;

    using Blacktau.OpenAuth.VersionOneA;

    using Xunit;

    public class NonceFactoryTests
    {
        public class TheConstructor
        {
            [Fact]
            public void Constructs()
            {
                var nonceFactory = new NonceFactory();

                Assert.NotNull(nonceFactory);
            }
        }

        public class TheGenerateNonceMethod
        {
            [Fact]
            public void GeneratesANonce()
            {
                var nonceFactory = new NonceFactory();

                var nonce = nonceFactory.GenerateNonce();

                Assert.NotNull(nonce);
                Assert.False(string.IsNullOrWhiteSpace(nonce));
            }

            [Fact]
            public void GeneratesAUniqueNonces()
            {
                var nonceFactory = new NonceFactory();

                var nonces = new List<string>();

                for (var i = 0; i < 1000; i++)
                {
                    var nonce = nonceFactory.GenerateNonce();
                    if (!nonces.Contains(nonce))
                    {
                        nonces.Add(nonce);
                    }
                }

                Assert.True(nonces.Count == 1000);
            }
        }
    }
}