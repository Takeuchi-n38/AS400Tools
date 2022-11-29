using System;
using System.Reactive.Linq;
using Prism.Services.Dialogs;
using System.Linq;

namespace Delta.AS400.Workstations
{
    public interface IMvvmDialogAware : IDialogAware
    {
        string IDialogAware.Title => throw new NotImplementedException();
        void IDialogAware.OnDialogOpened(IDialogParameters parameters)
        {
            var keys = parameters.Keys;
            var parameterList = new object[keys.Count()];
            for (int i = 0; i < keys.Count(); i++)
            {
                parameterList[i] = (parameters.GetValue<object>($"p{i}"));
            }
            SetParameters(parameterList);
            Main();
        }
        bool IDialogAware.CanCloseDialog() { return true; }
        void IDialogAware.OnDialogClosed() { }
        IDialogResult DialogResult()
        {
            IDialogParameters parameters = new DialogParameters();
            object[] parm = GetParameters();
            for (int i = 0; i < parm.Length; i++)
            {
                parameters.Add($"r{i}", parm[i]);
            }
            return new DialogResult(ButtonResult.OK, parameters);
        }

        object[] GetParameters();
        void SetParameters(object[] parameters);
        void Main();
    }

}
