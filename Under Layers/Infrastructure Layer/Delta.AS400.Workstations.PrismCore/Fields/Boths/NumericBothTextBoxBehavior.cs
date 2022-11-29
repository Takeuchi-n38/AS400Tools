using Delta.AS400.Workstations.Fields.Edts;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Delta.AS400.Workstations.Fields.Boths
{
    public abstract class NumericBothTextBoxBehavior : BothTextBoxBehavior
    {
        private KeyBinding SpaceKeyBinding { get; set; }

        private KeyBinding ShiftSpaceKeyBinding { get; set; }

        protected override void OnAttached()
        {

            //this.AssociatedObject.ContextMenu = null;
            InputMethod.SetIsInputMethodEnabled(this.AssociatedObject, false);
            InputMethod.SetPreferredImeConversionMode(this, ImeConversionModeValues.Alphanumeric);

            this.AssociatedObject.MaxLength=maxLength;

            this.SpaceKeyBinding = new KeyBinding(ApplicationCommands.NotACommand, Key.Space, ModifierKeys.None);
            this.ShiftSpaceKeyBinding = new KeyBinding(ApplicationCommands.NotACommand, Key.Space, ModifierKeys.Shift);

            AssociatedObject.InputBindings.Add(SpaceKeyBinding);
            AssociatedObject.InputBindings.Add(ShiftSpaceKeyBinding);

            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.InputBindings.Remove(SpaceKeyBinding);
            AssociatedObject.InputBindings.Remove(ShiftSpaceKeyBinding);

            this.SpaceKeyBinding = null;
            this.ShiftSpaceKeyBinding = null;

        }

        protected override bool parsable(string text)
        {
            return string.IsNullOrEmpty(edtcdeInstance.ValidateNotifyError(text));
        }

        public int DecimalPositions
        {
            get { return (int)this.GetValue(DecimalPositionsProperty); }
            set { this.SetValue(DecimalPositionsProperty, value); }
        }
        public static readonly DependencyProperty DecimalPositionsProperty = DependencyProperty.Register(
          "DecimalPositions", typeof(int), typeof(NumericBothTextBoxBehavior), new PropertyMetadata(0));

        public string Edtcde
        {
            get { return (string)this.GetValue(EdtcdeProperty); }
            set { this.SetValue(EdtcdeProperty, value); }
        }
        public static readonly DependencyProperty EdtcdeProperty = DependencyProperty.Register(
          "Edtcde", typeof(string), typeof(NumericBothTextBoxBehavior), new PropertyMetadata(""));

        private bool existThousandSeparator =>Edtcde.Equals("K");

        protected bool isInteger => DecimalPositions == 0;

        protected bool isDecimal => DecimalPositions > 0;

        protected abstract bool signed { get;}

        int maxLength => Length + (isDecimal ? 1:0) + thousandSeparatorCount;
        int intLength => Length - DecimalPositions;
        int thousandSeparatorCount => existThousandSeparator? ((intLength-1) / 3): 0;
        //protected abstract string EntryKeyboardShifts { get; }
        //{
        //    get { return (string)this.GetValue(EntryKeyboardShiftsProperty); }
        //    set { this.SetValue(EntryKeyboardShiftsProperty, value); }
        //}
        //public static readonly DependencyProperty EntryKeyboardShiftsProperty = DependencyProperty.Register(
        //  "EntryKeyboardShifts", typeof(string), typeof(BInputTextBoxBehavior), new PropertyMetadata(""));

        Edtcde _edtcdeInstance;
        Edtcde edtcdeInstance
        {
            get
            {
                return _edtcdeInstance ??= Edts.Edtcde.Of(Length, DecimalPositions,signed, Edtcde);
            }
        }
    }
}
