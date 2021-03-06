﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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

        public static bool IsBase64(this string source)
        {
            if (string.IsNullOrEmpty(source) || source.Length % 4 != 0
               || source.Contains(" ") || source.Contains("\t") ||
               source.Contains("\r") || source.Contains("\n"))
                return false;

            try
            {
                Convert.FromBase64String(source);
                return true;
            }
            catch {}
            return false;
        }

        public static string Base64Encode(this string source)
        {
            try
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(source);
                return Convert.ToBase64String(plainTextBytes);
            } 
            catch
            {
                return source;
            }
        }
        public static string Base64Decode(this string source)
        {
            try
            {
                byte[] base64EncodedBytes = Convert.FromBase64String(source);
                return Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch
            {
                return source;
            }
        }

        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: return input;
                case "": return input;
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }
    }
}
