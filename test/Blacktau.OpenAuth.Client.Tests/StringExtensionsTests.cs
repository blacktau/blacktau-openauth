namespace Blacktau.OpenAuth.Client.Tests
{
    using Xunit;

    public class StringExtensionsTests
    {
        public class TheUrlEncodeMethod
        {
            private const string RegularCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-._~";

            private const string NonRegularCharacters = "={[]}£$!\"";

            private const string NonRegularEncoded = "%3D%7B%5B%5D%7D%C2%A3%24%21%22";

            [Fact]
            public void RegularCharactersAreNotEncoded()
            {
                var encoded = RegularCharacters.UrlEncode();

                Assert.Equal(RegularCharacters, encoded);
            }

            [Fact]
            public void NonRegularCharactersAreUrlEncoded()
            {
                var encoded = NonRegularCharacters.UrlEncode();

                Assert.Equal(NonRegularEncoded, encoded);
            }
        }
    }
}