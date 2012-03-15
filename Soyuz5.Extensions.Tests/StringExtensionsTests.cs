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
    }
}