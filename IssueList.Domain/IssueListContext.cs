using IssueList.Domain.Model;
using IssueList.Domain.Model.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueList.Domain
{
    public partial class IssueListContext : DbContext
    {
        public IssueListContext() : base("name=DefaultConnection")
        {
            
        }

        //Models
        public virtual DbSet<Issue> Issue { get; set; }

        //configurations
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //removes default cascade deletion on relations
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Configuration
            modelBuilder.Configurations.Add(new IssueConfiguration());
            
        }
    }
}
