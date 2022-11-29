namespace Delta.RelationalDatabases.Mssql
{
    public class MssqlSchemaService

    {
        readonly DatabaseOperatedBySQL Database;
        MssqlSchemaService(DatabaseOperatedBySQL aDatabase)
        {
            Database = aDatabase;
        }

        public static MssqlSchemaService Of(DatabaseOperatedBySQL aDatabase)
        {
            return new MssqlSchemaService(aDatabase);
        }

        //public void Create(Schema aSchema, bool isRecreate)
        //{
        //    if(isRecreate)
        //    {
        //        ReCreate(aSchema);
        //    }
        //    else
        //    {
        //        CreateIfNotExist(aSchema);
        //    }
        //}

        public void ReCreate(Schema aSchema)
        {
            Drop(aSchema);
            Create(aSchema);
        }

        public void CreateIfNotExist(Schema aSchema)
        {
            //Drop(aSchema);
            if (IsExist(aSchema)) return;
            Create(aSchema);
        }

        void Drop(Schema aSchema)
        {
            Database.ExecuteNonQuery($"DROP SCHEMA {aSchema.Name} CASCADE");
        }

        void Create(Schema aSchema)
        {
            Database.ExecuteNonQuery($"CREATE SCHEMA {aSchema.Name}");
        }

        long CountBy(Schema aSchema)
        {
            return Database.ExecuteLong($"SELECT count(*) FROM information_schema.schemata WHERE LOWER(schema_name)= LOWER('{aSchema.Name}')");
        }

        bool IsExist(Schema aSchema)
        {
            return 0 < CountBy(aSchema);
        }

        //public void SetDefaultSchema(string schema_name)
        //{
        //    SetSearchPath(schema_name);
        //}

        //void SetSearchPath(Schema defaultSchema)
        //{
        //    SetSearchPath(defaultSchema.Name);
        //}
        //void SetSearchPath(string defaultSchemaName)
        //{
        //    Database.ExecuteNonQuery($"SET search_path TO {defaultSchemaName}");
        //}
        //void ResetSearchPath()
        //{
        //    SetSearchPath("\"$user\"");
        //}

        //void CreateByScript(string create_script, Schema defaultSchema)
        //{
        //    SetSearchPath(defaultSchema);
        //    Database.ExecuteNonQuery(create_script);
        //}

        //public void Save(Schema aSchema)
        //{
        //    Drop(aSchema);
        //    Create(aSchema);
        //}


    }
}
