using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using System;
using System.Windows.Controls;

namespace Delta.AS400.Workstations.Sndusrmsgs
{
    /// <summary>
    /// SndUsrInfMsgView.xaml の相互作用ロジック
    /// </summary>
    public partial class SndUsrInfMsgView : UserControl
    {
        public SndUsrInfMsgView()
        {
            InitializeComponent();
        }

        public static void ShowDialog(IDialogService _IDialogService, string msg)
        {
            var param = new DialogParameters();
            param.Add("msg", msg);

            _IDialogService.ShowDialog(nameof(SndUsrInfMsgView), param);

        }
    }

    public class SndUsrInfMsgViewModel : BindableBase, IDialogAware
    {
        /*
             SNDUSRMSG  MSG('Ｃ＆Ｓ　 月次処理が全て終了していません。バックア ップを中止します。') MSGTYPE(*INFO)
             SNDUSRMSG  MSG('Ｃ＆Ｓ　月次処理　Ｂ／Ｕ終了！')  MSGTYPE(*INFO)
             */

        public SndUsrInfMsgViewModel()
        {
            Msg = new ReactiveProperty<string>("");
            OkCommand = new ReactiveCommand();
            OkCommand.Subscribe(closeDialog);
        }

        public ReactiveCommand OkCommand { get; private set; }


        //public WorkStationCommand WorkStationCommand { get; } = new WorkStationCommand();

        //internal Action ExfmtHandler { set => WorkStationCommand.ExfmtHandler = value; }

        void IDialogAware.OnDialogOpened(IDialogParameters parameters)
        {
            Msg.Value = parameters.GetValue<string>("msg");
        }

        string IDialogAware.Title => "Ttl";

        bool IDialogAware.CanCloseDialog() { return true; }

        public event Action<IDialogResult> RequestClose;

        void closeDialog()
        {
            var result = new DialogResult(ButtonResult.OK);
            this.RequestClose?.Invoke(result);
        }

        void IDialogAware.OnDialogClosed()
        {

        }

        public ReactiveProperty<string> Msg { get; internal set; }
    }

}
