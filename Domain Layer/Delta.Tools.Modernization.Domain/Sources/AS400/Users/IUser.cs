using Delta.AS400.Libraries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Users
{
    public interface IUser
    {
        Library CurrentLibrary { get; }

        List<Library> LibraryList { get; }
    }
}
