using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delta.AS400.Indicators
{
    public class IndicatorDictionary
    {

        Dictionary<int, bool> keyValues = new Dictionary<int, bool>();

        public IndicatorDictionary()
        {
            for (int number = 1; number <= 99; number++)
            {
                keyValues.Add(number, false);
            }
        }

        public IndicatorDictionary(params int[] numbers)
        {
            foreach (var number in numbers)
            {
                keyValues.Add(number, false);
            }
        }

        public static IndicatorDictionary CreateBy<CommandType>(CommandType commandType) where CommandType : struct
        {

            var type = typeof(CommandType);
            if (!type.IsEnum)
            {
                throw new ArgumentException();
            }

            return new IndicatorDictionary((int[])Enum.GetValues(type));
        }

        public bool Get(int number)
        {
            return keyValues[number];
        }
        public string GetStr(int number)
        {
            return Get(number) ? "1" : "0";
        }

        public void Set(int number, bool value)
        {
            keyValues[number] = value;
        }

        public void SetStr(int number, string value)
        {
            Set(number, value == "1");
        }
        public void SetOn(int number)
        {
            keyValues[number] = true;
        }

        public void SetOff(params int[] numbers)
        {
            foreach (var number in numbers)
            {
                Set(number, false);
            }
        }

        public void SetByCommandNumber(int commandNumber)
        {
            if (commandNumber == -1)
            {
                throw new ArgumentNullException();
            }

            if (commandNumber == 0)//Run
            {
                SetOffAll();
            }
            else
            {
                SetOnAndSetOffOthers(commandNumber);
            }
        }

        void SetOnAndSetOffOthers(int targetNumber)
        {
            foreach (var number in new List<int>(keyValues.Keys))
            {
                if (number == targetNumber)
                {
                    if (!Get(targetNumber))
                    {
                        SetOn(targetNumber);
                    }
                }
                else
                {
                    if (Get(number))
                    {
                        SetOff(number);
                    }
                }
            }
        }

        public void SetOffAll()
        {
            foreach (var number in new List<int>(keyValues.Keys))
            {
                if (Get(number))
                {
                    SetOff(number);
                }
            }

        }

        public bool EqualsOn(int number)
        {
            return Get(number);
        }

        public bool EqualsOff(int number)
        {
            return !EqualsOn(number);
        }
    }
}
