using Delta.AS400.Indicators;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace Delta.AS400.Workstations
{
    public class WorkStationCommand : BindableBase
    {
        readonly internal IndicatorDictionary indicatorsForCommandButtons;

        public DelegateCommand<object>[] EnableFunctionCommands { get; } = new DelegateCommand<object>[25];

        public WorkStationCommand(params int[] numbers)
        {
            indicatorsForCommandButtons = new IndicatorDictionary(numbers);

            foreach (var number in numbers)
            {
                EnableFunctionCommands[number] ??= createFuntionCommandBy(number.ToString());
            }

        }

        internal bool GetIn(int indicatorNumber) => indicatorsForCommandButtons.Get(indicatorNumber);
        internal void SetIn(int indicatorNumber, bool value) => indicatorsForCommandButtons.Set(indicatorNumber, value);

        private DelegateCommand<string> functionCommand;
        public DelegateCommand<string> FunctionCommand
        {
            get
            {
                return functionCommand ??= new DelegateCommand<string>(executeFunctionCommand);
            }
        }

        public void executeFunctionCommand(string indicatorNumber)
        {
            indicatorsForCommandButtons.SetByCommandNumber(int.Parse(indicatorNumber));
            ExfmtHandler.Invoke();
        }

        private DelegateCommand<object> createFuntionCommandBy(string indicatorNumber)
        {
            return new DelegateCommand<object>(
                    (isEnable) =>
                    {
                        if ((bool)isEnable) executeFunctionCommand(indicatorNumber);
                    });
        }

        public Action ExfmtHandler { private get; set; }
    }
}
