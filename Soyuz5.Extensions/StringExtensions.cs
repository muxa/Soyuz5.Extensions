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

        /// <summary>
        /// Joins list of strings in a ice readable form, abbrreviating if necessary, and optionally using "and" instead of a standard separator for the last item.
        /// </summary>
        /// <param name="items">Items to join</param>
        /// <param name="separator">Separator to use</param>
        /// <param name="abbreviateAfter">If > 0 will join this number of items using a separator and abbreviate remaining ones</param>
        /// <param name="lastItemSeparator">If not abbreviated this value will be join the last item. Default null</param>
        /// <param name="abbreviationFormat">Format to abbreviate items ({0} will be replaced with the number of items remaining, {1} with total items). Default " and {0} more"</param>
        /// <returns></returns>
        public static string JoinReadable(this string[] items, string separator, int abbreviateAfter = -1,
                                          string abbreviationFormat = " and {0} more", string lastItemSeparator = null)
        {
            if (items == null)
                return null;

            if (items.Length == 0)
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < items.Length; i++)
            {
                if (abbreviateAfter > 0 && i == abbreviateAfter)
                {
                    sb.AppendFormat(abbreviationFormat, items.Length - abbreviateAfter, items.Length);
                    break;
                }

                if (i > 0 && i == items.Length - 1 && lastItemSeparator != null)
                {
                    sb.Append(lastItemSeparator).Append(items[i]);
                    break;
                }

                if (i > 0)
                    sb.Append(separator);

                sb.Append(items[i]);
            }

            return sb.ToString();
        }
    }
}