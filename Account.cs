using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QBWC_Service.Class
{
    public class Account
    {
        public string ListId { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public int AccountTypeId { get; set; }
        public int AccountSubTypeId { get; set; }
        public string Desc { get; set; }
        public string Status { get; set; }
        public string IsActive { get; set; }
        public string AccountNumber { get; set; }
    }
    public class Item
    {
        public string ListId { get; set; }
        public string ItemName { get; set; }
        public string Status { get; set; }
        public int Qty { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
    }

    public class GlAccount
    {
        public string ListId { get; set; }
        public string ItemName { get; set; }
        public string Status { get; set; }
        public int Qty { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
    }
    public class Bill
    {
        public string ListID { get; set; }
        public string TxnID { get; set; }
        public string TxnDate { get; set; }
        public string DueDate { get; set; }
        public string AmountDue { get; set; }
        public string IsPaid { get; set; }
        public int VendorId { get; set; }
        public string VendorListID { get; set; }
        public string VendorName { get; set; }
        public string RefNumber { get; set; }
        public string Memo { get; set; }
        public string VendorRef { get; set; }
        public string PaymentDate { get; set; }
        public int GLAccountId { get; set; }
        public int InvoiceType { get; set; }
        public string CC_GL_AccId_ListID { get; set; }
        public int CC_GL_AccId { get; set; }
        public List<billitem> ListItems { get; set; }
        public List<billExpense> ListExpense { get; set; }
        public List<PaymentTxn> ListPayment { get; set; }
        public int InvoiceId { get; set; }

    }
    public class billitem
    {
        public string TxnLineID { get; set; }
        public string itemListID { get; set; }
        public string ItemName { get; set; }
        public string Quantity { get; set; }
        public string Cost { get; set; }
        public string Amount { get; set; }
    }
    public class billExpense
    {
        public string TxnLineID { get; set; }
        public string ExpenseListID { get; set; }
        public string ExpenseName { get; set; }
        public string Amount { get; set; }
        public string Description { get; set; }
    }
    public class PaymentTxn
    {
        public string TxnLineID { get; set; }
        public string PaymentType { get; set; }
        public string TxnDate { get; set; }
        public string RefNumber { get; set; }
        public string LinkType { get; set; }
        public string Amount { get; set; }
        public string Note { get; set; }
    }

    public class QBWebConnecotr
    {
        public string PropertyId { get; set; }
        public string QBFileName { get; set; }
        public string QBPassword { get; set; }
        public string QBUserName { get; set; }

    }
}