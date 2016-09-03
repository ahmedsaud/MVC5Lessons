using SolutionName.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolutionName.Web.ViewModels
{
    public class ProductViewModel: IObjectWithState
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ObjectState ObjectState { get; set; }
        public byte[] RowVersion { get; set; }
    }
}