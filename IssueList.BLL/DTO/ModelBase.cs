using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IssueList.BLL.DTO
{
    /// <summary>
    /// Class for the models to make field projections
    /// </summary>
    /// <typeparam name="TEntity">Entity data ype</typeparam>
    /// <typeparam name="TDto">Data type for model</typeparam>
    [Serializable]
    public abstract class ModelBase<TEntity, TDto> 
    {

        protected ModelBase() { }

        public string Type
        {
            get
            {
                return typeof(TEntity).Name.ToString();
            }
        }

        /// <summary>
        /// Creates entity type TEntity from his model
        /// </summary>
        /// <returns></returns>
        public abstract TEntity ToEF();

        /// <summary>
        /// Expression that will allow to project the fields from the entity to the model
        /// </summary>
        protected abstract Expression<Func<TEntity, TDto>> ProyeccionExpression { get; }


       
    }
}
