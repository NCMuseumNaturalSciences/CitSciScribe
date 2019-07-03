using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using CitSciScribe.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CitSciScribe.Migrations
{
    //Package Manager Console - 
    //Add-Migration <Name>
    //Update-Database

    internal sealed class Configuration : DbMigrationsConfiguration<DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        private void SaveChanges(DBContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb, ex
                ); // Add the original exception as the innerException
            }
        }

        protected override void Seed(DBContext context)
        {
            var manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(
                    new DBContext()));
            var transcriptionContext = new DBContext();
            if (!transcriptionContext.Database.Exists() ||
                transcriptionContext.Users.Count(c => c.UserName == "ben.norton@naturalsciences.org") == 0)
            {
                var approverUser = new ApplicationUser
                {
                    Id = "6a99e602-5168-48c1-bed5-d4dbcd017d4b",
                    UserName = "approver@demo.com",
                    IsCollectionManager = true,
                    IsSiteAdmin = true,
                    Email = "approver@demo.com",
                    CanTranscribe = true,
                    EmailConfirmed = true,
                    CanApprove = true
                };
                manager.Create(approverUser, "approver123");

                var publicUser = new ApplicationUser
                {
                    Id = "6a99e602-5168-48c1-bed5-d4dbcd017d5b",
                    UserName = "user@demo.com",
                    IsCollectionManager = false,
                    IsSiteAdmin = false,
                    Email = "user@demo.com",
                    CanTranscribe = true,
                    EmailConfirmed = true,
                    CanApprove = false
                };
                manager.Create(publicUser, "q1w2e3r4!");
                SaveChanges(transcriptionContext);
            }
        }
    }
}