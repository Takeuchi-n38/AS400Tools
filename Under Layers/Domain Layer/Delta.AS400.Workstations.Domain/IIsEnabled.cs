using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.AS400.Workstations
{
    public interface IIsEnabled
    {
        public bool IsEnabled { get; set; }

        public void Enable()
        {
            IsEnabled = true;
        }
        public void Disable()
        {
            IsEnabled = false;
        }
    }
}
