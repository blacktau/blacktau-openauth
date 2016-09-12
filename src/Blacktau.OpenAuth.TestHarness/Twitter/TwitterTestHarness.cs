namespace Blacktau.OpenAuth.TestHarness.Twitter
{
    using System;
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.TestHarness.Twitter.Lists;
    using Blacktau.OpenAuth.TestHarness.Twitter.Statuses;

    public class TwitterTestHarness
    {
        public async Task Execute()
        {
            await GetUserTimeline();
            await GetMentionsTimeline();
            Print("Creating Test List");
            var listId = await CreateTestList();
            Print("list Created with id: " + listId);
            Print("showing list");
            await ShowTestList(listId);
            Print("Destroying list");
            await DestroyTestList(listId);
            Print("Done");
        }

        private static async Task DestroyTestList(string listId)
        {
            var destroyList = new DestroyList();
            await destroyList.Execute(listId);
        }

        private static async Task GetUserTimeline()
        {
            var getUserTimeline = new GetUserTimeline();
            var result = await getUserTimeline.Execute();
            Print(result);
        }

        private static async Task GetMentionsTimeline()
        {
            var getMentionsTimeline = new GetMentionsTimeline();
            string result = await getMentionsTimeline.Execute();
            Print(result);
        }

        private static async Task<string> CreateTestList()
        {
            var createList = new CreateList();
            return await createList.Execute();
        }

        private static async Task<string> ShowTestList(string listId)
        {
            var showList = new ShowList();
            var result = await showList.Execute(listId);
            Print(result);
            return result;
        }
        
        private static void Print(string response)
        {
            Console.WriteLine(response);
            Console.WriteLine();
        }
    }
}