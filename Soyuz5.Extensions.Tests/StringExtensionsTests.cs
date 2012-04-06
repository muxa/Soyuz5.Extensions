using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using NUnit.Framework;

namespace Soyuz5.Extensions.Tests
{
    /// <summary>
    /// Test for StringExtensions
    /// </summary>
    [TestFixture]
    public class StringExtensionsTests
    {
        #region Limit
        [Test]
        public void Limit_null()
        {
            Assert.AreEqual(null, ((string)null).Limit(10));
        }

        [Test]
        public void Limit_within_limit()
        {
            Assert.AreEqual("abc", "abc".Limit(3));
        }

        [Test]
        public void Limit_no_max_length()
        {
            Assert.AreEqual("abc", "abc".Limit(0));
        }

        [Test]
        public void Limit_limit()
        {
            Assert.AreEqual("ab", "abc".Limit(2));
        }

        [Test]
        public void Limit_limit_abbreviate_3()
        {
            Assert.AreEqual("a..", "abcde".Limit(3, ".."));
        }

        [Test]
        public void Limit_limit_abbreviate_2()
        {
            Assert.AreEqual("..", "abcde".Limit(2, ".."));
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Limit_limit_abbreviate_too_long()
        {
            Assert.AreEqual("..", "abc".Limit(2, "..."));
        }

        #endregion

        #region TitleCaseToWords

        [Test]
        public void TitleCaseToWords()
        {
            Assert.AreEqual("", "".TitleCaseToWords());
            Assert.AreEqual("T", "T".TitleCaseToWords());
            Assert.AreEqual("Test", "Test".TitleCaseToWords());
            Assert.AreEqual("Test One", "TestOne".TitleCaseToWords());
            Assert.AreEqual("Test One Two", "TestOneTwo".TitleCaseToWords());
        }

        #endregion

        #region ReplaceTokens

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReplaceTokens_empty_null()
        {
            Assert.AreEqual("", "".ReplaceTokens(null));
        }

        [Test]
        public void ReplaceTokens_no_token_in_template()
        {
            //IDictionary<string, object> replacements = new Dictionary<string, object>();
            dynamic args = new ExpandoObject();
            args.FirstName = "Mikhail";
            Assert.AreEqual("", "".ReplaceTokens((IDictionary<string, object>) args));
        }

        [Test]
        public void ReplaceTokens_token_in_last_position()
        {
            //IDictionary<string, object> replacements = new Dictionary<string, object>();
            dynamic args = new ExpandoObject();
            args.FirstName = "Mikhail";
            Assert.AreEqual("Hi Mikhail", "Hi ##FirstName##".ReplaceTokens((IDictionary<string, object>)args));
        }

        [Test]
        public void ReplaceTokens_case_insensitive()
        {
            //IDictionary<string, object> replacements = new Dictionary<string, object>();
            dynamic args = new ExpandoObject();
            args.FirstName = "Mikhail";
            Assert.AreEqual("Hi Mikhail", "Hi ##FIRSTname##".ReplaceTokens((IDictionary<string, object>)args));
        }

        [Test]
        public void ReplaceTokens_multiple_tokens()
        {
            //IDictionary<string, object> replacements = new Dictionary<string, object>();
            dynamic args = new ExpandoObject();
            args.FirstName = "Mikhail";
            args.LastName = "Diatchenko";
            Assert.AreEqual("Hi Mikhail Diatchenko", "Hi ##FirstName## ##LastName##".ReplaceTokens((IDictionary<string, object>)args));
        }

        [Test]
        public void ReplaceTokens_missing_token()
        {
            //IDictionary<string, object> replacements = new Dictionary<string, object>();
            dynamic args = new ExpandoObject();
            args.Test = "Mikhail";
            Assert.AreEqual("Hi ##FirstName##", "Hi ##FirstName##".ReplaceTokens((IDictionary<string, object>)args));
        }

        [Test]
        public void ReplaceTokens_anonymous_type()
        {
            dynamic args = new { FirstName = "Mikhail" };
            Assert.AreEqual("Hi Mikhail", "Hi ##FirstName##".ReplaceTokens((IDictionary<string, object>)DynamicHelper.ToDictionary(args)));
        }

        [Test]
        public void ReplaceTokens_format_num()
        {
            dynamic args = new { Num = 1.1 };
            Assert.AreEqual("1.10", "##Num:N2##".ReplaceTokens((IDictionary<string, object>)DynamicHelper.ToDictionary(args)));
        }

        [Test]
        public void ReplaceTokens_format_date()
        {
            dynamic args = new { Date = new DateTime(2012, 02, 17) };
            Assert.AreEqual("17/02/2012", "##Date:d##".ReplaceTokens((IDictionary<string, object>)DynamicHelper.ToDictionary(args)));
        }

        [Test]
        public void ReplaceTokens_format_missing()
        {
            dynamic args = new { Date = new DateTime(2012, 02, 17) };
            Assert.AreEqual("##Date:##", "##Date:##".ReplaceTokens((IDictionary<string, object>)DynamicHelper.ToDictionary(args)));
        }

        [Test]
        public void ReplaceTokens_format_token_missing()
        {
            dynamic args = new { Date = new DateTime(2012, 02, 17) };
            Assert.AreEqual("##DateX:d##", "##DateX:d##".ReplaceTokens((IDictionary<string, object>)DynamicHelper.ToDictionary(args)));
        }

        #endregion

        #region WrapLines

        [Test]
        public void WrapLines_2()
        {
            string longLine = "asdbefghifklmnop";
            Assert.AreEqual(@"asdbefghif
klmnop", longLine.WrapLines(10));
        }

        [Test]
        public void WrapLines_3()
        {
            string longLine = "asdbefghifklmnop";
            Assert.AreEqual(@"asdbefg
hifklmn
op", longLine.WrapLines(7));
        }

        #endregion


        #region JoinReadable

        [Test]
        public void JoinReadable_null()
        {
            Assert.AreEqual(null, ((string[])null).JoinReadable(", "));
        }

        [Test]
        public void JoinReadable_empty()
        {
            Assert.AreEqual("", new string[] { }.JoinReadable(", "));
        }

        [Test]
        public void JoinReadable_1()
        {
            Assert.AreEqual("a", new string[] { "a" }.JoinReadable(", "));
        }

        [Test]
        public void JoinReadable_2()
        {
            Assert.AreEqual("a, b", new string[] { "a", "b" }.JoinReadable(", "));
        }

        [Test]
        public void JoinReadable_2_last_sep()
        {
            Assert.AreEqual("a and b", new string[] { "a", "b" }.JoinReadable(", ", lastItemSeparator:" and "));
        }

        [Test]
        public void JoinReadable_3()
        {
            Assert.AreEqual("a, b, c", new string[] { "a", "b", "c" }.JoinReadable(", "));
        }

        [Test]
        public void JoinReadable_3_last_sep()
        {
            Assert.AreEqual("a, b and c", new string[] { "a", "b", "c" }.JoinReadable(", ", lastItemSeparator: " and "));
        }

        [Test]
        public void JoinReadable_abbreviated()
        {
            Assert.AreEqual("a, b, c (3 more, 6 total)", new string[] { "a", "b", "c", "d", "e", "f" }.JoinReadable(", ", abbreviateAfter: 3, abbreviationFormat:" ({0} more, {1} total)"));
            Assert.AreEqual("a, b, c and 3 more", new string[] { "a", "b", "c", "d", "e", "f" }.JoinReadable(", ", abbreviateAfter: 3));
            Assert.AreEqual("a, b, c", new string[] { "a", "b", "c"}.JoinReadable(", ", abbreviateAfter: 3));
            Assert.AreEqual("a, b, c and 1 more", new string[] { "a", "b", "c", "d" }.JoinReadable(", ", abbreviateAfter: 3));
            Assert.AreEqual("a and 3 more", new string[] { "a", "b", "c", "d" }.JoinReadable(", ", abbreviateAfter: 1));
            Assert.AreEqual("a, b, c, d", new string[] { "a", "b", "c", "d" }.JoinReadable(", ", abbreviateAfter: 0));
        }

        [Test]
        public void JoinReadable_abbreviated_with_total()
        {
            Assert.AreEqual("a, b, c (7 more, 10 total)", new string[] { "a", "b", "c", "d", "e", "f" }.JoinReadable(", ", abbreviateAfter: 3, abbreviationFormat: " ({0} more, {1} total)", totalItems:10));
        }

        [Test]
        public void JoinReadable_abbreviated_generic()
        {
            List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>(6);
            list.Add(new KeyValuePair<int, string>(0, "a"));
            list.Add(new KeyValuePair<int, string>(1, "b"));
            list.Add(new KeyValuePair<int, string>(2, "c"));
            list.Add(new KeyValuePair<int, string>(3, "d"));
            list.Add(new KeyValuePair<int, string>(4, "e"));

            Assert.AreEqual("a, b, c (2 more, 5 total)", list.JoinReadable(i => i.Value, ", ", abbreviateAfter: 3, abbreviationFormat: " ({0} more, {1} total)"));
        }

        [Test]
        public void JoinReadable_abbreviated_generic_with_total()
        {
            List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>(6);
            list.Add(new KeyValuePair<int, string>(0, "a"));
            list.Add(new KeyValuePair<int, string>(1, "b"));
            list.Add(new KeyValuePair<int, string>(2, "c"));
            list.Add(new KeyValuePair<int, string>(3, "d"));
            list.Add(new KeyValuePair<int, string>(4, "e"));

            Assert.AreEqual("a, b, c (7 more, 10 total)", list.JoinReadable(i => i.Value, ", ", abbreviateAfter: 3, abbreviationFormat: " ({0} more, {1} total)", totalItems:10));
        }

        #endregion
    }
}