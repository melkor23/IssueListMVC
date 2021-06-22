using Fleck;
using IssueList.Domain.Model;
using IssueList.Infraestructura.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace IssueList.BLL.DTO
{
    public class IssueModel : ModelBase<Issue, IssueModel>
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public SeverityEnum severity { get; set; }
        public StatusEnum status { get; set; }

        public int asignee { get; set; }


        protected override Expression<Func<Issue, IssueModel>> ProyeccionExpression => ExpressionSelector;

        public static Expression<Func<Issue, IssueModel>> ToModelConverterExpression
            => ExpressionSelector;

        public static readonly Expression<Func<Issue, IssueModel>> ExpressionSelector = i =>
            new IssueModel
            {
                id = i.Id,
                title = i.title,
                description = i.description,
                severity=i.severity,
                status=i.status,
                asignee= i.asignee
            };

        public override Issue ToEF()
        {
            Issue i = new Issue();

            id = i.Id;
            title = i.title;
            description = i.description;
            severity = i.severity;
            status = i.status;
            asignee = i.asignee;

            return i;
        }
    }
}
