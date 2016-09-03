using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionName.Model
{
    public class SalesOrderItem : IObjectWithState
    {
        public int SalesOrderItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }
        public Product Product { get; set; }
        public ObjectState ObjectState { get; set; }
        public string Code { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
