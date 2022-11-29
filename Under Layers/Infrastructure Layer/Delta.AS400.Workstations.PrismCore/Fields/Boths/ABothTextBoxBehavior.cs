using Delta.AS400.DataTypes.Characters;
using System.Windows.Input;

namespace Delta.AS400.Workstations.Fields.Boths
{
    public class ABothTextBoxBehavior : BothTextBoxBehavior
    {
        protected override void OnAttached()
        {

            //this.AssociatedObject.ContextMenu = null;
            InputMethod.SetIsInputMethodEnabled(this.AssociatedObject, false);
            InputMethod.SetPreferredImeConversionMode(this, ImeConversionModeValues.Alphanumeric);

            this.AssociatedObject.MaxLength = Length;

            this.AssociatedObject.CharacterCasing = System.Windows.Controls.CharacterCasing.Upper;

            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

        }

        protected override bool parsable(string text)
        {
            return CodePage290.Encodable(text);
        }
    }
}
