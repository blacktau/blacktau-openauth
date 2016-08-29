namespace Blacktau.OpenAuth.TestHarness
{
    using System;
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Basic;
    using Blacktau.OpenAuth.Interfaces;
    using Blacktau.OpenAuth.TestHarness.Facebook;

    public class Program
    {
        public static void Main(string[] args)
        {
            //TestTwitter();
            //TestTumblr();
            TestFacebook();
            Console.ReadLine();
        }

        private static void TestFacebook()
        {
            var facebookTest = new Facebook.GetMe();
            facebookTest.Execute().ContinueWith(PrintResponse).Wait();
        }

        private static void TestTumblr()
        {
            var tumblrTest = new Tumblr.AddDraftTextPost();
            tumblrTest.Execute().ContinueWith(PrintResponse).Wait();
        }

        private static void TestTwitter()
        {
            var twitterTest = new Twitter.GetUserTimeline();
            twitterTest.Execute().ContinueWith(PrintResponse).Wait();
        }


        private static void PrintResponse(Task<string> obj)
        {
            Console.WriteLine(obj.Result);
            Console.ReadLine();
        }
    }
}