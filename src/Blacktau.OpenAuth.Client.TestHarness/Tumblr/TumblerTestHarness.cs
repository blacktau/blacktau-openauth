namespace Blacktau.OpenAuth.Client.TestHarness.Tumblr
{
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Client.TestHarness.Twitter;

    using Microsoft.Extensions.Configuration;

    public class TumblerTestHarness : TestHarnessBase
    {
        private readonly TumblrProvider tumblrProvider;

        public TumblerTestHarness(IConfigurationRoot configuration)
        {
            this.tumblrProvider = new TumblrProvider(configuration);
        }

        public async Task Execute()
        {
            Print("Adding Draft Text Post");
            await this.ExecuteAddDraftTextPost();
            Print("Done");
        }

        private async Task ExecuteAddDraftTextPost()
        {
            var tumblrTest = new AddDraftTextPost(tumblrProvider);
            var result = await tumblrTest.Execute();
            Print(result);
        }
    }
}