using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using DX.Data.Xpo.Identity;
using DX.Data.Xpo.Identity.Persistent;
using DevExpress.Xpo;

namespace MasspackWebApi.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : XPIdentityUser<string, XpoApplicationUser>
    {
        //public ApplicationUser(XpoApplicationUser source) :
        //base(source)
        //{
        //}

        //public ApplicationUser(XpoApplicationUser source, int loadingFlags) :
        //    base(source, loadingFlags)
        //{
        //}

        //public ApplicationUser() :
        //    base()
        //{
        //}

        //public override void Assign(object source, int loadingFlags)
        //{
        //    base.Assign(source, loadingFlags);
        //    //XpoApplicationUser src = source as XpoApplicationUser;
        //    //if (src != null)
        //    //{
        //    //	// additional properties here
        //    //	this.PropertyA = src.PropertyA;
        //    //	// etc.				
        //    //}
        //}

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    [MapInheritance(MapInheritanceType.ParentTable)]
    public class XpoApplicationUser : XpoDxUser
    {
        public XpoApplicationUser(Session session) : base(session)
        {
        }
        //public override void Assign(object source, int loadingFlags)
        //{
        //    base.Assign(source, loadingFlags);
        //    //ApplicationUser src = source as ApplicationUser;
        //    //if (src != null)
        //    //{
        //    //	// additional properties here
        //    //	this.PropertyA = src.PropertyA;
        //    //	// etc.				
        //    //}
        //}
    }
    public class ApplicationRole : XPIdentityRole<XpoApplicationRole>
    {

        public ApplicationRole()
        { }
        public void Assign(object source, int loadingFlags)
        {
            //base.Assign(source, loadingFlags);
            //XpoApplicationRole src = source as XpoApplicationRole;
            //if (src != null)
            //{
            //  // additional properties here
            //  this.PropertyA = src.PropertyA;
            //  // etc.             
            //}
        }
    }
    [MapInheritance(MapInheritanceType.ParentTable)]
    public class XpoApplicationRole : XpoDxRole
    {
        public XpoApplicationRole(Session session) : base(session)
        {
        }
        public void Assign(object source, int loadingFlags)
        {
            //base.Assign(source, loadingFlags);
            //ApplicationUser src = source as ApplicationUser;
            //if (src != null)
            //{
            //  // additional properties here
            //  this.PropertyA = src.PropertyA;
            //  // etc.             
            //}
        }

        public static void AllRoles()
        {

        }
    }
    public class ApplicationDbContext
    {
        //public ApplicationDbContext()
        //    : base("DefaultConnection", throwIfV1Schema: false)
        //{
        //}

        public static DX.Data.Xpo.XpoDatabase Create()
        {
            //return new ApplicationDbContext();
            return new DX.Data.Xpo.XpoDatabase("DefaultConnection");

        }
    }
}