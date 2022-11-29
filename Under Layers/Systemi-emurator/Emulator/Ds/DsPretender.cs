using System;

namespace Systemi_emurator.Emulator.Ds
{
    public class DsPretender
    {
        private readonly int lengthOfSharedString;
        public string value;

        protected DsPretender(int lengthOfSharedString)
        {
            this.lengthOfSharedString = lengthOfSharedString;
            value = new string(' ', this.lengthOfSharedString);
        }

        protected void AssertMaxLength(string value, int maxLength)
        {
            if (value.Length > maxLength)
                throw new ArgumentOutOfRangeException(value);
        }

        protected void AssertMaxLength(short value, int maxLength)
        {
            AssertMaxLength(value.ToString(), maxLength);
        }

        public void SetValue(string value)
        {
            AssertMaxLength(value, lengthOfSharedString);
            this.value = value;
        }

        public string GetValue()
        {
            return value.Substring(0, lengthOfSharedString);
        }

        protected void SetPartOfValue(string part, int start, int length)
        {
            AssertMaxLength(part, length);
            var formatter = "{0,-" + length + "}";
            value = value.Substring(0, start) + string.Format(formatter, part) + value.Substring(start + length);
        }

        public void SetPartOfValue(short part, int start, int length)
        {
            SetPartOfValue(part.ToString(), start, length);
        }

        public string GetPartOfValue(int start, int length)
        {
            return value.Substring(start, start + length);
        }

        public short GetPartOfValueInShort(int start, int length)
        {
            return short.Parse(GetPartOfValue(start, length));
        }
    }
}
