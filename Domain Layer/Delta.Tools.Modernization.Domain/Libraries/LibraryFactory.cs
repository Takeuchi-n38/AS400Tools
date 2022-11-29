using Delta.AS400.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Libraries
{
    public abstract class LibraryFactory
    {

        public List<Library> Libraries;
        protected LibraryFactory(List<Library> Libraries)
        {
            this.Libraries = Libraries;
        }

        public abstract Library Create(string libraryName);

        public List<Library> GetLibraries(string libraryName)
        {
            var firstLibray = Create(libraryName);
            var libraries = new List<Library>() { firstLibray };
            libraries.AddRange(Libraries);
            //libraries.AddRange(Jobs.Job.Instance.LibraryList);

            //libraryLists.ToList().ForEach(libList => );
            return libraries;
        }
    }
}
