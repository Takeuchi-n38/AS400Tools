using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Delta.AS400.Tests.DBAssertions
{
    public class DBAssertionTestCaseDifinition
    {
        public List<DBAssertionTestCaseSchema> schemas;
        public DBAssertionTestCase itsDcsTestCase;
        public DBAssertionTestCaseType definitionType;

        public DBAssertionTestCaseDifinition(DBAssertionTestCase aDcsTestCase, DBAssertionTestCaseType aDefinitionType)
        {
            itsDcsTestCase = aDcsTestCase;
            definitionType = aDefinitionType;
            schemas = CreateSchemas();
        }

        public List<DBAssertionTestCaseSchema> CreateSchemas()
        {
            var schemaMap = new Dictionary<string, DBAssertionTestCaseSchema>();
            var schemaPrefix = itsDcsTestCase.TestCaseId() + "_" + definitionType.ToString();
            var tableNames = LoadTableNames();
            foreach (var tableName in tableNames)
            {
                string tableFullName = tableName;
                //      if (!tableName.contains(".")) {
                //        tableFullName = SeatlibSchema.create().name + "." + tableName;//旧形式seatlib専用
                //      }
                var partsOfTableFullName = tableFullName.Split(".");
                var simpleTableName = partsOfTableFullName[1];
                var schemaName = DBAssertionTestCaseSchema.CreateSchemaName(schemaPrefix, tableFullName);
                if (!schemaMap.ContainsKey(schemaName))
                {
                    schemaMap.Add(schemaName, new DBAssertionTestCaseSchema(schemaName));
                }
                schemaMap[schemaName].AddTable(simpleTableName);
            }

            return new List<DBAssertionTestCaseSchema>(schemaMap.Values);
        }

        public string CsvFileContainedFolderPath()
        {
            return itsDcsTestCase.TestCaseFolderPath() + "\\" + definitionType.ToString();
        }

        public string TableOrderingFilePath()
        {
            return CsvFileContainedFolderPath() + "\\table-ordering.txt";
        }

        public string ColumnExcludingFilePath()
        {
            return itsDcsTestCase.TestFolderPath() + "\\column-excluding.txt";
        }

        List<string> LoadTableNames()
        {
            var path = TableOrderingFilePath();
            var tableFullNames = new List<string>();
            try
            {
                tableFullNames.AddRange(File.ReadAllLines(path).ToList());
            }
            catch (IOException e)
            {
                throw new FileNotFoundException("not found file:" + path, e);
            }
            return tableFullNames;
        }

        public string MdfFileContainedFolderPath()
        {
            return itsDcsTestCase.TestCaseFolderPath() + "\\" + definitionType.ToString();
        }
    }

}
