using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.AS400.Environments
{
    public class DataAreaSingleValues
    {
        //*LDA
        //The value of the local data area is being retrieved.
        public string Lda = string.Empty;

        //*GDA
        //The value of the group data area is being retrieved. This value is valid only if this job is a group job.
        public string Gda = string.Empty;

        //* PDA
        //The value of the program initialization parameter data area is being retrieved. This value is valid only if this is a prestart job.
        public string Pda = string.Empty;

    }
}
