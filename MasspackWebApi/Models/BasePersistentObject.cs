using DevExpress.Xpo;

[NonPersistent]
public class BasePersistentObject : XPObject
{
    public BasePersistentObject(Session session) : base(session) { }

    //private int fId;
    //[Key]
    //public virtual int ID
    //{
    //    get { return this.fId; }
    //    set { this.SetPropertyValue<int>("ID", ref this.fId, value); }
    //}

    private bool fIsChanged = false;
    public bool IsChanged
    {
        get { return this.fIsChanged; }
    }

    protected override void OnChanged(string propertyName, object oldValue, object newValue)
    {
        this.fIsChanged = true;
        base.OnChanged(propertyName, oldValue, newValue);
    }

    protected override void OnSaved()
    {
        this.fIsChanged = false;
        base.OnSaved();
    }

    
}