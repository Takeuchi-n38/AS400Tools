using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.CSharp.Statements.Items.Variables
{
    public class TypeOfVariable
    {
        public readonly string Id;
        public string Spelling => Id == "unknown" ? "string" : Id;
        public readonly int Length;
        public readonly int NumberOfDecimalPlaces;
        public readonly int ArraySize;
        public TypeOfVariable ArrayItemType=> new TypeOfVariable(Id.Replace("[]",string.Empty),Length,NumberOfDecimalPlaces,-1);
        public string InitialValueSpelling
        {
            get
            {
                if (Spelling == "string") return "string.Empty";

                if (IsNumeric) return "0";

                if (Spelling == "byte") return "0";

                if(Spelling == "DateTime") return "null";

                if(Spelling == "char[]") return "null";

                if(Spelling == "string[]") return "null";

                if(Spelling == "int[]") return "null";

                if(Spelling == "decimal[]") return "null";

                if (Spelling.StartsWith("(")) return "null";

                throw new NotImplementedException();
                //return string.Empty;
            }
        }

        protected TypeOfVariable(string aId, int aLength, int aNumberOfDecimalPlaces, int aArraySize)
        {
            this.Id = aId;
            this.Length = aLength;
            this.NumberOfDecimalPlaces= aNumberOfDecimalPlaces;
            this.ArraySize= aArraySize;
        }

        static TypeOfVariable OfNoArray(string Id, int length)
        {
            return new TypeOfVariable(Id, length,-1, -1);
        }
        static TypeOfVariable OfInteger(string Id, int length)
        {
            return new TypeOfVariable(Id, length, 0, -1);
        }
        public static TypeOfVariable OfDecimal(int length, int aNumberOfDecimalPlaces)
        {
            return new TypeOfVariable("decimal", length, aNumberOfDecimalPlaces, -1);
        }

        public static TypeOfVariable OfNoLength(string Id) { return OfNoArray(Id, -1); }

        public static TypeOfVariable OfArray(string Id, int length, int aNumberOfDecimalPlaces, int ArraySize)
        {
            return new TypeOfVariable($"{Id}[]", length, aNumberOfDecimalPlaces, ArraySize);
        }

        static TypeOfVariable OfArray(string Id, int length, int ArraySize)
        {
            return OfArray(Id, length, -1, ArraySize);
        }
        //static TypeOfVariable OfCharArray(int ArraySize)
        //{
        //    return OfArray("char", -1, ArraySize);
        //}
        public static TypeOfVariable OfByteArray(int ArraySize)
        {
            return OfArray("byte", -1, ArraySize);
        }

        public static TypeOfVariable OfString(int length) => new TypeOfVariable("string", length, -1, -1);
        public static TypeOfVariable OfString(string length)
        {
            int result = -1;
            if (int.TryParse(length, out result))
            {
                return OfString(result);
            }
            else
            {
                //Console.WriteLine("length is not int.");
                //return OfString(1);
                throw new NotImplementedException();
            }
        }

        public bool IsChar => Id == "char";

        public bool IsString => Id == "string";

        public bool IsByte => Id == "byte";

        public static TypeOfVariable OfShort(int length) => OfInteger("short", length);

        public static TypeOfVariable OfShort(string length)
        {
            int result = -1;
            if (int.TryParse(length, out result))
            {
                return OfShort(result);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        public bool IsShort => Id == "short";

        public static TypeOfVariable OfInteger(int length)
        {
            if (length > 9) return OfLong(length);
            if (length > 4) return OfInt(length);
            return OfShort(length);
        }

        public static TypeOfVariable OfInt(int length) => OfInteger("int", length);
        public static TypeOfVariable OfInt() => OfInt(9);

        public static TypeOfVariable OfInt(string length)
        {
            int result = -1;
            if (int.TryParse(length, out result))
            {
                return OfInt(result);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        public bool IsInt => Id == "int";

        public static TypeOfVariable OfLong(int length) => OfInteger("long", length);

        public static TypeOfVariable OfLong(string length)
        {
            int result = -1;
            if (int.TryParse(length, out result))
            {
                return OfLong(result);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        public bool IsLong => Id == "long";

        public bool IsInteger => IsShort || IsInt || IsLong;

        //public static TypeOfVariable OfDecimal(int length,int aNumberOfDecimalPlaces) => OfDecimal("decimal", length, aNumberOfDecimalPlaces);

        public static TypeOfVariable OfDecimal(string length, int aNumberOfDecimalPlaces)
        {
            int result = -1;
            if (int.TryParse(length, out result))
            {
                return OfDecimal(result, aNumberOfDecimalPlaces);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public bool IsDecimal => Id == "decimal";

        public bool IsNumeric => IsInteger || IsDecimal;

        public static TypeOfVariable OfByte(string length)
        {
            int result = -1;
            if (int.TryParse(length, out result))
            {
                return OfNoArray("byte", result);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public static TypeOfVariable OfDateTime(string length)
        {
            int result = -1;
            if (int.TryParse(length, out result))
            {
                return OfNoArray("DateTime", result);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        public bool IsDateTime => Id == "DateTime";

        public static TypeOfVariable OfBool = OfNoLength("bool");

        public bool IsArray => Spelling.EndsWith("[]");

        public static TypeOfVariable OfUnknown = OfNoLength("unknown");
        public bool IsUnknown => Equals(OfUnknown);

        public TypeOfVariable Of(int length)
        {
            return OfNoArray(Id, length);
        }

        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            //if (Id == "unknown") return false;

            if (Id != ((TypeOfVariable)obj).Id) return false;

            if (Spelling != ((TypeOfVariable)obj).Spelling) return false;

            if (Length != ((TypeOfVariable)obj).Length) return false;

            if (NumberOfDecimalPlaces != ((TypeOfVariable)obj).NumberOfDecimalPlaces) return false;

            return true;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            //throw new NotImplementedException();
            return base.GetHashCode();
        }

    }
}
