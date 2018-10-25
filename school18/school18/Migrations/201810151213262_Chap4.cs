namespace school18.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Chap4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Budget = c.Decimal(nullable: false, storeType: "money"),
                        StartDate = c.DateTime(nullable: false),
                        PersonID = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentID)
                .ForeignKey("dbo.Instructor", t => t.PersonID)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.Instructor",
                c => new
                    {
                        PersonID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        HireDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PersonID);
            
            CreateTable(
                "dbo.OfficeAssignment",
                c => new
                    {
                        PersonID = c.Int(nullable: false),
                        Location = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.PersonID)
                .ForeignKey("dbo.Instructor", t => t.PersonID)
                .Index(t => t.PersonID);

            CreateTable(
        "dbo.CourseInstructor",
        c => new
        {
            CourseID = c.Int(nullable: false),
            PersonID = c.Int(nullable: false),
        })
        .PrimaryKey(t => new { t.CourseID, t.PersonID })
        .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
        .ForeignKey("dbo.Instructor", t => t.PersonID, cascadeDelete: true)
        .Index(t => t.CourseID)
        .Index(t => t.PersonID);

            // Create  a department for course to point to.
            Sql("INSERT INTO dbo.Department (Name, Budget, StartDate) VALUES ('Temp', 0.00, GETDATE())");
            //  default value for FK points to department created above.
            AddColumn("dbo.Course", "DepartmentID", c => c.Int(nullable: false, defaultValue: 1));
            //AddColumn("dbo.Course", "DepartmentID", c => c.Int(nullable: false));

            AlterColumn("dbo.Course", "Title", c => c.String(maxLength: 50));
            AddForeignKey("dbo.Course", "DepartmentID", "dbo.Department", "DepartmentID", cascadeDelete: true);
            CreateIndex("dbo.Course", "DepartmentID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseInstructor", "PersonID", "dbo.Instructor");
            DropForeignKey("dbo.CourseInstructor", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Course", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Department", "PersonID", "dbo.Instructor");
            DropForeignKey("dbo.OfficeAssignment", "PersonID", "dbo.Instructor");
            DropIndex("dbo.CourseInstructor", new[] { "PersonID" });
            DropIndex("dbo.CourseInstructor", new[] { "CourseID" });
            DropIndex("dbo.OfficeAssignment", new[] { "PersonID" });
            DropIndex("dbo.Department", new[] { "PersonID" });
            DropIndex("dbo.Course", new[] { "DepartmentID" });
            AlterColumn("dbo.Course", "Title", c => c.String());
            DropColumn("dbo.Course", "DepartmentID");
            DropTable("dbo.CourseInstructor");
            DropTable("dbo.OfficeAssignment");
            DropTable("dbo.Instructor");
            DropTable("dbo.Department");
        }
    }
}
