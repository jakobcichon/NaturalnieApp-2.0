using NaturalnieApp2.Models;
using NaturalnieApp2.Services.Database.Providers;
using NaturalnieApp2.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Interfaces.Database
{
    internal interface IProductProvider
    {
        public ProductModel GetProductFromProductDTO(ProductDTO productDTO);
    }
}
