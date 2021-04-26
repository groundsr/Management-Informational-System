using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.Model
{
    public class Document
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime AddedDate { get; set; }
        public string Path { get; set; }

    }
}
