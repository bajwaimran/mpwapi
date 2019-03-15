using System;
using DevExpress.Xpo;

namespace MasspackWebApi.Models
{

    public class TutorialType : XPObject
    {
        public TutorialType() : base() { }

        public TutorialType(Session session) : base(session) { }

        string _Name;
        public string Name
        {
            get => _Name;
            set => SetPropertyValue(nameof(Name), ref _Name, value);
        }

        [Association("TutorialType-Tutorials")]
        public XPCollection<Tutorial> Tutorials
        {
            get => GetCollection<Tutorial>("Tutorials");
        }
    }


    public class Tutorial : XPObject
    {
        public Tutorial() : base() { }

        public Tutorial(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        TutorialType _TutorialType;
        [Association("TutorialType-Tutorials")]
        public TutorialType TutorialType
        {
            get => _TutorialType;
            set => SetPropertyValue(nameof(TutorialType), ref _TutorialType, value);
        }

        string _ButtonText;
        public string ButtonText
        {
            get => _ButtonText;
            set => SetPropertyValue(nameof(ButtonText), ref _ButtonText, value);
        }
        string _Heading;
        public string Heading
        {
            get => _Heading;
            set => SetPropertyValue(nameof(Heading), ref _Heading, value);
        }
        string _Text;
        [Size(SizeAttribute.Unlimited)]
        public string Text
        {
            get => _Text;
            set => SetPropertyValue(nameof(Text), ref _Text, value);
        }
        DateTime _Timestamp;
        public DateTime Timestamp
        {
            get => _Timestamp;
            set => SetPropertyValue(nameof(Timestamp), ref _Timestamp, value);
        }


    }

}