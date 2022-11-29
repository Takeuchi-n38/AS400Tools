using Delta.AS400.DDSs;
using System;
using System.Collections.Generic;

namespace Delta.AS400.DDSs.Printers
{
    public class ReportLine : RecordFormatSpecification
    {
        public readonly ReportPage Page;
        public readonly int Number;
        public ReportLine(ReportPage page, int lineNumber, int capacityEachLine) : base(capacityEachLine)
        {
            Page = page;
            Number = lineNumber;
        }

        [Obsolete("AddReportItemOnLeftJustified->AddOnLeftJustified")]
        public void AddReportItemOnLeftJustified(byte[] aCCSID930Bytes, int startPositionInLine)
        {
            AddOnLeftJustified(aCCSID930Bytes, startPositionInLine);
        }
        [Obsolete("AddReportItemOnLeftJustified->AddOnLeftJustified")]

        public void AddReportItemOnLeftJustified(string utf16Strings, int startPositionInLine)
        {
            AddOnLeftJustified(utf16Strings, startPositionInLine);
        }

        //public void AddReportItem(byte[] aCCSID930Bytes, int endPositionInLine)
        //{
        //    Add(FieldSpecification.OfRightJustified(aCCSID930Bytes, endPositionInLine));
        //}
        //*
        //* @deprecated change order of parameter

        //@Deprecated
        //public void AddReportItem(int endPositionInLine, byte[] aCCSID930Bytes)
        //{
        //    AddReportItem(aCCSID930Bytes, endPositionInLine);
        //}
        //void AddReportItem(int value, int endPositionInLine)
        //{
        //    AddReportItem(value.ToString(), endPositionInLine);
        //}
        [Obsolete("AddReportItemOnRightJustified->AddOnRightJustified")]

        public void AddReportItemOnRightJustified(byte[] aCCSID930Bytes, int endPositionInLine)
        {
            AddOnRightJustified(aCCSID930Bytes, endPositionInLine);
        }
        [Obsolete("AddReportItemOnRightJustified->AddOnRightJustified")]

        public void AddReportItemOnRightJustified(string utf16Strings, int endPositionInLine)
        {
            AddOnRightJustified(utf16Strings, endPositionInLine);
        }

        //*
        //* @deprecated change order of parameter

        //@Deprecated
        //void AddReportItem(int endPositionInLine, string value, int paddedFullLength)
        //{
        //    AddReportItem(value, paddedFullLength, endPositionInLine);
        //}

        //void AddReportItem(string value, int paddedFullLength, int endPositionInLine)
        //{
        //    string padding = string.Empty;
        //    var legthOfValue = CCSID930.GetByteLength(value);
        //    if (legthOfValue < paddedFullLength)
        //    {
        //        padding = new string(' ', paddedFullLength - legthOfValue);
        //    }
        //    AddReportItem(value + padding, endPositionInLine);
        //}

        //public void AddReportItem(string yyyyMMdd, DateFormat dateFormat, int endPositionInLine)
        //{
        //    AddReportItem(Date.Of(yyyyMMdd).ToStringInyyyyMMddWithSlush(), endPositionInLine);
        //}

        //public enum DateFormat
        //{
        //    Y
        //}
    }
}
