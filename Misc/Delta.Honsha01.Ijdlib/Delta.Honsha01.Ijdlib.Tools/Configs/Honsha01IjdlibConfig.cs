using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.Tools.AS400.Configs;
using Delta.Honsha01;
using Delta.Tools.AS400.Libraries;
using System;
using System.Collections.Generic;
using Delta.AS400.DataAreas;

namespace Delta.Honsha01.Ijdlib.Tools.Configs
{
    public class Honsha01IjdlibConfig : IAnalyzerConfig, IGeneratorConfig
    {

        public static Library MainLibrary = Honsha01LibraryList.Ijdlib;

        public static Honsha01IjdlibConfig Of() => new Honsha01IjdlibConfig();

        Honsha01IjdlibConfig()
        {
        }

        Library IToolConfig.MainLibrary => MainLibrary;

        List<Library> IToolConfig.LibraryList => new List<Library>() {
                        MainLibrary,
                        };

        LibraryFactory IToolConfig.LibraryFactory { get; } = IjdlibLibraryFactory.Of(MainLibrary);

        List<ObjectID> IAnalyzerConfig.CheckedObjectIDs()
        {
            var rtn = new List<ObjectID>();
            //rtn.Add(Honsha01LibraryList.Prodlib.ObjectIDOf("QCMDEXC"));
            return rtn;
        }

        List<ObjectID> IAnalyzerConfig.EntryObjectIDs()
        {
            var rtn = new List<ObjectID>();

            //rtn.Add(Honsha01LibraryList.Ijdlib.ObjectIDOf("JIBDB1D0"));
            //rtn.Add(Honsha01LibraryList.Ijdlib.ObjectIDOf("JIJD05D0"));
            rtn.Add(Honsha01LibraryList.Ijdlib.ObjectIDOf("JIJD05W0"));
            rtn.Add(Honsha01LibraryList.Ijdlib.ObjectIDOf("JIJD06W0"));
            rtn.Add(Honsha01LibraryList.Ijdlib.ObjectIDOf("JIJD15W0"));


            //=========ここから下調査用===============
            /**
            //rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("JLFB05D0"));//HONSHA01 / LFBLIB / JLFB05D0（AUTO / 400）
            //rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("JLFB10D0"));//HONSHA01 / LFBLIB / JLFB10D0（HONSHA01 / SEATLIB / CJIK170内でSBMJOB）
            //rtn.Add(((IToolConfig)this).MainLibrary.ObjectIDOf("JLFB15D0"));//HONSHA01 / LFBLIB / JLFB15D0（AUTO / 400）

            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("JLFE10D0"));
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("JLFE20D0"));
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("JLFE30D0"));
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("JLFE40D0"));

            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("CJIE000")); //(TOYOLIB / CKLE001が起点）
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("CJIEA01"));
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("CJIEA02"));
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("CDSLD"));//画面
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("CJIERERUN"));
            **/

            return rtn;
        }

