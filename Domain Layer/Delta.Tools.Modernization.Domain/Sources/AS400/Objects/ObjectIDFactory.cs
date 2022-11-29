using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.Tools.AS400.Libraries;
using Delta.Tools.AS400.Programs.CLs.Lines;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Objects
{
    public class ObjectIDFactory
    {
        public readonly LibraryFactory LibraryFactory;
        public ObjectIDFactory(LibraryFactory LibraryFactory)
        {
            this.LibraryFactory= LibraryFactory;
        }

        public ObjectID Create(Library library, string objectName)
        {
            var p = CreateLibraryObjectName(library, objectName);
            return OfWithOvrdbf(p);
        }

        static ObjectID OfWithOvrdbf(ObjectID objectID)
        {
            var instance = OvrdbfList.Instance.Find(objectID.Name);
            if (instance != null) return instance;
            return objectID;
        }

        public void Override(ObjectID FromFileObjectID, ObjectID ToFileObjectID)
        {
            OvrdbfList.Instance.Override(FromFileObjectID, ToFileObjectID);
        }

        public void DeleteAll()
        {
            OvrdbfList.Instance.DeleteAll();
        }

        public ObjectID CreateLibraryObjectName(Library defaultLibrary, string spelling)
        {

            var nn = LibraryObjectName(spelling);
            if (nn.libraryName == string.Empty) return defaultLibrary.ObjectIDOf(spelling);

            return LibraryFactory.Create(nn.libraryName).ObjectIDOf(nn.objectName);
        }

        static (string libraryName, string objectName) LibraryObjectName(string spelling)
        {
            if (!spelling.Contains("/")) return (string.Empty, spelling);
            var splits = spelling.Split('/');
            if (splits.Length == 2) return CreateLibraryObjectName2(spelling);
            if (splits.Length == 4) return CreateLibraryObjectName4(spelling);
            throw new ArgumentException();
        }

        static (string libraryName, string objectName) CreateLibraryObjectName2(string library_objectName)
        {
            var splits = library_objectName.Split('/');
            if (splits.Length != 2) throw new ArgumentException();
            var libraryName = splits[0];
            var newObjectName = splits[1];
            return (libraryName, newObjectName);
        }

        static (string libraryName, string objectName) CreateLibraryObjectName4(string splling)
        {
            var splits = splling.Split('/');
            if (splits.Length != 4) throw new ArgumentException();
            var libraryName = splits[2];
            var newObjectName = splits[1];
            return (libraryName, newObjectName);
        }
    }
}
