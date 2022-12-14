using Delta.IBMPowerSystems;
using System.Linq;
using System.Text;
using Xunit;

namespace Delta.RelationalDatabases.Db2fori.Tests
{
    public class DB2foriOperatorTest
    {
        public static DB2foriOperator TestDB2foriOperator = DB2foriOperator.TestOf();

        [Fact]
        public void GetHexStrings()
        {
            var table = Table.Of("IIDLIB", "CONTRL");
            var x = TestDB2foriOperator.GetHexStrings(table);// "SELECT CONID, CONVA1 FROM IIDLIB.CONTRL WHERE CONID ='AUTWARI'");
            Assert.Equal("C1E4E3E6C1D9C9404040F2F0F2F1F1F1F0F2404040404040404040404040F2F0F2F2F0F3F0F040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040400E47D648F644AE468B45C10F4040404040404040404040404040404040404040404040404040404040404040404040404040", x.First());
        }
        [Fact]
        public void GetHexStringsNull()
        {
            var table = Table.Of("IIDLIB", "CONTRLa");
            var x = TestDB2foriOperator.GetHexStrings(table);// "SELECT CONID, CONVA1 FROM IIDLIB.CONTRL WHERE CONID ='AUTWARI'");
            Assert.Equal("C1E4E3E6C1D9C9404040F2F0F2F1F1F1F0F2404040404040404040404040F2F0F2F2F0F3F0F040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040404040400E47D648F644AE468B45C10F4040404040404040404040404040404040404040404040404040404040404040404040404040", x.First());
        }
        //[Fact]
        //public void CRTPF()
        //{
        //    var cmd = "CRTPF FILE(TANALIB/TANAPF) RCDLEN(96) IGCDTA(*YES) OPTION(*NOSRC *NOLIST) SIZE(*NOMAX) LVLCHK(*NO)";
        //    TestDB2foriOperator.Qcmdexc(cmd);
        //}

        //[Fact]
        //public void CALL()
        //{
        //    TestDB2foriOperator.QcmdexcCall("TKKRLIB", "BRKMSG");
        //}

        [Fact]
        public void DB2except()
        {
            var oneKeyName = "HWHIN";
            var actualTableName = "CIID030A1.HINWAR";
            var expectedTableName = "CIID030E1.HINWAR";
            var query = $"SELECT COUNT(X.{oneKeyName}) FROM (SELECT * FROM {actualTableName} EXCEPT SELECT * FROM {expectedTableName} UNION ALL SELECT * FROM {expectedTableName} EXCEPT SELECT * FROM {actualTableName}) AS X";
            var diffCnt = TestDB2foriOperator.ExecuteLong(query);
            Assert.Equal(0, diffCnt);
        }
    }
}
