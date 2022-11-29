using System.Collections.Generic;
using System;

namespace Delta.RelationalDatabases
{
    public abstract class SchemaRepository
    {

        protected readonly IDatabaseOperatedBySQL Database;
        protected SchemaRepository(IDatabaseOperatedBySQL aDatabase)
        {
            Database = aDatabase;
        }

        //public static SchemaRepository Of(IDatabaseOperatedBySQL aDatabase)
        //{
        //    return new SchemaRepository(aDatabase);
        //}

        //protected void Create(Schema aSchema)
        //{
        //    Database.ExecuteNonQuery($"create schema {aSchema.Name}");
        //}

        //protected virtual void Drop(Schema aSchema)
        //{
        //    throw new NotImplementedException();
        //}

        //public int CountBy(string schema_name);

        //public bool IsExist(string schema_name);

        //  void add(Schema schema);
        //  
        //  void remove(Schema schema);
        //  
        //  boolean contains(Schema schema);

        //public void Recreate(string schema_name, ICollection<string> init_scripts);

        //public void SetDefaultSchema(string schema_name);
    }
}
