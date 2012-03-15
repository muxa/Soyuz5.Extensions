namespace System
{
    public static class ConversionExtensions
    {
        public static int? NullIfZero(this int value)
        {
            if (value == 0) return null;

            return value;
        } 
    }
}