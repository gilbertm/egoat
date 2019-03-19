using System;
using System.Threading.Tasks;


namespace eGoatDDD.Persistence.Repository
{

    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : class;

        Task<int> Commit();

        void Rollback();
    }

}
