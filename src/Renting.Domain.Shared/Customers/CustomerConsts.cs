using System;
using System.Collections.Generic;
using System.Text;

namespace Renting.Customers
{
    public class CustomerConsts
    {
        public const int MaxNameLength = 50;
        public const int MinNameLength = 3;
        public const string EmailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        public const string PhoneRegex = @"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$";
    }
}
