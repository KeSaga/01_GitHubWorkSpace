using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EssentialTools.Models
{
    public interface IValueCalculator
    {
        decimal ValueProducts(IEnumerable<Product> products);
    }
}
