using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.TrimSystem.MstControls
{
    public interface IMstControlRepository 
    {
        IQueryable<MstControl> FindAll();

        public string GetProcessDate()
        {
            return FindAll().Where(item => item.ControlId == "AIC").Select(item => item.Value1).FirstOrDefault();
        }

    }
}
