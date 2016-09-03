using SolutionName.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolutionName.Web.ViewModels
{
    public static class Helpers
    {
        public static SalesOrderViewModel createFromSalesOrder(SalesOrder _salesOrder)
        {
            SalesOrderViewModel salesOrderViewModel = new SalesOrderViewModel();
            salesOrderViewModel.SalesOrderId        = _salesOrder.SalesOrderId;
            salesOrderViewModel.CustomerName        = _salesOrder.CustomerName;
            salesOrderViewModel.PONumber            = _salesOrder.PONumber;
            salesOrderViewModel.ObjectState         = ObjectState.Unchanged;
            salesOrderViewModel.RowVersion          = _salesOrder.RowVersion;

            foreach (SalesOrderItem salesOrderItem in _salesOrder.SalesOrderItems)
            {
                SalesOrderItemViewModel salesOrderItemViewModel = new SalesOrderItemViewModel();
                salesOrderItemViewModel.SalesOrderItemId        = salesOrderItem.SalesOrderItemId;
                salesOrderItemViewModel.ProductId               = salesOrderItem.ProductId;
                salesOrderItemViewModel.Quantity                = salesOrderItem.Quantity;
                salesOrderItemViewModel.UnitPrice               = salesOrderItem.UnitPrice;
                salesOrderItemViewModel.ObjectState             = ObjectState.Unchanged;
                salesOrderItemViewModel.SalesOrderId            = salesOrderItem.SalesOrderId;
                salesOrderItemViewModel.RowVersion              = salesOrderItem.RowVersion;

                salesOrderViewModel.SalesOrderItems.Add(salesOrderItemViewModel);
            }

            return salesOrderViewModel;
        }

        public static SalesOrder createFromSalesOrderModelView(SalesOrderViewModel _salesOrderModelView)
        {
            SalesOrder salesOrder   = new SalesOrder();
            salesOrder.SalesOrderId = _salesOrderModelView.SalesOrderId;
            salesOrder.CustomerName = _salesOrderModelView.CustomerName;
            salesOrder.PONumber     = _salesOrderModelView.PONumber;
            salesOrder.ObjectState  = _salesOrderModelView.ObjectState;
            salesOrder.RowVersion   = _salesOrderModelView.RowVersion;
            int temporarySalesOrderItemId = -1; 

            foreach (SalesOrderItemViewModel salesOrderItemViewModel in _salesOrderModelView.SalesOrderItems)
            {
                SalesOrderItem salesOrderItem   = new SalesOrderItem();
                salesOrderItem.SalesOrderItemId = salesOrderItemViewModel.SalesOrderItemId;
                salesOrderItem.ProductId        = salesOrderItemViewModel.ProductId;
                salesOrderItem.Quantity         = salesOrderItemViewModel.Quantity;
                salesOrderItem.UnitPrice        = salesOrderItemViewModel.UnitPrice;
                salesOrderItem.ObjectState      = salesOrderItemViewModel.ObjectState;
                salesOrderItem.SalesOrderId     = _salesOrderModelView.SalesOrderId;
                salesOrderItem.RowVersion       = salesOrderItemViewModel.RowVersion;
                
                if (salesOrderItem.ObjectState == ObjectState.Added)
                {
                    salesOrderItem.SalesOrderItemId = temporarySalesOrderItemId;
                    temporarySalesOrderItemId--;
                }

                salesOrder.SalesOrderItems.Add(salesOrderItem);
            }

            return salesOrder;
        }

        public static ProductViewModel createProductViewModelFromProduct(Product _product)
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Code = _product.Code;
            productViewModel.Name = _product.Name;
            productViewModel.ObjectState = _product.ObjectState;
            productViewModel.ProductId = _product.ProductId;
            productViewModel.RowVersion = _product.RowVersion;

            return productViewModel;
        }
        
        public static string getMessageToClient(ObjectState _objectState, string _customerName)
        {
            string messageToClient = string.Empty;

            switch (_objectState)
            {
                case ObjectState.Added:
                    {
                        messageToClient = string.Format("A sales order for {0} has been added to the database.", _customerName);
                    }
                    break;
                case ObjectState.Modified:
                    {
                        messageToClient = string.Format("The customer name for the sales order has been updated to {0} in the database", _customerName);
                    }
                    break;
                case ObjectState.Deleted:
                    {
                        messageToClient = "You are about to permanently deleting this sales order";
                    }
                    break;
                default:
                    messageToClient = string.Format("The orignal value of Customer Name is {0}", _customerName);
                    break;
            }

            return messageToClient;
        }
    }
}