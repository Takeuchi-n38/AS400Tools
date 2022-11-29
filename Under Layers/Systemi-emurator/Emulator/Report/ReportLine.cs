using Domain.Type.Character;
using Domain.Type.Date;
using System.Collections.Generic;

namespace System.Emulater.Report
{
    public class ReportLine
    {
        public readonly ReportPage page;
        public readonly int number;
        public ReportLine(ReportPage page, int lineNumber)
        {
            this.page = page;
            this.number = lineNumber;
            this.items = new List<ReportItemInLine>();
        }

        public readonly List<ReportItemInLine> items;

        public void AddReportItem(ReportItemInLine item)
        {
            items.Add(item);
        }

        //*
        //* @deprecated change order of parameter

        //@Deprecated
        public void AddReportItem(int endPositionInLine, string value)
        {
            AddReportItem(value, endPositionInLine);
        }

        public void AddReportItem(string value, int endPositionInLine)
        {
            AddReportItem(new ReportItemInLine(value, endPositionInLine));
        }

        //*
        //* @deprecated change order of parameter

        //@Deprecated
        public void AddReportItem(int endPositionInLine, string value, int paddedFullLength)
        {
            AddReportItem(value, paddedFullLength, endPositionInLine);
        }

        public void AddReportItem(string value, int paddedFullLength, int endPositionInLine)
        {
            string padding = String.Empty;
            var legthOfValue = EbcdicCharacter.GetByteLength(value);
            if (legthOfValue < paddedFullLength)
            {
                padding = new string(' ', paddedFullLength - legthOfValue);
            }
            AddReportItem(value + padding, endPositionInLine);
        }

        public void AddReportItem(string yyyyMMdd, DateFormat dateFormat, int endPositionInLine)
        {
            AddReportItem(Date.Of(yyyyMMdd).ToStringInyyyyMMddWithSlush(), endPositionInLine);
        }

        public enum DateFormat
        {
            Y
        }
    }
}
