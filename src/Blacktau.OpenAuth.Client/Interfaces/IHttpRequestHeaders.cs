namespace Blacktau.OpenAuth.Client.Interfaces
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Net.Http.Headers;

    public interface IHttpRequestHeaders
    {
        void Add(string name, IEnumerable<string> values);

        void Add(string name, string value);

        void Clear();

        bool Contains(string name);

        IEnumerator<KeyValuePair<string, IEnumerable<string>>> GetEnumerator();

        IEnumerable GetValues(string name);

        bool Remove(string name);

        bool TryAddWithoutValidation(string name, IEnumerable<string> values);

        bool TryAddWithoutValidation(string name, string value);

        bool TryGetValues(string name, out IEnumerable<string> values);

        HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue> Accept { get; }

        HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptCharset { get; }

        HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptEncoding { get; }

        HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptLanguage { get; }

        AuthenticationHeaderValue Authorization { get; set; }

        CacheControlHeaderValue CacheControl { get; set; }

        HttpHeaderValueCollection<string> Connection { get; }

        bool? ConnectionClose { get; set; }

        DateTimeOffset? Date { get; set; }

        HttpHeaderValueCollection<NameValueWithParametersHeaderValue> Expect { get; }

        bool? ExpectContinue { get; set; }

        string From { get; set; }

        string Host { get; set; }

        HttpHeaderValueCollection<EntityTagHeaderValue> IfMatch { get; }

        DateTimeOffset? IfModifiedSince { get; set; }

        HttpHeaderValueCollection<EntityTagHeaderValue> IfNoneMatch { get; }

        RangeConditionHeaderValue IfRange { get; set; }

        DateTimeOffset? IfUnmodifiedSince { get; set; }

        int? MaxForwards { get; set; }

        HttpHeaderValueCollection<NameValueHeaderValue> Pragma { get; }

        AuthenticationHeaderValue ProxyAuthorization { get; set; }

        RangeHeaderValue Range { get; set; }

        Uri Referrer { get; set; }

        HttpHeaderValueCollection<TransferCodingWithQualityHeaderValue> TE { get; }

        HttpHeaderValueCollection<string> Trailer { get; }

        HttpHeaderValueCollection<TransferCodingHeaderValue> TransferEncoding { get; }

        bool? TransferEncodingChunked { get; set; }

        HttpHeaderValueCollection<ProductHeaderValue> Upgrade { get; }

        HttpHeaderValueCollection<ProductInfoHeaderValue> UserAgent { get; }

        HttpHeaderValueCollection<ViaHeaderValue> Via { get; }

        HttpHeaderValueCollection<WarningHeaderValue> Warning { get; }
    }
}