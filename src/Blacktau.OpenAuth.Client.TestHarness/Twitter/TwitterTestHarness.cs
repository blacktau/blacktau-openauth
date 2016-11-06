namespace Blacktau.OpenAuth.Client.TestHarness.Twitter
{
    using System;
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Client.TestHarness.Twitter.Lists;
    using Blacktau.OpenAuth.Client.TestHarness.Twitter.Statuses;

    using Microsoft.Extensions.Configuration;

    public class TwitterTestHarness : TestHarnessBase
    {
        private readonly TwitterProvider twitterProvider;

        public TwitterTestHarness(IConfigurationRoot configuration)
        {
            this.twitterProvider = new TwitterProvider(configuration);
        }

        public async Task Execute()
        {
            await this.GetUserTimeline();
            await this.GetMentionsTimeline();
            Print("Creating Test List");
            var listId = await this.CreateTestList();
            Print("list Created with id: " + listId);
            Print("showing list");
            await this.ShowTestList(listId);
            Print("Destroying list");
            await this.DestroyTestList(listId);
            Print("Done");
        }

        private async Task<string> CreateTestList()
        {
            var createList = new CreateList(this.twitterProvider);
            return await createList.Execute();
        }

        private async Task DestroyTestList(string listId)
        {
            var destroyList = new DestroyList(this.twitterProvider);
            await destroyList.Execute(listId);
        }

        private async Task GetMentionsTimeline()
        {
            var getMentionsTimeline = new GetMentionsTimeline(this.twitterProvider);
            string result = await getMentionsTimeline.Execute();
            Print(result);
        }

        private async Task GetUserTimeline()
        {
            var getUserTimeline = new GetUserTimeline(this.twitterProvider);
            var result = await getUserTimeline.Execute();
            Print(result);
        }

        private async Task<string> ShowTestList(string listId)
        {
            var showList = new ShowList(this.twitterProvider);
            var result = await showList.Execute(listId);
            Print(result);
            return result;
        }
    }
}