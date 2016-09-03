using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolutionName.Model;

namespace SolutionName.DataLayer
{
    class ProductConfiguration: EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            Property(p => p.Code).HasMaxLength(30).IsRequired();
            Property(p => p.Name).HasMaxLength(30).IsRequired();
            Ignore(p => p.ObjectState);
            Property(p => p.RowVersion).IsRowVersion();
        }
    }
}
