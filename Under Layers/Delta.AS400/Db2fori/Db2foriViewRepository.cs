using Delta.Utilities.RelationalDatabases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.AS400.Db2fori
{
    public class Db2foriViewRepository : IViewRepository
    {
        readonly ICanBeOperatedBySQL Database;
        ICanBeOperatedBySQL IViewRepository.Database => Database;

        public Db2foriViewRepository(ICanBeOperatedBySQL aDatabase)
        {
            Database = aDatabase;
        }

        void IViewRepository.CreateViewForDifferenceByExceptTable(string differenceViewName, string targetTableName, string target_joined_column_names, string exceptTableName, string except_joined_column_names)
        {
            Database.ExecuteNonQuery($"create or replace view {differenceViewName} as select {target_joined_column_names} from {targetTableName} except select {except_joined_column_names} from {exceptTableName}");
        }

    }
}
