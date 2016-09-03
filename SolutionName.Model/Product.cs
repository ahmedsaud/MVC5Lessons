using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionName.Model
{
    public class Product : IObjectWithState
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ObjectState ObjectState { get; set;}
        public byte[] RowVersion { get; set; }
    }
}