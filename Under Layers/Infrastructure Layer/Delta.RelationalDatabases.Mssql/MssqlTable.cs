using System.Text;

namespace Delta.RelationalDatabases.Mssql
{
    public class MssqlTable: Table
    {
        public MssqlTable(Schema aSchema, string aName, string aSummary) : base(aSchema, aName, aSummary)
        {

        }
        
 //List<string> uniques, 
        public string Ddl()
        {
            var create = new StringBuilder();

            //IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PERSON]') AND type in (N'U'))
            create.Append($"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{SchemaName}].[{Name}]') AND type in (N'U'))");
            create.Append($"CREATE TABLE [{SchemaName}].[{Name}](");

            if (!(Summary.Length == 0))
            {
                create.Append(" --");
                create.Append(Summary);
            }
            create.AppendLine();

            // When no key, add "id"
            //string primarykey;
            //if (keys.Count == 0 || !uniques.Contains(name.ToString()))
            //{
            //    create.Append("    " + "id serial NOT NULL" + ",\n");
            //    primarykey = "id";
            //}
            //else
            //{
            //    primarykey = string.Join(", ", keys.ToArray()).Replace("#", "i").Replace("\\", "o").Replace(".", "p");
            //}
            /*
[ID] [int] NOT NULL PRIMARY KEY,
[NAME] [nvarchar](50) NULL,
[GENDER] [int] NULL
 */
            // Add field to create
            foreach (var coulumn in Columns)
            {
                create.AppendLine("    " + ((MssqlColumn) coulumn).Ddl());
                //if (KeyNames.Contains(field.Name))
                //{
                //    create.Append("    " + FieldToString(field.name + "4s", string.Empty, CHARACTER_VARYING, length4s(field.length, field.type), 0) + "\n");
                //}
            }

            create.AppendLine($"    CONSTRAINT {Name}_pkey PRIMARY KEY ({string.Join(", ", KeyNames.ToArray())})");

            create.AppendLine(")");

            return create.ToString();
        }

    }

}
