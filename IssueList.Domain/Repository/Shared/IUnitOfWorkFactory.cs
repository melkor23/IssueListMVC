using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueList.Domain.Repository.Shared
{

    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();

    }
}
