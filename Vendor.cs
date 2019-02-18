using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace QBWC_Service.Class
{
    public class Vendor
    {
        public string ListId { get; set; }
        public string FullName { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string CompanyName { get; set; }
        public string AccountNumber { get; set; }
        public string VendorEmail { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string VendorContact { get; set; }
        public string TaxID { get; set; }
        public string SSN { get; set; }
        public string VendorNumber { get; set; }
        public string Comments { get; set; }
        public DateTime CreateDate { get; set; }
        public string IsActive { get; set; }
        public string Fax { get; set; }
    }
}