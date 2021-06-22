using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IssueList.Domain.Repository.Shared
{
    /// <summary>
    /// Genereric repository class
    /// </summary>
    public class BaseRepository
    {
        protected IUnitOfWork UoW { get; set; }

        protected DbContext Context
        {
            get
            {
                return (DbContext)UoW.Context;
            }
        }

        static readonly Dictionary<Type, List<PropertyInfo>> _cacheKeyValues = new Dictionary<Type, List<PropertyInfo>>();

        protected BaseRepository(IUnitOfWork uow)
        {
            UoW = uow;
        }

        public static object[] GetKeyValues<T>(T entity) where T : class
        {
            var keyProperties = GetKeyProperties<T>();
            var keyValues = new object[keyProperties.Count];
            int indice = 0;
            foreach (var key in keyProperties)
            {
                keyValues[indice] = keyProperties[indice].GetValue(entity, null);
                indice++;
            }
            return keyValues;
        }

        public static List<PropertyInfo> GetKeyProperties<T>() where T : class
        {
            if (!_cacheKeyValues.ContainsKey(typeof(T)))
            {
                var keyProperties = typeof(T).GetProperties()
                    .Where(p => Attribute.GetCustomAttribute(p, typeof(KeyAttribute), true) != null)
                    .OrderBy(p => ((ColumnAttribute)Attribute.GetCustomAttribute(p, typeof(ColumnAttribute), true))?.Order ?? int.MaxValue)
                    .ToList();
                _cacheKeyValues[typeof(T)] = keyProperties;
            }
            return _cacheKeyValues[typeof(T)];
        }
    }
}
