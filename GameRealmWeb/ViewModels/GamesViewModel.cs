using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameRealmWeb.ViewModels
{
    public class GamesViewModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? Release { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public int? Quantity { get; set; }
        [DataType(DataType.ImageUrl)]
        public string image { get; set; }
    }
}
