namespace Login.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addusermodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_m_role",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tb_m_user",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Role_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tb_m_role", t => t.Role_id)
                .Index(t => t.Role_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_m_user", "Role_id", "dbo.tb_m_role");
            DropIndex("dbo.tb_m_user", new[] { "Role_id" });
            DropTable("dbo.tb_m_user");
            DropTable("dbo.tb_m_role");
        }
    }
}