        List<ObjectID> IGeneratorConfig.GenerateCLObjectIDs()
        {
            var rtn = new List<ObjectID>();

            //2	CB	HONSHA01	OPLIB	AYAE050	183		
            //1	CL	HONSHA01	DSLDLIB	CDSLD	6	2	0
            //2	CL	HONSHA01	DSLDLIB	CDSLDMAIN	169	37	1
            //3	CL	HONSHA01	DSLDLIB	CDSLDOK	25	9	1
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("CJIE000"));//1	CL	HONSHA01	DSLDLIB	CJIE000	107	38	0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("CJIE290"));//2	CL	HONSHA01	DSLDLIB	CJIE290	117	33	0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("CJIE310"));//2	CL	HONSHA01	DSLDLIB	CJIE310	234	66	0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("CJIE39A"));//2	CL	HONSHA01	DSLDLIB	CJIE39A	83	17	0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("CJIE44A"));//2	CL	HONSHA01	DSLDLIB	CJIE44A	195	49	0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("CJIEA01"));//1	CL	HONSHA01	DSLDLIB	CJIEA01	42	15	0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("CJIEA02"));//1	CL	HONSHA01	DSLDLIB	CJIEA02	38	22	0
            //1	CL	HONSHA01	DSLDLIB	CJIERERUN	40	14	0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("CJIESNDSKJ"));//2	CL	HONSHA01	DSLDLIB	CJIESNDSKJ	76	15	0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("CJIESNDSKL"));//2	CL	HONSHA01	DSLDLIB	CJIESNDSKL	75	15	0
            //rtn.Add(Honsha01LibraryList.Hostlib.ObjectIDOf("C_LDLABLD"));//2	CL	HONSHA01	HOSTLIB	C_LDLABLD	265	82	0
            //rtn.Add(Honsha01LibraryList.Hostlib.ObjectIDOf("C_SHTOYLD"));//2	CL	HONSHA01	HOSTLIB	C_SHTOYLD	264	81	0
            //3	CL	HONSHA01	MIBLIB	CFTPSND01	608	294	0
            //3	CL	HONSHA01	OPLIB	CYAE320	14	4	0
            //4	NotFoundSource	HONSHA01	DSLDLIB	&JOB	0		
            //3	NotFoundSource	HONSHA01	PRODLIB	QCMDEXC	0		
            //4	NotFoundSource	HONSHA01	USL2LIB	PJIETIM	0		
            //4	NotFoundSource	HONSHA01	USL2LIB	QCMDEXC	0		

            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("CLFE032"));            //HONSHA01,LFELIB,CLFE032,31,10,0
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("CLFE032S"));            //HONSHA01,LFELIB,CLFE032S,56,11,0
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("CLFE033"));            //HONSHA01,LFELIB,CLFE033,31,10,0
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("CLFE033S"));            //HONSHA01,LFELIB,CLFE033S,56,11,0
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("CYBX030"));            //HONSHA01,LFELIB,CYBX030,18,5,0
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("JLFE10D0"));            //HONSHA01,LFELIB,JLFE10D0,289,78,0
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("JLFE20D0"));            //HONSHA01,LFELIB,JLFE20D0,352,130,0
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("JLFE30D0"));            //HONSHA01,LFELIB,JLFE30D0,205,45,0
            //rtn.Add(Honsha01LibraryList.Miblib.ObjectIDOf("CFTPSND01"));//HONSHA01,MIBLIB,CFTPSND01,608,294,0

            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("CLFE032"));            //CL,HONSHA01,DSLDLIB,CDSLD,6,2,0
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("CLFE032S"));            //CL,HONSHA01,DSLDLIB,CDSLDMAIN,169,37,1
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("CLFE033"));            //CL,HONSHA01,DSLDLIB,CDSLDOK,25,9,1
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("CLFE033S"));            //CL,HONSHA01,DSLDLIB,CJIERERUN,40,14,0

            return rtn;
        }

        List<ObjectID> IGeneratorConfig.GenerateRPG3ObjectIDs()
        {
            var rtn = new List<ObjectID>();



            //rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE000"));//HONSHA01,DSLDLIB,PJIE000,26,14,0,1,0 // 別途方法にて移行済みなので、Serviceとしては移行しない
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE001"));//HONSHA01,DSLDLIB,PJIE001,43,20,0,2,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE002"));//HONSHA01,DSLDLIB,PJIE002,64,27,0,3,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE29X"));//HONSHA01,DSLDLIB,PJIE29X,24,10,0,1,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE300"));//HONSHA01,DSLDLIB,PJIE300,825,244,0,10,1
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE310"));//HONSHA01,DSLDLIB,PJIE310,356,104,0,4,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE31B"));//HONSHA01,DSLDLIB,PJIE31B,106,32,0,2,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE32A"));//HONSHA01,DSLDLIB,PJIE32A,77,29,0,1,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE330"));//HONSHA01,DSLDLIB,PJIE330,296,79,0,6,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE340"));//HONSHA01,DSLDLIB,PJIE340,173,44,0,4,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE341"));//HONSHA01,DSLDLIB,PJIE341,172,42,0,4,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE350"));//HONSHA01,DSLDLIB,PJIE350,674,76,0,3,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE351"));//HONSHA01,DSLDLIB,PJIE351,744,77,0,3,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE361"));//HONSHA01,DSLDLIB,PJIE361,275,58,0,2,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE36X"));//HONSHA01,DSLDLIB,PJIE36X,296,41,0,1,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE36Y"));//HONSHA01,DSLDLIB,PJIE36Y,332,43,0,1,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE39D"));//HONSHA01,DSLDLIB,PJIE39D,869,258,0,10,1
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE44B"));//HONSHA01,DSLDLIB,PJIE44B,621,152,0,3,1
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIE600"));//3,R3,HONSHA01,DSLDLIB,PJIE600,1193,307,1,2,0
            rtn.Add(Honsha01LibraryList.Dsldlib.ObjectIDOf("PJIEDAT"));//HONSHA01,DSLDLIB,PJIEDAT,39,14,0,2,0
            //3	R3	HONSHA01	MIBLIB	AMIB002	16	8	0	2	0
            //3	R3	HONSHA01	MIBLIB	AMIB003	26	14	0	1	0
            //4	R3	HONSHA01	MIBLIB	FTPCHG	32	14	0	1	0
            //4	R3	HONSHA01	MIBLIB	FTPCHG2	83	24	0	1	0
            //4	R3	HONSHA01	MIBLIB	PERR010	20	10	0	1	0
            //4	R3	HONSHA01	MIBLIB	PERR020	23	10	0	1	0
            //4	R3	HONSHA01	MIBLIB	PFLNMGET	52	13	0	1	0
            //4	R3	HONSHA01	MIBLIB	PIPGET	40	14	0	1	0
            //4	R3	HONSHA01	MIBLIB	PYBZ1200	41	14	0	2	0
            //4	R3	HONSHA01	OPLIB	AYAE320	42	19	0	1	0
            //2	R3	HONSHA01	PRODLIB	AYAE011	22	8	0	1	0
            //2	R3	HONSHA01	PRODLIB	AYAE020	33	10	0	1	0
            //4	R3	HONSHA01	QGPL	FTPCHK	30	5	0	1	0
            //3	R3	HONSHA01	SEATLIB	FTPCHKRCV	27	2	0	1	0
            //3	R3	HONSHA01	SEATLIB	FTPCHKSND	30	4	0	1	0
            //3	R3	HONSHA01	USL2LIB	PJIE290	134	35	0	4	0
            //3	R3	HONSHA01	USL2LIB	PJIE295	180	55	0	3	0
            //3	R3	HONSHA01	USL2LIB	PJIE296	23	2	0	2	0
            //3	R3	HONSHA01	USL2LIB	PJIE305	34	1	0	2	0
            //3	R3	HONSHA01	USL2LIB	PJIE31A	277	89	0	3	0
            //3	R3	HONSHA01	USL2LIB	PJIE320	275	107	0	5	0
            //3	R3	HONSHA01	USL2LIB	PJIE32B	334	120	0	6	0
            //3	R3	HONSHA01	USL2LIB	PJIE360	259	58	0	2	0
            //3	R3	HONSHA01	USL2LIB	PJIE365	21	0	0	1	0
            //3	R3	HONSHA01	USL2LIB	PJIE39A	104	30	0	3	0
            //3	R3	HONSHA01	USL2LIB	PJIE39B	130	46	0	3	0
            //3	R3	HONSHA01	USL2LIB	PJIE39C	82	39	0	2	0
            //3	R3	HONSHA01	USL2LIB	PJIE44A	612	148	0	3	1
            //3	R3	HONSHA01	USL2LIB	PJIE550	1085	314	1	2	0



            //rtn.Add(Honsha01LibraryList.Kl_lib.ObjectIDOf("AKLB050A"));//HONSHA01,KL_LIB,AKLB050A,127,44,0,6,1
            //rtn.Add(Honsha01LibraryList.Kl_lib.ObjectIDOf("AKLB060"));//HONSHA01,KL_LIB,AKLB060,46,18,0,2,1
            //rtn.Add(Honsha01LibraryList.Kl_lib.ObjectIDOf("AKLB100PA"));//HONSHA01,KL_LIB,AKLB100PA,205,27,0,5,1
            //rtn.Add(Honsha01LibraryList.Kl_lib.ObjectIDOf("AKLB090"));//HONSHA01,KL_LIB,AKLB090,47,7,0,3,0
            //rtn.Add(Honsha01LibraryList.Kl_lib.ObjectIDOf("AKLB866"));//HONSHA01,KL_LIB,AKLB866,79,36,0,3,0

            //rtn.Add(Honsha01LibraryList.Lfblib.ObjectIDOf("PLFB200"));//HONSHA01,LFBLIB,PLFB200,15,8,0,2,0
            //rtn.Add(Honsha01LibraryList.Lfblib.ObjectIDOf("PLFB205"));//HONSHA01,LFBLIB,PLFB205,143,27,0,3,0
            //rtn.Add(Honsha01LibraryList.Lfblib.ObjectIDOf("PLFB210"));//HONSHA01,LFBLIB,PLFB210,68,22,0,3,0
            //rtn.Add(Honsha01LibraryList.Lfblib.ObjectIDOf("PLFB220"));//HONSHA01,LFBLIB,PLFB220,177,39,0,5,0
            //rtn.Add(Honsha01LibraryList.Lfblib.ObjectIDOf("PLFB230"));//HONSHA01,LFBLIB,PLFB230,48,17,0,3,0
            //rtn.Add(Honsha01LibraryList.Lfblib.ObjectIDOf("PLFB240"));//HONSHA01,LFBLIB,PLFB240,139,31,0,3,0
            //rtn.Add(Honsha01LibraryList.Lfblib.ObjectIDOf("PLFB250"));//HONSHA01,LFBLIB,PLFB250,67,16,0,3,0
            //rtn.Add(Honsha01LibraryList.Lfblib.ObjectIDOf("PLFB260"));//HONSHA01,LFBLIB,PLFB260,142,42,0,2,1
            //rtn.Add(Honsha01LibraryList.Lfblib.ObjectIDOf("PLFB270"));//HONSHA01,LFBLIB,PLFB270,64,0,0,2,0
            //rtn.Add(Honsha01LibraryList.Lfblib.ObjectIDOf("PLFB275"));//HONSHA01,LFBLIB,PLFB275,128,8,0,2,1
            //rtn.Add(Honsha01LibraryList.Lfblib.ObjectIDOf("PLFB280"));//HONSHA01,LFBLIB,PLFB280,34,0,0,2,0
            //rtn.Add(Honsha01LibraryList.Lfblib.ObjectIDOf("PLFB285"));//HONSHA01,LFBLIB,PLFB285,104,7,0,2,1
            //rtn.Add(Honsha01LibraryList.Lfblib.ObjectIDOf("PLFBXXX"));//HONSHA01,LFBLIB,PLFBXXX,281,117,0,5,0

            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("PLFE032"));//HONSHA01,LFELIB,PLFE032,169,29,0,3,0
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("PLFE110"));//HONSHA01,LFELIB,PLFE110,18,11,0,2,0
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("PLFE120"));//HONSHA01,LFELIB,PLFE120,433,122,0,8,0
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("PLFE130"));//HONSHA01,LFELIB,PLFE130,122,27,0,1,1
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("PLFE210"));//HONSHA01,LFELIB,PLFE210,160,49,0,5,0
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("PLFE220"));//HONSHA01,LFELIB,PLFE220,69,24,0,2,0
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("PLFE300"));//HONSHA01,LFELIB,PLFE300,116,39,0,4,0
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("PLFE310"));//HONSHA01,LFELIB,PLFE310,150,40,0,3,0
            //rtn.Add(Honsha01LibraryList.Lfelib.ObjectIDOf("PLFE320"));//HONSHA01,LFELIB,PLFE320,312,75,0,6,0

            //rtn.Add(Honsha01LibraryList.Miblib.ObjectIDOf("AMIB002"));//HONSHA01,MIBLIB,AMIB002,16,8,0,2,0
            //rtn.Add(Honsha01LibraryList.Miblib.ObjectIDOf("AMIB003"));//HONSHA01,MIBLIB,AMIB003,26,14,0,1,0
            //rtn.Add(Honsha01LibraryList.Miblib.ObjectIDOf("FTPCHG"));//HONSHA01,MIBLIB,FTPCHG,32,14,0,1,0
            //rtn.Add(Honsha01LibraryList.Miblib.ObjectIDOf("FTPCHG2"));//HONSHA01,MIBLIB,FTPCHG2,83,24,0,1,0
            //rtn.Add(Honsha01LibraryList.Miblib.ObjectIDOf("PERR010"));//HONSHA01,MIBLIB,PERR010,20,10,0,1,0
            //rtn.Add(Honsha01LibraryList.Miblib.ObjectIDOf("PERR020"));//HONSHA01,MIBLIB,PERR020,23,10,0,1,0
            //rtn.Add(Honsha01LibraryList.Miblib.ObjectIDOf("PFLNMGET"));//HONSHA01,MIBLIB,PFLNMGET,52,13,0,1,0
            //rtn.Add(Honsha01LibraryList.Miblib.ObjectIDOf("PIPGET"));//HONSHA01,MIBLIB,PIPGET,40,14,0,1,0
            //rtn.Add(Honsha01LibraryList.Miblib.ObjectIDOf("PYBZ1200"));//HONSHA01,MIBLIB,PYBZ1200,41,14,0,2,0

            //rtn.Add(Honsha01LibraryList.Oplib.ObjectIDOf("AYAE320"));//HONSHA01,OPLIB,AYAE320,42,19,0,1,0
            //rtn.Add(Honsha01LibraryList.Oplib.ObjectIDOf("AYAE500"));//HONSHA01,OPLIB,AYAE500,23,3,0,2,0

            //rtn.Add(Honsha01LibraryList.Prodlib.ObjectIDOf("AYAE011"));//HONSHA01,PRODLIB,AYAE011,22,8,0,1,0
            //rtn.Add(Honsha01LibraryList.Prodlib.ObjectIDOf("AYAE020"));//HONSHA01,PRODLIB,AYAE020,33,10,0,1,0

            //rtn.Add(Honsha01LibraryList.Qgpl.ObjectIDOf("FTPCHK"));//HONSHA01,QGPL,FTPCHK,30,5,0,1,0

            //rtn.Add(Honsha01LibraryList.Seatlib.ObjectIDOf("FTPCHKRCV"));//HONSHA01,SEATLIB,FTPCHKRCV,27,2,0,1,0
            //rtn.Add(Honsha01LibraryList.Seatlib.ObjectIDOf("FTPCHKSND"));//HONSHA01,SEATLIB,FTPCHKSND,30,4,0,1,0

            //rtn.Add(Honsha01LibraryList.Usl2lib.ObjectIDOf("PJIE290"));//HONSHA01,USL2LIB,PJIE290,134,35,0,4,0
            //rtn.Add(Honsha01LibraryList.Usl2lib.ObjectIDOf("PJIE295"));//HONSHA01,USL2LIB,PJIE295,180,55,0,3,0
            //rtn.Add(Honsha01LibraryList.Usl2lib.ObjectIDOf("PJIE296"));//HONSHA01,USL2LIB,PJIE296,23,2,0,2,0
            //rtn.Add(Honsha01LibraryList.Usl2lib.ObjectIDOf("PJIE305"));//HONSHA01,USL2LIB,PJIE305,34,1,0,2,0
            //rtn.Add(Honsha01LibraryList.Usl2lib.ObjectIDOf("PJIE31A"));//HONSHA01,USL2LIB,PJIE31A,277,89,0,3,0
            //rtn.Add(Honsha01LibraryList.Usl2lib.ObjectIDOf("PJIE320"));//HONSHA01,USL2LIB,PJIE320,275,107,0,5,0
            //rtn.Add(Honsha01LibraryList.Usl2lib.ObjectIDOf("PJIE32B"));//HONSHA01,USL2LIB,PJIE32B,334,120,0,6,0
            //rtn.Add(Honsha01LibraryList.Usl2lib.ObjectIDOf("PJIE360"));//HONSHA01,USL2LIB,PJIE360,259,58,0,2,0
            //rtn.Add(Honsha01LibraryList.Usl2lib.ObjectIDOf("PJIE365"));//HONSHA01,USL2LIB,PJIE365,21,0,0,1,0
            //rtn.Add(Honsha01LibraryList.Usl2lib.ObjectIDOf("PJIE39A"));//HONSHA01,USL2LIB,PJIE39A,104,30,0,3,0
            //rtn.Add(Honsha01LibraryList.Usl2lib.ObjectIDOf("PJIE39B"));//HONSHA01,USL2LIB,PJIE39B,130,46,0,3,0
            //rtn.Add(Honsha01LibraryList.Usl2lib.ObjectIDOf("PJIE39C"));//HONSHA01,USL2LIB,PJIE39C,82,39,0,2,0
            //rtn.Add(Honsha01LibraryList.Usl2lib.ObjectIDOf("PJIE44A"));//HONSHA01,USL2LIB,PJIE44A,612,148,0,3,1 //2021.9.10追加

            //rtn.Add(Honsha01LibraryList.Weblib.ObjectIDOf("PYBZ100"));//HONSHA01,WEBLIB,PYBZ100,41,14,0,2,0

            //rtn.Add(Honsha01LibraryList.Ybclib.ObjectIDOf("AYBCE07"));//HONSHA01,YBCLIB,AYBCE07,57,24,0,2,0

            //rtn.Add(Honsha01LibraryList.Usl2lib.ObjectIDOf("PJIE550"));//3,R3,HONSHA01,USL2LIB,PJIE550,1085,314,1,2,0

            /*
4,NotFoundSource,HONSHA01,DSLDLIB,&JOB,0
4,NotFoundSource,HONSHA01,USL2LIB,PJIETIM,0
 */

            return rtn;
            /*         
            rtn.Add(Honsha01LibraryList.Lfblib.ObjectIDOf("PLFB200"));//HONSHA01,LFBLIB,PLFB200,15,8,0,2,0
            rtn.Add(Honsha01LibraryList.Lfblib.ObjectIDOf("PLFB210"));//HONSHA01,LFBLIB,PLFB210,68,22,0,3,0
            rtn.Add(Honsha01LibraryList.Lfblib.ObjectIDOf("PLFBXXX"));//HONSHA01,LFBLIB,PLFBXXX,281,117,0,5,0
            */

        }

        List<ObjectID> IGeneratorConfig.GenerateRPG4ObjectIDs()
        {
            var rtn = new List<ObjectID>();
            return rtn;
        }

        List<ObjectID> IGeneratorConfig.NoGenerateServiceObjectIDs()
        {
            var rtn = new List<ObjectID>();
            return rtn;
        }

        List<DataArea> IToolConfig.DataAreas()
        {
            var rtn = new List<DataArea>();
            //rtn.Add(DataArea.Of(Honsha01LibraryList.Comnlib, "LIBDTAARA"));
            //rtn.Add(DataArea.Of(Honsha01LibraryList.Comnlib, "SRCDTAARA"));
            //rtn.Add(DataArea.Of(Honsha01LibraryList.Lfelib, "DTAJUN"));
            return rtn;
        }

        List<Library> IGeneratorConfig.LibrariesOfDB2foriRepository()
        {
            return new List<Library>() {
                //Honsha01LibraryList.Prodlib,
                //Honsha01LibraryList.Oplib,
                //Honsha01LibraryList.Master2,//更新ありのファイルもあるのでそちらは別途検討の必要あり
                //Honsha01LibraryList.Toyolib,
                //Honsha01LibraryList.Seatlib,
                //Honsha01LibraryList.Usl2lib,
                //Honsha01LibraryList.Wrkcat1//,
                //Honsha01LibraryList.Wrkcat2//Postgresのものは中間ファイルと思われるのでインメモリとして実装するかもしれないもの
                };

        }

    }
}
