using Delta.RelationalDatabases;
using System.Collections.Generic;
using System.Linq;

namespace Delta.AS400.Tests.DBAssertions
{
    public class DBAssertionTestCaseTable : Table
    {

        public DBAssertionTestCaseTable(Schema aSchema, string aName) : base(aSchema, aName)
        {
            excludeFromExceptColumnNames = new List<string>();
        }


        public override Table CreateWithChangingSchema(Schema newSchema)
        {
            var rtn = new DBAssertionTestCaseTable(new DBAssertionTestCaseSchema(newSchema.Name), Name);
            Columns.ForEach(col => rtn.AddColumn(col.clone()));
            excludeFromExceptColumnNames.ForEach(colName => rtn.AddExceptColumn(colName));
            return rtn;
        }

        private List<string> excludeFromExceptColumnNames;
        public void AddExceptColumn(string columnName)
        {
            excludeFromExceptColumnNames.Add(columnName);
        }


        public override string JoinedColumnNames()
        {
            return string.Join(",", Columns.Where(c => !excludeFromExceptColumnNames.Contains(c.Name)).ToList().Select(c => c.Name.ToString()).ToArray());
        }
    }

}
