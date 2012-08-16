// based on: http://www.gutgames.com/post/Lorem-Ipsum-Generator-in-C.aspx

using System.Text;

namespace System
{
    public static class RandomExtensions
    {
        /// <summary>
        /// returns a random date/time for a specific date range.
        /// </summary>
        /// <param name="rnd"> </param>
        /// <param name="start">Start time</param>
        /// <param name="end">End time</param>
        /// <returns>A random date/time between the start and end times</returns>
        public static DateTime NextDate(this Random rnd, DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new ArgumentException("The start value must be earlier than the end value");
            }
            return start + new TimeSpan((long)(new TimeSpan(end.Ticks - start.Ticks).Ticks * rnd.NextDouble()));
        }

        /// <summary>
        /// Creates a Lorem Ipsum sentence.
        /// </summary>
        /// <param name="rnd"> </param>
        /// <param name="numberOfWords">Number of words for the sentence</param>
        /// <returns>A string containing Lorem Ipsum text</returns>
        public static string NextLoremIpsum(this Random rnd, int numberOfWords)
        {
            var builder = new StringBuilder();
            builder.Append(ToFirstCharacterUpperCase(Words[rnd.Next(Words.Length)]));
            for (int x = 1; x < numberOfWords; ++x)
            {
                builder.Append(" ").Append(Words[rnd.Next(Words.Length)]);
            }
            builder.Append(".");
            return builder.ToString();
        }

        /// <summary>
        /// Creates a Lorem Ipsum paragraph.
        /// </summary>
        /// <param name="rnd"> </param>
        /// <param name="numberOfParagraphs">Number of paragraphs</param>
        /// <param name="maxSentenceLength">Maximum sentence length</param>
        /// <param name="minSentenceLength">Minimum sentence length</param>
        /// <param name="numberOfSentences">Number of sentences per paragraph</param>
        /// <param name="beforeParagraph">Allows to pass custom paragraph opening. Parameter is the paragraph index. </param>
        /// <param name="afterParagraph">Allows to pass custom paragraph closing. Parameter is the paragraph index. </param>
        /// <returns>A string containing Lorem Ipsum text</returns>
        public static string NextLoremIpsum(this Random rnd, int numberOfParagraphs, int numberOfSentences, int minSentenceLength, int maxSentenceLength, Func<int, string> beforeParagraph = null, Func<int, string> afterParagraph = null)
        {
            var builder = new StringBuilder();
            /*
                        builder.Append("Lorem ipsum dolor sit amet. ");
            */
            for (int x = 0; x < numberOfParagraphs; ++x)
            {
                if (beforeParagraph != null)
                    builder.Append(beforeParagraph(x));
                for (int y = 0; y < numberOfSentences; ++y)
                {
                    builder.Append(rnd.NextLoremIpsum(rnd.Next(minSentenceLength, maxSentenceLength))).Append(" ");
                }
                if (afterParagraph != null)
                    builder.Append(afterParagraph(x));
                else
                    builder.AppendLine();
            }
            return builder.ToString();
        }

        /// <summary>
        /// Takes the first character of an input string and makes it uppercase
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>String with the first character capitalized</returns>
        private static string ToFirstCharacterUpperCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;
            for (int x = 0; x < input.Length; ++x)
            {
                if (input[x] != ' ' && input[x] != '\t')
                {
                    return char.ToUpper(input[x]) + input.Substring(1);
                }
            }
            return input;
        }

        private static readonly string[] Words = new string[] { "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod",
         "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua",
         "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita",
         "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet",
         "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod",
         "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua",
         "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita",
         "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet",
         "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod",
         "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua",
         "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita",
         "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "duis",
         "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse", "molestie",
         "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eros", "et",
         "accumsan", "et", "iusto", "odio", "dignissim", "qui", "blandit", "praesent", "luptatum", "zzril", "delenit",
         "augue", "duis", "dolore", "te", "feugait", "nulla", "facilisi", "lorem", "ipsum", "dolor", "sit", "amet",
         "consectetuer", "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet",
         "dolore", "magna", "aliquam", "erat", "volutpat", "ut", "wisi", "enim", "ad", "minim", "veniam", "quis",
         "nostrud", "exerci", "tation", "ullamcorper", "suscipit", "lobortis", "nisl", "ut", "aliquip", "ex", "ea",
         "commodo", "consequat", "duis", "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate",
         "velit", "esse", "molestie", "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at",
         "vero", "eros", "et", "accumsan", "et", "iusto", "odio", "dignissim", "qui", "blandit", "praesent", "luptatum",
         "zzril", "delenit", "augue", "duis", "dolore", "te", "feugait", "nulla", "facilisi", "nam", "liber", "tempor",
         "cum", "soluta", "nobis", "eleifend", "option", "congue", "nihil", "imperdiet", "doming", "id", "quod", "mazim",
         "placerat", "facer", "possim", "assum", "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing",
         "elit", "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam",
         "erat", "volutpat", "ut", "wisi", "enim", "ad", "minim", "veniam", "quis", "nostrud", "exerci", "tation",
         "ullamcorper", "suscipit", "lobortis", "nisl", "ut", "aliquip", "ex", "ea", "commodo", "consequat", "duis",
         "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse", "molestie",
         "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eos", "et", "accusam",
         "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea",
         "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit",
         "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut",
         "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et",
         "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no",
         "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit",
         "amet", "consetetur", "sadipscing", "elitr", "at", "accusam", "aliquyam", "diam", "diam", "dolore", "dolores",
         "duo", "eirmod", "eos", "erat", "et", "nonumy", "sed", "tempor", "et", "et", "invidunt", "justo", "labore",
         "stet", "clita", "ea", "et", "gubergren", "kasd", "magna", "no", "rebum", "sanctus", "sea", "sed", "takimata",
         "ut", "vero", "voluptua", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit",
         "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut",
         "labore", "et", "dolore", "magna", "aliquyam", "erat", "consetetur", "sadipscing", "elitr", "sed", "diam",
         "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed",
         "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea",
         "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum" };


    }
}