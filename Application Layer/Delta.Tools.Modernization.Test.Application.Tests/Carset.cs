using System;
using System.Collections.Generic;

#nullable disable

namespace Delta.Honsha01.Iidlib.Carsets
{
    public partial class Carset
    {
        public int Crpw { get; set; }
        public string Crpw4s { get; set; }
        public string Crdpn { get; set; }
        public string Crdpn4s { get; set; }
        public string Crcol { get; set; }
        public string Crcol4s { get; set; }
        public string Criro { get; set; }
        public string Crpos { get; set; }
        public string Crpos4s { get; set; }
        public string Crspn { get; set; }
        public string Crspn4s { get; set; }
        public string Crhri { get; set; }
        public string Crhri4s { get; set; }
        public int Crymd { get; set; }
        public string Crymd4s { get; set; }
        public string Crpn { get; set; }
        public string Crpn4s { get; set; }
        public int Crqty { get; set; }
        public string Crnpn { get; set; }
        public string Crnai { get; set; }
        public string Crbit { get; set; }
    }
}
//0000      A*カーセットテーブル
//0001      A*
//0002      A                                      UNIQUE
//0003      A          R CARSETR                   TEXT('レコード様式')
//0004      A*
//0005      A            CRPW           5S 0       COLHDG('マツダ計画週')
//0006      A            CRDPN          4A         COLHDG('代表')
//0007      A            CRCOL          3A         COLHDG('内装色')
//0008      A            CRIRO          2A         COLHDG('色')
//0009      A            CRPOS          2A         COLHDG('位置')
//0010      A            CRSPN         16A         COLHDG('セット品番')
//0011      A            CRHRI          4A         COLHDG('払い')
//0012      A            CRYMD          8S 0       COLHDG('年月')
//0013      A            CRPN          16A         COLHDG('シート品番')
//0014      A            CRQTY          8S 0       COLHDG('数量')
//0015      A            CRNPN         16A         COLHDG('実際構成品番')
//0016      A            CRNAI          1A         COLHDG('内示有無')
//0017      A            CRBIT          1A         COLHDG('品番割出済')
//0018      A*
//0019      A          K CRPW
//0020      A          K CRDPN
//0021      A          K CRCOL
//0022      A          K CRPOS
//0023      A          K CRSPN
//0024      A          K CRHRI
//0025      A          K CRYMD
//0026      A          K CRPN
//1.0.0.1

//04
//0000      A*コントロールマスター
//0001      A*
//0002      A          R CARSETR                   PFILE(CARSET)
//0003      A*
//0004      A          K CRPW
//0005      A          K CRYMD
//0006      A          K CRQTY
//0007      A          K CRDPN
//0008      A          K CRCOL
//0009      A          K CRPOS
//0010      A          K CRHRI
//0011      A          K CRPN
//0012      A*
//0013      A          O CRNAI                     COMP(EQ '1')
//0014      A          O CRBIT                     COMP(EQ '1')
