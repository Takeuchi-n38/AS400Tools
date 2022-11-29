using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Mails
{
    public interface IMailSender
    {
        void Send(MailObject mailObject);
        
    }
}
