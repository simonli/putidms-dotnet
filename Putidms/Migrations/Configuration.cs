namespace Putidms.Migrations
{
    using Putidms.Domain.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Putidms.Domain.DAL.EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Putidms.Domain.DAL.EFDbContext";
        }

        protected override void Seed(Putidms.Domain.DAL.EFDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            User user = new User
            {
                UserName = "putiadmin",
                Password = "RstsJNcPqh7my6mbjtslaQ==",
                Name = "��������Ա",
                RoleType = Role.SuperAdmin,
                CreateTime = DateTime.Now,
                Email = "simonli@live.com",
                Sex = 1,
                PermissionInType = PermissionIn.Division
            };

            Division div = new Division { Name = "������ѧ��", Desc = "������Ժ�ܲ�", CreateTime = DateTime.Now };
            Department dept = new Department { Name = "������԰����ѧ��", Desc = "������԰����ѧ��", Division = div, CreateTime = DateTime.Now };

            context.Divisions.AddOrUpdate(p => p.Name, div);
            context.Departments.AddOrUpdate(p => p.Name, dept);
            context.Users.AddOrUpdate(p => p.UserName, user);
            context.SaveChanges();

        }
    }
}
