namespace Blacktau.OpenAuth.Client.TestHarness
{
    using System;
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Client.TestHarness.Facebook;
    using Blacktau.OpenAuth.Client.TestHarness.Tumblr;
    using Blacktau.OpenAuth.Client.TestHarness.Twitter;

    using Microsoft.Extensions.Configuration;

    public class Program
    {
        public static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder().AddUserSecrets();
            var configuration = configurationBuilder.Build();

            TestTwitter(configuration);
            TestTumblr(configuration);
            TestFacebook(configuration);
            Console.ReadLine();
        }

        private static void TestTwitter(IConfigurationRoot configuration)
        {
            var twitterTestHarness = new TwitterTestHarness(configuration);
            twitterTestHarness.Execute().Wait();
        }

        private static void TestFacebook(IConfigurationRoot configuration)
        {
            var testHarness = new FacebookTestHarness(configuration);
            testHarness.Execute().Wait();
        }

        private static void TestTumblr(IConfigurationRoot configuration)
        {
            var testHarness = new TumblerTestHarness(configuration);
            testHarness.Execute().Wait();
        }
        
        private static void PrintResponse(Task<string> obj)
        {
            if (obj.IsFaulted)
            {
                Console.WriteLine(obj.Exception.ToString());
                return;
            }

            Console.WriteLine(obj.Result);
        }
    }
}