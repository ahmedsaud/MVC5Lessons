using SolutionName.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolutionName.Web.ViewModels
{
    public class SalesOrderViewModel : IObjectWithState
    {
        public SalesOrderViewModel()
        {
            SalesOrderItems = new List<SalesOrderItemViewModel>();
            SalesOrderItemsToDelete = new List<string>();
            //products = new List<ProductViewModel>();
        }

        public int SalesOrderId { get; set; }
        public string Code { get; set; }
        public string CustomerName { get; set; }
        public string PONumber { get; set; }
        public string MessageToClient { get; set; }
        public ObjectState ObjectState{ get; set; }
        public List<SalesOrderItemViewModel> SalesOrderItems { get; set; }
        public List<string> SalesOrderItemsToDelete { get; set; }
        public byte[] RowVersion { get; set; }
        //public List<ProductViewModel> products;
    }
}