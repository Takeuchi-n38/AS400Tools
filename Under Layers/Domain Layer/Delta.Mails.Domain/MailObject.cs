using System;
using System.Collections.Generic;
using System.IO;

namespace Delta.Mails
{
    public class MailObject
    {
        public List<(string name, string address)> Froms = new List<(string name, string address)>();
        public MailObject AddFrom(string address)
        {
            Froms.Add(("",address));
            return this;
        }

        public List<(string name, string address)> Tos = new List<(string name, string address)>();
        public MailObject AddTo(string address)
        {
            Tos.Add(("", address));
            return this;
        }

        public List<(string name, string address)> Ccs = new List<(string name, string address)>();
        public MailObject AddCc(string address)
        {
            Ccs.Add(("", address));
            return this;
        }

        public List<(string name, string address)> Bccs = new List<(string name, string address)>();
        public MailObject AddBcc(string address)
        {
            Bccs.Add(("", address));
            return this;
        }

        string subject=string.Empty;
        public MailObject SetSubject(string subject)
        {
            this.subject=subject;
            return this;
        }
        public string Subject =>subject;

        string body = string.Empty;
        public MailObject SetBody(string body)
        {
            this.body = body;
            return this;
        }
        public string Body => body;



        public List<(Stream stream, string name)> AttachementFiles = new List<(Stream stream, string name)>();
        public MailObject AddAttachementFile(Stream stream, string name)
        {
            AttachementFiles.Add((stream, name));
            return this;
        }


    }

}
