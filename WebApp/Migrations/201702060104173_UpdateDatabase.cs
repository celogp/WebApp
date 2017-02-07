namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        email = c.String(maxLength: 100),
                        namePerson = c.String(maxLength: 100),
                        Skype = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 15),
                        LinkeDin = c.String(maxLength: 100),
                        State = c.String(maxLength: 10),
                        City = c.String(maxLength: 20),
                        Portifolio = c.String(maxLength: 100),
                        HoursWork = c.Int(nullable: false),
                        TimeWork = c.Int(nullable: false),
                        SalaryHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NameBank = c.String(),
                        linkTeste = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.People");
            DropTable("dbo.Person");
        }
    }
}
