using System.Collections.Generic;
using System.Linq;

namespace System.Emulater.Indicator
{
    public class ArrayIndicatorList
    {

        public List<ArrayIndicator> list = new List<ArrayIndicator>();

        public ArrayIndicator Add(int index)
        {
            ArrayIndicator instance = ArrayIndicator.CreateBy(index);
            list.Add(instance);
            return instance;
        }

        public bool GetEQ_ONby(int index)
        {
            return list.FirstOrDefault(current => current.index == index).EqualOn();
        }

        public ArrayIndicatorList SetIndicators(ArrayIndicatorList sourceIndicators)
        {
            list.ForEach(current => current.SetBy(sourceIndicators.GetEQ_ONby(current.index)));
            return this;
        }

    }
}