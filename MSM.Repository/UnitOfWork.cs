using MSM.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MSM.Model;

namespace MSM.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly MsmDbContext Context;
        public UnitOfWork(MsmDbContext _context)
        {
            Context = _context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (Context == null) return;

            Context.Dispose();
        }

        /// <summary>
        /// 提交所有更新
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }
}
