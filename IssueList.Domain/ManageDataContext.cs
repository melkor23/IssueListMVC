using IssueList.Domain.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueList.Domain
{
    /// <summary>
    /// Clase que permite instanciar y crear una unidad de trabajo
    /// </summary>
    public static class ManagerDataContext
    {

        public static IUnitOfWork GetUow()
        {
            return UnitOfWorkStore.Current;
        }

        public static void Dispose()
        {
            UnitOfWorkStore.Current.Dispose();
        }
    }
}
