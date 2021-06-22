using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueList.Domain.Model
{
    public class Entity<T>
    {
        [Key]
        public virtual T Id { get; set; }

    }
}
