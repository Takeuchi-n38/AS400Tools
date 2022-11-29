using Delta.AS400.Misc;
using Delta.Tools.AS400.Generator;
using Prism.Mvvm;
using System.Windows;

namespace Delta.Tools.AS400.Modernizer.GuiApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StartUpWindowView : Window
    {
        public StartUpWindowView()
        {
            InitializeComponent();
        }
    }
    //            _IDialogService.ShowDialog(nameof(DmenmainView), null, null);// 97 " C CALL 'CMENMAIN'"

    public class StartUpWindowViewModel : BindableBase
    {
        static int[] numbers = new int[] { 3, 5, 7, 8, 13, 16, 24 };
        public WorkStationCommand WorkStationCommand { get; } = new WorkStationCommand(numbers);

        //IDialogService DialogService;

        public StartUpWindowViewModel()
        {
            //this.DialogService = dialogService;

            //GeneratorDelegatingService.Instance.Main();

        }



    }

}
/* DSP
DMENMENU
DMENURI
DMENSEI
DMENMON
DMENGEN
PQEA010D
PQEA020D
PQEA700D
PQEA720D
PQEA710D
PQEA060D
PQEA030D
PQEA070D
PQEB010D
PQEB030D
PQEA730D
PQEB040D
PQEB070D
PQEB060D
PQEA500D
PQEA550D
PQEA510D
PQEA520D
PQEA530D
PQEA540D
PQEA560D
PQEA570D

 */
