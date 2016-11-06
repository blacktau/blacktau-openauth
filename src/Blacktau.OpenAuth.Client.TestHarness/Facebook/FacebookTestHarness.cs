namespace Blacktau.OpenAuth.Client.TestHarness.Facebook
{
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Client.TestHarness.Tumblr;

    using Microsoft.Extensions.Configuration;

    public class FacebookTestHarness : TestHarnessBase
    {
        private readonly FacebookProvider provider;

        public FacebookTestHarness(IConfigurationRoot configuration)
        {
            this.provider = new FacebookProvider(configuration);
        }

        public async Task Execute()
        {
            this.Print("Executing GetMe");
            await this.ExecuteGetMe();
            this.Print("Done");
        }

        private async Task ExecuteGetMe()
        {
            var test = new GetMe(this.provider);
            var result = await test.Execute();
            this.Print(result);
        }
    }
}