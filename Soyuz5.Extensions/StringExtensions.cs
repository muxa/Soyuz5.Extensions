using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Text
{
    public static class StringExtensions
    {
        /// <summary>
        /// Trims the string to not exceed max length
        /// </summary>
        /// <param name="s"></param>
        /// <param name="maxLength"></param>
        /// <param name="ellipsis">What to have at the end of limited string (e.g. "My long s..")</param>
        /// <returns></returns>
        public static string Limit(this string s, int maxLength, string ellipsis = null)
        {
            if (s == null) return s;

            if (maxLength <= 0 || s.Length <= maxLength) return s;

            if (!string.IsNullOrEmpty(ellipsis))
            {
                if (ellipsis.Length > maxLength)
                    throw new ArgumentOutOfRangeException(
                        "ellipsis", "Ellipsis can not be longer than max length");
                return s.Substring(0, maxLength - ellipsis.Length) + ellipsis;
            }

            return s.Substring(0, maxLength);
        }

        /// <summary>
        /// Splits string that's in title case into words. Useful for converting enum values to display names.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string TitleCaseToWords(this string s)
        {
            if (string.IsNullOrEmpty(s)) return s;

            StringBuilder sb = new StringBuilder();

            if (s.Length > 0)
            {
                sb.Append(s[0]);

                for (int i = 1; i < s.Length; i++)
                {
                    if (char.ToUpper(s[i]) == s[i])
                    {
                        sb.Append(' ');
                    }
                    sb.Append(s[i]);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Replaces tokens in the form of ##TokenName## (and with formatting ##TokenName:f##)
        /// </summary>
        /// <param name="template">Tokens should be embedded as ##TokenName##. Formatting is supported via ##TokenName:f## where f is standard format value (e.g. d for dates)</param>
        /// <param name="replacements">Keys are token names, values are values. Supports <see cref="ExpandoObject"/> too. To pass a generic objects use <see cref="DynamicHelper.ToDictionary"/> </param>
        /// <returns></returns>
        public static string ReplaceTokens(this string template, IDictionary<string, object> replacements)
        {
            if (replacements == null)
            {
                throw new ArgumentNullException("replacements");
            }
            if (string.IsNullOrEmpty(template)) return template;

            // parse the message into an array of tokens
            Regex regex = new Regex("(##[^#]+##)");
            Regex formatRe = new Regex("##([^#:]+):([^#]+)##");
            string[] tokens = regex.Split(template);

            IDictionary<string, object> newRep = new Dictionary<string, object>(replacements.Count);
            foreach (KeyValuePair<string, object> keyValuePair in replacements)
            {
                newRep.Add("##" + keyValuePair.Key.ToUpper() + "##", keyValuePair.Value);
            }

            // the new message from the tokens
            var sb = new StringBuilder((int) ((double) template.Length*1.1));
            foreach (string token in tokens)
            {
                if (regex.IsMatch(token))
                {
                    if (newRep.ContainsKey(token.ToUpper()))
                    {
                        sb.Append(newRep[token.ToUpper()]);
                        continue;
                    }

                    Match match = formatRe.Match(token);
                    if (match.Success)
                    {
                        string t = "##" + match.Groups[1].Value.ToUpper() + "##";
                        if (newRep.ContainsKey(t))
                        {
                            sb.AppendFormat("{0:" + match.Groups[2].Value + "}", newRep[t]);
                            continue;
                        }
                    }
                }

                sb.Append(token);
            }

            return sb.ToString();
        }

        /*public static string SimpleTemplate(string template, Dictionary<string, string> replacements)
        {
            StringBuilder sb = new StringBuilder(template, (int)((double)template.Length * 1.1));

            // the new message from the tokens
            foreach (string token in tokens)
                sb.Append(replacements.ContainsKey(token) ? replacements[token] : token);

            return sb.ToString();
        }*/

        /// <summary>
        /// Wraps string to limit line width. Note: it ignores any exising line breaks.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="lineLength"></param>
        /// <returns></returns>
        public static string WrapLines(this string s, int lineLength)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            if (lineLength <= 0)
                return s;

            if (s.Length < lineLength)
                return s;

            StringBuilder sb = new StringBuilder();

            int start = 0;
            while (start + lineLength < s.Length)
            {
                sb.AppendLine(s.Substring(start, lineLength));

                start += lineLength;
            }

            sb.AppendLine(s.Substring(start));

            sb.Remove(sb.Length - 2, 2);

            return sb.ToString();
        }
    }
}