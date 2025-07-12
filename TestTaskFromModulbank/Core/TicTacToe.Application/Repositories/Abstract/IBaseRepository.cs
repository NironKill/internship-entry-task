using System.Linq.Expressions;

namespace TicTacToe.Application.Repositories.Abstract
{
    public interface IBaseRepository<TEntity, TEntityCreateDTO, TEntityGetDTO>
    {
        Task<Guid> Create(TEntityCreateDTO dto, Func<TEntityCreateDTO, TEntity> map, CancellationToken cancellationToken);
        Task<bool> Delete(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<TEntityGetDTO> Update(Expression<Func<TEntity, bool>> predicate, Action<TEntity> update, Func<TEntity, TEntityGetDTO> map, CancellationToken cancellationToken);

        Task<TEntityGetDTO> Get(Expression<Func<TEntity, bool>> predicate, Func<TEntity, TEntityGetDTO> map, CancellationToken cancellationToken);
        Task<ICollection<TEntityGetDTO>> GetAll(Func<TEntity, TEntityGetDTO> map, CancellationToken cancellationToken);
        Task<ICollection<TEntityGetDTO>> GetAll(Expression<Func<TEntity, bool>> predicate, Func<TEntity, TEntityGetDTO> map, CancellationToken cancellationToken);
    }
}
