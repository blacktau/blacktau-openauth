namespace Blacktau.OpenAuth.Interfaces
{
    using System.Threading.Tasks;

    public interface IOpenAuthClient
    {
        HttpMethod Method { get; set; }

        string Url { get; set; }

        Task<string> Execute();

        void AddQueryParameter(string name, string value);

        void AddBodyParameter(string name, string value);

        void ClearParameters();
    }
}