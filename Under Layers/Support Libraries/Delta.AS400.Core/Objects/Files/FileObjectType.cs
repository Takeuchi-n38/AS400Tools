using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.AS400.Objects.Files
{
    public class FileObjectType: ObjectType
    {
        public static FileObjectType Of = new FileObjectType();
        public static LFFileObjectType LFFileObjectType = LFFileObjectType.Of;

    }

}
