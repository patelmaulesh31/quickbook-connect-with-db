using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QBWC_Service.Class
{
    public class QuickbookLog
    {
        public int QBStatusId { get; set; }
        public int PropertyId { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public int AccountSubTypeId { get; set; }
        public int AccountId { get; set; }
        public int InvoiceId { get; set; }
        public string Process { get; set; }
        public string Status { get; set; }
        public string StatusDes { get; set; }
        public DateTime importdate { get; set; }
        public string QuickbookListId { get; set; }
        public string QuickbookFullName { get; set; }
        public string ResponseType { get; set; }
        public DateTime JEtransactionDate { get; set; }
    }

    public class QBItemResponse
    {
        public int QBItemID { get; set; }
        public int ItemName { get; set; }
        public int QBItemListID { get; set; }
        public int Status { get; set; }
    }

    public class JournalEntry
    {
        public string DebitAccountQBListId { get; set; }
        public string DebitAccountQBFullName { get; set; }
        public string DebitAccountQBAmount { get; set; }
        public string CreditAccountQBListId { get; set; }
        public string CreditAccountQBFullName { get; set; }
        public string CreditAccountQBAmount { get; set; }
        public string Memo { get; set; }
    }

    public class WebConnectorDetails
    {
        public int QBConnectorID { get; set; }
        public int PropertyID { get; set; }
        public string PropertyCode { get; set; }
        public string PropertyName { get; set; }
        public string QBFilename { get; set; }
        public string QBFilePath { get; set; }
        public string QBUsername { get; set; }
        public string QBPassword { get; set; }
        public string FIleAppURL { get; set; }
        public string FileAppSupportURL { get; set; }
        public string FileAppDescription { get; set; }
        public string FileownerGUID { get; set; }
        public string FileIdGUID { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
    }
}