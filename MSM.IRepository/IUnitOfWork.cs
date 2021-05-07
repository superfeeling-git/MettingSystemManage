using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSM.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 提交所有更改
        /// </summary>
        Task<int> SaveChangesAsync();
    }
}
