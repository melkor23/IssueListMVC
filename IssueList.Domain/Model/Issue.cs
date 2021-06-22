using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using IssueList.Infraestructura.Enum;

namespace IssueList.Domain.Model
{
    public class Issue : Entity<int>
    {
        [Key]
        [Column("IssueId")]
        public override int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public SeverityEnum severity { get; set; }
        public StatusEnum status { get; set; }

        public int asignee { get; set; }
    }
}
