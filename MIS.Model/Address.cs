using System;
using System.Collections.Generic;
using System.Text;

namespace MSI.Model
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
    }
}
