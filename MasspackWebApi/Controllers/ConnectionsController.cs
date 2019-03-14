using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using BestellErfassung.DomainObjects.Tools;
using MasspackWebApi.Models;

namespace MasspackWebApi.Controllers
{
    [Authorize]
    public class ConnectionsController : ApiController
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        static Connection connection;
        public IHttpActionResult Get()
        {
            //Update();
            var item = unitOfWork.FindObject<SQLConnection>(CriteriaOperator.Parse("Oid==?", 1));
            connection = new Connection
            {
                IP = item.IP,
                Datenbank = item.Datenbank,
                Port = item.Port,
                user = item.user,
                password = item.password,
                Mitarbeiter = item.Mitarbeiter
            };
            return Ok(connection);
        }
        //public void Update()
        //{
        //    var item = unitOfWork.FindObject<SQLConnection>(CriteriaOperator.Parse("Oid==?", 1));
        //    connection = new Connection
        //    {
        //        IP = item.IP,
        //        Datenbank = item.Datenbank,
        //        Port = item.Port,
        //        user = item.user,
        //        password = item.password,
        //        Mitarbeiter = item.Mitarbeiter
        //    };
        //}
        // GET: api/Connections/5
        public IHttpActionResult Get(int id)
        {
            var item = unitOfWork.FindObject<SQLConnection>(CriteriaOperator.Parse("Oid==?", 1));
            connection = new Connection
            {
                IP = item.IP,
                Datenbank = item.Datenbank,
                Port = item.Port,
                user = item.user,
                password = item.password,
                Mitarbeiter = item.Mitarbeiter
            };
            return Ok(connection);
        }

        // POST: api/Connections
        [HttpPost]
        public IHttpActionResult Post(Connection item)
        {
            int cnt = (int)unitOfWork.Evaluate<SQLConnection>(CriteriaOperator.Parse("count"), null);
            if (cnt == 0)
            {
                new SQLConnection(unitOfWork)
                {
                    IP = item.IP,
                    Port = item.Port,
                    Aktuell = item.Aktuell,
                    Datenbank = item.Datenbank,
                    Mitarbeiter = item.Mitarbeiter,
                    user = item.user,
                    password = item.password
                };
                unitOfWork.CommitChanges();
                return Ok(item);
            }
            else
            {
                var model = unitOfWork.FindObject<SQLConnection>(CriteriaOperator.Parse("Oid==?", 1));
                model.IP = item.IP;
                model.Mitarbeiter = item.Mitarbeiter;
                model.Datenbank = item.Datenbank;
                model.user = item.user;
                model.password = item.password;
                model.Port = item.Port;
                model.Save();
                unitOfWork.CommitChanges();
                return Ok(item);
            }




        }

        // PUT: api/Connections/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Connections/5
        public void Delete(int id)
        {
        }
    }
}
