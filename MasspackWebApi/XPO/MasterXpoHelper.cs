using BestellErfassung.DomainObjects.Tools;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Data.Filtering;

namespace MasspackWebApi.XPO
{
    public static class MasterXpoHelper
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
            var uow = XpoHelper.GetNewUnitOfWork();
            var sqlConnection = uow.FindObject<SQLConnection>(CriteriaOperator.Parse("Oid==?", 1));
            //string conn = MySqlConnectionProvider.GetConnectionString("localhost", "root", "", "CUFE");
            string sqlConnectionString = MSSqlConnectionProvider.GetConnectionString(sqlConnection.IP, sqlConnection.user, sqlConnection.password, sqlConnection.Datenbank);
            //string conn = System.Configuration.ConfigurationManager.ConnectionStrings[sqlConnectionString].ConnectionString;
            XPDictionary dict = new ReflectionDictionary();
            IDataStore store = XpoDefault.GetConnectionProvider(sqlConnectionString, AutoCreateOption.None);
            //DevExpress.Xpo.Metadata.ReflectionClassInfo.SuppressSuspiciousMemberInheritanceCheck = true;
            //dict.GetDataStoreSchema(typeof(Inscripcion).Assembly);
            //IDataLayer dl = new ThreadSafeDataLayer(dict, store);
            ////using (UnitOfWork uow = new UnitOfWork(dl))
            ////{
            ////    int cnt = (int)uow.Evaluate<CUFE.Models.Country>(CriteriaOperator.Parse("count"), null);
            ////    if (cnt == 0)
            ////    {
            ////        new CUFE.Models.Country(uow) { CountryName = "Germany" };
            ////        uow.CommitChanges();
            ////    }
            ////}
            //return dl;

           return XpoDefault.GetDataLayer(sqlConnectionString, AutoCreateOption.SchemaAlreadyExists);
        }
    }
}