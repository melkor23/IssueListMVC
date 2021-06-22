using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace IssueList.Domain.Repository.Shared
{
    internal class UnitOfWorkStore
    {
        private const string HTTPCONTEXTKEY = "Issue.HttpContext.Key";

        private static readonly Hashtable _threads = new Hashtable();

        public static void Commit()
        {
            IUnitOfWork unitOfWork = GetUnitOfWork();

            if (unitOfWork != null)
            {
                unitOfWork.Commit();
            }
        }

        public static IUnitOfWork Current
        {
            get
            {
                IUnitOfWork unitOfWork = GetUnitOfWork();

                if (unitOfWork == null)
                {
                    unitOfWork = new UnitOfWorkFactory().Create();
                    SaveUnitOfWork(unitOfWork);
                }

                return unitOfWork;
            }
        }

        private static readonly Object _lockObject = new object();

        public static void Crear(IUnitOfWork uow)
        {

            lock (_lockObject)
            {
                SaveUnitOfWork(uow);
            }

        }



        private static IUnitOfWork GetUnitOfWork()
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items.Contains(HTTPCONTEXTKEY))
                {
                    return (IUnitOfWork)HttpContext.Current.Items[HTTPCONTEXTKEY];
                }

                return null;
            }
            else
            {
                Thread thread = Thread.CurrentThread;
                if (string.IsNullOrEmpty(thread.Name))
                {
                    thread.Name = Guid.NewGuid().ToString();
                    return null;
                }
                else
                {
                    lock (_threads.SyncRoot)
                    {
                        return (IUnitOfWork)_threads[Thread.CurrentThread.Name];
                    }
                }
            }
        }

        private static void SaveUnitOfWork(IUnitOfWork unitOfWork)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Items[HTTPCONTEXTKEY] = unitOfWork;
            }
            else
            {
                Thread thread = Thread.CurrentThread;
                if (string.IsNullOrEmpty(thread.Name))
                {
                    thread.Name = Guid.NewGuid().ToString();

                }
                lock (_threads.SyncRoot)
                {
                    _threads[Thread.CurrentThread.Name] = unitOfWork;
                }
            }
        }
    }
}

