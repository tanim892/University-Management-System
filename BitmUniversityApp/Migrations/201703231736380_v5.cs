namespace BitmUniversityApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassRoomAllocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        DayId = c.Int(nullable: false),
                        StartTime = c.Double(nullable: false),
                        EndTime = c.Double(nullable: false),
                        RoomStatus = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.Days", t => t.DayId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .Index(t => t.CourseId)
                .Index(t => t.DayId)
                .Index(t => t.DepartmentId)
                .Index(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassRoomAllocations", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.ClassRoomAllocations", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ClassRoomAllocations", "DayId", "dbo.Days");
            DropForeignKey("dbo.ClassRoomAllocations", "CourseId", "dbo.Courses");
            DropIndex("dbo.ClassRoomAllocations", new[] { "RoomId" });
            DropIndex("dbo.ClassRoomAllocations", new[] { "DepartmentId" });
            DropIndex("dbo.ClassRoomAllocations", new[] { "DayId" });
            DropIndex("dbo.ClassRoomAllocations", new[] { "CourseId" });
            DropTable("dbo.ClassRoomAllocations");
        }
    }
}
