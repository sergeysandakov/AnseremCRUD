using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnseremCRUD.ViewModels
{
    public class SaleViewModel
    {
        public int SaleId { get; set; }
        public string SaleName { get; set; }
        public int CounterpartyId { get; set; }
        public int ContactId { get; set; }
        public int CityId { get; set; }

    }
}