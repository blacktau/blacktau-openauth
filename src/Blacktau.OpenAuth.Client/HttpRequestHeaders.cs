namespace Blacktau.OpenAuth.Client
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Net.Http.Headers;

    using Blacktau.OpenAuth.Client.Interfaces;

    public class HttpRequestHeaders : IHttpRequestHeaders
    {
        private readonly System.Net.Http.Headers.HttpRequestHeaders headers;

        public HttpRequestHeaders(System.Net.Http.Headers.HttpRequestHeaders headers)
        {
            this.headers = headers;
        }

        public HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue> Accept => this.headers.Accept;

        public HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptCharset => this.headers.AcceptCharset;

        public HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptEncoding => this.headers.AcceptEncoding;

        public HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptLanguage => this.headers.AcceptLanguage;

        public AuthenticationHeaderValue Authorization
        {
            get
            {
                return this.headers.Authorization;
            }

            set
            {
                this.headers.Authorization = value;
            }
        }

        public CacheControlHeaderValue CacheControl
        {
            get
            {
                return this.headers.CacheControl;
            }

            set
            {
                this.headers.CacheControl = value;
            }
        }

        public HttpHeaderValueCollection<string> Connection => this.headers.Connection;

        public bool? ConnectionClose
        {
            get
            {
                return this.headers.ConnectionClose;
            }

            set
            {
                this.headers.ConnectionClose = value;
            }
        }

        public DateTimeOffset? Date
        {
            get
            {
                return this.headers.Date;
            }

            set
            {
                this.headers.Date = value;
            }
        }

        public HttpHeaderValueCollection<NameValueWithParametersHeaderValue> Expect => this.headers.Expect;

        public bool? ExpectContinue
        {
            get
            {
                return this.headers.ExpectContinue;
            }

            set
            {
                this.headers.ExpectContinue = value;
            }
        }

        public string From
        {
            get
            {
                return this.headers.From;
            }

            set
            {
                this.headers.From = value;
            }
        }

        public string Host
        {
            get
            {
                return this.headers.Host;
            }

            set
            {
                this.headers.Host = value;
            }
        }

        public HttpHeaderValueCollection<EntityTagHeaderValue> IfMatch => this.headers.IfMatch;

        public DateTimeOffset? IfModifiedSince
        {
            get
            {
                return this.headers.IfModifiedSince;
            }

            set
            {
                this.headers.IfModifiedSince = value;
            }
        }

        public HttpHeaderValueCollection<EntityTagHeaderValue> IfNoneMatch => this.headers.IfNoneMatch;

        public RangeConditionHeaderValue IfRange
        {
            get
            {
                return this.headers.IfRange;
            }

            set
            {
                this.headers.IfRange = value;
            }
        }

        public DateTimeOffset? IfUnmodifiedSince
        {
            get
            {
                return this.headers.IfUnmodifiedSince;
            }

            set
            {
                this.headers.IfUnmodifiedSince = value;
            }
        }

        public int? MaxForwards
        {
            get
            {
                return this.headers.MaxForwards;
            }

            set
            {
                this.headers.MaxForwards = value;
            }
        }

        public HttpHeaderValueCollection<NameValueHeaderValue> Pragma => this.headers.Pragma;

        public AuthenticationHeaderValue ProxyAuthorization
        {
            get
            {
                return this.headers.ProxyAuthorization;
            }

            set
            {
                this.headers.ProxyAuthorization = value;
            }
        }

        public RangeHeaderValue Range
        {
            get
            {
                return this.headers.Range;
            }

            set
            {
                this.headers.Range = value;
            }
        }

        public Uri Referrer
        {
            get
            {
                return this.headers.Referrer;
            }

            set
            {
                this.headers.Referrer = value;
            }
        }

        public HttpHeaderValueCollection<TransferCodingWithQualityHeaderValue> TE => this.headers.TE;

        public HttpHeaderValueCollection<string> Trailer => this.headers.Trailer;

        public HttpHeaderValueCollection<TransferCodingHeaderValue> TransferEncoding => this.headers.TransferEncoding;

        public bool? TransferEncodingChunked
        {
            get
            {
                return this.headers.TransferEncodingChunked;
            }

            set
            {
                this.headers.TransferEncodingChunked = value;
            }
        }

        public HttpHeaderValueCollection<ProductHeaderValue> Upgrade => this.headers.Upgrade;

        public HttpHeaderValueCollection<ProductInfoHeaderValue> UserAgent => this.headers.UserAgent;

        public HttpHeaderValueCollection<ViaHeaderValue> Via => this.headers.Via;

        public HttpHeaderValueCollection<WarningHeaderValue> Warning => this.headers.Warning;

        public void Add(string name, IEnumerable<string> values)
        {
            this.headers.Add(name, values);
        }

        public void Add(string name, string value)
        {
            this.headers.Add(name, value);
        }

        public void Clear()
        {
            this.headers.Clear();
        }

        public bool Contains(string name)
        {
            return this.headers.Contains(name);
        }

        public IEnumerator<KeyValuePair<string, IEnumerable<string>>> GetEnumerator()
        {
            return this.headers.GetEnumerator();
        }

        public IEnumerable GetValues(string name)
        {
            return this.headers.GetValues(name);
        }

        public bool Remove(string name)
        {
            return this.headers.Remove(name);
        }

        public bool TryAddWithoutValidation(string name, IEnumerable<string> values)
        {
            return this.headers.TryAddWithoutValidation(name, values);
        }

        public bool TryAddWithoutValidation(string name, string value)
        {
            return this.headers.TryAddWithoutValidation(name, value);
        }

        public bool TryGetValues(string name, out IEnumerable<string> values)
        {
            return this.headers.TryGetValues(name, out values);
        }
    }
}