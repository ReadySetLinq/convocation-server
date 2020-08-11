using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Linq;

namespace ConvocationServer.Extensions
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, bool ignoreCase)
        {
            if (ignoreCase)
            {
                try
                {
                    Assert.IsTrue(source.ToUpper().Contains(toCheck.ToUpper()));
                    return true;
                }
                catch { return false; }
            }
            else
                return source.Contains(toCheck);
        }

        public static string[] Wrap(this string source, int max)
        {
            var charCount = 0;
            var lines = source.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return lines.GroupBy(w => (charCount += (((charCount % max) + w.Length + 1 >= max)
                            ? max - (charCount % max) : 0) + w.Length + 1) / max)
                        .Select(g => string.Join(" ", g.ToArray()))
                        .ToArray();
        }

        public static string Truncate(this string source, int maxLength, string suffix = "...")
        {
            string str = source;
            if (maxLength > 0)
            {
                int length = maxLength - suffix.Length;
                if (length <= 0)
                {
                    return str;
                }
                if ((str != null) && (str.Length > maxLength))
                {
                    return (str.Substring(0, length).TrimEnd(new char[0]) + suffix);
                }
            }
            return str;
        }

        public static string ReplaceLastOccurrence(this string source, string Find, string Replace)
        {
            int Place = source.LastIndexOf(Find);
            string result = source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }

        public static string GetChunks(this string source)
        {
            string[] _chunks = source.Wrap(90);
            string str;
            if (_chunks.Length == 1)
            {
                str = String.Format("{0}", _chunks[0]);
            }
            else if (_chunks.Length == 2)
            {
                str = String.Format("{0}\n{1}", _chunks[0], _chunks[1]);
            }
            else
            {
                str = String.Format("{0}\n{1}\n{2}", _chunks[0], _chunks[1], Truncate(string.Join(" ", _chunks.Skip(2)), 90));
            }
            return str;
        }

        public static JObject ValidateJSON(this string s)
        {
            try
            {
                return JObject.Parse(s);
            }
            catch
            {
                return null;
            }
        }
    }
}
