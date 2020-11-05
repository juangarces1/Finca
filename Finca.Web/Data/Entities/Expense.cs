using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finca.Web.Data.Entities
{
    public class Expense
    {
        public int Id { get; set; }

        public string Detalle { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Valor { get; set; }
    }
}
