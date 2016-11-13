namespace Blacktau.OpenAuth.Client.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Xunit;

    public class QueryParameterExtensionTests
    {
        public class TheQueryParameterStringToDictionaryMethod
        {
            [Fact]
            public void GivenQueryStringReturnsDictionary()
            {
                var queryString = "ts=1231231232222&st=223232&page=32&ap=123442&whatever=somewordshere";

                var result = queryString.QueryParameterStringToDictionary();

                Assert.IsAssignableFrom<IDictionary<string, string>>(result);
            }

            [Fact]
            public void GivenNullReturnsEmptyDictionary()
            {
                string queryString = null;

                var result = queryString.QueryParameterStringToDictionary();

                Assert.IsAssignableFrom<IDictionary<string, string>>(result);
                Assert.Equal(0, result.Count);
            }

            [Fact]
            public void GivenEmptyStringReturnsEmptyDictionary()
            {
                string queryString = string.Empty;

                var result = queryString.QueryParameterStringToDictionary();

                Assert.IsAssignableFrom<IDictionary<string, string>>(result);
                Assert.Equal(0, result.Count);
            }

            [Fact]
            public void GivenNonQueryStringReturnsEmptyDictionary()
            {
                string queryString = "not a query string";

                var result = queryString.QueryParameterStringToDictionary();

                Assert.IsAssignableFrom<IDictionary<string, string>>(result);
                Assert.Equal(0, result.Count);
            }

            [Fact]
            public void GivenSingleEntryStringReturnsDictionaryLength1()
            {
                string queryString = "parameter1=one";

                var result = queryString.QueryParameterStringToDictionary();

                Assert.IsAssignableFrom<IDictionary<string, string>>(result);
                Assert.Equal(1, result.Count);
            }

            [Fact]
            public void GivenSingleEntryStringReturnsDictionaryContainingEntryAndValue()
            {
                string queryString = "parameter1=one";

                var result = queryString.QueryParameterStringToDictionary();

                Assert.IsAssignableFrom<IDictionary<string, string>>(result);
                Assert.Contains("parameter1", result.Keys);
                Assert.Equal("one", result["parameter1"]);
            }

            [Fact]
            public void GivenMultipleEntryStringReturnsDictionaryContainingEqualEntries()
            {
                int max = (new Random()).Next(2, 10000);

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < max; i++)
                {
                    sb.Append($"parameter{i}=value{i}&");
                }

                string queryString = sb.ToString();

                var result = queryString.QueryParameterStringToDictionary();

                Assert.IsAssignableFrom<IDictionary<string, string>>(result);
                Assert.Equal(max, result.Count);
            }

        }

        public class TheToQueryStringMethod
        {
            [Fact]
            public void GivenNullReturnsEmptyString()
            {
                IDictionary<string, string> parameters = null;

                var result = parameters.ToQueryString();

                Assert.Equal(string.Empty, result);
            }

            [Fact]
            public void GivenEmptyDictionaryReturnsEmptyString()
            {
                IDictionary<string, string> parameters = new Dictionary<string, string>();

                var result = parameters.ToQueryString();

                Assert.Equal(string.Empty, result);
            }

            [Fact]
            public void GivenDictionaryWithOneValueReturnsQueryString()
            {
                IDictionary<string, string> parameters = new Dictionary<string, string>() { { "parameter1", "value1" } };

                var result = parameters.ToQueryString();

                Assert.Equal("parameter1=value1", result);
            }

            [Fact]
            public void GivenDictionaryWithMultipleEntriesReturnsQueryString()
            {
                IDictionary<string, string> parameters = new Dictionary<string, string>()
                                                             {
                                                                     { "parameter1", "value1" },
                                                                     { "parameter2", "value2" },
                                                                     { "parameter3", "value3" },
                                                                     { "parameter4", "value4" },
                                                                     { "parameter5", "value5" },

                                                             };

                var result = parameters.ToQueryString();

                Assert.Equal("parameter1=value1&parameter2=value2&parameter3=value3&parameter4=value4&parameter5=value5", result);
            }


        }
    }
}