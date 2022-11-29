using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Reactive.Linq;
using Delta.AS400.Indicators;
using System.Linq;

namespace Delta.AS400.Workstations
{
    public class DialogViewModel : BindableBase
    {
        protected DialogViewModel()
        {
            AttentionCommand.Subscribe(ExecuteAttentionCommand);
            FunctionCommand.Subscribe(ExecuteFunctionCommand);
        }

        public virtual void SetParameters(object[] parameters)
        {

        }

        #region indicators
        protected IndicatorDictionary IndicatorsForCommandButtons { get; } = new IndicatorDictionary(Enumerable.Range(1, 24).ToArray());
        protected string In01 { get => IndicatorsForCommandButtons.GetStr(1); set => IndicatorsForCommandButtons.SetStr(1, value); }
        protected string In02 { get => IndicatorsForCommandButtons.GetStr(2); set => IndicatorsForCommandButtons.SetStr(2, value); }
        protected string In03 { get => IndicatorsForCommandButtons.GetStr(3); set => IndicatorsForCommandButtons.SetStr(3, value); }
        protected string In04 { get => IndicatorsForCommandButtons.GetStr(4); set => IndicatorsForCommandButtons.SetStr(4, value); }
        protected string In05 { get => IndicatorsForCommandButtons.GetStr(5); set => IndicatorsForCommandButtons.SetStr(5, value); }
        protected string In06 { get => IndicatorsForCommandButtons.GetStr(6); set => IndicatorsForCommandButtons.SetStr(6, value); }
        protected string In07 { get => IndicatorsForCommandButtons.GetStr(7); set => IndicatorsForCommandButtons.SetStr(7, value); }
        protected string In08 { get => IndicatorsForCommandButtons.GetStr(8); set => IndicatorsForCommandButtons.SetStr(8, value); }
        protected string In09 { get => IndicatorsForCommandButtons.GetStr(9); set => IndicatorsForCommandButtons.SetStr(9, value); }
        protected string In10 { get => IndicatorsForCommandButtons.GetStr(10); set => IndicatorsForCommandButtons.SetStr(10, value); }
        protected string In11 { get => IndicatorsForCommandButtons.GetStr(11); set => IndicatorsForCommandButtons.SetStr(11, value); }
        protected string In12 { get => IndicatorsForCommandButtons.GetStr(12); set => IndicatorsForCommandButtons.SetStr(12, value); }
        protected string In13 { get => IndicatorsForCommandButtons.GetStr(13); set => IndicatorsForCommandButtons.SetStr(13, value); }
        protected string In14 { get => IndicatorsForCommandButtons.GetStr(14); set => IndicatorsForCommandButtons.SetStr(14, value); }
        protected string In15 { get => IndicatorsForCommandButtons.GetStr(15); set => IndicatorsForCommandButtons.SetStr(15, value); }
        protected string In16 { get => IndicatorsForCommandButtons.GetStr(16); set => IndicatorsForCommandButtons.SetStr(16, value); }
        protected string In17 { get => IndicatorsForCommandButtons.GetStr(17); set => IndicatorsForCommandButtons.SetStr(17, value); }
        protected string In18 { get => IndicatorsForCommandButtons.GetStr(18); set => IndicatorsForCommandButtons.SetStr(18, value); }
        protected string In19 { get => IndicatorsForCommandButtons.GetStr(19); set => IndicatorsForCommandButtons.SetStr(19, value); }
        protected string In20 { get => IndicatorsForCommandButtons.GetStr(20); set => IndicatorsForCommandButtons.SetStr(20, value); }
        protected string In21 { get => IndicatorsForCommandButtons.GetStr(21); set => IndicatorsForCommandButtons.SetStr(21, value); }
        protected string In22 { get => IndicatorsForCommandButtons.GetStr(22); set => IndicatorsForCommandButtons.SetStr(22, value); }
        protected string In23 { get => IndicatorsForCommandButtons.GetStr(23); set => IndicatorsForCommandButtons.SetStr(23, value); }
        protected string In24 { get => IndicatorsForCommandButtons.GetStr(24); set => IndicatorsForCommandButtons.SetStr(24, value); }
        public ReactivePropertySlim<bool> ReactiveIn25 { get; } = new ReactivePropertySlim<bool>();
        protected string In25 { get => ReactiveIn25.Value ? "1" : "0"; set => ReactiveIn25.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn26 { get; } = new ReactivePropertySlim<bool>();
        protected string In26 { get => ReactiveIn26.Value ? "1" : "0"; set => ReactiveIn26.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn27 { get; } = new ReactivePropertySlim<bool>();
        protected string In27 { get => ReactiveIn27.Value ? "1" : "0"; set => ReactiveIn27.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn28 { get; } = new ReactivePropertySlim<bool>();
        protected string In28 { get => ReactiveIn28.Value ? "1" : "0"; set => ReactiveIn28.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn29 { get; } = new ReactivePropertySlim<bool>();
        protected string In29 { get => ReactiveIn29.Value ? "1" : "0"; set => ReactiveIn29.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn30 { get; } = new ReactivePropertySlim<bool>();
        protected string In30 { get => ReactiveIn30.Value ? "1" : "0"; set => ReactiveIn30.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn31 { get; } = new ReactivePropertySlim<bool>();
        protected string In31 { get => ReactiveIn31.Value ? "1" : "0"; set => ReactiveIn31.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn32 { get; } = new ReactivePropertySlim<bool>();
        protected string In32 { get => ReactiveIn32.Value ? "1" : "0"; set => ReactiveIn32.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn33 { get; } = new ReactivePropertySlim<bool>();
        protected string In33 { get => ReactiveIn33.Value ? "1" : "0"; set => ReactiveIn33.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn34 { get; } = new ReactivePropertySlim<bool>();
        protected string In34 { get => ReactiveIn34.Value ? "1" : "0"; set => ReactiveIn34.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn35 { get; } = new ReactivePropertySlim<bool>();
        protected string In35 { get => ReactiveIn35.Value ? "1" : "0"; set => ReactiveIn35.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn36 { get; } = new ReactivePropertySlim<bool>();
        protected string In36 { get => ReactiveIn36.Value ? "1" : "0"; set => ReactiveIn36.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn37 { get; } = new ReactivePropertySlim<bool>();
        protected string In37 { get => ReactiveIn37.Value ? "1" : "0"; set => ReactiveIn37.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn38 { get; } = new ReactivePropertySlim<bool>();
        protected string In38 { get => ReactiveIn38.Value ? "1" : "0"; set => ReactiveIn38.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn39 { get; } = new ReactivePropertySlim<bool>();
        protected string In39 { get => ReactiveIn39.Value ? "1" : "0"; set => ReactiveIn39.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn40 { get; } = new ReactivePropertySlim<bool>();
        protected string In40 { get => ReactiveIn40.Value ? "1" : "0"; set => ReactiveIn40.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn41 { get; } = new ReactivePropertySlim<bool>();
        protected string In41 { get => ReactiveIn41.Value ? "1" : "0"; set => ReactiveIn41.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn42 { get; } = new ReactivePropertySlim<bool>();
        protected string In42 { get => ReactiveIn42.Value ? "1" : "0"; set => ReactiveIn42.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn43 { get; } = new ReactivePropertySlim<bool>();
        protected string In43 { get => ReactiveIn43.Value ? "1" : "0"; set => ReactiveIn43.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn44 { get; } = new ReactivePropertySlim<bool>();
        protected string In44 { get => ReactiveIn44.Value ? "1" : "0"; set => ReactiveIn44.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn45 { get; } = new ReactivePropertySlim<bool>();
        protected string In45 { get => ReactiveIn45.Value ? "1" : "0"; set => ReactiveIn45.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn46 { get; } = new ReactivePropertySlim<bool>();
        protected string In46 { get => ReactiveIn46.Value ? "1" : "0"; set => ReactiveIn46.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn47 { get; } = new ReactivePropertySlim<bool>();
        protected string In47 { get => ReactiveIn47.Value ? "1" : "0"; set => ReactiveIn47.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn48 { get; } = new ReactivePropertySlim<bool>();
        protected string In48 { get => ReactiveIn48.Value ? "1" : "0"; set => ReactiveIn48.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn49 { get; } = new ReactivePropertySlim<bool>();
        protected string In49 { get => ReactiveIn49.Value ? "1" : "0"; set => ReactiveIn49.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn50 { get; } = new ReactivePropertySlim<bool>();
        protected string In50 { get => ReactiveIn50.Value ? "1" : "0"; set => ReactiveIn50.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn51 { get; } = new ReactivePropertySlim<bool>();
        protected string In51 { get => ReactiveIn51.Value ? "1" : "0"; set => ReactiveIn51.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn52 { get; } = new ReactivePropertySlim<bool>();
        protected string In52 { get => ReactiveIn52.Value ? "1" : "0"; set => ReactiveIn52.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn53 { get; } = new ReactivePropertySlim<bool>();
        protected string In53 { get => ReactiveIn53.Value ? "1" : "0"; set => ReactiveIn53.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn54 { get; } = new ReactivePropertySlim<bool>();
        protected string In54 { get => ReactiveIn54.Value ? "1" : "0"; set => ReactiveIn54.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn55 { get; } = new ReactivePropertySlim<bool>();
        protected string In55 { get => ReactiveIn55.Value ? "1" : "0"; set => ReactiveIn55.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn56 { get; } = new ReactivePropertySlim<bool>();
        protected string In56 { get => ReactiveIn56.Value ? "1" : "0"; set => ReactiveIn56.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn57 { get; } = new ReactivePropertySlim<bool>();
        protected string In57 { get => ReactiveIn57.Value ? "1" : "0"; set => ReactiveIn57.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn58 { get; } = new ReactivePropertySlim<bool>();
        protected string In58 { get => ReactiveIn58.Value ? "1" : "0"; set => ReactiveIn58.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn59 { get; } = new ReactivePropertySlim<bool>();
        protected string In59 { get => ReactiveIn59.Value ? "1" : "0"; set => ReactiveIn59.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn60 { get; } = new ReactivePropertySlim<bool>();
        protected string In60 { get => ReactiveIn60.Value ? "1" : "0"; set => ReactiveIn60.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn61 { get; } = new ReactivePropertySlim<bool>();
        protected string In61 { get => ReactiveIn61.Value ? "1" : "0"; set => ReactiveIn61.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn62 { get; } = new ReactivePropertySlim<bool>();
        protected string In62 { get => ReactiveIn62.Value ? "1" : "0"; set => ReactiveIn62.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn63 { get; } = new ReactivePropertySlim<bool>();
        protected string In63 { get => ReactiveIn63.Value ? "1" : "0"; set => ReactiveIn63.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn64 { get; } = new ReactivePropertySlim<bool>();
        protected string In64 { get => ReactiveIn64.Value ? "1" : "0"; set => ReactiveIn64.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn65 { get; } = new ReactivePropertySlim<bool>();
        protected string In65 { get => ReactiveIn65.Value ? "1" : "0"; set => ReactiveIn65.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn66 { get; } = new ReactivePropertySlim<bool>();
        protected string In66 { get => ReactiveIn66.Value ? "1" : "0"; set => ReactiveIn66.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn67 { get; } = new ReactivePropertySlim<bool>();
        protected string In67 { get => ReactiveIn67.Value ? "1" : "0"; set => ReactiveIn67.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn68 { get; } = new ReactivePropertySlim<bool>();
        protected string In68 { get => ReactiveIn68.Value ? "1" : "0"; set => ReactiveIn68.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn69 { get; } = new ReactivePropertySlim<bool>();
        protected string In69 { get => ReactiveIn69.Value ? "1" : "0"; set => ReactiveIn69.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn70 { get; } = new ReactivePropertySlim<bool>();
        protected string In70 { get => ReactiveIn70.Value ? "1" : "0"; set => ReactiveIn70.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn71 { get; } = new ReactivePropertySlim<bool>();
        protected string In71 { get => ReactiveIn71.Value ? "1" : "0"; set => ReactiveIn71.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn72 { get; } = new ReactivePropertySlim<bool>();
        protected string In72 { get => ReactiveIn72.Value ? "1" : "0"; set => ReactiveIn72.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn73 { get; } = new ReactivePropertySlim<bool>();
        protected string In73 { get => ReactiveIn73.Value ? "1" : "0"; set => ReactiveIn73.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn74 { get; } = new ReactivePropertySlim<bool>();
        protected string In74 { get => ReactiveIn74.Value ? "1" : "0"; set => ReactiveIn74.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn75 { get; } = new ReactivePropertySlim<bool>();
        protected string In75 { get => ReactiveIn75.Value ? "1" : "0"; set => ReactiveIn75.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn76 { get; } = new ReactivePropertySlim<bool>();
        protected string In76 { get => ReactiveIn76.Value ? "1" : "0"; set => ReactiveIn76.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn77 { get; } = new ReactivePropertySlim<bool>();
        protected string In77 { get => ReactiveIn77.Value ? "1" : "0"; set => ReactiveIn77.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn78 { get; } = new ReactivePropertySlim<bool>();
        protected string In78 { get => ReactiveIn78.Value ? "1" : "0"; set => ReactiveIn78.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn79 { get; } = new ReactivePropertySlim<bool>();
        protected string In79 { get => ReactiveIn79.Value ? "1" : "0"; set => ReactiveIn79.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn80 { get; } = new ReactivePropertySlim<bool>();
        protected string In80 { get => ReactiveIn80.Value ? "1" : "0"; set => ReactiveIn80.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn81 { get; } = new ReactivePropertySlim<bool>();
        protected string In81 { get => ReactiveIn81.Value ? "1" : "0"; set => ReactiveIn81.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn82 { get; } = new ReactivePropertySlim<bool>();
        protected string In82 { get => ReactiveIn82.Value ? "1" : "0"; set => ReactiveIn82.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn83 { get; } = new ReactivePropertySlim<bool>();
        protected string In83 { get => ReactiveIn83.Value ? "1" : "0"; set => ReactiveIn83.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn84 { get; } = new ReactivePropertySlim<bool>();
        protected string In84 { get => ReactiveIn84.Value ? "1" : "0"; set => ReactiveIn84.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn85 { get; } = new ReactivePropertySlim<bool>();
        protected string In85 { get => ReactiveIn85.Value ? "1" : "0"; set => ReactiveIn85.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn86 { get; } = new ReactivePropertySlim<bool>();
        protected string In86 { get => ReactiveIn86.Value ? "1" : "0"; set => ReactiveIn86.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn87 { get; } = new ReactivePropertySlim<bool>();
        protected string In87 { get => ReactiveIn87.Value ? "1" : "0"; set => ReactiveIn87.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn88 { get; } = new ReactivePropertySlim<bool>();
        protected string In88 { get => ReactiveIn88.Value ? "1" : "0"; set => ReactiveIn88.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn89 { get; } = new ReactivePropertySlim<bool>();
        protected string In89 { get => ReactiveIn89.Value ? "1" : "0"; set => ReactiveIn89.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn90 { get; } = new ReactivePropertySlim<bool>();
        protected string In90 { get => ReactiveIn90.Value ? "1" : "0"; set => ReactiveIn90.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn91 { get; } = new ReactivePropertySlim<bool>();
        protected string In91 { get => ReactiveIn91.Value ? "1" : "0"; set => ReactiveIn91.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn92 { get; } = new ReactivePropertySlim<bool>();
        protected string In92 { get => ReactiveIn92.Value ? "1" : "0"; set => ReactiveIn92.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn93 { get; } = new ReactivePropertySlim<bool>();
        protected string In93 { get => ReactiveIn93.Value ? "1" : "0"; set => ReactiveIn93.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn94 { get; } = new ReactivePropertySlim<bool>();
        protected string In94 { get => ReactiveIn94.Value ? "1" : "0"; set => ReactiveIn94.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn95 { get; } = new ReactivePropertySlim<bool>();
        protected string In95 { get => ReactiveIn95.Value ? "1" : "0"; set => ReactiveIn95.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn96 { get; } = new ReactivePropertySlim<bool>();
        protected string In96 { get => ReactiveIn96.Value ? "1" : "0"; set => ReactiveIn96.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn97 { get; } = new ReactivePropertySlim<bool>();
        protected string In97 { get => ReactiveIn97.Value ? "1" : "0"; set => ReactiveIn97.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn98 { get; } = new ReactivePropertySlim<bool>();
        protected string In98 { get => ReactiveIn98.Value ? "1" : "0"; set => ReactiveIn98.Value = value == "1"; }
        public ReactivePropertySlim<bool> ReactiveIn99 { get; } = new ReactivePropertySlim<bool>();
        protected string In99 { get => ReactiveIn99.Value ? "1" : "0"; set => ReactiveIn99.Value = value == "1"; }
        #endregion indicators

        public ReactiveCommand<string> AttentionCommand { get; } = new ReactiveCommand<string>();

        public void ExecuteAttentionCommand(string indicatorNumber)
        {
            IndicatorsForCommandButtons.SetByCommandNumber(int.Parse(indicatorNumber));
            AfterExfmt();
        }

        public ReactiveCommand<string> FunctionCommand { get; } = new ReactiveCommand<string>();

        protected void ExecuteFunctionCommand(string indicatorNumber)
        {
            IndicatorsForCommandButtons.SetByCommandNumber(int.Parse(indicatorNumber));
            ReadRecordFormat();
            AfterExfmt();
        }

        public virtual void Main()
        {
            Init();
            BeforeExfmt();
        }

        public virtual void Init()
        {

        }

        public virtual void BeforeExfmt()
        {

        }
        public virtual void AfterExfmt()
        {

        }

        protected virtual void ReadRecordFormat()
        {

        }

        public virtual object[] GetParameters()
        {
            return new object[0];
        }

        protected virtual void CloseDialog()
        {

        }


    }

}
