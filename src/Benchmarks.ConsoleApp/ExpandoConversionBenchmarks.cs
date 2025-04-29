using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks.ConsoleApp
{
    [MemoryDiagnoser]
    public class ExpandoConversionBenchmarks
    {
        private RpcHttp _input;

        [GlobalSetup]
        public void Setup()
        {
            _input = new RpcHttp
            {
                Method = "POST",
                Query = new Dictionary<string, string> { { "q", "value" } },
                StatusCode = 200,
                EnableContentNegotiation = true,
                Headers = new Dictionary<string, string> { { "X-Test", "123" } },
                Cookies = new List<RpcHttpCookie>
            {
                new RpcHttpCookie("session", "abc123", new CookieOptions { Path = "/", HttpOnly = true })
            },
                Body = new RpcHttpBody("sample body")
            };
        }

        [Benchmark(Baseline = true)]
        public object Original()
        {
            return OriginalConvert(_input);
        }

        [Benchmark]
        public object Optimized()
        {
            return OptimizedConvert(_input);
        }

        public static object OriginalConvert(RpcHttp inputMessage)
        {
            if (inputMessage == null)
            {
                return null;
            }

            dynamic expando = new ExpandoObject();
            expando.method = inputMessage.Method;
            expando.query = inputMessage.Query as IDictionary<string, string>;
            expando.statusCode = inputMessage.StatusCode;
            expando.headers = inputMessage.Headers.ToDictionary(p => p.Key, p => (object)p.Value);
            expando.enableContentNegotiation = inputMessage.EnableContentNegotiation;

            expando.cookies = new List<Tuple<string, string, CookieOptions>>();
            foreach (RpcHttpCookie cookie in inputMessage.Cookies)
            {
                expando.cookies.Add(RpcHttpCookieConverter(cookie));
            }

            if (inputMessage.Body != null)
            {
                expando.body = inputMessage.Body.ToObject();
            }

            return expando;
        }

        public static object OptimizedConvert(RpcHttp inputMessage)
        {
            if (inputMessage == null)
            {
                return null;
            }

            var expando = new ExpandoObject();
            var dict = (IDictionary<string, object>)expando;

            dict["method"] = inputMessage.Method;

            if (inputMessage.Query is IDictionary<string, string> queryDict)
            {
                dict["query"] = queryDict;
            }

            dict["statusCode"] = inputMessage.StatusCode;

            if (inputMessage.Headers is { Count: > 0 })
            {
                var headers = new Dictionary<string, object>(inputMessage.Headers.Count);
                foreach (var kvp in inputMessage.Headers)
                {
                    headers[kvp.Key] = kvp.Value;
                }
                dict["headers"] = headers;
            }

            dict["enableContentNegotiation"] = inputMessage.EnableContentNegotiation;

            if (inputMessage.Cookies is { Count: > 0 } cookies)
            {
                var cookieList = new List<Tuple<string, string, CookieOptions>>(cookies.Count);
                for (int i = 0; i < cookies.Count; i++)
                {
                    cookieList.Add(RpcHttpCookieConverter(cookies[i]));
                }
                dict["cookies"] = cookieList;
            }
            else
            {
                dict["cookies"] = Array.Empty<Tuple<string, string, CookieOptions>>();
            }

            if (inputMessage.Body is not null)
            {
                dict["body"] = inputMessage.Body.ToObject();
            }

            return expando;
        }

        // --- Supporting types ---
        public static Tuple<string, string, CookieOptions> RpcHttpCookieConverter(RpcHttpCookie cookie)
            => Tuple.Create(cookie.Name, cookie.Value, cookie.Options);

        public class RpcHttp
        {
            public string Method { get; set; }
            public object Query { get; set; }
            public int StatusCode { get; set; }
            public Dictionary<string, string> Headers { get; set; }
            public bool EnableContentNegotiation { get; set; }
            public List<RpcHttpCookie> Cookies { get; set; }
            public RpcHttpBody Body { get; set; }
        }

        public class RpcHttpCookie
        {
            public string Name { get; }
            public string Value { get; }
            public CookieOptions Options { get; }

            public RpcHttpCookie(string name, string value, CookieOptions options)
            {
                Name = name;
                Value = value;
                Options = options;
            }
        }

        public class RpcHttpBody
        {
            private readonly string _data;
            public RpcHttpBody(string data) => _data = data;
            public object ToObject() => _data;
        }

        public class CookieOptions
        {
            public string Path { get; set; }
            public bool HttpOnly { get; set; }
        }
    }

}
