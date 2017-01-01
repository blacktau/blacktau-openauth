namespace Blacktau.OpenAuth.Client.TestHarness.Flickr
{
    using System.Threading.Tasks;


    using Microsoft.Extensions.Configuration;

    public class FlickrTestHarness : TestHarnessBase
    {
        private readonly FlickrProvider flickrProvider;

        public FlickrTestHarness(IConfigurationRoot configuration)
        {
            this.flickrProvider = new FlickrProvider(configuration);
        }

        public async Task Execute()
        {
            this.Print("testing Login");
            var testResult = await this.TestLogin();
            this.Print("Test Result: " + testResult);

            this.Print("testing GetPhotosetsList");
            var photosets = await this.GetPhotosetsList();
            this.Print("GetPhotosetsList Result: " + photosets);

            this.Print("Done");
        }

        private async Task<string> GetPhotosetsList()
        {
            var test = new Photosets.GetList(this.flickrProvider);
            return await test.Execute();
        }

        private async Task<string> TestLogin()
        {
            var testLogin = new TestLogin(this.flickrProvider);
            return await testLogin.Execute();
        }

    }
}