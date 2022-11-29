using Delta.Guis;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.AS400.Workstations
{
    public class SubFileControlDialogViewModel<SubFileRecordViewModel, SubFileRecordItem> : DialogViewModel, IIsVisible, IIsEnabled
        where SubFileRecordViewModel : ISubFileRecordViewModel<SubFileRecordViewModel, SubFileRecordItem>, new()
        where SubFileRecordItem : new()
    {
        bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set { SetProperty(ref isVisible, value); }
        }

        private bool _IsEnabled = false;
        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set { SetProperty(ref _IsEnabled, value); }
        }

        private ObservableCollection<SubFileRecordViewModel> _SubFileItemViewModels = new ObservableCollection<SubFileRecordViewModel>(
            Enumerable.Range(1, 17).Select(_ => { var item = new SubFileRecordViewModel(); item.SetNull(); return item; }));//常に画面と一致。入力値も反映。
        public ObservableCollection<SubFileRecordViewModel> SubFileItemViewModels
        {
            get { return _SubFileItemViewModels; }
            set { SetProperty(ref _SubFileItemViewModels, value); }
        }

        private List<SubFileRecordViewModel> _SubFileItems = new List<SubFileRecordViewModel>(
            Enumerable.Range(1, 17).Select(_ => { var item = new SubFileRecordViewModel(); item.SetNull(); return item; }));//READCされるまで画面からの入力値は反映されない

        public void SubFileDisplay()
        {
            //A  30                                  SFLDSP
            for (var viewRowNumber = 1; viewRowNumber <= 17; viewRowNumber++)
            {
                var subfileRecord = _SubFileItems[viewRowNumber - 1];
                var viewRow = SubFileItemViewModels[viewRowNumber - 1];
                if (subfileRecord.IsNull)
                {
                    viewRow.Clear();
                    viewRow.Hide();
                }
                else
                {
                    viewRow.Rrn = viewRowNumber;
                    viewRow.Write(subfileRecord.Read());
                    viewRow.Show();
                }
            }
        }

        public void SubFileDisplayControl()
        {
            ((IIsVisible)this).Show();//A  30                                  SFLDSPCTL
        }

        public void SubFileClear()
        {
            _SubFileItems.ToList().ForEach(vm => vm.SetNull());//A  31                                  SFLCLR
        }

        public void Write(SubFileRecordItem subFileRecordItem, int recordNumber)//subfilerecordの書き込み
        {
            _SubFileItems[recordNumber - 1].Rrn = recordNumber;
            _SubFileItems[recordNumber - 1].Write(subFileRecordItem);
        }

        public SubFileRecordViewModel Chain(int viewRowNumber)//subfilerecordのランダム読み込み
        {
            return SubFileItemViewModels[viewRowNumber - 1];
            //SubFileItemViewModel.Rrn = _SubFileItems[recordNumber - 1].Rrn;
            //SubFileItemViewModel.Write(_SubFileItems[recordNumber - 1].Read());
        }

        internal List<SubFileRecordViewModel> Readc()//変更のあったsubfilerecordの順次読み込み
        {
            var updateList = new List<SubFileRecordViewModel>();
            for (int i = 0; i < _SubFileItems.Count; i++)
            {
                var originalR = _SubFileItems[i].Read();
                var objectVM = _SubFileItemViewModels[i];

                if (!objectVM.IsModified(originalR)) continue;

                var r = objectVM.Read();
                var vm = new SubFileRecordViewModel();
                vm.Write(r);
                updateList.Add(vm);

                _SubFileItems[i].Write(r);
            }
            return updateList;
        }

        internal SubFileRecordViewModel SubFileItemViewModel { get; set; } = new SubFileRecordViewModel();

        private SubFileRecordViewModel _SelectedSubFileItemViewModel;
        public SubFileRecordViewModel SelectedSubFileItemViewModel
        {
            get { return _SelectedSubFileItemViewModel; }
            set { SetProperty(ref _SelectedSubFileItemViewModel, value); }
        }

        public int SubFileCursorRelativeRecordNumber => SelectedSubFileItemViewModel == null ? 1 : SelectedSubFileItemViewModel.Rrn;//A                                      SFLCSRRRN(&CSR)
    }

}
