using Microsoft.EntityFrameworkCore;

namespace WebApi.Services
{
    public static class DbContextExtend
    {
        /// <summary>
        /// 只读
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static DbContext Read(this DbContext dbContext)
        {
            if (dbContext is DBContext)
            {
                return ((DBContext)dbContext).Read();
            }
            else
                throw new Exception();
        }
        /// <summary>
        /// 读写
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static DbContext ReadWrite(this DbContext dbContext)
        {
            if (dbContext is DBContext)
            {
                return ((DBContext)dbContext).ReadWrite();
            }
            else
                throw new Exception();
        }
    }
}
