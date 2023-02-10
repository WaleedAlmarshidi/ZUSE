using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Configuration;
using ZUSE.Shared.Models;
namespace ZUSE.Server.Data
{
    public class ZUSE_dbContext : DbContext
    {
        //public tathkara_dbContext()
        //{
        //}
        public ZUSE_dbContext(DbContextOptions<ZUSE_dbContext> options)
            : base(options)
        { }
        public DbSet<Session> sessions { get; set; } = null!;
        public DbSet<ZUSEClient> ZUSEClients { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            //"Server=tcp:tathkaraserver.database.windows.net,1433;Initial Catalog=tathkara_db;Persist Security Info=False;User ID=waleed;Password=TathkaradbTat2000%;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
////            dotnet user-secrets init
////dotnet user - secrets set ConnectionStrings:Chinook "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chinook"
////            if (!optionsBuilder.IsConfigured)
////            {
////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
////                optionsBuilder.UseSqlServer(ConfigurationManager.("default"));
////            }
//        }
        // we override the OnModelCreating method here.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>().HasIndex(
                    session => new
                    {
                        session.id,
                        session.business_reference
                    }

                );
            modelBuilder.Entity<ZUSEClient>().HasKey(serviceProvider => new
            {
                serviceProvider.reference_id,
                serviceProvider.branch_id
            });
            modelBuilder.Entity<Session>().HasKey(session => new
            {
                session.id,
                session.business_reference
            });
        }
    }
}
