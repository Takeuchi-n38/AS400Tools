using Delta.AS400.Libraries;
using Delta.Tools.AS400.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Jobs
{
    public class Job
    {
        public static Job Instance = new Job();

        public Library CurrentLibrary { get; set; }

        internal List<Library> LibraryList { get; set; } = new List<Library>();

        public void Login(IUser user)
        {
            ChangeLibrary(user.LibraryList, user.CurrentLibrary);
        }

        public void AddLibrary(Library library)
        {
            LibraryList.Add(library);
        }
        public void RemoveLibrary(Library library)
        {
            LibraryList.Remove(library);
        }

        public void ChangeLibrary(List<Library> libraryList, Library currentLibrary)
        {
            LibraryList.Clear();
            LibraryList.AddRange(libraryList);
            CurrentLibrary = currentLibrary;
        }
    }
}
