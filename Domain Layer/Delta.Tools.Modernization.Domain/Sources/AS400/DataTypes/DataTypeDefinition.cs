using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.AS400.DataTypes
{
    public class DataTypeDefinition : IDataTypeDefinition
    {
        readonly string Name;
        string IDataTypeDefinition.Name => Name;

        readonly string Length;
        string IDataTypeDefinition.Length => Length;

        readonly string InternalDataType;
        string IDataTypeDefinition.InternalDataType => InternalDataType;

        readonly string DecimalPositions;
        string IDataTypeDefinition.DecimalPositions => DecimalPositions;
        readonly string Summary;
        string IDataTypeDefinition.Summary => Summary;


        DataTypeDefinition(string name, string length, string internalDataType, string decimalPositions, string aSummary)
        {
            Name = name;
            Length = length;
            InternalDataType = internalDataType;
            DecimalPositions = decimalPositions;
            this.Summary=aSummary;
        }

        public static DataTypeDefinition Of(string name, string length, string internalDataType, string decimalPositions, string aSummary)
        {
            return new DataTypeDefinition(name, length, internalDataType, decimalPositions, aSummary);
        }
        public static DataTypeDefinition Of(string name, string length, string internalDataType, string decimalPositions)
        {
            return Of(name, length, internalDataType, decimalPositions,string.Empty);
        }

        #region "equals"

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var target = (DataTypeDefinition)obj;

            if (Name != target.Name) return false;

            if (Length != target.Length) return false;

            if (InternalDataType != target.InternalDataType) return false;

            if (DecimalPositions != target.DecimalPositions) return false;

            return true;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Length.GetHashCode() ^ InternalDataType.GetHashCode() ^ DecimalPositions.GetHashCode();
        }

        #endregion

    }
}
