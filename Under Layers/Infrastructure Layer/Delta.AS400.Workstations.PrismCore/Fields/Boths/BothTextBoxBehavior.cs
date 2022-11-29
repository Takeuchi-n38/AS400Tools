using Microsoft.Xaml.Behaviors;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Delta.AS400.Workstations.Fields.Boths
{
    [TypeConstraint(typeof(TextBox))]
    public abstract class BothTextBoxBehavior : Behavior<TextBox>
    {

        private CommandBinding PasteCommandBinding { get; set; }

        protected override void OnAttached()
        {

            //this.AssociatedObject.ContextMenu = null;
            //InputMethod.SetIsInputMethodEnabled(this.AssociatedObject, false);
            //InputMethod.SetPreferredImeState(this, InputMethodState.On);
            //InputMethod.SetPreferredImeConversionMode(this, ImeConversionModeValues.Alphanumeric);

            this.AssociatedObject.PreviewTextInput += PreviewTextInput;

            this.PasteCommandBinding = new CommandBinding(ApplicationCommands.Paste, ExecutePaste);

            AssociatedObject.CommandBindings.Add(PasteCommandBinding);

            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.CommandBindings.Remove(PasteCommandBinding);

            this.PasteCommandBinding = null;

            this.AssociatedObject.PreviewTextInput -= PreviewTextInput;
        }

        private void ExecutePaste(object sender, ExecutedRoutedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;

            var after = previewText(textbox, Clipboard.GetText());

            if (parsable(after))
            {
                textbox.Paste();
            }
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            

            var after = previewText((TextBox)sender,e.Text);

            if (!parsable(after))
            {
                e.Handled = true;
            }
        }

        string previewText(TextBox textbox,string input)
        {

            var pos = textbox.SelectionStart;
            var len = textbox.SelectionLength;
            var before = textbox.Text;

            var after = before.Substring(0, pos) + input + before.Substring(pos + len);

            return after;
        }

        public int Length
        {
            get { return (int)this.GetValue(LengthProperty); }
            set { this.SetValue(LengthProperty, value); }
        }
        public static readonly DependencyProperty LengthProperty = DependencyProperty.Register(
          "Length", typeof(int), typeof(BothTextBoxBehavior), new PropertyMetadata(0));

        //convert: x => x.ToString("#,0.00"), 
        //        convertBack: x => toDecimal(x),
        //        ignoreValidationErrorValue: true)
        //SetValidateNotifyError

        protected abstract bool parsable(string text);


    }
}
