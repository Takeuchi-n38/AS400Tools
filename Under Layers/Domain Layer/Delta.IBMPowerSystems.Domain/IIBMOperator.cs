using System;
using System.Collections.Generic;

namespace Delta.IBMPowerSystems
{
    public interface IIBMOperator
    {

        void ExecuteNonQuery(string nonQuery);

        byte[] DownloadData(string aTargetLibraryName, string aTargetFileName);

        List<byte[]> DownloadData(string aTargetLibraryName, string aTargetFileName,int fileLength);

    }

    public static class IIBMOperatorExtensions
    {

        public static void QcmdexcCall(this IIBMOperator target,string targetLibrary, string targetName)
        {
            var callCmd = $"CALL {targetLibrary}/{targetName}";
            target.Qcmdexc(callCmd);
        }

        public static void Qcmdexc(this IIBMOperator target,string callCmd)
        {
            var length = callCmd.Length.ToString("D3");
            target.ExecuteNonQuery($"CALL QSYS.QCMDEXC('{callCmd}', 0000000{length}.00000)");
        }

    }
}
