using System.Text;

namespace Delta.RelationalDatabases.Mssql
{
    public class MssqlColumnDataType: ColumnDataType
    {
        public override string DefaultValue => IsSerial?string.Empty : $"DEFAULT {(Name == CHARACTER_VARYING ? "''" : Name == BPCHAR ? "''" : "0")}";

        MssqlColumnDataType(string name, int length, int scale) : base(name, length, scale)
        {

        }
        MssqlColumnDataType(string name, int length) : base(name, length)
        {

        }
        MssqlColumnDataType(string name) : base(name)
        {

        }

        static readonly string SERIAL = "IDENTITY";
        public static MssqlColumnDataType Serial => new MssqlColumnDataType(SERIAL);
        bool IsSerial => Name.Equals(SERIAL);

        static readonly string BPCHAR = "NCHAR";
        public static MssqlColumnDataType Bpchar(int length) => new MssqlColumnDataType(BPCHAR, length);
        bool IsBpchar => Name.Equals(BPCHAR);

        static readonly string CHARACTER_VARYING = "NVARCHAR";
        public static MssqlColumnDataType CharacterVarying(int length)=> new MssqlColumnDataType(CHARACTER_VARYING,length);
        bool IsCharacterVarying => Name.Equals(CHARACTER_VARYING);

        static readonly string NUMERIC = "NUMERIC";
        public static MssqlColumnDataType Numeric(int length, int scale) => new MssqlColumnDataType(NUMERIC, length, scale);
        bool IsNumeric => Name.Equals(NUMERIC);
        public static MssqlColumnDataType OfNumeric(int length, int scale)
        {
            return (scale == 0) ? OfInteger(length) : Numeric(length, scale);
        }

        public static MssqlColumnDataType OfInteger(int length)
        {
            if (length > 9) return Bigint(length);
            if (length > 4) return Integer(length);
            return Smallint(length);
        }

        static readonly string BIGINT = "BIGINT";
        public static MssqlColumnDataType Bigint(int length) => new MssqlColumnDataType(BIGINT, length);
        static readonly string INTEGER = "INT";
        public static MssqlColumnDataType Integer(int length) => new MssqlColumnDataType(INTEGER,length);
        static readonly string SMALLINT = "SMALLINT";
        public static MssqlColumnDataType Smallint(int length) => new MssqlColumnDataType(SMALLINT, length);

        //private void SetType(string rpgDBtype, int length, int decimalPart)
        //{
        //    // Character: (A, J, O, "") and length > 0 and decimalPart == 0
        //    //if ((string.Compare(RPG_DB_TYPE_A, rpgDBtype, StringComparison.OrdinalIgnoreCase) == 0 ||
        //    //     string.Compare(RPG_DB_TYPE_J, rpgDBtype, StringComparison.OrdinalIgnoreCase) == 0 ||
        //    //     string.Compare(RPG_DB_TYPE_O, rpgDBtype, StringComparison.OrdinalIgnoreCase) == 0 ||
        //    //     string.Empty.Equals(rpgDBtype.Trim())) && length > 0 && decimalPart == 0)
        //    //{
        //    //    type = CHARACTER_VARYING;
        //    //    defaultValue = "''";
        //    //    return;
        //    //}

        //    // Number: (P, S, "") and length > 0 and decimalPart >= 0
        //    //if ((string.Compare(RPG_DB_TYPE_P, rpgDBtype, StringComparison.OrdinalIgnoreCase) == 0 ||
        //    //     string.Compare(RPG_DB_TYPE_S, rpgDBtype, StringComparison.OrdinalIgnoreCase) == 0 ||
        //    //     string.Empty.Equals(rpgDBtype.Trim())) && length > 0 && decimalPart >= 0)
        //    //{
        //    //    if (decimalPart > 0 || length > 18)
        //    //    {
        //    //        type = NUMERIC;
        //    //    }
        //    //    else
        //    //    { // decimalPart == 0
        //    //        if (length <= 4)
        //    //        {
        //    //            type = SMALLINT;
        //    //        }
        //    //        else if (length <= 9)
        //    //        {
        //    //            type = INTEGER;
        //    //        }
        //    //        else if (length <= 18)
        //    //        {
        //    //            type = BIGINT;
        //    //        }
        //    //    }

        //    //    defaultValue = "0";

        //    //    return;
        //    //}

        //    //// Number: (B)
        //    //if (string.Compare(RPG_DB_TYPE_B, rpgDBtype, StringComparison.OrdinalIgnoreCase) == 0)
        //    //{
        //    //    int intLength = (Math.Pow(2, length) - 1).ToString().Length;
        //    //    if (decimalPart > 0 || intLength > 18)
        //    //    {
        //    //        type = NUMERIC;
        //    //    }
        //    //    else
        //    //    { // decimalPart == 0
        //    //        if (intLength <= 4)
        //    //        {
        //    //            type = SMALLINT;
        //    //        }
        //    //        else if (intLength <= 9)
        //    //        {
        //    //            type = INTEGER;
        //    //        }
        //    //        else if (intLength <= 18)
        //    //        {
        //    //            type = BIGINT;
        //    //        }
        //    //    }

        //    //    defaultValue = "0";

        //    //    return;
        //    //}

        //}

        public MssqlColumnDataType Of4s => Bpchar(IsCharacterVarying || IsBpchar ? this.Length * 2 : this.Length);

        public string Ddl()
        {

            var script = new StringBuilder();
            if (IsSerial)
            {
                script.Append($"INT IDENTITY");
            }
            else
            if (IsCharacterVarying||IsBpchar)
            {
                script.Append($"{Name}({Length})");
            }
            else
            if (IsNumeric)
            {
                script.Append($"{Name}({Length}, {Scale})");
            }
            else
            {
                script.Append(Name);
            }
            script.Append(" NOT NULL ");
            script.Append(DefaultValue);
            return script.ToString();
        }

        // public int Id { get; set; }
        //id serial NOT NULL,// CONSTRAINT aicfl_pkey PRIMARY KEY (id)
    }
}
