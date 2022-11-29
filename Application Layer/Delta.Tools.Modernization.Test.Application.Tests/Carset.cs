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
//0000      A*�J�[�Z�b�g�e�[�u��
//0001      A*
//0002      A                                      UNIQUE
//0003      A          R CARSETR                   TEXT('���R�[�h�l��')
//0004      A*
//0005      A            CRPW           5S 0       COLHDG('�}�c�_�v��T')
//0006      A            CRDPN          4A         COLHDG('��\')
//0007      A            CRCOL          3A         COLHDG('�����F')
//0008      A            CRIRO          2A         COLHDG('�F')
//0009      A            CRPOS          2A         COLHDG('�ʒu')
//0010      A            CRSPN         16A         COLHDG('�Z�b�g�i��')
//0011      A            CRHRI          4A         COLHDG('����')
//0012      A            CRYMD          8S 0       COLHDG('�N��')
//0013      A            CRPN          16A         COLHDG('�V�[�g�i��')
//0014      A            CRQTY          8S 0       COLHDG('����')
//0015      A            CRNPN         16A         COLHDG('���ۍ\���i��')
//0016      A            CRNAI          1A         COLHDG('�����L��')
//0017      A            CRBIT          1A         COLHDG('�i�Ԋ��o��')
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
//0000      A*�R���g���[���}�X�^�[
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
