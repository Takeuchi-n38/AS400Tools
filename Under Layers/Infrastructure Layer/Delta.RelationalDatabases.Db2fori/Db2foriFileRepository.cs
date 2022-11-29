using Delta.IBMPowerSystems;
using System;
using System.Collections.Generic;

namespace Delta.RelationalDatabases.Db2fori
{
    public abstract class Db2foriFileRepository : ObjectOfSchemaRepository
    {

        public string LibraryName { get;set;}

        public readonly string FileName;

        protected override Table TargetTable => Table.Of(Schema.Of(LibraryName), FileName);

        protected IIBMOperator IBMOperator => (IIBMOperator)Database;

        [Obsolete("廃止予定です。　Db2foriFileRepository(string aIP, string aLibraryName, string aFileName)を使ってください。")]

        protected Db2foriFileRepository(DatabaseOperatedBySQL aDatabase, string aLibraryName, string aFileName) : base(aDatabase)
        {
            LibraryName = aLibraryName;
            FileName = aFileName;
        }

        protected Db2foriFileRepository(string aIP, string aUserID, string aPassword, string aLibraryName, string aFileName) : base(DB2foriOperator.Of(aIP,aUserID,aPassword))
        {
            LibraryName = aLibraryName;
            FileName = aFileName;
        }

        protected Db2foriFileRepository(string aIP, string aLibraryName, string aFileName) : this(aIP, "QUSER", "QUSER", aLibraryName, aFileName)
        { 
        }

        //public static Db2foriFileRepository Of(ICanBeOperatedBySQL aDatabase,string aLibraryName, string aFileName)
        //{
        //    return new Db2foriFileRepository(aDatabase,aLibraryName,aFileName);
        //}


        public override void CreateViewForDifferenceByExceptTable(View differenceView, Table targetTable, string target_joined_column_names, Table exceptTable, string except_joined_column_names)
        {
            Database.ExecuteNonQuery($"create or replace view {differenceView.Name} as select {target_joined_column_names} from {targetTable.Name} except select {except_joined_column_names} from {exceptTable.Name}");
        }

        public byte[] DownloadData()
        {
            return IBMOperator.DownloadData(LibraryName,FileName);
        }

        public List<byte[]> DownloadData(int fileLength)
        {
            return IBMOperator.DownloadData(LibraryName, FileName, fileLength);
        }

        //public void QcmdexcCall(string targetLibrary, string targetName)
        //{
        //    IBMOperator.QcmdexcCall(cmd);
        //}
    }
}
