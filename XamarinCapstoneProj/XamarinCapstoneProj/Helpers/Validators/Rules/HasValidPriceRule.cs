using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinCapstoneProj.Helpers.Validators.Rules
{
    public class HasValidPriceRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value is Decimal price)
            {
                //Decimal today = Decimal.Today;
                //int age = today.Year - bday.Year;
                Decimal maxPrice = 300.00M;
                Decimal minPrice = 1.00M;
                return (minPrice <= price && price <= maxPrice);
            }

            return false;
        }
    }
}
