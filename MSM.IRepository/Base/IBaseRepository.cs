using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSM.IRepository.Base
{
    public interface IBaseRepository<TEntity, TKey>
        where TEntity:class,new()
        where TKey:struct
    {
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        Task CreateAsync(TEntity Entity);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="Entity"></param>
        void Update(TEntity Entity);

        /// <summary>
        /// 根据条件进行更新
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TEntity>> entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="Entity"></param>
        void Deleted(TEntity Entity);

        /// <summary>
        /// 根据条件进行删除
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> whereLambda);

        /// <summary>
        /// 判断指定实体是否存在
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<bool> IsExist(Expression<Func<TEntity, bool>> whereLambda);

        /// <summary>
        /// 根据条件表达式获取集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);


        /// <summary>
        /// 根据条件表达式获取集合-延迟查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> FindQueryable(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据主键值返回单条实体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(TKey ID);

        /// <summary>
        /// 根据条件表达式返回第一个对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 异步获取所有数据
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetAll();
    }
}
