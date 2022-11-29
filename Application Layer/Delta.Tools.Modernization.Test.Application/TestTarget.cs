using Delta.AS400.Objects;
using Delta.RelationalDatabases;
using System;

namespace Delta.Tools.Modernization.Test
{
    public class TestTarget
    {
        readonly ObjectID TargetObjectID;
        TestTarget(ObjectID aTargetObjectID)
        {
            TargetObjectID = aTargetObjectID;
        }

        public static TestTarget Of(ObjectID aTargetObjectID)
        {
            return new TestTarget(aTargetObjectID);
        }

        string TestTargetName => TargetObjectID.Name;
        string ClassPrefix() => $"{TargetObjectID.Library.Partition.Name}.{TargetObjectID.Library.Name}.{TargetObjectID.Name}s.{TargetObjectID.Name.ToPascalCase()}";

        public string SetupSchemaName(string caseName) => $"{TestTargetName}S{caseName}";
        public Schema SetupSchema(string caseName) => Schema.Of(SetupSchemaName(caseName));

        public string ActualSchemaName(string caseName) => $"{TestTargetName}A{caseName}";
        public Schema ActualSchema(string caseName) => Schema.Of(ActualSchemaName(caseName));
        public Schema ActualSchema() => Schema.Of(TargetObjectID.Library.Name);

        public string ExpectedSchemaName(string caseName) => $"{TestTargetName}E{caseName}";
        public Schema ExpectedSchema(string caseName) => Schema.Of(ExpectedSchemaName(caseName));

        public string ServiceName => $"{ClassPrefix()}Service";

        public string FormatterName => $"{ClassPrefix()}Formatter";

        public static View ExcludeViewOf(Schema expectedSchema, string targetTableName) => View.Of(expectedSchema, $"{targetTableName}EV");

        public static View ExcludeViewOf(Table expectedTable) => ExcludeViewOf(expectedTable.Schema, expectedTable.Name);
        public View ExcludeViewOf(string caseName, string tableName) => ExcludeViewOf(ExpectedSchema(caseName).CreateTableOf(tableName));

        public static View IncludeViewOf(Schema expectedSchema, string targetTableName) => View.Of(expectedSchema, $"{targetTableName}IV");
        public static View IncludeViewOf(Table expectedTable) => IncludeViewOf(expectedTable.Schema, expectedTable.Name);
        public View IncludeViewOf(string caseName, string tableName) => IncludeViewOf(ExpectedSchema(caseName).CreateTableOf(tableName));

        public string SetupSavfName => $"{TestTargetName}S";
        public string ActualSavfName => $"{TestTargetName}A";

        public string ExpectedSavfName => $"{TestTargetName}E";

    }
}
