using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Utilities.RelationalDatabases.Postgres
{
    public class PostgresViewRepository: IViewRepository
    {
        readonly ICanBeOperatedBySQL Database;
        ICanBeOperatedBySQL IViewRepository.Database => Database;

        public PostgresViewRepository(ICanBeOperatedBySQL aDatabase)
        {
            Database = aDatabase;
        }

        //public new void CreateViewForDifferenceByExceptTable(string difference_view_name, string target_table_name, string except_table_name)
        //{
        //    CreateViewForDifferenceByExceptTable(difference_view_name, target_table_name, "*", except_table_name, "*");
        //}

        //public void CreateViewForDifferenceByExceptTable(string difference_view_name, Table targetTable, Table exceptTable)
        //{
        //    string target_table_name = targetTable.FullName();
        //    string target_joined_column_names =
        //        targetTable.columns.Count == 0 ? "*" : targetTable.JoinedColumnNames();
        //    string except_table_name = exceptTable.FullName();
        //    string except_joined_column_names =
        //        exceptTable.columns.Count == 0 ? "*" : exceptTable.JoinedColumnNames();

        //    ((IViewRepository)this).CreateViewForDifferenceByExceptTable(difference_view_name, target_table_name,
        //        target_joined_column_names, except_table_name, except_joined_column_names);
        //}

        //void IViewRepository.CreateViewForDifferenceByExceptTable(string differenceViewName, string targetTableName, string target_joined_column_names, string exceptTableName, string except_joined_column_names)
        //{
        //    Database.ExecuteNonQuery($"CREATE OR REPLACE VIEW {differenceViewName} WITH (security_barrier=false) AS SELECT {target_joined_column_names} FROM {targetTableName} EXCEPT ALL SELECT {except_joined_column_names} FROM {exceptTableName}");
        //}



        //public int count(string view_name)
        //{
        //    return GetCount($"SELECT count(*) as count FROM  {view_name}", "count");
        //}

        public int ReturnIntQuery(string returnIntQuery)
        {
            return GetCount(returnIntQuery, "sum_of_diff_count");
        }

        public int GetCount(string rawSqlString, string column_name)
        {
            var result = Database.RawSqlQuerySingleColumn<int>(rawSqlString).FirstOrDefault();
            return result;
        }

        //public void CreateByScript(string create_script, string default_schema)
        //{
        //    cmd.ExecuteSqlRaw($"SET search_path TO {default_schema};{create_script}");
        //}

        //public void ResetSearchPath()
        //{
        //    SetSearchPath("$user");
        //}

        //public void SetSearchPath(string default_schema)
        //{
        //    cmd.ExecuteSqlRaw($"SET search_path TO {default_schema}");
        //}
    }
}
