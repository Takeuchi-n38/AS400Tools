using Delta.AS400.DataTypes.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.AS400.DDSs
{
    public class RecordFormatSpecification
    {
        public readonly List<FieldSpecification> FieldSpecifications;

        readonly int RecordLength;
        public RecordFormatSpecification(int RecordLength)
        {
            FieldSpecifications = new List<FieldSpecification>();
            this.RecordLength = RecordLength;
        }

        public static RecordFormatSpecification OfLeftJustified(byte[] bytes)
        {
            var recordFormat = new RecordFormatSpecification(bytes.Length);
            recordFormat.AddOnLeftJustified(bytes);
            return recordFormat;
        }

        public byte[] ToBytes()
        {
            var bytes = Enumerable.Range(1, RecordLength).Select(i => CodePage290.ByteOfSpace).ToArray();

            FieldSpecifications.ForEach(f =>
            {
                Array.Copy(f.CCSID930Bytes, 0, bytes, f.StartIndexInLine, f.CCSID930Bytes.Length);
            }
            );

            return bytes;
        }

        public string ToLine()
        {
            return CodePage930.ToStringFrom(ToBytes());
        }

        public void Add(FieldSpecification field)
        {
            FieldSpecifications.Add(field);
        }

        public void Add(IEnumerable<string> values)
        {
            var startPositionInLine = 1;
            values.ToList().ForEach(v =>
            {
                AddOnLeftJustified(v, startPositionInLine);
                startPositionInLine += CodePage930.GetByteLength(v);
            });
        }

        public void AddOnLeftJustified(byte[] aCCSID930Bytes)
        {
            Add(FieldSpecification.OfLeftJustified(aCCSID930Bytes, 1));
        }

        public void AddOnLeftJustified(byte[] aCCSID930Bytes, int startPositionInLine)
        {
            Add(FieldSpecification.OfLeftJustified(aCCSID930Bytes, startPositionInLine));
        }
        public void AddOnLeftJustified(string utf16Strings, int startPositionInLine)
        {
            AddOnLeftJustified(CodePage930.ToBytesFrom(utf16Strings), startPositionInLine);
        }

        public void AddOnRightJustified(byte[] aCCSID930Bytes, int endPositionInLine)
        {
            Add(FieldSpecification.OfRightJustified(aCCSID930Bytes, endPositionInLine));
        }
        public void AddOnRightJustified(string utf16Strings, int endPositionInLine)
        {
            AddOnRightJustified(CodePage930.ToBytesFrom(utf16Strings), endPositionInLine);
        }
    }
}
