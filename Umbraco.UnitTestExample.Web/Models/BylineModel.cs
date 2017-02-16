using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Umbraco.UnitTestExample.Web.Controllers
{
    public class BylineModel
    {
        public string Author { get; }
        public DateTime? Date { get; }

        public BylineModel(string author, DateTime? date)
        {
            Author = author;
            Date = date;
        }
    }
}