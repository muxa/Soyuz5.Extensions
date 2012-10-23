using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
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
        /// <param name="openingToken">String to mark the beginning of the token. Default "##".</param>
        /// <param name="closingToken">String to mark the ending of the token. Default "##".</param>
        /// <returns></returns>
        public static string ReplaceTokens(this string template, IDictionary<string, object> replacements, string openingToken = "##", string closingToken = "##")
        {
            if (replacements == null)
            {
                throw new ArgumentNullException("replacements");
            }
            if (string.IsNullOrEmpty(template)) return template;

            // parse the message into an array of tokens
            Regex regex = new Regex("(" + Regex.Escape(openingToken) + ".+?" + Regex.Escape(closingToken) + ")");
            Regex formatRe = new Regex(Regex.Escape(openingToken) + "(.+?):(.+?)" + Regex.Escape(closingToken));
            string[] tokens = regex.Split(template);

            IDictionary<string, object> newRep = new Dictionary<string, object>(replacements.Count);
            foreach (KeyValuePair<string, object> keyValuePair in replacements)
            {
                newRep.Add(openingToken + keyValuePair.Key.ToUpper() + closingToken, keyValuePair.Value);
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
                        string t = openingToken + match.Groups[1].Value.ToUpper() + closingToken;
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
        /// Joins list of strings in a nice readable form, abbrreviating if necessary, and optionally using "and" instead of a standard separator for the last item.
        /// </summary>
        /// <param name="items">Items to join</param>
        /// <param name="separator">Separator to use</param>
        /// <param name="abbreviateAfter">If > 0 will join this number of items using a separator and abbreviate remaining ones</param>
        /// <param name="lastItemSeparator">If not abbreviated this value will be join the last item. Default null</param>
        /// <param name="abbreviationFormat">Format to abbreviate items ({0} will be replaced with the number of items remaining, {1} with total items). Default " and {0} more"</param>
        /// <param name="totalItems">Heuristics so that the method does not have to enumerate though all items it order to abbreviate</param>
        /// <returns></returns>
        public static string JoinReadable(this IEnumerable<string> items, string separator, int abbreviateAfter = -1,
                                          string abbreviationFormat = " and {0} more", string lastItemSeparator = null, int totalItems = -1)
        {
            if (items == null)
                return null;

            IEnumerator<string> enumerator = items.GetEnumerator();

            if (!enumerator.MoveNext())
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            int itemsWritten = 0;
            do
            {
                string current = enumerator.Current;

                if (abbreviateAfter > 0 && itemsWritten == abbreviateAfter)
                {
                    int remainingCount = 1; // we already move to first of remaining items
                    if (totalItems < 0)
                    {
                        while (enumerator.MoveNext())
                        {
                            remainingCount++;
                        }
                    } 
                    else
                    {
                        // we have the totals, so we don't have to enumerate
                        remainingCount = totalItems - itemsWritten;
                    }
                    sb.AppendFormat(abbreviationFormat, remainingCount, itemsWritten + remainingCount);
                    break;
                }

                bool moreItems = enumerator.MoveNext();

                if (itemsWritten > 0 && lastItemSeparator != null && !moreItems)
                {
                    sb.Append(lastItemSeparator).Append(current);
                    break;
                }

                if (itemsWritten > 0)
                    sb.Append(separator);

                itemsWritten++;

                sb.Append(current);

                if (!moreItems)
                    break;
            } while (true);

            return sb.ToString();
        }

        /// <summary>
        /// Joins list of items in a nice readable form, abbrreviating if necessary, and optionally using "and" instead of a standard separator for the last item.
        /// </summary>
        /// <param name="items">Items to join</param>
        /// <param name="stringFunc"></param>
        /// <param name="separator">Separator to use</param>
        /// <param name="abbreviateAfter">If > 0 will join this number of items using a separator and abbreviate remaining ones</param>
        /// <param name="lastItemSeparator">If not abbreviated this value will be join the last item. Default null</param>
        /// <param name="abbreviationFormat">Format to abbreviate items ({0} will be replaced with the number of items remaining, {1} with total items). Default " and {0} more"</param>
        /// <param name="totalItems">Heuristics so that the method does not have to enumerate though all items it order to abbreviate</param>
        /// <returns></returns>
        public static string JoinReadable<T>(this IEnumerable<T> items, Func<T, string> stringFunc,  string separator, int abbreviateAfter = -1,
                                          string abbreviationFormat = " and {0} more", string lastItemSeparator = null, int totalItems = -1)
        {
            if (items == null)
                return null;

            IEnumerator<T> enumerator = items.GetEnumerator();

            if (!enumerator.MoveNext())
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            int itemsWritten = 0;
            do
            {
                T current = enumerator.Current;

                if (abbreviateAfter > 0 && itemsWritten == abbreviateAfter)
                {
                    int remainingCount = 1; // we already move to first of remaining items
                    if (totalItems < 0)
                    {
                        while (enumerator.MoveNext())
                        {
                            remainingCount++;
                        }
                    }
                    else
                    {
                        // we have the totals, so we don't have to enumerate
                        remainingCount = totalItems - itemsWritten;
                    }
                    sb.AppendFormat(abbreviationFormat, remainingCount, itemsWritten + remainingCount);
                    break;
                }

                bool moreItems = enumerator.MoveNext();

                if (itemsWritten > 0 && lastItemSeparator != null && !moreItems)
                {
                    sb.Append(lastItemSeparator).Append(stringFunc(current));
                    break;
                }

                if (itemsWritten > 0)
                    sb.Append(separator);

                itemsWritten++;

                sb.Append(stringFunc(current));

                if (!moreItems)
                    break;
            } while (true);

            return sb.ToString();
        }

        /// <summary>
        /// Generates next item in sequence using a text prefix and a numberical suffix. 
        /// E.g. prefix of "SOYUZ5", lenght of 5 and prefix length of 3 will generate "SOY001" on empty sequence, or if no items with prefix "SOY" exists..
        /// </summary>
        /// <param name="existingItems">Sequence of existing items. If null or empty will create first item.</param>
        /// <param name="prefixSeed">Prefix to use in the generate code. This can be an arbitrary "seed" which will be trimmed to appropriate width to be used in code.</param>
        /// <param name="length">Generated string length</param>
        /// <param name="prefixLength">Maximum length of the prefix part in generated string. If 0 will not use a prefix</param>
        /// <param name="ignoreCase">If true will ignore case when searching for existing code with specified prefix.</param>
        /// <returns></returns>
        public static string SequentialNext(this IEnumerable<string> existingItems, string prefixSeed, int length, int prefixLength, bool ignoreCase = true)
        {
            if (length < prefixLength)
                throw new ArgumentOutOfRangeException("prefixLength", "Prefix length must not be greater than length");

            if (prefixSeed == null)
                prefixSeed = string.Empty;

            // prepare prefix
            prefixSeed = Regex.Replace(prefixSeed, @"\W", "");

            string prefix = prefixSeed.Length > prefixLength
                              ? prefixSeed.Substring(0, prefixLength)
                              : prefixSeed;

            string[] strings = existingItems != null ? existingItems.ToArray() : new string[0];

            var tempResult = GetCode(strings, prefix, length, prefixLength, ignoreCase);
            // it may be that the lookup found existing item and the next item would add another digit to the number,
            // resulting in a shorter code part, which should also be checked for duplicates
            while (tempResult.Item1.Length < prefix.Length)
            {
                prefix = tempResult.Item1;
                tempResult = GetCode(strings, prefix, length, prefixLength, ignoreCase);
            }
            return tempResult.Item2;
        }

        private static Tuple<string, string> GetCode(string[] existingStrings, string prefix, int resultLength, int prefixLength, bool ignoreCase)
        {
            string matchPattern;
            if (prefix.Length == resultLength)
            {
                // search for entire code without number
                matchPattern = '^' + Regex.Escape(prefix) + '$';
            }
            else
            {
                matchPattern = '^' + Regex.Escape(prefix) + @"(\d+)$";
            }

            RegexOptions options = ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;

            var matchingCodes =
                existingStrings.Where(c => c.StartsWith(prefix) && Regex.IsMatch(c, matchPattern, options))
                    .ToArray();

            int nextCounter = 0;

            if (matchingCodes.Length > 0)
            {
                if (prefix.Length == resultLength)
                {
                    nextCounter = 1;
                }
                else
                {
                    nextCounter =
                        matchingCodes
                            .Select(c => int.Parse(Regex.Match(c, matchPattern, options).Groups[1].Value))
                            .Max() + 1;
                }
            }
            else if (prefix.Length < resultLength)
            {
                nextCounter = 1;
            }

            if (nextCounter == 0)
            {
                // result is the prefix on its own, since there's no number part
                return new Tuple<string, string>(prefix, prefix);
            }

            string numberPart = nextCounter.ToString(CultureInfo.InvariantCulture);

            // pad numbers with 0
            while (numberPart.Length + prefix.Length < resultLength)
            {
                numberPart = '0' + numberPart;
            }
            // shorten code part is too long (if number is too high)
            while (prefix.Length > 0 && numberPart.Length + prefix.Length > resultLength)
            {
                prefix = prefix.Remove(prefix.Length - 1, 1);
            }
            //return code + numberPart;
            return new Tuple<string, string>(prefix, prefix + numberPart);
        }
    }
}