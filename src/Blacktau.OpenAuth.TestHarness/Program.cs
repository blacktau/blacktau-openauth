namespace Blacktau.OpenAuth.TestHarness
{
    using System;
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Basic;
    using Blacktau.OpenAuth.Interfaces;

    public class Program
    {
        private const string TumblrAccessToken = "<Tumblr Access Token Goes Here>";

        private const string TumblrAccessTokenSecret = "<Tumblr Access Token Secret Goes Here>";

        private const string TwitterAccessToken = "<Twitter Access Token Goes Here>";

        private const string TwitterAccessTokenSecret = "<Twitter Access Token Secret Goes Here>";

        private const string TumblrApplicationKey = "<Tumblr Application/Consumer Key Goes Here>";

        private const string TumblrApplicationSecret = "<Tumblr Application/Consumer Secret Goes Here>";

        private const string TwitterApplicationKey = "<Twitter Application/Consumer Key Goes Here>";

        private const string TwitterApplicationSecret = "<Twitter Application/Consumer Secret Goes Here>";

        public static void Main(string[] args)
        {
            TestTwitter();
            TestTumblr();
            Console.ReadLine();
        }
        
        private static void TestTumblr()
        {
            IApplicationCredentials applicationCredentials = CreateTumblrApplicationCredentials();

            IAuthorizationInformation authorizationInformation = CreateTumblrAuthorizationInformation();

            var openAuthClientFactory = new OpenAuthClientFactory(applicationCredentials, authorizationInformation);

            var openAuthClient = openAuthClientFactory.CreateOpenAuthClient("https://api.tumblr.com/v2/blog/photography.blacktau.com/post", HttpMethod.Post, OpenAuthVersion.OneA);

            openAuthClient.AddBodyParameter("type", "text");
            openAuthClient.AddBodyParameter("state", "draft");
            openAuthClient.AddBodyParameter("title", "A test from Blacktau.OpenAuth");
            openAuthClient.AddBodyParameter("body", "just a test post from Blacktau.OpenAuth to see if everything works. Ԑթל֍ﭠ");

            openAuthClient.Execute().ContinueWith(PrintResponse);
        }


        private static void TestTwitter()
        {
            IApplicationCredentials applicationCredentials = CreateTwitterApplicationCredentials();

            IAuthorizationInformation authorizationInformation = CreateTwitterAuthorizationInformation();

            var openAuthClientFactory = new OpenAuthClientFactory(applicationCredentials, authorizationInformation);

            var openAuthClient = openAuthClientFactory.CreateOpenAuthClient("https://api.twitter.com/1.1/statuses/user_timeline.json", HttpMethod.Get, OpenAuthVersion.OneA);

            openAuthClient.AddQueryParameter("screen_name", "blacktau");
            openAuthClient.AddQueryParameter("count", "2");

            openAuthClient.Execute().ContinueWith(PrintResponse);
        }

        private static ApplicationCredentials CreateTumblrApplicationCredentials()
        {
            return new ApplicationCredentials
            {
                ApplicationKey = TumblrApplicationKey,
                ApplicationSecret = TumblrApplicationSecret
            };
        }

        private static AuthorizationInformation CreateTumblrAuthorizationInformation()
        {
            return new AuthorizationInformation(TumblrAccessToken) { AccessTokenSecret = TumblrAccessTokenSecret };
        }

        private static ApplicationCredentials CreateTwitterApplicationCredentials()
        {
            return new ApplicationCredentials
            {
                ApplicationKey = TwitterApplicationKey,
                ApplicationSecret = TwitterApplicationSecret
            };
        }

        private static AuthorizationInformation CreateTwitterAuthorizationInformation()
        {
            return new AuthorizationInformation(TwitterAccessToken) { AccessTokenSecret = TwitterAccessTokenSecret };
        }

        private static void PrintResponse(Task<string> obj)
        {
            Console.WriteLine(obj.Result);
            Console.ReadLine();
        }
    }
}