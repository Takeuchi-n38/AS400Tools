using Delta.AS400.Objects;
using Delta.Tools.AS400.Libraries;
using Delta.Tools.AS400.Sources;
using System.Collections.Generic;

namespace Delta.Tools.AS400.Structures
{
    public class StructureBuilder
    {
        public StructureBuilder(LibraryFactory LibraryFactory, ISourceFactory aS400SourceFileReader, IStructureFactory aS400StructureFactory)
        {
            this.LibraryFactory = LibraryFactory;
            this.aS400SourceFileReader = aS400SourceFileReader;
            this.aS400StructureFactory = aS400StructureFactory;
        }
        readonly LibraryFactory LibraryFactory;
        readonly ISourceFactory aS400SourceFileReader;
        readonly IStructureFactory aS400StructureFactory;

        ObjectID ResolveObjectID(ObjectID objectID)
        {
            if (aS400SourceFileReader.SourceExists(objectID)) return objectID;

            var libraryList = LibraryFactory.Libraries;// .GetLibraries(objectID.Library.Name);

            foreach (var library in libraryList)
            {
                var curObjectID = objectID.CreateWithDiffLibrary(library);
                if (aS400SourceFileReader.SourceExists(curObjectID)) return curObjectID;
            }

            return null;

        }

        public IStructure Create(ObjectID objectID)
        {
            //IStructure structure;

            //if (StructureDictionary.TryGetValue(objectID, out structure)) return structure;

            var resolvedObjectID = ResolveObjectID(objectID);

            if (resolvedObjectID == null)
            {
                var notFoundSourceStructure = NotFoundSourceStructure.Of(objectID);
                //StructureDictionary.Add(objectID, notFoundSourceStructure);
                return notFoundSourceStructure;
            }

            //if (StructureDictionary.TryGetValue(resolvedObjectID, out structure)) return structure;

            var source = aS400SourceFileReader.Read(resolvedObjectID);

            var newStructure = aS400StructureFactory.Create(source);

            //StructureDictionary.Add(newStructure.ObjectID, newStructure);

            return newStructure;

        }

        //protected abstract IStructure Create(Source source);

        internal static Dictionary<ObjectID, IStructure> StructureDictionary = new Dictionary<ObjectID, IStructure>();


    }

}
