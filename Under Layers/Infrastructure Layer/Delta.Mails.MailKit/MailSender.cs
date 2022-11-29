using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Delta.Mails.MailKit
{


    public class MailSender: IMailSender
    {
        readonly int port = 25;
        readonly string host;
        MailSender(string host)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            this.host=host;
        }

        public static MailSender Of=>new MailSender("192.168.11.10");

        void IMailSender.Send(MailObject mailObject)
        {
           
            using (var smtp = new SmtpClient())
            {
                smtp.Connect(host, port, SecureSocketOptions.Auto);
                //smtp.Authenticate("<id>", "<password>");

                var enc = Encoding.GetEncoding("iso-2022-jp");

                var mail = new MimeMessage();
                mailObject.Froms.ForEach(item=> mail.From.Add(new MailboxAddress(enc, item.name, item.address)));
                mailObject.Tos.ForEach(item => mail.To.Add(new MailboxAddress(enc, item.name, item.address)));
                mailObject.Ccs.ForEach(item => mail.Cc.Add(new MailboxAddress(enc, item.name, item.address)));
                mailObject.Bccs.ForEach(item => mail.Bcc.Add(new MailboxAddress(enc, item.name, item.address)));

                mail.Headers.Replace(HeaderId.Subject, enc, mailObject.Subject);

                var multipart = new Multipart("mixed");
                var textPart = new TextPart("plain");
                textPart.SetText(enc, mailObject.Body);
                textPart.ContentTransferEncoding = MimeKit.ContentEncoding.SevenBit;// "iso-2022-jp"で送るので、"Content-Transfer-Encoding"に"7bit"を指定
                multipart.Add(textPart);

                mailObject.AttachementFiles.ForEach(item => 
                {
                    var mimeType = MimeTypes.GetMimeType(item.name); //ファイルの拡張子からMIMEタイプを取得する //=> image/jpeg
                    var attachment = new MimePart(mimeType);
                    attachment.Content = new MimeContent(item.stream);
                    attachment.ContentDisposition = new ContentDisposition();
                    attachment.ContentTransferEncoding = ContentEncoding.Base64;
                    attachment.FileName = item.name;
                    multipart.Add(attachment);
                });

                mail.Body = multipart;
                smtp.Send(mail);  

                smtp.Disconnect(true);
            }

        }

    }
}
