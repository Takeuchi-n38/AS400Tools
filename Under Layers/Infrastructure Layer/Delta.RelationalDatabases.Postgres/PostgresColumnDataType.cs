using System.Text;

namespace Delta.RelationalDatabases.Postgres
{
    public class PostgresColumnDataType: ColumnDataType
    {
        public override string DefaultValue => IsSerial?string.Empty : $"DEFAULT {(Name == CHARACTER_VARYING ? "''::character varying" : Name == BPCHAR ? "''::bpchar" : "0")}";

        PostgresColumnDataType(string name, int length, int scale) : base(name, length, scale)
        {

        }
        PostgresColumnDataType(string name, int length) : base(name, length)
        {

        }
        PostgresColumnDataType(string name) : base(name)
        {

        }

        static readonly string SERIAL = "serial";
        public static PostgresColumnDataType Serial => new PostgresColumnDataType(SERIAL);
        bool IsSerial => Name.Equals(SERIAL);

        static readonly string BPCHAR = "bpchar";
        public static PostgresColumnDataType Bpchar(int length) => new PostgresColumnDataType(BPCHAR, length);
        bool IsBpchar => Name.Equals(BPCHAR);

        static readonly string CHARACTER_VARYING = "character varying";
        public static PostgresColumnDataType CharacterVarying(int length)=> new PostgresColumnDataType(CHARACTER_VARYING,length);
        bool IsCharacterVarying => Name.Equals(CHARACTER_VARYING);

        static readonly string NUMERIC = "numeric";
        public static PostgresColumnDataType Numeric(int length, int scale) => new PostgresColumnDataType(NUMERIC, length, scale);
        bool IsNumeric => Name.Equals(NUMERIC);
        public static PostgresColumnDataType OfNumeric(int length, int scale)
        {
            return (scale == 0) ? OfInteger(length) : Numeric(length, scale);
        }

        public static PostgresColumnDataType OfInteger(int length)
        {
            if (length > 9) return Bigint(length);
            if (length > 4) return Integer(length);
            return Smallint(length);
        }

        static readonly string BIGINT = "bigint";
        public static PostgresColumnDataType Bigint(int length) => new PostgresColumnDataType(BIGINT, length);
        static readonly string INTEGER = "integer";
        public static PostgresColumnDataType Integer(int length) => new PostgresColumnDataType(INTEGER,length);
        static readonly string SMALLINT = "smallint";
        public static PostgresColumnDataType Smallint(int length) => new PostgresColumnDataType(SMALLINT, length);

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
        public PostgresColumnDataType Of4s => Bpchar(IsCharacterVarying ? this.Length * 2 : this.Length);

        public string Ddl()
        {
            var script = new StringBuilder();
            if (IsSerial)
            {
                script.Append($"{Name}");
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
