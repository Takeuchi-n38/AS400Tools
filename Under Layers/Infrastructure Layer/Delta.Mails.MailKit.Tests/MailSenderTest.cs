using System;
using Xunit;
using System.IO;
using System.Text;

namespace Delta.Mails.MailKit.Tests
{
    public class MailSenderTest
    {
        //[Fact]
        //public void SendMail()
        //{
        //    IMailSender stu=MailSender.Of;

        //    IMailObjectRepository repo=new Ciid020MailObjectRepository();
        //    stu.Send(repo.Load("CIID020", "02"));
        //}

        //class Ciid020MailObjectRepository: IMailObjectRepository
        //{
        //    MailObject IMailObjectRepository.Load(string Popgm, string Pogrop)
        //    {
        //        var obj = new MailObject();
        //        obj.AddFrom("AS-400@KK-DCS.CO.JP")//MALINF.MIFRAD
        //        ;

        //        if (Pogrop == "01")
        //        {

        //        }
        //        else
        //        if (Pogrop == "02")
        //        {
        //            obj.SetSubject("���U�茋�ʍ\���`�F�b�N����")//MALINF.MIHED
        //            ;

        //            //MALATC
        //            //"MTPGM","MTGROP","MTREN","MTLIB","MTFILE","MTFLNM"
        //            //CIID020   ,"02",1,IIDLIB    ,AUTCHK    ,AUTW-CHECK.CSV                                    
        //            var body = new StringBuilder();
        //            body.AppendLine(" �����b�ɂȂ�܂��B ");
        //            body.AppendLine("");
        //            body.AppendLine(" ���T�̊��U�茋�ʂɑ΂��\���`�F�b�N���s�����Ƃ���G���[������܂����� ");
        //            body.AppendLine(" �ł����肵�܂��B ");
        //            body.AppendLine("");
        //            body.AppendLine(" �ȏ��낵�����肢�������܂��B ");
        //            body.AppendLine("");
        //            body.AppendLine("");
        //            body.AppendLine("  �ȉ��Z�p��� ");
        //            body.AppendLine("IIDLIB/CIID020");

        //            obj.SetBody(body.ToString());

        //            //MALINF
        //            //"MIPGM","MIGROP","MIHEAD","MILIB","MIFIL","MIMBR","MIFRAD"
        //            //CIID020   ,"02",���U�茋�ʍ\���`�F�b�N����,IIDLIB    ,QTXTSRC   ,AUCHKER   ,AS-400@KK-DCS.CO.JP                               

        //            /*MALADR
        //            MAPGM	MAGROP	MADIV	MAREN	MAADRS
        //            CIID020   	2	1	1	Y-SEKIMOTO@DELTAKOGYO.CO.JP                       
        //            CIID020   	2	1	2	YAMAWAKI-T@DELTAKOGYO.CO.JP                       
        //            CIID020   	2	1	4	NISHIMURA-Y@KK-DCS.CO.JP                          
        //            CIID020   	2	1	5	YYAMAMOTO@KK-DCS.CO.JP                            
        //            CIID020   	2	1	38	MATSUHASHI-KK@DELTAKOGYO.CO.JP                    
        //            CIID020   	2	1	39	TSUYAMA-C@DELTAKOGYO.CO.JP                        
        //            CIID020   	2	2	2	S-IKEMOTO@KK-DCS.CO.JP                            
        //            CIID020   	2	2	3	KSONOU@KK-DCS.CO.JP                               
        //             */

        //            obj
        //            .AddTo("stanaka@kk-dcs.co.jp")//MALADR.MAADRS MADIV=1
        //            .AddCc("stanaka@kk-dcs.co.jp")//DIV2
        //            .AddBcc("stanaka@kk-dcs.co.jp")//DIV3
        //            .AddAttachementFile(new StreamReader(@"C:\Delta\TestDatas\Shared.OPLIB.CYAE350\AUTW-CHECK.CSV").BaseStream, "AUTW-CHECK.CSV")//MALATC.MTFLNM
        //            ;

        //        }
        //        else
        //        {
        //            throw new NotImplementedException();
        //        }

        //        return obj;

        //    }

        //}

    }
}
