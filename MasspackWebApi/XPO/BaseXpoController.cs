using System.Web.Mvc;
using DevExpress.Xpo;

public class BaseXpoController : Controller, IXpoController
{
    public BaseXpoController()
    {
        XpoSession = CreateSession();
    }
    Session fXpoSession;
    public Session XpoSession
    {
        get { return fXpoSession; }
        private set { fXpoSession = value; }
    }
    protected virtual Session CreateSession()
    {
        return XpoHelper.GetNewSession();
    }
    public interface IXpoController
    {
        Session XpoSession { get; }
    }
}