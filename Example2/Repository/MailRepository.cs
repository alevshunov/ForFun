using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example2.Repository
{
    public class MailRepository : IMailRepository
    {
        public class MailInfo
        {
            public MailInfo(string receiver, string text)
            {
                Receiver = receiver;
                Text = text;
            }

            public string Receiver { get; private set; }

            public string Text { get; private set; }
        }

        static MailRepository()
        {
            MailsStore = new List<MailInfo>();
            MailsStore.Add(new MailInfo("admin", "Hello from guest!"));
            MailsStore.Add(new MailInfo("admin", "Hello from hacker!"));
            MailsStore.Add(new MailInfo("admin", "Hello from tester!"));
            MailsStore.Add(new MailInfo("hacker", "Hello from Admin, OMG!"));
        }

        private static readonly List<MailInfo> MailsStore;

        public IQueryable<MailInfo> Mails
        {
            get { return MailsStore.AsQueryable(); }
        }

    }
}