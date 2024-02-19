using Microsoft.AspNetCore.Mvc;

namespace TP4.Models.Repository
{
    public interface IDataRepository<TEntity>
    {
        ActionResult<IEnumerable<TEntity>> GetAll();
        ActionResult<TEntity> GetById(int id);
        Task<ActionResult<TEntity>> GetByStringAsync(string str);
        void Add(TEntity entity);
        void Update(TEntity entityToUpdate, TEntity entity);
        void Delete(TEntity entity);
    }
}
