using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakhaTyla.Web.Protos
{
    public partial class DecimalValue
    {
        private const decimal NanoFactor = 1_000_000_000;

        public DecimalValue(long units, int nanos)
        {
            Units = units;
            Nanos = nanos;
        }

        public static implicit operator decimal(DecimalValue decimalValue) => decimalValue.ToDecimal();

        public static implicit operator DecimalValue(decimal value) => FromDecimal(value);

        public decimal ToDecimal()
        {
            return Units + Nanos / NanoFactor;
        }

        public static DecimalValue FromDecimal(decimal value)
        {
            var units = decimal.ToInt64(value);
            var nanos = decimal.ToInt32((value - units) * NanoFactor);
            return new DecimalValue(units, nanos);
        }
    }
}
