using Delta.AS400.DataTypes.Characters;
using Delta.AS400.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.Tools.Modernization.Test
{
    public abstract class EntityTestHelper
    {
        public virtual bool IsCreateTable => true;

        public virtual bool IsOutputHeader => true;
        public virtual IEnumerable<string> ColumnNames()
        {
            return new List<string>();
        }

        public readonly ObjectID ObjectID;
        protected EntityTestHelper(ObjectID aObjectID)
        {
            ObjectID = aObjectID;
        }

        public abstract IEnumerable<string> ToStringValues(byte[] CCSID930Bytes);

        public IEnumerable<string> ToStringValuesWithSingleQuote(string lineNumber, byte[] CCSID930Bytes)
        {
            return ToStringValuesWithQuote(lineNumber.ToString(), CCSID930Bytes, "'");
        }

        public IEnumerable<string> ToStringValuesWithDoubleQuote(string lineNumber, byte[] CCSID930Bytes)
        {
            return ToStringValuesWithQuote(lineNumber.ToString(), CCSID930Bytes, "\"");
        }

        public abstract IEnumerable<string> ToStringValuesWithQuote(string lineNumber, byte[] CCSID930Bytes, string quote);
        public string ToStringValueFromStringWithQuote(byte[] aCCSID930Bytes, int aStartPosition, int aEndPosition, string quote)
        {
            var stringValue = CodePage930.ToStringFrom(aCCSID930Bytes, aStartPosition, aEndPosition).TrimEnd();
            if (quote == "'")
            {
                stringValue = stringValue.Replace(quote, new string(quote[0], 2));
            }
            return $"{quote}{stringValue}{quote}";
        }

    }

    public class NonDDSPhycicalFileHelper : EntityTestHelper
    {
        public override bool IsCreateTable => false;

        public override bool IsOutputHeader => false;

        readonly int FileLength;
        NonDDSPhycicalFileHelper(int FileLength, ObjectID aObjectID) : base(aObjectID)
        {
            this.FileLength = FileLength;
        }

        public static NonDDSPhycicalFileHelper Of(int FileLength, ObjectID aObjectID) => new NonDDSPhycicalFileHelper(FileLength, aObjectID);

        public override IEnumerable<string> ToStringValues(byte[] CCSID930Bytes)
        {
            yield return CodePage930.ToStringFrom(CCSID930Bytes, 1, FileLength);
        }

        public override IEnumerable<string> ToStringValuesWithQuote(string lineNumber, byte[] CCSID930Bytes, string quote)
        {
            yield return $"{CodePage930.ToStringFrom(CCSID930Bytes, 1, FileLength).TrimEnd()}";
        }

    }

    public abstract class Db2foriEntityTestHelper : EntityTestHelper
    {
        public override bool IsCreateTable => false;
        public override bool IsOutputHeader => true;

        protected Db2foriEntityTestHelper(ObjectID aObjectID) : base(aObjectID)
        {
        }

    }

    public abstract class EFEntityTestHelper<TypeOfEFEntity> : EntityTestHelper
    {

        protected EFEntityTestHelper(ObjectID aObjectID) : base(aObjectID)
        {
        }

        public IEnumerable<string> CSVAllStrings(List<TypeOfEFEntity> entities)
        {
            var csvAll = new List<string>();
            csvAll.Add(ColumnNames().Aggregate((all, cur) => $"{all},{cur}"));
            entities.ForEach(entity => csvAll.Add(EntityToString(entity)));
            return csvAll;
        }

        public abstract IEnumerable<string> ToStringValuesWithQuote(TypeOfEFEntity entity, string quote);

        public IEnumerable<string> ToStringValuesWithDoubleQuote(TypeOfEFEntity entity)
        {
            return ToStringValuesWithQuote(entity, "\"");
        }

        public string EntityToString(TypeOfEFEntity entity)
        {
            return ToStringValuesWithDoubleQuote(entity).Aggregate((all, cur) => $"{all},{cur}");
        }
    }
}
