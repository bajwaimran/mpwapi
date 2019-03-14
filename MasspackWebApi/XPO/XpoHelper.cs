using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using DX.Utils.Data;

public static class XpoHelper
{
    public static Session GetNewSession()
    {
        return new Session(DataLayer);
    }

    public static UnitOfWork GetNewUnitOfWork()
    {
        return new UnitOfWork(DataLayer);
    }

    private readonly static object lockObject = new object();

    static volatile IDataLayer fDataLayer;
    static IDataLayer DataLayer
    {
        get
        {
            if (fDataLayer == null)
            {
                lock (lockObject)
                {
                    if (fDataLayer == null)
                    {
                        fDataLayer = GetDataLayer();
                    }
                }
            }
            return fDataLayer;
        }
    }

    public static IDataLayer GetDataLayer()
    {
        XpoDefault.Session = null;
        //string conn = MySqlConnectionProvider.GetConnectionString("localhost", "root", "", "CUFE");
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        XPDictionary dict = new ReflectionDictionary();
        IDataStore store = XpoDefault.GetConnectionProvider(conn, AutoCreateOption.DatabaseAndSchema);
        //DevExpress.Xpo.Metadata.ReflectionClassInfo.SuppressSuspiciousMemberInheritanceCheck = true;
        //dict.GetDataStoreSchema(typeof(Inscripcion).Assembly);
        IDataLayer dl = new ThreadSafeDataLayer(dict, store);
        //using (UnitOfWork uow = new UnitOfWork(dl))
        //{
        //    int cnt = (int)uow.Evaluate<CUFE.Models.Country>(CriteriaOperator.Parse("count"), null);
        //    if (cnt == 0)
        //    {
        //        new CUFE.Models.Country(uow) { CountryName = "Germany" };
        //        uow.CommitChanges();
        //    }
        //}
        return dl;
    }
}