using System;
using System.Windows.Controls;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;

namespace Delta.AS400.Workstations.Sndusrmsgs
{ 
    /// <summary>
    /// SndUsrInqMsgView.xaml の相互作用ロジック
    /// </summary>
    public partial class SndUsrInqMsgView : UserControl
    {
        //public SndUsrInqMsgView()
        //{
        //    InitializeComponent();
        //}

        public static bool ShowDialog(IDialogService _IDialogService, string msg, string dft)
        {
            var param = new DialogParameters();
            param.Add("plist", SndUsrInqMsgPlist.Of(msg, dft));

            var result=false;
            _IDialogService.ShowDialog(nameof(SndUsrInqMsgView), param,
                x => {
                    result = x.Result.Equals(ButtonResult.Yes);
                });

            return result;

        }
    }

    public class SndUsrInqMsgViewModel : BindableBase, IDialogAware
    {
        /*
             SNDUSRMSG  MSG('Ｃ＆Ｓ　月次処理後Ｂ／Ｕ　 を開始します。Ｙ／Ｎ') VALUES('Y' 'N')  DFT('N') MSGRPY(&ANS)
             SNDUSRMSG  MSG('予備用Ｂ／Ｕ用のＬＴＯテープは入って いますか？Ｙ／Ｎ') VALUES('Y' 'N')  DFT('N') MSGRPY(&ANS)
             SNDUSRMSG  MSG(&MID *TCAT &TXT *TCAT  '確認せよ！(Y)') VALUES('Y')  DFT('Y') MSGRPY(&ANS)
             SNDUSRMSG  MSG(&TXT *TCAT 'ＯＫですか？  (Y/N)')  VALUES('Y' 'N') DFT('N') MSGRPY(&ANS)
             */

        public SndUsrInqMsgViewModel()
        {
            Msg = new ReactiveProperty<string>("");
            Dft = new ReactiveProperty<string>("");
            RunCommand = new ReactiveCommand();
            RunCommand.Subscribe(closeDialog);
        }
        public ReactiveCommand RunCommand { get; private set; }

        void IDialogAware.OnDialogOpened(IDialogParameters parameters)
        {
            var plist = parameters.GetValue<SndUsrInqMsgPlist>("plist");
            Msg.Value = plist.Msg;
            Dft.Value = plist.Dft;
        }

        string IDialogAware.Title => "Ttl";

        bool IDialogAware.CanCloseDialog() { return true; }

        public event Action<IDialogResult> RequestClose;

        void closeDialog()
        {
            var result = new DialogResult(Dft.Value.Equals("Y") ? ButtonResult.Yes : ButtonResult.No);
            this.RequestClose?.Invoke(result);
        }

        void IDialogAware.OnDialogClosed()
        {

        }

        public ReactiveProperty<string> Msg { get; internal set; }

        public ReactiveProperty<string> Dft { get; set; }

    }

}
