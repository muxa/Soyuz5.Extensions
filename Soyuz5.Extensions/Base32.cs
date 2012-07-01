using System.Text;

namespace System
{
    /// <summary>
    /// Based on http://www.codeproject.com/Tips/76650/Base32-base32url-base64url-and-z-base-32-encoding
    /// </summary>
    public static class Base32
    {
        public const char StandardPaddingChar = '=';
        public const string Base32StandardAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        public const string ZBase32Alphabet = "ybndrfg8ejkmcpqxot1uwisza345h769";

        /// <summary>
        /// Encodes data using standard Base32 encoding (using RFC 4648 Base alphabet), optionaly padding the result.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="usePadding"> </param>
        /// <returns></returns>
        public static string ToBase32StringStandard(this byte[] data, bool usePadding = false)
        {
            return ToBase32String(data, Base32StandardAlphabet, usePadding, StandardPaddingChar);
        }

        public static string ToBase32String(this byte[] data, string alphabet, bool usePadding, char paddingChar)
        {
            if (alphabet.Length != 32)
            {
                throw new ArgumentException("Alphabet must be exactly 32 characters long for base 32 encoding.");
            }

            StringBuilder result = new StringBuilder(Math.Max((int)Math.Ceiling(data.Length * 8 / 5.0), 1));

            byte[] emptyBuff = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            byte[] buff = new byte[8];
            // take input five bytes at a time to chunk it up for encoding
            for (int i = 0; i < data.Length; i += 5)
            {
                int bytes = Math.Min(data.Length - i, 5);
                // parse five bytes at a time using an 8 byte ulong
                Array.Copy(emptyBuff, buff, emptyBuff.Length);
                Array.Copy(data, i, buff, buff.Length - (bytes + 1), bytes);
                Array.Reverse(buff);
                ulong val = BitConverter.ToUInt64(buff, 0);
                for (int bitOffset = ((bytes + 1) * 8) - 5; bitOffset > 3; bitOffset -= 5)
                {
                    result.Append(alphabet[(int)((val >> bitOffset) & 0x1f)]);
                }
            }
            if (usePadding)
            {
                result.Append(string.Empty.PadRight((result.Length % 8) == 0 ? 0 : (8 - (result.Length % 8)), paddingChar));
            }
            return result.ToString();
        }
    }
}