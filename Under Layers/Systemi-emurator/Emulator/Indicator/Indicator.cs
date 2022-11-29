using System.Collections.Generic;
using static System.Emulater.Indicator.IndicatorValue;

namespace System.Emulater.Indicator
{
    public abstract class Indicator
    {

        private readonly IndicatorType type;
        private EIndicatorValue value;

        private void SetValue(EIndicatorValue value)
        {
            this.value = value;
            NotifyObservers();
        }

        protected Indicator(IndicatorType type)
        {
            this.type = type;
            SetValue(EIndicatorValue.OFF);
        }

        private bool EqualsBy(EIndicatorValue value)
        {
            return this.value.Equals(value);
        }

        public bool EqualOn()
        {
            return EqualsBy(EIndicatorValue.ON);
        }

        public bool EqualOff()
        {
            return EqualsBy(EIndicatorValue.OFF);
        }

        public void SetOn()
        {
            SetValue(EIndicatorValue.ON);
        }

        public void SetOff()
        {
            SetValue(EIndicatorValue.OFF);
        }

        public void SetBy(bool isOn)
        {
            SetValue(IndicatorValue.CreateBy(isOn));
        }

        public void SetBy(Indicator sourceIndicator)
        {
            SetValue(sourceIndicator.value);
        }

        private List<IIndicatorObserver> observers = new List<IIndicatorObserver>();

        public Indicator AddObserver(IIndicatorObserver listener)
        {
            observers.Add(listener);
            return this;
        }

        private void NotifyObservers()
        {
            foreach (IIndicatorObserver observer in observers)
            {
                if (EqualOn())
                {
                    observer.IndicatorValueOnned(this);
                    observer.IndicatorValueChanged(this);
                }
            }
        }

    }
}