using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueList.Domain.Model.Configuration
{
    public class IssueConfiguration : EntityTypeConfiguration<Issue>
    {
        public IssueConfiguration()
    {
            Property(e => e.title).HasMaxLength(50);

            Property(e => e.description).HasMaxLength(50);
        }
}
}
