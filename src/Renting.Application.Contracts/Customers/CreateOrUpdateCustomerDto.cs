﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Renting.Customers
{
    public class CreateOrUpdateCustomerDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
