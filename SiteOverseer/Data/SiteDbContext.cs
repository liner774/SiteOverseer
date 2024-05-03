using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SiteOverseer.Models;
namespace SiteOverseer.Data
{
    public class SiteDbContext : DbContext
    {
        public SiteDbContext(DbContextOptions<SiteDbContext> options) : base(options) { }

        public DbSet<Company> MS_Company { get; set; }
        public DbSet<Menugp> MS_Menugp { get; set; }
        public DbSet<City> MS_City { get; set; }
        public DbSet<Menuaccess> MS_Menuaccess { get; set; }
        public DbSet<User> MS_User { get; set; }
        public DbSet<FacilityType> MS_Facilitytype { get; set; }
        public DbSet<Contractor> MS_Contractor { get; set; }
        public DbSet<Facility> MS_Facility { get; set; }
        public DbSet<FacilityTask> MS_Facilitytask { get; set; }
        public DbSet<Wbs> MS_Wbs { get; set; }
        public DbSet<WbsDetail> MS_Wbsdetail { get; set; }
        public DbSet<TranTypeCode> MS_Trantypecode { get; set; }
        public DbSet<TranTypeReason> MS_Trantypereason { get; set; }
        public DbSet<FacilityProgress> PMS_Facilityprogress { get; set; }
        public DbSet<FacilityProgressDocument> PMS_Facilityprogressdocument { get; set; }
        public DbSet<FacilityTranLog> PMS_Facilitytranlog { get; set; }



    }
}