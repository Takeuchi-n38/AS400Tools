using System;

namespace Delta.Utilities.RelationalDatabases
{
    public interface IViewRepository
    {
        ICanBeOperatedBySQL Database { get; }

        void CreateViewForDifferenceByExceptTable(string differenceViewName, string targetTableName, string exceptTableName)
        {
            CreateViewForDifferenceByExceptTable(differenceViewName, targetTableName, "*", exceptTableName, "*");
        }

        void CreateViewForDifferenceByExceptTable(string differenceViewName, string targetTableName, string target_joined_column_names, string exceptTableName, string except_joined_column_names)
        {
            throw new NotImplementedException();
        }

        //void CreateViewForDifferenceByExceptTable(string difference_view_name, string target_table_name, string except_table_name);

        //void CreateViewForDifferenceByExceptTable(string difference_view_name, Table targetTable, Table exceptTable);

        //void CreateView(string view_name, string view_body);

        //int ReturnIntQuery(string returnIntQuery);
    }
}
