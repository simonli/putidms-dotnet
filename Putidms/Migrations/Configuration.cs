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
                Name = "超级管理员",
                RoleType = Role.SuperAdmin,
                CreateTime = DateTime.Now,
                Email = "simonli@live.com",
                Sex = 1,
                PermissionInType = PermissionIn.Division
            };

            Division div = new Division { Name = "苏州修学处", Desc = "菩提书院总部", CreateTime = DateTime.Now };
            Department dept = new Department { Name = "苏州西园寺修学点", Desc = "苏州西园寺修学点", Division = div, CreateTime = DateTime.Now };

            context.Divisions.AddOrUpdate(p => p.Name, div);
            context.Departments.AddOrUpdate(p => p.Name, dept);
            context.Users.AddOrUpdate(p => p.UserName, user);
            context.SaveChanges();

        }
    }
}
