namespace school18.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Course",
                c => new
                {
                    CourseID = c.Int(nullable: false),
                    Title = c.String(),
                    Credits = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.CourseID);

            CreateTable(
                "dbo.Enrollment",
                c => new
                {
                    EnrollmentID = c.Int(nullable: false, identity: true),
                    CourseID = c.Int(nullable: false),
                    PersonID = c.Int(nullable: false),
                    Grade = c.Int(),
                })
                .PrimaryKey(t => t.EnrollmentID)
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.PersonID);

            CreateTable(
                "dbo.Student",
                c => new
                {
                    PersonID = c.Int(nullable: false, identity: true),
                    LastName = c.String(),
                    FirstMidName = c.String(),
                    EnrollmentDate = c.DateTime(nullable: false),
                    ImgUrl = c.String(),
                })
                .PrimaryKey(t => t.PersonID);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Enrollment", "PersonID", "dbo.Student");
            DropForeignKey("dbo.Enrollment", "CourseID", "dbo.Course");
            DropIndex("dbo.Enrollment", new[] { "PersonID" });
            DropIndex("dbo.Enrollment", new[] { "CourseID" });
            DropTable("dbo.Student");
            DropTable("dbo.Enrollment");
            DropTable("dbo.Course");
        }
    }
}
