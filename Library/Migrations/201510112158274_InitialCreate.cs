namespace Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        author = c.String(nullable: false),
                        publisher = c.String(nullable: false),
                        publicationYear = c.Int(nullable: false),
                        language = c.String(nullable: false, maxLength: 2),
                        series = c.String(),
                        number = c.String(),
                        summery = c.String(),
                        category = c.String(),
                        copies = c.Int(nullable: false),
                        picture = c.Binary(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Borrowers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        firstName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        sex = c.String(nullable: false, maxLength: 1),
                        phone = c.String(nullable: false),
                        address = c.String(nullable: false),
                        mail = c.String(),
                        userId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.userId)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        password = c.String(),
                        isAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Borrows",
                c => new
                    {
                        seqNumber = c.Int(nullable: false, identity: true),
                        bookId = c.Int(nullable: false),
                        borrowerId = c.Int(nullable: false),
                        borrowDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.seqNumber)
                .ForeignKey("dbo.Books", t => t.bookId, cascadeDelete: true)
                .ForeignKey("dbo.Borrowers", t => t.borrowerId, cascadeDelete: true)
                .Index(t => t.bookId)
                .Index(t => t.borrowerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Borrows", "borrowerId", "dbo.Borrowers");
            DropForeignKey("dbo.Borrows", "bookId", "dbo.Books");
            DropForeignKey("dbo.Borrowers", "userId", "dbo.Users");
            DropIndex("dbo.Borrows", new[] { "borrowerId" });
            DropIndex("dbo.Borrows", new[] { "bookId" });
            DropIndex("dbo.Borrowers", new[] { "userId" });
            DropTable("dbo.Borrows");
            DropTable("dbo.Users");
            DropTable("dbo.Borrowers");
            DropTable("dbo.Books");
        }
    }
}
