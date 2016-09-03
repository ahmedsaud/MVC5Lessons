using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SolutionName.Model;
using SolutionName.Web.ViewModels;
using SolutionName.DataLayer;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace SolutionName.Web
{
    public class SalesController : Controller
    {
        private SalesContext salesContext = new SalesContext();

        public ActionResult Index()
        {
            return View(salesContext.SalesOrders.ToList());
        }

        public ActionResult Details(string code)
        {
            if (code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            SalesOrder salesorder = salesContext.SalesOrders.Where<SalesOrder>(so => so.Code == code).FirstOrDefault<SalesOrder>();

            if (salesorder == null)
            {
                return HttpNotFound();
            }

            SalesOrderViewModel salesOrderViewModel = SolutionName.Web.ViewModels.Helpers.createFromSalesOrder(salesorder);
            salesOrderViewModel.MessageToClient = "I originated from the view model, rather than the model";

            return View(salesOrderViewModel);
        }

        public ActionResult Create()
        {
            SalesOrderViewModel salesOrderViewModel = new SalesOrderViewModel();
            salesOrderViewModel.ObjectState = ObjectState.Added;
            return View(salesOrderViewModel);
        }

        public ActionResult Edit(string code)
        {
            if (code == string.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SalesOrder salesorder = salesContext.SalesOrders
                .Where(b => b.Code == code)
                .FirstOrDefault<SalesOrder>();

            if (salesorder == null)
            {
                return HttpNotFound();
            }

            SalesOrderViewModel salesOrderViewModel = SolutionName.Web.ViewModels.Helpers.createFromSalesOrder(salesorder);
            //salesOrderViewModel.products = this.GetAllProducts();
            /*
            foreach (var salesOrderItem in salesOrderViewModel.SalesOrderItems)
            {
                //salesOrderItem.ChosenItem = ViewModels.Helpers.createProductViewModelFromProduct(salesContext.Products.Find(salesOrderItem.ProductId));
                salesOrderItem.ChosenItemId = salesOrderItem.ProductId;
            }
             * */
            salesOrderViewModel.MessageToClient = ViewModels.Helpers.getMessageToClient(salesOrderViewModel.ObjectState, salesOrderViewModel.CustomerName);

            return View(salesOrderViewModel);
        }

        public List<ProductViewModel> GetAllProducts_List()
        {
            List<Product>           products = salesContext.Products.ToList<Product>();
            List<ProductViewModel>  productViewModels = new List<ProductViewModel>();
            ProductViewModel        productViewModel;

            foreach (var product in products)
            {
                productViewModel = ViewModels.Helpers.createProductViewModelFromProduct(product);
                productViewModels.Add(productViewModel);
            }

            return productViewModels;
        }

        public JsonResult GetAllProducts(string code)
        {
            List<Product>           products;
            List<ProductViewModel>  productViewModels;
            Product                 product;
            ProductViewModel        productViewModel;

            if (code == string.Empty)
            {
                
                products = salesContext.Products.ToList<Product>();
                productViewModels = new List<ProductViewModel>();

                foreach (var item in products)
                {
                    productViewModel = ViewModels.Helpers.createProductViewModelFromProduct(item);
                    productViewModels.Add(productViewModel);
                }

                return Json(new { productViewModels }, JsonRequestBehavior.AllowGet);
            }

            product = salesContext.Products
                .Where(pr => pr.Code == code.ToString())
               .FirstOrDefault<Product>();
            
            productViewModel = ViewModels.Helpers.createProductViewModelFromProduct(product);

            return Json(new { productViewModel }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string code)
        {
            if (code == string.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SalesOrder salesorder = salesContext.SalesOrders.Where(so => so.Code == code.ToString())
               .FirstOrDefault<SalesOrder>();

            if (salesorder == null)
            {
                return HttpNotFound();
            }

            SalesOrderViewModel salesOrderViewModel = SolutionName.Web.ViewModels.Helpers.createFromSalesOrder(salesorder);
            salesOrderViewModel.MessageToClient = ViewModels.Helpers.getMessageToClient(salesOrderViewModel.ObjectState, salesOrderViewModel.CustomerName);
            salesOrderViewModel.ObjectState = ObjectState.Deleted;

            return View(salesOrderViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                salesContext.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult Save(SalesOrderViewModel salesOrderViewModel)
        {
            try
            {
                SalesOrder salesOrder = SolutionName.Web.ViewModels.Helpers.createFromSalesOrderModelView(salesOrderViewModel);
                salesContext.SalesOrders.Attach(salesOrder);

                if (salesOrderViewModel.ObjectState == ObjectState.Deleted)
                {
                    foreach (SalesOrderItemViewModel salesOrderItemViewModel in salesOrderViewModel.SalesOrderItems)
                    {
                        SalesOrderItem salesOrderItem = salesContext.SalesOrderItems
                            .Where(soi => soi.Code == salesOrderItemViewModel.Code)
                            .FirstOrDefault<SalesOrderItem>();

                        if (salesOrderItem != null)
                        {
                            salesOrderItem.ObjectState = ObjectState.Deleted;
                        }
                    }
                }
                else
                {
                    foreach (string salesOrderItemCode in salesOrderViewModel.SalesOrderItemsToDelete)
                    {
                        SalesOrderItem salesOrderItem = salesContext.SalesOrderItems
                            .Where(soi => soi.Code == salesOrderItemCode)
                            .FirstOrDefault<SalesOrderItem>();

                        if (salesOrderItem != null)
                        {
                            salesOrderItem.ObjectState = ObjectState.Deleted;
                        }
                    }
                }
                salesContext.ApplyChanges();
                //salesContext.ChangeTracker.Entries<IObjectWithState>().Single().State = SolutionName.DataLayer.Helpers.ConvertState(salesOrderViewModel.ObjectState);
                salesContext.SaveChanges();

                if (salesOrder.ObjectState == ObjectState.Deleted)
                {
                    return Json(new { newLocation = "/Sales/Index/" });
                }

                string messageToClient = ViewModels.Helpers.getMessageToClient(salesOrder.ObjectState, salesOrder.CustomerName);
                salesOrderViewModel = ViewModels.Helpers.createFromSalesOrder(salesOrder);
                salesOrderViewModel.MessageToClient = messageToClient;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        salesOrderViewModel.MessageToClient = string.Format
                            ("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw;
            }
            
            return Json(new { salesOrderViewModel });
        }
    }
}