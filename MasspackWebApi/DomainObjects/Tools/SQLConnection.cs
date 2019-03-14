using DevExpress.Xpo;
using System;

namespace BestellErfassung.DomainObjects.Tools
{
    //[DefaultClassOptions]

    public class SQLConnection : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public SQLConnection(Session session)
            : base(session)
        {
        }

        private string _IP;
        [Size(120)]
        public string IP
        {
            get { return _IP; }
            set { SetPropertyValue<string>("IP", ref _IP, value); }
        }


        private string _Port;
        [Size(120)]
        public string Port
        {
            get { return _Port; }
            set { SetPropertyValue<string>("Port", ref _Port, value); }
        }

        private string _Datenbank;
        [Size(120)]
        public string Datenbank
        {
            get { return _Datenbank; }
            set { SetPropertyValue<string>("Datenbank", ref _Datenbank, value); }
        }

        private string _user;
        [Size(120)]
        public string user
        {
            get { return _user; }
            set { SetPropertyValue<string>("user", ref _user, value); }
        }

        private string _password;
        [Size(120)]
        public string password
        {
            get { return _password; }
            set { SetPropertyValue<string>("password", ref _password, value); }
        }
        private int _Mitarbeiter;
        public int Mitarbeiter
        {
            get { return _Mitarbeiter; }
            set { SetPropertyValue<int>("Mitarbeiter", ref _Mitarbeiter, value); }
        }


        private string _Aktuell;
        [Size(120)]
        public string Aktuell
        {
            get { return _Aktuell; }
            set { SetPropertyValue<string>("Aktuell", ref _Aktuell, value); }
        }

        private DateTime _Datum;
        public DateTime Datum
        {
            get { return _Datum; }
            set { SetPropertyValue<DateTime>("Datum", ref _Datum, value); }
        }

    }
}
