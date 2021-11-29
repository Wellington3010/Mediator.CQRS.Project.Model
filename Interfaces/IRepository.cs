using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mediator_cqrs_project.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> FindAll();
        Task<IEnumerable<T>> FindByType(int code);
        Task<T> FindByCode(string code);

        Task Save(T data);

        Task Update(T data);

        Task Delete(T data);
    }
}
