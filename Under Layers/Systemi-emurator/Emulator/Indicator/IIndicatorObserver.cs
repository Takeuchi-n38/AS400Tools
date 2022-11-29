
namespace System.Emulater.Indicator
{

    public interface IIndicatorObserver
    {

        void IndicatorValueOnned(Indicator onnedIndicator);

        void IndicatorValueChanged(Indicator valueChangedIndicator);

    }
}