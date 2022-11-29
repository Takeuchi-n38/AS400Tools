using Delta.Tools.AS400.DDSs;
using Delta.Tools.AS400.Sources;
using Delta.Tools.Sources.Statements;
using System.Collections.Immutable;

namespace Delta.Tools.AS400.DDSs.PrinterFiles
{
    public class PRTStructure : DdsStructure
    {
        public readonly ImmutableList<IStatement> Elements;

        public readonly ImmutableList<PrintLineList> PrintLineLists;

        public PRTStructure(Source source, ImmutableList<IStatement> Elements, ImmutableList<PrintLineList> PrintLineLists) : base(source)
        {
            this.Elements = Elements;
            this.PrintLineLists = PrintLineLists;
        }

    }
}
//0000      A*****************************************************************
//0001      A*     SYSTEM        : 売上管理                                *
//0002      A*     SUB SYSTEM    :                                           *
//0003      A*     DSP FILE ID.  :  PQEA100P                                 *
//0004      A*     DSP FILE記述: 海外売上チェック                        *
//0005      A*                      (ｺﾝﾊﾟｲﾙ  L=66 W=132 OF=63)               *
//0006      A*                                                               *
//0007      A*     AUTHOR        :  2000.09.20   T.SAKO                      *
//0008      A*     UPDATE        :  XXXX.XX.XX                               *
//0009      A*****************************************************************
//0010      A*===============================================================*
//0011      A*               見　　出　　し　　印　　刷
//0012      A*===============================================================*
//0013      A          R HDR10                     SKIPB(3)
//0014      A                                     4'PQEA100'
//0015      A                                    51'海　外　売　上　チ　ェ　ッ　ク'
//0016      A                                   110'OP.'
//0017      A                                   114DATE
//0018      A                                      EDTWRD('0  /  /  ')
//0019      A                                   124'P.'
//0020      A                                   126PAGNBR EDTCDE(Z)
//0021      A                                      SPACEA(2)
//0022      A*
//0023      A                                     1'得意先 品番'
//0024      A                                    30'量 売上日  得納品№'
//0025      A                                    59'数量'
//0026      A                                    75'単価'
//0027      A                                    91'金額 品名'
//0028      A                                   119'理科目'
//0029      A                                      SPACEA(1)
//0030      A*
//0031      A                                     3'区製デ単課納品+
//0032      A                                      №'
//0033      A                                    30'メッセージ'
//0034      A                                      SPACEA(1)
//0035      A*
//0036      A*===============================================================*
//0037      A*               明　　細　　印　　刷
//0038      A*===============================================================*
//0039      A          R DTL10
//0040      A            UW1CUST   R              2REFFLD(CUST     URIWK1)
//0041      A***         UBCUST1   R              7REFFLD(CUST1    URIWK1)
//0042      A            WPARTNO       20        10
//0043      A            UW1RSCD   R             31REFFLD(RSCD     URIWK1)
//0044      A            WERR1          1        32
//0045      A            WSALEDT        8  0     34
//0046      A                                      EDTWRD('0    /  /  ')
//0047      A            UW1CDEVRNOR             45REFFLD(CDEVRNO  URIWK1)
//0048      A            WQTY      R             55REFFLD(QTY      URIWK1)
//0049      A                                      EDTCDE(J)
//0050      A            WPRICE        11  2     69
//0051      A                                      EDTCDE(1)
//0052      A            WMONEY         9  0     85
//0053      A                                      EDTCDE(J)
//0054      A            UW1PARTNM R             99REFFLD(PARTNM   URIWK1)
//0055      A            UW1REASCD R            120REFFLD(REASCD   URIWK1)
//0056      A            UW1ITEM1  R            123REFFLD(ITEM1    URIWK1)
//0057      A            UW1ITEM2  R            127REFFLD(ITEM2    URIWK1)
//0058      A                                      SPACEA(1)
//0059      A*
//0060      A            UW1DAIKB  R              4REFFLD(DAIKB    URIWK1)
//0061      A            WERR2          1         5
//0062      A            UW1PARTGR R              8REFFLD(PARTGR   URIWK1)
//0063      A            WERR3          1        10
//0064      A            WDATAKB        1        12
//0065      A            WERR4          1        13
//0066      A            WPRICEKB       1        16
//0067      A            WERR5          1        17
//0068      A            WTAXKB         1        19
//0069      A            WERR6          1        20
//0070      A***         UBDEVRNO  R    1        22REFFLD(DEVRNO   URIWK1)
//0071      A            WMSG1         15O       30
//0072      A            WMSG2         13O       46
//0073      A            WMSG3         25O       60
//0074      A            WMSG4         13O       85
//0075      A            WMSG5         12O       98
//0076      A                                      SPACEA(2)
//0077      A*===============================================================*
//0078      A*                    ERROR 印　　刷
//0079      A*===============================================================*
//0080      A          R ERR10                     SPACEB(1)
//0081      A*
//0082      A                                    15'＊＊＊'
//0083      A                                    26'該当データがありません。'
//0084      A                                    56'＊＊＊'
//0085      A                                      SPACEA(1)