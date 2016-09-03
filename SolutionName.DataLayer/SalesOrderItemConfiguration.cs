using SolutionName.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionName.DataLayer
{
    class SalesOrderItemConfiguration : EntityTypeConfiguration<SalesOrderItem>
    {
        public SalesOrderItemConfiguration()
        {
            //Property(so => so.ProductCode).HasMaxLength(15).IsRequired();
            Property(so => so.Quantity).IsRequired();
            Property(so => so.UnitPrice).IsRequired();
            Ignore(so => so.ObjectState);
            Property(so => so.RowVersion).IsRowVersion();
        }
    }
}
