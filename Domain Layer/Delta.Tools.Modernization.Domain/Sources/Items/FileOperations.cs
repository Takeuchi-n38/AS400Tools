using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.Sources.Items
{
    public enum FileOperations
    {
        UnKnown = 1,
        Insert = 2,
        Read = 4,
        Update = 8,
        Delete = 16,
        DeleteAll = 32,
        InsertAll = 64,
        ReferAsDefinition = 128,
        Create = 256,
        CreateAsView = 512,
        Drop = 1024,
        Vacuum = 2048,
        Interactive = 4096,
        Alias = 8192,
        UsedByAlias = 16384,
        CreateAsDefinition = 32768,
        CheckExist = 65536,
    }
}
