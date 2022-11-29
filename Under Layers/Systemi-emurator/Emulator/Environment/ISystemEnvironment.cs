using System;

namespace Systemi_emurator.Emulator.Environment
{
    public interface ISystemEnvironment
    {
        public virtual DateTime Time()
        {
            return DateTime.Now;
        }

        public string TimeToHHmmssyyyyMMdd()
        {
            return Time().ToString("HHmmssyyyyMMdd");
        }

        public long TimeToHHmmssyyyyMMddLong()
        {
            return long.Parse(TimeToHHmmssyyyyMMdd());
        }

        public string TimeToyyyyMMddHHmmss()
        {
            return Time().ToString("yyyyMMddHHmmss");
        }

        public long TimeToyyyyMMddHHmmssLong()
        {
            return long.Parse(TimeToyyyyMMddHHmmss());
        }

        public string TimeToHHmmss()
        {
            return Time().ToString("HHmmss");
        }

        public string TimeToyyyyMMdd()
        {
            return Time().ToString("yyyyMMdd");
        }

        public int TimeToHHmmssLInt()
        {
            return int.Parse(TimeToHHmmss());
        }

        public int TimeToyyyyMMddInt()
        {
            return int.Parse(TimeToyyyyMMdd());
        }
    }
}

