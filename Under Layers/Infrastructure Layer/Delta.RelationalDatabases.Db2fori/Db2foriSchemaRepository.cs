using Delta.RelationalDatabases.OleDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.RelationalDatabases.Db2fori
{
    public class Db2foriSchemaRepository : SchemaRepository
    {

        Db2foriSchemaRepository(IDatabaseOperatedBySQL aDatabase) :base(aDatabase)
        {
        }

        public static Db2foriSchemaRepository Of(IDatabaseOperatedBySQL aDatabase) => new Db2foriSchemaRepository(aDatabase);

    }
}
