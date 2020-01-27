namespace Login.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addemailonusermodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_m_user", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_m_user", "Email");
        }
    }
}
