using SolutionName.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolutionName.Web.ViewModels
{
    public class SalesOrderItemViewModel : IObjectWithState
    {
        public int SalesOrderItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; }
        public decimal UnitPrice { get; set; }
        public int SalesOrderId { get; set; }
        public ObjectState ObjectState { get; set; }
        public byte[] RowVersion { get; set; }
        public ProductViewModel ChosenItem { get; set; }
        public int ChosenItemId { get; set; }
    }
}