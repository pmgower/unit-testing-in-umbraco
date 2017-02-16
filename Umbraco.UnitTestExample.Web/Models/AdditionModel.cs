using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Umbraco.UnitTestExample.Web.Controllers
{
    public class AdditionModel
    {
        public bool IsPosted { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Sum { get; set; }
    }
}