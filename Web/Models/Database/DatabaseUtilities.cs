using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;


namespace Roomie.Web.Helpers
{
    public static class DatabaseUtilities
    {
        public static ObjectContext GetObjectContext(this DbContext context)
        {
            // from http://social.msdn.microsoft.com/Forums/en-US/adonetefx/thread/0c1f8425-55f4-4600-9605-a925220d5a25/
            return ((IObjectContextAdapter)context).ObjectContext;
        }

        public static void Reset(this DbContext context)
        {
            context.Clear();
            context.CreateTables();
        }

        public static void Clear(this DbContext context)
        {
            int triesLeft = 5;
            var database = context.Database;
            // this method uses snippits from http://stackoverflow.com/questions/4031431/entity-framework-ctp-4-code-first-custom-database-initializer
            try
            {
                //delete all constraints
                database.ExecuteSqlCommand(
                    @"select  
                'ALTER TABLE ' + so.table_name + ' DROP CONSTRAINT ' + so.constraint_name  
                from INFORMATION_SCHEMA.TABLE_CONSTRAINTS so"
                );

                //delete all tables
                database.ExecuteSqlCommand(
                    @"declare @cmd varchar(4000)
                declare cmds cursor for 
                Select
                    'drop table [' + Table_Name + ']'
                From
                    INFORMATION_SCHEMA.TABLES

                open cmds
                while 1=1
                begin
                    fetch cmds into @cmd
                    if @@fetch_status != 0 break
                    print @cmd
                    exec(@cmd)
                end
                close cmds
                deallocate cmds"
                );
            }
            catch (Exception exception)
            {
                triesLeft--;
                if (triesLeft <= 0 || !exception.Message.Contains("rop"))
                    throw;
            }

        }

        public static void CreateTables(this DbContext context)
        {
            var script = context.GetObjectContext().CreateDatabaseScript();
            context.Database.ExecuteSqlCommand(script);
        }

        public static void Seed(this RoomieDatabaseContext context)
        {
            //TODO: fill in with good, general user stuff
        }
    }
}
