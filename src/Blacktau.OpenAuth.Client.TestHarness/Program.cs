namespace Blacktau.OpenAuth.Client.TestHarness
{
    using System;
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Client.TestHarness.Facebook;
    using Blacktau.OpenAuth.Client.TestHarness.Tumblr;
    using Blacktau.OpenAuth.Client.TestHarness.Twitter;

    public class Program
    {
        public static void Main(string[] args)
        {
            TestTwitter();
            TestTumblr();
            TestFacebook();
            Console.ReadLine();
        }

        private static void TestTwitter()
        {
            var twitterTestHarness = new TwitterTestHarness();
            twitterTestHarness.Execute().Wait();
        }

        private static void TestFacebook()
        {
            var facebookTest = new GetMe();
            facebookTest.Execute().ContinueWith(PrintResponse).Wait();
        }

        private static void TestTumblr()
        {
            var tumblrTest = new AddDraftTextPost();
            tumblrTest.Execute().ContinueWith(PrintResponse).Wait();
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