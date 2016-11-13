namespace Blacktau.OpenAuth.Client
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using Blacktau.OpenAuth.Client.Interfaces;

    internal class HttpClient : IHttpClient
    {
        private readonly System.Net.Http.HttpClient client;

        public HttpClient()
        {
            this.client = new System.Net.Http.HttpClient();
            this.DefaultRequestHeaders = new HttpRequestHeaders(this.client.DefaultRequestHeaders);
        }

        public HttpClient(HttpMessageHandler handler)
        {
            this.client = new System.Net.Http.HttpClient(handler);
            this.DefaultRequestHeaders = new HttpRequestHeaders(this.client.DefaultRequestHeaders);
        }

        public HttpClient(HttpMessageHandler handler, bool disposeHandler)
        {
            this.client = new System.Net.Http.HttpClient(handler, disposeHandler);
            this.DefaultRequestHeaders = new HttpRequestHeaders(this.client.DefaultRequestHeaders);
        }

        public Uri BaseAddress
        {
            get
            {
                return this.client.BaseAddress;
            }

            set
            {
                this.client.BaseAddress = value;
            }
        }

        public IHttpRequestHeaders DefaultRequestHeaders { get; }

        public long MaxResponseContentBufferSize
        {
            get
            {
                return this.client.MaxResponseContentBufferSize;
            }

            set
            {
                this.client.MaxResponseContentBufferSize = value;
            }
        }

        public TimeSpan Timeout
        {
            get
            {
                return this.client.Timeout;
            }

            set
            {
                this.client.Timeout = value;
            }
        }

        public void CancelPendingRequests()
        {
            this.client.CancelPendingRequests();
        }

        public Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return this.client.DeleteAsync(requestUri);
        }

        public Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken)
        {
            return this.client.DeleteAsync(requestUri, cancellationToken);
        }

        public Task<HttpResponseMessage> DeleteAsync(Uri requestUri)
        {
            return this.client.DeleteAsync(requestUri);
        }

        public Task<HttpResponseMessage> DeleteAsync(Uri requestUri, CancellationToken cancellationToken)
        {
            return this.client.DeleteAsync(requestUri, cancellationToken);
        }

        public void Dispose()
        {
            this.client.Dispose();
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return this.client.GetAsync(requestUri);
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption)
        {
            return this.client.GetAsync(requestUri, completionOption);
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        {
            return this.client.GetAsync(requestUri, completionOption, cancellationToken);
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken)
        {
            return this.client.GetAsync(requestUri, cancellationToken);
        }

        public Task<HttpResponseMessage> GetAsync(Uri requestUri)
        {
            return this.client.GetAsync(requestUri);
        }

        public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption)
        {
            return this.client.GetAsync(requestUri, completionOption);
        }

        public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        {
            return this.client.GetAsync(requestUri, completionOption, cancellationToken);
        }

        public Task<HttpResponseMessage> GetAsync(Uri requestUri, CancellationToken cancellationToken)
        {
            return this.client.GetAsync(requestUri, cancellationToken);
        }

        public Task<byte[]> GetByteArrayAsync(string requestUri)
        {
            return this.client.GetByteArrayAsync(requestUri);
        }

        public Task<byte[]> GetByteArrayAsync(Uri requestUri)
        {
            return this.client.GetByteArrayAsync(requestUri);
        }

        public Task<Stream> GetStreamAsync(string requestUri)
        {
            return this.client.GetStreamAsync(requestUri);
        }

        public Task<Stream> GetStreamAsync(Uri requestUri)
        {
            return this.client.GetStreamAsync(requestUri);
        }

        public Task<string> GetStringAsync(string requestUri)
        {
            return this.client.GetStringAsync(requestUri);
        }

        public Task<string> GetStringAsync(Uri requestUri)
        {
            return this.client.GetStringAsync(requestUri);
        }

        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            return this.client.PostAsync(requestUri, content);
        }

        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return this.client.PostAsync(requestUri, content, cancellationToken);
        }

        public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content)
        {
            return this.client.PostAsync(requestUri, content);
        }

        public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return this.client.PostAsync(requestUri, content, cancellationToken);
        }

        public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
        {
            return this.client.PutAsync(requestUri, content);
        }

        public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return this.client.PutAsync(requestUri, content, cancellationToken);
        }

        public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content)
        {
            return this.client.PutAsync(requestUri, content);
        }

        public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return this.client.PutAsync(requestUri, content, cancellationToken);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return this.client.SendAsync(request);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption)
        {
            return this.client.SendAsync(request, completionOption);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        {
            return this.client.SendAsync(request, completionOption, cancellationToken);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return this.client.SendAsync(request, cancellationToken);
        }
    }
}