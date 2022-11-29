using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Guis
{
    public interface IIsVisible
    {
        public bool IsVisible { get; set; }

        public void Show()
        {
            IsVisible = true;
        }

        public void Hide()
        {
            IsVisible = false;
        }
    }
}
