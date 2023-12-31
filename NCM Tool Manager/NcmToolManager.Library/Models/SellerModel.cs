﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NcmToolManager.Library.Models
{
    public class SellerModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public SellerModel() { }
        public SellerModel(string name)
        {
            Name = name;
        }
        public SellerModel(string name, string address, string city, string postalCode, string country)
        {
            Name = name;
            Address = address;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }
    }
}
