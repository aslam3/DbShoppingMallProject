using Microsoft.EntityFrameworkCore.Migrations;

namespace DbShoppingMallProject.Data.Migrations
{
    public partial class serviceProc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Create Proc Sp_ServiceAllData
                                    As
                                    Begin
                                    Select *from Services
                                    End");
            migrationBuilder.Sql(@"Create Proc Sp_ServiceDetails
                                    @id int
                                    As
                                    Begin
                                    Select *from Services where Id=@id
                                    End");
            migrationBuilder.Sql(@"Create Proc Sp_ServiceCreate
                                    @title nvarchar(MAX) ,@descrip nvarchar(MAX),@stallId int
                                    As
                                    Begin
                                    insert into Services values(@title,@descrip,@stallId)
                                    End");
            migrationBuilder.Sql(@"Create Proc Sp_ServiceUpdate
                                    @title nvarchar(MAX) ,@descrip nvarchar(MAX),@stallId int,@id int
                                    As
                                    Begin
                                    Update Services set Title =@title,Description= @descrip,stallId= @stallId where Id=@id 
                                    End");
            migrationBuilder.Sql(@"Create Proc Sp_ServiceDelete
                                    @id int
                                    As
                                    Begin
                                    Delete from Services where Id=@id
                                    End");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
