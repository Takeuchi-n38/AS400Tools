using Delta.AS400.Misc;
using Prism.Mvvm;
using System.Windows.Controls;

namespace Delta.Tools.AS400.Modernizer.Generators
{
    /// <summary>
    /// GeneratorMainView.xaml の相互作用ロジック
    /// </summary>
    public partial class GeneratorMainView : UserControl
    {
        public GeneratorMainView()
        {
            InitializeComponent();
        }
    }

    public class GeneratorMainViewModel : BindableBase
    {
        static int[] numbers = new int[] { 3, 5, 7, 8, 13, 16, 24 };
        public WorkStationCommand WorkStationCommand { get; } = new WorkStationCommand(numbers);

        //IDialogService DialogService;

        //public GeneratorMainViewModel(ModernizeService modernizer)
        //{
        //    //this.DialogService = dialogService;


        //}



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
