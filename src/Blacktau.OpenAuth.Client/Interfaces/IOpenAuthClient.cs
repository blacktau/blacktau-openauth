namespace Blacktau.OpenAuth.Client.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOpenAuthClient
    {
        HttpMethod Method { get; set; }

        string Url { get; set; }

        IReadOnlyDictionary<string, string> QueryParameters { get; }

        IReadOnlyDictionary<string, string> BodyParameters { get; }

        void AddBodyParameter(string name, string value);

        void AddQueryParameter(string name, string value);

        void ClearParameters();

        Task<string> Execute();
    }
}