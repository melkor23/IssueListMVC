using IssueList.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueList.BLL.BLL
{
    public interface ICrud<T> where T : Entity<int>
    {
        void Create(T entity);

        void Update(T entity);

        void Delete(object id);

    }
}
