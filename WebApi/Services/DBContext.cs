using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApi.Model;

namespace WebApi.Services
{
    public class DBContext : DbContext
    {
        private DBConnectionOption _readWriteOption;
        public DBContext(IOptionsMonitor<DBConnectionOption> options)
        {
            _readWriteOption = options.CurrentValue;
        }

        public DbContext ReadWrite()
        {
            //主库  读+写
            this.Database.GetDbConnection().ConnectionString = this._readWriteOption.WriteConnection;
            return this;
        }
        public DbContext Read()
        {
            //从库
            this.Database.GetDbConnection().ConnectionString = this._readWriteOption.ReadConnection;
            return this;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._readWriteOption.WriteConnection); //默认主库
        }
        public DbSet<SysUser> SysUser { get; set; }
    }
}
