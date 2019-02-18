using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Xml;
using System.Text.RegularExpressions;
using Interop.QBXMLRP2Lib;
using System.Data.SqlClient;
using QBWC_Service.Class;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Web.Hosting;

namespace QBWC_Service.Class
{

    public class Helper
    {

        public static string PropertyId = ConfigurationManager.AppSettings["PropertyId"];

        public static QBWebConnecotr GetQuickBookDetails(SqlConnection con, String PropertyCode)
        {
            QBWebConnecotr obj = new QBWebConnecotr();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from QBWebConnector where PropertyCode = '" + PropertyCode + "' and IsActive = 1 and IsDelete = 0";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        obj.PropertyId = rdr["PropertyID"].ToString();
                        obj.QBFileName = rdr["QBFilePath"].ToString();
                        obj.QBPassword = rdr["QBPassword"].ToString();
                        obj.QBUserName = rdr["QBUsername"].ToString();
                    }
                }
                con.Close();

            }
            catch (Exception ex)
            {
                obj = new QBWebConnecotr();
            }
            return obj;


        }

        public static string GetXMLBlob_VendorsQuery()
        {
            return
            "<?xml version=\"1.0\"?><?qbxml version=\"13.0\"?><QBXML><QBXMLMsgsRq onError=\"stopOnError\">" +
                "<VendorQueryRq  requestID=\"2\">" +
                         "<ActiveStatus>All</ActiveStatus>" +
                        "<IncludeRetElement>ListID</IncludeRetElement>" +
                        "<IncludeRetElement>EditSequence</IncludeRetElement>" +
                        "<IncludeRetElement>Name</IncludeRetElement>" +
                        "<IncludeRetElement>FullName</IncludeRetElement>" +
                        "<IncludeRetElement>IsActive</IncludeRetElement>" +
                        "<IncludeRetElement>VendorAddress</IncludeRetElement>" +
                        "<IncludeRetElement>Phone</IncludeRetElement>" +
                        "<IncludeRetElement>AltPhone</IncludeRetElement>" +
                        "<IncludeRetElement>Fax</IncludeRetElement>" +
                        "<IncludeRetElement>Contact</IncludeRetElement>" +
                        "<IncludeRetElement>AltContact</IncludeRetElement>" +
                        "<IncludeRetElement>Email</IncludeRetElement>" +
                        "<IncludeRetElement>Notes</IncludeRetElement>" +
                        "<IncludeRetElement>AccountNumber</IncludeRetElement>" +
                "</VendorQueryRq>" +
                "</QBXMLMsgsRq></QBXML>";

        }
        public static string GetXMLBlob_CustomersQuery()
        {
            return
            "<?xml version=\"1.0\"?><?qbxml version=\"13.0\"?><QBXML><QBXMLMsgsRq onError=\"stopOnError\">" +
                "<CustomerQueryRq  requestID=\"2\">" +
                         "<ActiveStatus>ActiveOnly</ActiveStatus>" +
                        "<IncludeRetElement>ListID</IncludeRetElement>" +
                        "<IncludeRetElement>EditSequence</IncludeRetElement>" +
                        "<IncludeRetElement>Name</IncludeRetElement>" +
                        "<IncludeRetElement>FullName</IncludeRetElement>" +
                        "<IncludeRetElement>IsActive</IncludeRetElement>" +
                        "<IncludeRetElement>BillAddress</IncludeRetElement>" +
                        "<IncludeRetElement>Phone</IncludeRetElement>" +
                        "<IncludeRetElement>Email</IncludeRetElement>" +
                        "<IncludeRetElement>AccountNumber</IncludeRetElement>" +
                "</CustomerQueryRq>" +
                "</QBXMLMsgsRq></QBXML>"; ;

        }

        public static string GetXMLBlob_AccountsQuery()
        {
            return
            "<?xml version=\"1.0\"?><?qbxml version=\"13.0\"?><QBXML><QBXMLMsgsRq onError=\"stopOnError\">" +
                "<AccountQueryRq   requestID=\"2\">" +
                        "<ActiveStatus>All</ActiveStatus>" +
                        "<IncludeRetElement>ListID</IncludeRetElement>" +
                        "<IncludeRetElement>Name</IncludeRetElement>" +
                        "<IncludeRetElement>FullName</IncludeRetElement>" +
                        "<IncludeRetElement>ParentRef</IncludeRetElement>" +
                        "<IncludeRetElement>Sublevel</IncludeRetElement>" +
                        "<IncludeRetElement>IsActive</IncludeRetElement>" +
                        "<IncludeRetElement>AccountType</IncludeRetElement>" +
                        "<IncludeRetElement>Desc</IncludeRetElement>" +
                        "<IncludeRetElement>AccountNumber</IncludeRetElement>" +
                "</AccountQueryRq>" +
                "</QBXMLMsgsRq></QBXML>";

        }

        public static string GetXMLBlob_ItemsQuery()
        {
            return
            "<?xml version=\"1.0\"?><?qbxml version=\"13.0\"?><QBXML><QBXMLMsgsRq onError=\"stopOnError\">" +
                "<ItemQueryRq    requestID=\"2\">" +
                        "<ActiveStatus>All</ActiveStatus>" +
                        "<IncludeRetElement>ListID</IncludeRetElement>" +
                        "<IncludeRetElement>Name</IncludeRetElement>" +
                        "<IncludeRetElement>FullName</IncludeRetElement>" +
                        "<IncludeRetElement>Sublevel</IncludeRetElement>" +
                        "<IncludeRetElement>IsActive</IncludeRetElement>" +
                "</ItemQueryRq>" +
                "</QBXMLMsgsRq></QBXML>";

        }


        public static string GetXMLBlob_CreditCardChargeBillQuery()
        {
            return
            "<?xml version=\"1.0\" ?><?qbxml version=\"13.0\"?>" +
            "<QBXML><QBXMLMsgsRq onError = \"stopOnError\">" +
                                "<CreditCardChargeQueryRq requestID=\"2\">" +
                                "<IncludeRetElement>TxnID</IncludeRetElement>" +
                                "<IncludeRetElement>TimeCreated</IncludeRetElement>" +
                                "<IncludeRetElement>TxnNumber</IncludeRetElement>" +
                                "<IncludeRetElement>AccountRef</IncludeRetElement>" +
                                "<IncludeRetElement>PayeeEntityRef</IncludeRetElement>" +
                                "<IncludeRetElement>TxnDate</IncludeRetElement>" +
                                "<IncludeRetElement>Amount</IncludeRetElement>" +
                                "<IncludeRetElement>CurrencyRef</IncludeRetElement>" +
                                "<IncludeRetElement>RefNumber</IncludeRetElement>" +
                                "<IncludeRetElement>Memo</IncludeRetElement>" +
                                "<IncludeRetElement>ExternalGUID</IncludeRetElement>" +
                                "<IncludeRetElement>ExpenseLineRet</IncludeRetElement>" +
                                "<IncludeRetElement>ItemLineRet</IncludeRetElement>" +
                                "<IncludeRetElement>ItemGroupLineRet</IncludeRetElement>" +
                                "<IncludeRetElement>DataExtRet</IncludeRetElement>" +
                                "</CreditCardChargeQueryRq>" +
         "</QBXMLMsgsRq></QBXML>";
        }


        public static string GetXMLBlob_BillQuery()
        {
            return
            "<?xml version=\"1.0\" ?><?qbxml version=\"13.0\"?><QBXML><QBXMLMsgsRq onError = \"stopOnError\">" +
                                "<BillQueryRq requestID=\"2\">" +
                                   "<IncludeRetElement>ListID</IncludeRetElement>" +
                                   "<IncludeRetElement>TxnDate</IncludeRetElement>" +
                                   "<IncludeRetElement>DueDate</IncludeRetElement>" +
                                   "<IncludeRetElement>FullName</IncludeRetElement>" +
                                   "<IncludeRetElement>PaidStatus</IncludeRetElement>" +
                                   "<IncludeRetElement>VendorRef</IncludeRetElement>" +
                                   "<IncludeRetElement>IsPaid</IncludeRetElement>" +
                                   "<IncludeRetElement>TxnID</IncludeRetElement>" +
                                   "<IncludeRetElement>LinkedTxn</IncludeRetElement>" +
                                   "<IncludeRetElement>ItemLineRet</IncludeRetElement>" +
                                    "<IncludeRetElement>ExpenseLineRet</IncludeRetElement>" +
                                   "<IncludeRetElement>ExternalGUID</IncludeRetElement>" +
                                   "<IncludeRetElement>AmountDue</IncludeRetElement>" +
                                   "<IncludeRetElement>CurrencyRef</IncludeRetElement>" +
                                   "<IncludeRetElement>Amount</IncludeRetElement>" +
                                   "<IncludeRetElement>BillableStatus</IncludeRetElement>" +
                                   "<IncludeRetElement>LinkedTxn</IncludeRetElement>" +
                                "</BillQueryRq>" +
                                "</QBXMLMsgsRq></QBXML>";
        }



        public static bool Check_CustomervailableFOrInsert(SqlConnection con)
        {
            bool IsCustomerstatus = false;
            //check vendor available for insert 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Ap_VendorMaster values(1,1,'Test','Rigel Test','1','600 Centerpoint Blvd','Qwintry Ste 106232','New Castle','Delaware',19720,1,8586336353,8586336353,8586336353,'rahimova.adel@mail.ru','1900-01-02 00:00:00',1,'Test',1,'Rigel','www.rigelnetworks.com',1,1,1,1000.55,2000.44,'SSN1001',100,'8000000A-1548685147','Rigel Networks',1,''1900-01-01 00:00:00.000)";
                //select  a.*,b.Property_name  as CompanyName  from[dbo].[AP_VendorMaster] as a inner join [dbo].[Property_Master] as b on a.PropertyId = b.PropertyID where a.PropertyId = " + PropertyId + " and a.QuickbookStatus = 0 and a.IsActive = 1 and a.QuickbookListId is null";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    IsCustomerstatus = true;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "Check_CustomervailableFOrInsert";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
               // bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }
            return IsCustomerstatus;
        }

        public static bool Check_ItemvailableFOrInsert(SqlConnection con)
        {
            bool IsItemstatus = false;
            //check vendor available for insert 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "select ItemName from (SELECT  a.[InventoryItem] as ItemName FROM[dbo].[AP_InvoiceLines] as a inner join[dbo].[AP_Invoices] as b on b.[InvoiceId] = a.[InvoiceId] where a.[InventoryItem] is not null and a.[InventoryItem] <> '' and b.PropertyId = "+ PropertyId+" union SELECT  a.[ExpenseItem] as ItemName FROM[dbo].[AP_InvoiceLines] as a inner join[dbo].[AP_Invoices] as b on b.[InvoiceId] = a.[InvoiceId] where a.[ExpenseItem] is not null and a.[ExpenseItem] <> '' and b.PropertyId = "+PropertyId+") as c where c.ItemName not in (SELECT[ItemName] FROM [dbo].[QBItemResponse])";
            cmd.CommandText = "select distinct(txtItemName),intQuantity, fltItemPrice,txtItemDescription  from tblmstItems where QBListItemId is null and QBItemStatus=0 ";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    IsItemstatus = true;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "Check_ItemvailableFOrInsert";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog); ;
            }
            return IsItemstatus;
        }



        public static bool Check_XMLBlob_JournalEntryQuery(SqlConnection con)
        {
            bool IsJournalEntryAvailable = false;
            // Insert JournalEntry
            ArrayList req = new ArrayList();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select  distinct  CONVERT(varchar, itemdate, 101) as itemdate from PMSDataEntry p inner join GL_accounts g on p.GLAccountID = g.GL_Account_Id inner join GL_Account_Type ga on g.GL_Account_Type_Id = ga.GL_Account_Type_Id where FormId IN(select FormId from FB_FormSetUp where PropertyId = " + PropertyId + ") and(quickbooklistid is not null and quickbookfullname is not null) and(p.QuickBookStatus = 0 and p.QuickBookStatus is not null)";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    IsJournalEntryAvailable = true;
                }

                con.Close();

            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.JEtransactionDate = DateTime.Now;
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "Inngenius to Quickbook";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "Journal Entry";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertJEQuickbookLog(con, objQuickbookLog); ;
            }
            return IsJournalEntryAvailable;
        }
        /// <summary>
        /// only Get Approved Invoice and GLAccount is imported into QuickBOok
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        /// 


        //public static bool Check_InvoiceavailableFOrInsert(SqlConnection con)
        //{
        //    bool IsInvoicestatus = false;
        //    //check vendor available for insert 
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = con;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = "insert into ap_invoices values(18,1,1,101,'25/01/19','101','25/01/19','28/01/19',1000,'Rigel Networks','28/01/19',1,'Rigel','25/01/19','Rigel Network',1,'001','25/01/19',1000,'25/01/19','Rigel','25/01/19','8000000A-1548685147','Rigel Networks',1,1,1)";
        //    try
        //    {
        //        if (con.State == ConnectionState.Open)
        //            con.Close();
        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();
        //        if (rdr.HasRows)
        //        {
        //            while (rdr.Read())
        //            {
        //                decimal InvoiceAmt = Convert.ToDecimal(rdr["InvoiceAmt"].ToString());
        //                if (InvoiceAmt > 0)
        //                {
        //                    IsInvoicestatus = true;
        //                }
        //            }
        //        }
        //        con.Close();

        //    }
        //    catch (Exception ex)
        //    {
        //        QuickbookLog objQuickbookLog = new QuickbookLog();
        //        objQuickbookLog.VendorId = 0;
        //        objQuickbookLog.Status = ex.Message;
        //        objQuickbookLog.QuickbookListId = "";
        //        objQuickbookLog.StatusDes = "Imported Not successfully";
        //        objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
        //        objQuickbookLog.Process = "Check_InvoiceavailableFOrInsert";
        //        objQuickbookLog.importdate = DateTime.Now;
        //        objQuickbookLog.ResponseType = "";
        //        objQuickbookLog.AccountSubTypeId = 0;
        //        objQuickbookLog.InvoiceId = 0;
        //        objQuickbookLog.AccountId = 0;
        //        objQuickbookLog.QuickbookFullName = "";
        //        objQuickbookLog.VendorName = "";
        //        //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
        //    }
        //    return IsInvoicestatus;
        //}
        // New Define Metgid
        public static bool Check_InvoiceavailableFOrInsert(SqlConnection con)
        {
            string constr = ConfigurationManager.ConnectionStrings["DevelopmentEntities"].ConnectionString;
            bool IsInvoicestatus = false;
            //check vendor available for insert 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select  top 100 * from tblmstorders  where  QBInvoiceStatus=0 and QBInvoiceListId is NULL";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        decimal fltTotalAmount = Convert.ToDecimal(rdr["fltTotalAmount"].ToString());
                        if (fltTotalAmount > 0)
                        {
                            IsInvoicestatus = true;
                        }
                    }
                }
                con.Close();



            }

            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "Check_InvoiceavailableFOrInsert";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }
            return IsInvoicestatus;
        }
// New Define Method 
        public static ArrayList InsertXMLBlob_BillsQuery(SqlConnection con)
        {
            // Insert Invoice
            ArrayList req = new ArrayList();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT c.intCustomerId,c.txtFirstName,c.txtAddress,c.txtCity,c.intStateId,c.txtZipCode,c.QBCustListId,o.intOrderId,o.dtOrderDate,o.txtBillingFirstName,i.intItemId,i.txtItemName,i.intQuantity,i.fltItemPrice from tblmstCustomer c  left join tblmstorders o  on c.intCustomerId = o.intOrderId   right outer join tblmstitems i on i.intItemId = c.intCustomerId";
                //SELECT c.intCustomerId,c.txtFirstName,c.txtAddress,c.txtCity,c.intStateId,c.txtZipCode,c.QBCustListId,o.intOrderId,o.dtOrderDate,o.txtBillingFirstName,i.intItemId,i.txtItemName,i.intQuantity,i.fltItemPrice from tblmstCustomer c  left join tblmstorders o  on c.intCustomerId = o.intOrderId   right outer join tblmstitems i on i.intItemId = c.intCustomerId
                //";
            //select  top 100 * from tblmstorders  where  QBInvoiceStatus=0 and QBInvoiceListId is NULL

            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    decimal InvoiceAmt = Convert.ToDecimal(rdr["fltItemPrice"].ToString());
                    if (InvoiceAmt > 0)
                    {
                        string strrequestxml = "";
                        XmlDocument inputxmldoc;
                        strrequestxml = "";
                        inputxmldoc = null;
                        inputxmldoc = new XmlDocument();
                        inputxmldoc.AppendChild(inputxmldoc.CreateXmlDeclaration("1.0", null, null));
                        inputxmldoc.AppendChild(inputxmldoc.CreateProcessingInstruction("qbxml", "version=\"13.0\""));

                        XmlElement qbxml = inputxmldoc.CreateElement("QBXML");
                        inputxmldoc.AppendChild(qbxml);
                        XmlElement qbxmlmsgsrq = inputxmldoc.CreateElement("QBXMLMsgsRq");
                        qbxml.AppendChild(qbxmlmsgsrq);
                        qbxmlmsgsrq.SetAttribute("onError", "stopOnError");

                        XmlElement custaddrq = inputxmldoc.CreateElement("InvoiceAddRq");
                        qbxmlmsgsrq.AppendChild(custaddrq);
                        custaddrq.SetAttribute("requestID", rdr["intOrderId"].ToString());

                        XmlElement customerAdd = inputxmldoc.CreateElement("InvoiceAdd");
                        custaddrq.AppendChild(customerAdd);

                        XmlElement CustomerRef = inputxmldoc.CreateElement("CustomerRef");
                        customerAdd.AppendChild(CustomerRef);
                        CustomerRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = rdr["QBCustListId"].ToString();
                        CustomerRef.AppendChild(inputxmldoc.CreateElement("FullName")).InnerText = rdr["txtFirstName"].ToString();

                        customerAdd.AppendChild(inputxmldoc.CreateElement("TxnDate")).InnerText = Convert.ToString(rdr["dtOrderDate"]) == "" ? "" : (Convert.ToDateTime(rdr["dtOrderDate"]).ToString("yyyy-MM-dd"));
                        customerAdd.AppendChild(inputxmldoc.CreateElement("RefNumber")).InnerText = rdr["intCustomerId"].ToString();

                        XmlElement customeraddress = inputxmldoc.CreateElement("BillAddress");
                        customerAdd.AppendChild(customeraddress);
                        customeraddress.AppendChild(inputxmldoc.CreateElement("Addr1")).InnerText = rdr["txtAddress"].ToString();
                        customeraddress.AppendChild(inputxmldoc.CreateElement("City")).InnerText = rdr["txtCity"].ToString();
                        customeraddress.AppendChild(inputxmldoc.CreateElement("State")).InnerText = rdr["intStateId"].ToString();
                        customeraddress.AppendChild(inputxmldoc.CreateElement("PostalCode")).InnerText = rdr["txtZipCode"].ToString();

                        customerAdd.AppendChild(inputxmldoc.CreateElement("PONumber")).InnerText = rdr["intOrderId"].ToString();
                        customerAdd.AppendChild(inputxmldoc.CreateElement("DueDate")).InnerText = Convert.ToString(rdr["dtOrderDate"]) == "" ? "" : (Convert.ToDateTime(rdr["dtshippedDate"]).ToString("yyyy-MM-dd"));
                       // customerAdd.AppendChild(inputxmldoc.CreateElement("Memo")).InnerText = Convert.ToString(rdr["txtOrderStatus"]) == "" ? "" : rdr["txtOrderStatus"].ToString();
                        ////int InvoiceId = Convert.ToInt32(rdr["intOrderId"].ToString());
                        ////List<Item> objlistitem = GetItemsByInvoiceId(InvoiceId, con);
                      
                        //foreach (var item in objlistitem)
                        //{
                            XmlElement InvoiceLineAdd = inputxmldoc.CreateElement("InvoiceLineAdd");
                            customerAdd.AppendChild(InvoiceLineAdd);
                            XmlElement ItemRef = inputxmldoc.CreateElement("ItemRef");
                            InvoiceLineAdd.AppendChild(ItemRef);
                            ItemRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = rdr["QBListItemId"].ToString();
                            ItemRef.AppendChild(inputxmldoc.CreateElement("FullName")).InnerText = rdr["txtItemName"].ToString();
                            InvoiceLineAdd.AppendChild(inputxmldoc.CreateElement("Quantity")).InnerText = rdr["intQuantity"].ToString();
                            InvoiceLineAdd.AppendChild(inputxmldoc.CreateElement("Rate")).InnerText = rdr["fltItemPrice"].ToString();
                        ////}

                     
                        
                        //XmlElement custaddrq = inputxmldoc.CreateElement("ItemInventoryAddRq");
                        //qbxmlmsgsrq.AppendChild(custaddrq);
                        //custaddrq.SetAttribute("requestID", "InvoiceLineId".ToString());

                        //XmlElement customerAdd = inputxmldoc.CreateElement("ItemInventoryAdd");
                        //custaddrq.AppendChild(customerAdd);
                        //string ItemName = Convert.ToString("ItemName111");
                        //customerAdd.AppendChild(inputxmldoc.CreateElement("Name")).InnerText = ItemName.Length > 20 ? ItemName.Substring(0, 20) : ItemName;

                        //XmlElement IncomeAccountRef = inputxmldoc.CreateElement("IncomeAccountRef");
                        //customerAdd.AppendChild(IncomeAccountRef);
                        //IncomeAccountRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = "8000002B-1548860425".ToString();
                        //IncomeAccountRef.AppendChild(inputxmldoc.CreateElement("FullName")).InnerText = "IncomeAccountRef".ToString();

                        //XmlElement COGSAccountRef = inputxmldoc.CreateElement("COGSAccountRef");
                        //customerAdd.AppendChild(COGSAccountRef);
                        //COGSAccountRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = "80000029-1548149102".ToString();
                        //COGSAccountRef.AppendChild(inputxmldoc.CreateElement("FullName")).InnerText = "Cost of Goods Sold".ToString();

                        //XmlElement AssetAccountRef = inputxmldoc.CreateElement("AssetAccountRef");
                        //customerAdd.AppendChild(AssetAccountRef);
                        //AssetAccountRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = "8000002D-1548861151".ToString();
                        //AssetAccountRef.AppendChild(inputxmldoc.CreateElement("FullName")).InnerText = "AssetAccountRef".ToString();



                        string input = inputxmldoc.OuterXml;
                        //step3: do the qbxmlrp request
                        strrequestxml = inputxmldoc.OuterXml;
                        //System.IO.File.AppendAllText(@"D:\Request.txt", strrequestxml + "\n\n");
                        req.Add(strrequestxml);
                    }
                }
                con.Close();

            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "InsertXMLBlob_InvoicesQuery";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                // bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }

            return req;
        }


        /// <summary>
        /// only Get Approved Invoice and GLAccount is imported into QuickBOok
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        public static bool Check_VendorCreditInvoiceavailableFOrInsert(SqlConnection con)
        {
            bool IsVendorCreditInvoicestatus = false;
            //check vendor available for insert 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT a.* ,[b].[Address1],[b].[Address2],[b].[City],[b].[State],[b].[PostalCode],[a].[QuickbookListId],[a].[QuickbookFullName] FROM [dbo].[AP_Invoices] as a inner join[dbo].[AP_VendorMaster] as b on a.[VendorId] = b.VendorId where a.InvoiceType = 1 and a.QuickbookStatus = 0 and b.IsActive = 1 and a.QuickbookListId is null and a.statusid = 4 and a.PropertyId =" + PropertyId + " and a.InvoiceId not in (select distinct  InvoiceId from[dbo].[AP_InvoiceLines] where [AppliedGL_Account_Id] in (SELECT[GL_Account_Id] FROM [dbo].[GL_Accounts] where [QuickbookListId] is null and[GL_Account_Id] in (SELECT distinct  a.[AppliedGL_Account_Id] FROM [dbo].[AP_InvoiceLines] as a inner join[dbo].[AP_Invoices] as b on b.[InvoiceId] = a.[InvoiceId] inner join[dbo].[GL_Accounts] as c on c.[GL_Account_Id] = a.[AppliedGL_Account_Id] where a.[AppliedGL_Account_Id]  <> 0 )))";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        decimal InvoiceAmt = Convert.ToDecimal(rdr["InvoiceAmt"].ToString());
                        if (InvoiceAmt < 0)
                        {
                            IsVendorCreditInvoicestatus = true;
                        }
                    }
                }
                con.Close();

            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "Check_InvoiceavailableFOrInsert";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }
            return IsVendorCreditInvoicestatus;
        }

        /// <summary>
        /// only Get Approved Credit-Cardit CardChargeInvoice Invoice and GLAccount is imported into QuickBOok
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        public static bool Check_CreditCardChargeInvoiceavailableFOrInsert(SqlConnection con)
        {
            bool IsVendorCreditInvoicestatus = false;
            //check vendor available for insert 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT a.* ,[b].[Address1],[b].[Address2],[b].[City],[b].[State],[b].[PostalCode],[a].[QuickbookListId],[a].[QuickbookFullName],g.QuickbookListId as GL_QuickbookListID FROM [dbo].[AP_Invoices] as a inner join[dbo].[AP_VendorMaster] as b on a.[VendorId] = b.VendorId inner join [GL_Accounts] as g on a.CC_GL_AccId = g.GL_Account_Id  where a.InvoiceType = 2 and a.QuickbookStatus = 0 and b.IsActive = 1 and a.QuickbookListId is null and a.statusid = 4 and a.PropertyId =" + PropertyId + " and a.InvoiceId not in (select distinct  InvoiceId from[dbo].[AP_InvoiceLines] where [AppliedGL_Account_Id] in (SELECT[GL_Account_Id] FROM [dbo].[GL_Accounts] where [QuickbookListId] is null and[GL_Account_Id] in (SELECT distinct  a.[AppliedGL_Account_Id] FROM [dbo].[AP_InvoiceLines] as a inner join[dbo].[AP_Invoices] as b on b.[InvoiceId] = a.[InvoiceId] inner join[dbo].[GL_Accounts] as c on c.[GL_Account_Id] = a.[AppliedGL_Account_Id] where a.[AppliedGL_Account_Id]  <> 0 )))";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        decimal InvoiceAmt = Convert.ToDecimal(rdr["InvoiceAmt"].ToString());

                        IsVendorCreditInvoicestatus = true;
                    }
                }
                con.Close();

            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "Check_CreditCarditCardChargeInvoiceavailableFOrInsert";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }
            return IsVendorCreditInvoicestatus;
        }


        public static bool UpdateVendor(SqlConnection con, QuickbookLog objQuickbookLog)
        {
            bool IsUpdateVendor = false;
            try
            {
                using (SqlCommand cmmd = new SqlCommand("UPDATE AP_VendorMaster SET QuickbookStatus = 1 , QuickbookListId = '" + objQuickbookLog.QuickbookListId + "', QuickbookFullName = '" + objQuickbookLog.QuickbookFullName + "' where QuickbookStatus = 0 and QuickbookListId is null and PropertyId = " + PropertyId + " and IsActive = 1 and VendorId=" + objQuickbookLog.VendorId, con))
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Open();
                    int rows = cmmd.ExecuteNonQuery();
                    con.Close();
                    if (rows > 0)
                        IsUpdateVendor = true;
                }
            }
            catch (Exception ex)
            {
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "UpdateVendor";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }
            return IsUpdateVendor;
        }

        public static bool UpdateInvoice(SqlConnection con, QuickbookLog objQuickbookLog)
        {
            bool IsUpdateInvoice = false;
            try
            {
                using (SqlCommand cmmd = new SqlCommand("UPDATE AP_Invoices SET QuickbookStatus = 1 , QuickbookListId = '" + objQuickbookLog.QuickbookListId + "'  where QuickbookStatus = 0 and PropertyId =" + PropertyId + " and QuickbookListId is null and InvoiceId = " + objQuickbookLog.InvoiceId, con))
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Open();
                    int rows = cmmd.ExecuteNonQuery();
                    con.Close();
                    if (rows > 0)
                        IsUpdateInvoice = true;
                }
            }
            catch (Exception ex)
            {
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "UpdateInvoice";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }


            return IsUpdateInvoice;
        }

        public static bool UpdateInvoiceForCreditCardCharge(SqlConnection con, QuickbookLog objQuickbookLog)
        {
            bool IsUpdateInvoice = false;
            try
            {
                using (SqlCommand cmmd = new SqlCommand("UPDATE AP_Invoices SET QuickbookStatus = 1, StatusId=8 , QuickbookListId = @QuickbookListId  where QuickbookStatus = 0 and PropertyId =" + PropertyId + " and QuickbookListId is null and InvoiceId = " + objQuickbookLog.InvoiceId, con))
                {
                    if (objQuickbookLog.QuickbookListId != null)
                        cmmd.Parameters.AddWithValue("@QuickbookListId", objQuickbookLog.QuickbookListId);
                    else
                        cmmd.Parameters.AddWithValue("@QuickbookListId", DBNull.Value);

                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Open();
                    int rows = cmmd.ExecuteNonQuery();
                    con.Close();
                    if (rows > 0)
                    {
                        IsUpdateInvoice = true;
                    }
                }
            }
            catch (Exception ex)
            {
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "UpdateInvoice";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }


            return IsUpdateInvoice;
        }

        public static bool UpdateInvoicebystatusfalse(SqlConnection con, QuickbookLog objQuickbookLog)
        {
            bool IsUpdateInvoice = false;
            try
            {
                using (SqlCommand cmmd = new SqlCommand("UPDATE AP_Invoices SET QuickbookStatus = 0 , QuickbookListId = @QuickbookListId where QuickbookStatus = 0 and PropertyId =" + PropertyId + " and QuickbookListId is null and InvoiceId = " + objQuickbookLog.InvoiceId, con))
                {
                    if (objQuickbookLog.QuickbookListId != "")
                        cmmd.Parameters.AddWithValue("@QuickbookListId", objQuickbookLog.QuickbookListId);
                    else
                        cmmd.Parameters.AddWithValue("@QuickbookListId", DBNull.Value);

                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Open();
                    int rows = cmmd.ExecuteNonQuery();
                    con.Close();
                    if (rows > 0)
                        IsUpdateInvoice = true;
                }
            }
            catch (Exception ex)
            {
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "UpdateInvoice";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }


            return IsUpdateInvoice;
        }

        public static ArrayList InsertXMLBlob_VendorsQuery(SqlConnection con)
        {
            // Insert Vendors
            ArrayList req = new ArrayList();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select  a.*,b.Property_name  as CompanyName from AP_VendorMaster as a inner join Property_Master b on a.PropertyId = b.PropertyID where a.PropertyId = " + PropertyId + " and a.QuickbookStatus = 0 and a.QuickbookListId is null and a.IsActive = 1";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string strrequestxml = "";
                    XmlDocument inputxmldoc;
                    strrequestxml = "";
                    inputxmldoc = null;
                    inputxmldoc = new XmlDocument();
                    inputxmldoc.AppendChild(inputxmldoc.CreateXmlDeclaration("1.0", null, null));
                    inputxmldoc.AppendChild(inputxmldoc.CreateProcessingInstruction("qbxml", "version=\"13.0\""));

                    XmlElement qbxml = inputxmldoc.CreateElement("QBXML");
                    inputxmldoc.AppendChild(qbxml);
                    XmlElement qbxmlmsgsrq = inputxmldoc.CreateElement("QBXMLMsgsRq");
                    qbxml.AppendChild(qbxmlmsgsrq);
                    qbxmlmsgsrq.SetAttribute("onError", "stopOnError");
                    XmlElement custaddrq = inputxmldoc.CreateElement("VendorAddRq");
                    qbxmlmsgsrq.AppendChild(custaddrq);
                    custaddrq.SetAttribute("requestID", rdr["VendorId"].ToString());

                    XmlElement customerAdd = inputxmldoc.CreateElement("VendorAdd");
                    custaddrq.AppendChild(customerAdd);
                    //customerAdd.AppendChild(inputxmldoc.CreateElement("Name")).InnerText = rdr["VendorName"].ToString() + "(" + rdr["VendorId"].ToString() + ")";
                    customerAdd.AppendChild(inputxmldoc.CreateElement("Name")).InnerText = rdr["VendorName"].ToString();
                    customerAdd.AppendChild(inputxmldoc.CreateElement("IsActive")).InnerText = "1";
                    customerAdd.AppendChild(inputxmldoc.CreateElement("CompanyName")).InnerText = rdr["CompanyName"].ToString() ?? "";
                    customerAdd.AppendChild(inputxmldoc.CreateElement("Salutation")).InnerText = "";// "mr/mrs";
                    string VendorName = Convert.ToString(rdr["VendorName"]);
                    customerAdd.AppendChild(inputxmldoc.CreateElement("FirstName")).InnerText = VendorName.Length > 20 ? VendorName.Substring(0, 20) : VendorName;
                    customerAdd.AppendChild(inputxmldoc.CreateElement("LastName")).InnerText = "";

                    XmlElement customeraddress = inputxmldoc.CreateElement("VendorAddress");
                    customerAdd.AppendChild(customeraddress);
                    customeraddress.AppendChild(inputxmldoc.CreateElement("Addr1")).InnerText = rdr["Address1"].ToString();
                    customeraddress.AppendChild(inputxmldoc.CreateElement("Addr2")).InnerText = rdr["Address2"].ToString();
                    customeraddress.AppendChild(inputxmldoc.CreateElement("City")).InnerText = rdr["City"].ToString();
                    customeraddress.AppendChild(inputxmldoc.CreateElement("State")).InnerText = rdr["State"].ToString();
                    customeraddress.AppendChild(inputxmldoc.CreateElement("PostalCode")).InnerText = rdr["PostalCode"].ToString();

                    customerAdd.AppendChild(inputxmldoc.CreateElement("Phone")).InnerText = rdr["VendorContact"].ToString();
                    customerAdd.AppendChild(inputxmldoc.CreateElement("Fax")).InnerText = rdr["Fax"].ToString();
                    customerAdd.AppendChild(inputxmldoc.CreateElement("Email")).InnerText = rdr["VendorEmail"].ToString();
                    customerAdd.AppendChild(inputxmldoc.CreateElement("AccountNumber")).InnerText = rdr["AccountNumber"].ToString();

                    string input = inputxmldoc.OuterXml;
                    //step3: do the qbxmlrp request
                    strrequestxml = inputxmldoc.OuterXml;
                    // System.IO.File.AppendAllText(@"D:\Request.txt", strrequestxml + "\n\n");
                    req.Add(strrequestxml);

                }
                con.Close();

            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "InsertXMLBlob_CustomersQuery";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }

            return req;
        }
        //Get Vendor List and convert into Customer XML Format
        public static ArrayList InsertXMLBlob_CustomersQuery(SqlConnection con)
        {
            // Insert Vendors
            ArrayList req = new ArrayList();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 100 * from tblmstCustomer where  QBCustListId is null and QBCustStatus=0";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string strrequestxml = "";
                    XmlDocument inputxmldoc;
                    strrequestxml = "";
                    inputxmldoc = null;
                    inputxmldoc = new XmlDocument();
                    inputxmldoc.AppendChild(inputxmldoc.CreateXmlDeclaration("1.0", null, null));
                    inputxmldoc.AppendChild(inputxmldoc.CreateProcessingInstruction("qbxml", "version=\"13.0\""));

                    XmlElement qbxml = inputxmldoc.CreateElement("QBXML");
                    inputxmldoc.AppendChild(qbxml);
                    XmlElement qbxmlmsgsrq = inputxmldoc.CreateElement("QBXMLMsgsRq");
                    qbxml.AppendChild(qbxmlmsgsrq);
                    qbxmlmsgsrq.SetAttribute("onError", "stopOnError");
                    XmlElement custaddrq = inputxmldoc.CreateElement("CustomerAddRq");
                    qbxmlmsgsrq.AppendChild(custaddrq);
                    custaddrq.SetAttribute("requestID", rdr["intCustomerId"].ToString());

                    XmlElement customerAdd = inputxmldoc.CreateElement("CustomerAdd");
                    custaddrq.AppendChild(customerAdd);
                    customerAdd.AppendChild(inputxmldoc.CreateElement("Name")).InnerText = rdr["txtFirstName"].ToString() + "(" + rdr["intCustomerId"].ToString() + ")";
                    customerAdd.AppendChild(inputxmldoc.CreateElement("IsActive")).InnerText = "1";
                    customerAdd.AppendChild(inputxmldoc.CreateElement("CompanyName")).InnerText = rdr["txtCompanyName"].ToString() ?? "";
                    customerAdd.AppendChild(inputxmldoc.CreateElement("Salutation")).InnerText = "";// "mr/mrs";
                    string VendorName = Convert.ToString(rdr["txtLastName"]);
                    customerAdd.AppendChild(inputxmldoc.CreateElement("FirstName")).InnerText = VendorName.Length > 20 ? VendorName.Substring(0, 20) : VendorName;
                    customerAdd.AppendChild(inputxmldoc.CreateElement("LastName")).InnerText = "";

                    XmlElement customeraddress = inputxmldoc.CreateElement("BillAddress");
                    customerAdd.AppendChild(customeraddress);
                    customeraddress.AppendChild(inputxmldoc.CreateElement("Addr1")).InnerText = rdr["txtAddress"].ToString();
                    customeraddress.AppendChild(inputxmldoc.CreateElement("City")).InnerText = rdr["txtCity"].ToString();
                    customeraddress.AppendChild(inputxmldoc.CreateElement("State")).InnerText = rdr["intStateId"].ToString();
                    customeraddress.AppendChild(inputxmldoc.CreateElement("PostalCode")).InnerText = rdr["txtZipCode"].ToString();

                    customerAdd.AppendChild(inputxmldoc.CreateElement("Phone")).InnerText = rdr["txtPhoneNo"].ToString();

                    customerAdd.AppendChild(inputxmldoc.CreateElement("Email")).InnerText = rdr["txtEmailAddress"].ToString();
                    customerAdd.AppendChild(inputxmldoc.CreateElement("AccountNumber")).InnerText = rdr["txtUserId"].ToString();

                    string input = inputxmldoc.OuterXml;
                    //step3: do the qbxmlrp request
                    strrequestxml = inputxmldoc.OuterXml;
                    // System.IO.File.AppendAllText(@"D:\Request.txt", strrequestxml + "\n\n");
                    req.Add(strrequestxml);

                }
                con.Close();

            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "InsertXMLBlob_CustomersQuery";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }

            return req;
        }

        public static int InsertCustomerQuickbook_to_system(SqlConnection con, Vendor objvendor)
        {

            int InsertedVendorId = 0;
            //check vendor available for insert 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM [dbo].[AP_VendorMaster] where  QuickbookStatus = 1 and QuickbookListId ='" + objvendor.ListId + "'";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    using (SqlCommand cmdd = new SqlCommand("UPDATE [dbo].[AP_VendorMaster] set PropertyId = @PropertyId ,VendorName = @VendorName,VendorEmail = @VendorEmail,VendorContact = @VendorContact,AccountNumber = @AccountNumber,Address1 = @Address1,City=@City,State=@State,PostalCode=@PostalCode, CreateDate=@CreateDate,QuickbookFullName=@QuickbookFullName, IsActive=@IsActive, QuickbookStatus = 1,InActiveDateQB = @InActiveDateQB,VendorNumber=@VendorNumber,Comments=@Comments,Fax=@Fax where  QuickbookStatus = 1 and QuickbookListId ='" + objvendor.ListId + "'", con))
                    {
                        cmdd.CommandType = CommandType.Text;
                        cmdd.Parameters.AddWithValue("@PropertyId", PropertyId);
                        cmdd.Parameters.AddWithValue("@VendorName", objvendor.VendorName);
                        cmdd.Parameters.AddWithValue("@VendorEmail", objvendor.VendorEmail);
                        cmdd.Parameters.AddWithValue("@VendorContact", objvendor.VendorContact);
                        cmdd.Parameters.AddWithValue("@AccountNumber", objvendor.AccountNumber);
                        cmdd.Parameters.AddWithValue("@Address1", objvendor.Address1);
                        cmdd.Parameters.AddWithValue("@City", objvendor.City);
                        cmdd.Parameters.AddWithValue("@State", objvendor.State);
                        cmdd.Parameters.AddWithValue("@PostalCode", objvendor.PostalCode);
                        cmdd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                        cmdd.Parameters.AddWithValue("@QuickbookListId", objvendor.ListId);
                        cmdd.Parameters.AddWithValue("@QuickbookFullName", objvendor.FullName);
                        cmdd.Parameters.AddWithValue("@IsActive", Convert.ToBoolean(objvendor.IsActive));
                        cmdd.Parameters.AddWithValue("@VendorNumber", objvendor.ListId);
                        cmdd.Parameters.AddWithValue("@Comments", objvendor.Comments);
                        cmdd.Parameters.AddWithValue("@Fax", objvendor.Fax);
                        if (objvendor.IsActive == "false")
                            cmdd.Parameters.AddWithValue("@InActiveDateQB", DateTime.Now);
                        else
                            cmdd.Parameters.AddWithValue("@InActiveDateQB", DBNull.Value);
                        if (con.State == ConnectionState.Open)
                            con.Close();
                        con.Open();
                        cmdd.ExecuteNonQuery();
                        con.Close();
                    }
                    InsertedVendorId = 0;
                }
                else
                {
                    using (SqlCommand cmdd = new SqlCommand("INSERT INTO [dbo].[AP_VendorMaster] (PropertyId ,VendorName,VendorEmail,VendorContact,AccountNumber,Address1,City,State ,PostalCode, CreateDate,QuickbookListId,QuickbookFullName, IsActive, QuickbookStatus,InActiveDateQB,VendorNumber,Comments,Fax) output INSERTED.VendorId VALUES (@PropertyId, @VendorName, @VendorEmail,@VendorContact, @AccountNumber, @Address1,@City, @State, @PostalCode, @CreateDate,@QuickbookListId,@QuickbookFullName,@IsActive,1,@InActiveDateQB,@VendorNumber,@Comments,@Fax)", con))
                    {
                        cmdd.CommandType = CommandType.Text;
                        cmdd.Parameters.AddWithValue("@PropertyId", PropertyId);
                        cmdd.Parameters.AddWithValue("@VendorName", objvendor.VendorName);
                        cmdd.Parameters.AddWithValue("@VendorEmail", objvendor.VendorEmail);
                        cmdd.Parameters.AddWithValue("@VendorContact", objvendor.VendorContact);
                        cmdd.Parameters.AddWithValue("@AccountNumber", objvendor.AccountNumber);
                        cmdd.Parameters.AddWithValue("@Address1", objvendor.Address1);
                        cmdd.Parameters.AddWithValue("@City", objvendor.City);
                        cmdd.Parameters.AddWithValue("@State", objvendor.State);
                        cmdd.Parameters.AddWithValue("@PostalCode", objvendor.PostalCode);
                        cmdd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                        cmdd.Parameters.AddWithValue("@QuickbookListId", objvendor.ListId);
                        cmdd.Parameters.AddWithValue("@QuickbookFullName", objvendor.FullName);
                        cmdd.Parameters.AddWithValue("@IsActive", Convert.ToBoolean(objvendor.IsActive));
                        cmdd.Parameters.AddWithValue("@VendorNumber", objvendor.ListId);
                        cmdd.Parameters.AddWithValue("@Comments", objvendor.Comments);
                        cmdd.Parameters.AddWithValue("@Fax", objvendor.Fax);
                        if (objvendor.IsActive == "false")
                            cmdd.Parameters.AddWithValue("@InActiveDateQB", DateTime.Now);
                        else
                            cmdd.Parameters.AddWithValue("@InActiveDateQB", DBNull.Value);
                        if (con.State == ConnectionState.Open)
                            con.Close();
                        con.Open();
                        int VendorID = (int)cmdd.ExecuteScalar();
                        con.Close();
                        InsertedVendorId = VendorID;
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "InsertCustomerQuickbook_to_system";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }
            return InsertedVendorId;
        }


        public static int InsertInvoiceQuickbook_to_system(SqlConnection con, Bill objbill)
        {
            int InsertedInvoiceId = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            try
            {
                cmd.CommandText = "SELECT * FROM [dbo].[AP_Invoices] where  QuickbookStatus = 1 and QuickbookListId ='" + objbill.TxnID + "'";
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        string InvID = rdr["InvoiceId"].ToString();
                        InsertedInvoiceId = Convert.ToInt32(InvID);
                    }
                    SqlCommand cmdvendor = new SqlCommand();
                    cmdvendor.Connection = con;
                    cmdvendor.CommandType = CommandType.Text;
                    cmdvendor.CommandText = "SELECT * FROM [dbo].[AP_VendorMaster] where QuickbookListId ='" + objbill.VendorListID + "'";
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Open();
                    SqlDataReader rdrVendor = cmdvendor.ExecuteReader();
                    if (rdrVendor.HasRows)
                    {
                        while (rdrVendor.Read())
                        {
                            string VendorID = rdrVendor["VendorId"].ToString();
                            objbill.VendorId = Convert.ToInt32(VendorID);
                        }

                        SqlCommand cmdCC_GL_AccId = new SqlCommand();
                        cmdCC_GL_AccId.Connection = con;
                        cmdCC_GL_AccId.CommandType = CommandType.Text;
                        cmdCC_GL_AccId.CommandText = "SELECT * FROM [dbo].[GL_Accounts] where QuickbookListId ='" + objbill.CC_GL_AccId_ListID + "'";
                        if (con.State == ConnectionState.Open)
                            con.Close();
                        con.Open();
                        SqlDataReader rdrCC_GL_AccId = cmdCC_GL_AccId.ExecuteReader();
                        while (rdrCC_GL_AccId.Read())
                        {
                            string CC_GL_AccId = rdrCC_GL_AccId["GL_Account_Id"].ToString();
                            objbill.CC_GL_AccId = Convert.ToInt32(CC_GL_AccId);
                        }

                        // record not available then insert
                        using (SqlCommand cmdd = new SqlCommand("Update [dbo].[AP_Invoices] set PropertyId=@PropertyId ,VendorId=@VendorId,InvoiceNo=@InvoiceNo,InvoiceDte=@InvoiceDte,InvoiceRef=@InvoiceRef,PO_No=@PO_No,GL_PostingDte=@GL_PostingDte,DueDte=@DueDte,InvoiceAmt=@InvoiceAmt,Approver=@Approver,ApprovedDte=@ApprovedDte,StatusId=@StatusId,Creator=@Creator,CreatedDte=@CreatedDte,Notes=@Notes,PaidByManualCheck=@PaidByManualCheck,ManualCheckNo=@ManualCheckNo,ManualCheckDte=@ManualCheckDte,ManualCheckAmt=@ManualCheckAmt,ExportedDte=@ExportedDte,ExportedBy=@ExportedBy,paymentDate=@paymentDate,QuickbookListId=@QuickbookListId,QuickbookFullName=@QuickbookFullName,QuickbookStatus=@QuickbookStatus,InvoiceType=@InvoiceType,CC_GL_AccId=@CC_GL_AccId where  QuickbookStatus = 1 and QuickbookListId ='" + objbill.TxnID + "'", con))
                        {
                            cmdd.CommandType = CommandType.Text;
                            cmdd.Parameters.AddWithValue("@PropertyId", PropertyId);
                            cmdd.Parameters.AddWithValue("@VendorId", objbill.VendorId);
                            cmdd.Parameters.AddWithValue("@InvoiceNo", objbill.TxnID);
                            cmdd.Parameters.AddWithValue("@InvoiceDte", Convert.ToDateTime(objbill.TxnDate));
                            cmdd.Parameters.AddWithValue("@InvoiceRef", objbill.RefNumber);
                            cmdd.Parameters.AddWithValue("@PO_No", DBNull.Value);
                            cmdd.Parameters.AddWithValue("@GL_PostingDte", Convert.ToDateTime(objbill.TxnDate));
                            cmdd.Parameters.AddWithValue("@DueDte", Convert.ToDateTime(objbill.DueDate));
                            cmdd.Parameters.AddWithValue("@InvoiceAmt", objbill.AmountDue);
                            cmdd.Parameters.AddWithValue("@Approver", DBNull.Value);
                            cmdd.Parameters.AddWithValue("@ApprovedDte", DBNull.Value);
                            if (objbill.IsPaid == "true")
                                cmdd.Parameters.AddWithValue("@StatusId", 8);
                            else if (objbill.IsPaid == "false")
                                cmdd.Parameters.AddWithValue("@StatusId", 7);
                            cmdd.Parameters.AddWithValue("@Creator", DBNull.Value);
                            cmdd.Parameters.AddWithValue("@CreatedDte", DateTime.Now);
                            cmdd.Parameters.AddWithValue("@Notes", objbill.Memo);
                            cmdd.Parameters.AddWithValue("@PaidByManualCheck", 0);
                            cmdd.Parameters.AddWithValue("@ManualCheckNo", DBNull.Value);
                            cmdd.Parameters.AddWithValue("@ManualCheckDte", DBNull.Value);
                            cmdd.Parameters.AddWithValue("@ManualCheckAmt", Convert.ToDecimal("0.00"));
                            cmdd.Parameters.AddWithValue("@ExportedDte", DBNull.Value);
                            cmdd.Parameters.AddWithValue("@ExportedBy", DBNull.Value);
                            if (objbill.PaymentDate != "")
                                cmdd.Parameters.AddWithValue("@paymentDate", Convert.ToDateTime(objbill.PaymentDate));
                            else
                                cmdd.Parameters.AddWithValue("@paymentDate", DBNull.Value);
                            cmdd.Parameters.AddWithValue("@QuickbookListId", objbill.TxnID);
                            cmdd.Parameters.AddWithValue("@QuickbookFullName", objbill.VendorName);
                            cmdd.Parameters.AddWithValue("@QuickbookStatus", 1);
                            cmdd.Parameters.AddWithValue("@InvoiceType", objbill.InvoiceType);
                            cmdd.Parameters.AddWithValue("@CC_GL_AccId", objbill.CC_GL_AccId);

                            if (con.State == ConnectionState.Open)
                                con.Close();
                            con.Open();
                            cmdd.ExecuteNonQuery();
                            con.Close();

                            //foreach (var item in objbill.ListItems)
                            //{

                            //    SqlCommand cmdinvoicelines = new SqlCommand();
                            //    cmdinvoicelines.Connection = con;
                            //    cmdinvoicelines.CommandType = CommandType.Text;
                            //    cmdinvoicelines.CommandText = "SELECT * FROM [dbo].[AP_InvoiceLines] where QuickbookStatus=1 and InvoiceId ='" + InsertedInvoiceId + "' and QuickbookListId='" + item.TxnLineID + "'";
                            //    if (con.State == ConnectionState.Open)
                            //        con.Close();
                            //    con.Open();
                            //    SqlDataReader rdrinvoicelines = cmdinvoicelines.ExecuteReader();
                            //    if (rdrinvoicelines.HasRows)
                            //    {
                            //        using (SqlCommand cmditem = new SqlCommand("Update [dbo].[AP_InvoiceLines] set InvoiceId=@InvoiceId,LineType=@LineType,AppliedGL_Account_Id=@AppliedGL_Account_Id,InventoryItem=@InventoryItem,ExpenseItem=@ExpenseItem,ExpenseDepartmentId=@ExpenseDepartmentId,InvoiceLineRef=@InvoiceLineRef,LineAmt=@LineAmt,LineQty=@LineQty,UOM=@UOM,QuickbookListId=@QuickbookListId,QuickbookFullName=@QuickbookFullName,QuickbookStatus=@QuickbookStatus where QuickbookStatus=1 and InvoiceId ='" + InsertedInvoiceId + "' and QuickbookListId='" + item.TxnLineID + "'", con))
                            //        {
                            //            cmditem.CommandType = CommandType.Text;
                            //            cmditem.Parameters.AddWithValue("@InvoiceId", InsertedInvoiceId);
                            //            cmditem.Parameters.AddWithValue("@LineType", "I");
                            //            cmditem.Parameters.AddWithValue("@AppliedGL_Account_Id", 0);
                            //            cmditem.Parameters.AddWithValue("@InventoryItem", item.ItemName);
                            //            cmditem.Parameters.AddWithValue("@ExpenseItem", item.ItemName);
                            //            cmditem.Parameters.AddWithValue("@ExpenseDepartmentId", DBNull.Value);
                            //            cmditem.Parameters.AddWithValue("@InvoiceLineRef", DBNull.Value);
                            //            cmditem.Parameters.AddWithValue("@LineAmt", item.Amount);
                            //            cmditem.Parameters.AddWithValue("@LineQty", item.Quantity);
                            //            cmditem.Parameters.AddWithValue("@UOM", DBNull.Value);
                            //            cmditem.Parameters.AddWithValue("@QuickbookListId", item.TxnLineID);
                            //            cmditem.Parameters.AddWithValue("@QuickbookFullName", item.ItemName);
                            //            cmditem.Parameters.AddWithValue("@QuickbookStatus", 1);
                            //            if (con.State == ConnectionState.Open)
                            //                con.Close();
                            //            con.Open();
                            //            cmditem.ExecuteNonQuery();
                            //            con.Close();
                            //        }
                            //    }
                            //    else
                            //    {
                            //        //using (SqlCommand cmditem = new SqlCommand("Insert into [dbo].[AP_InvoiceLines] (InvoiceId,LineType,AppliedGL_Account_Id,InventoryItem,ExpenseItem,ExpenseDepartmentId,InvoiceLineRef,LineAmt,LineQty,UOM,QuickbookListId,QuickbookFullName,QuickbookStatus)output INSERTED.InvoiceLineId VALUES (@InvoiceId,@LineType,@AppliedGL_Account_Id,@InventoryItem,@ExpenseItem,@ExpenseDepartmentId,@InvoiceLineRef,@LineAmt,@LineQty,@UOM,@QuickbookListId,@QuickbookFullName,@QuickbookStatus)", con))
                            //        //{
                            //        //    cmditem.CommandType = CommandType.Text;
                            //        //    cmditem.Parameters.AddWithValue("@InvoiceId", InsertedInvoiceId);
                            //        //    cmditem.Parameters.AddWithValue("@LineType", "I");
                            //        //    cmditem.Parameters.AddWithValue("@AppliedGL_Account_Id", 0);
                            //        //    cmditem.Parameters.AddWithValue("@InventoryItem", item.ItemName);
                            //        //    cmditem.Parameters.AddWithValue("@ExpenseItem", item.ItemName);
                            //        //    cmditem.Parameters.AddWithValue("@ExpenseDepartmentId", DBNull.Value);
                            //        //    cmditem.Parameters.AddWithValue("@InvoiceLineRef", DBNull.Value);
                            //        //    cmditem.Parameters.AddWithValue("@LineAmt", item.Amount);
                            //        //    cmditem.Parameters.AddWithValue("@LineQty", item.Quantity);
                            //        //    cmditem.Parameters.AddWithValue("@UOM", DBNull.Value);
                            //        //    cmditem.Parameters.AddWithValue("@QuickbookListId", item.TxnLineID);
                            //        //    cmditem.Parameters.AddWithValue("@QuickbookFullName", item.ItemName);
                            //        //    cmditem.Parameters.AddWithValue("@QuickbookStatus", 1);
                            //        //    if (con.State == ConnectionState.Open)
                            //        //        con.Close();
                            //        //    con.Open();
                            //        //    cmditem.ExecuteNonQuery();
                            //        //    con.Close();
                            //        //}
                            //    }
                            //}
                            using (SqlCommand cmddeleteinvoicelines = new SqlCommand("delete from [dbo].[AP_InvoiceLines] where QuickbookStatus=0 and InvoiceId = '" + InsertedInvoiceId + "' and QuickbookListId is null", con))
                            {
                                if (con.State == ConnectionState.Open)
                                    con.Close();
                                con.Open();
                                cmddeleteinvoicelines.ExecuteNonQuery();
                                con.Close();
                            }

                            foreach (var itemExpense in objbill.ListExpense)
                            {
                                SqlCommand cmdinvoicelines = new SqlCommand();
                                cmdinvoicelines.Connection = con;
                                cmdinvoicelines.CommandType = CommandType.Text;
                                cmdinvoicelines.CommandText = "SELECT * FROM [dbo].[AP_InvoiceLines] where QuickbookStatus=1 and InvoiceId ='" + InsertedInvoiceId + "' and QuickbookListId='" + itemExpense.TxnLineID + "'";
                                if (con.State == ConnectionState.Open)
                                    con.Close();
                                con.Open();
                                SqlDataReader rdrinvoicelines = cmdinvoicelines.ExecuteReader();
                                if (rdrinvoicelines.HasRows)
                                {
                                    //Update Current Expases detail
                                    using (SqlCommand cmditem = new SqlCommand("Update [dbo].[AP_InvoiceLines] set InvoiceId=@InvoiceId,LineType=@LineType,AppliedGL_Account_Id=@AppliedGL_Account_Id,InventoryItem=@InventoryItem,ExpenseItem=@ExpenseItem,ExpenseDepartmentId=@ExpenseDepartmentId,InvoiceLineRef=@InvoiceLineRef,LineAmt=@LineAmt,LineQty=@LineQty,UOM=@UOM,QuickbookListId=@QuickbookListId,QuickbookFullName=@QuickbookFullName,QuickbookStatus=@QuickbookStatus where QuickbookStatus=1 and InvoiceId ='" + InsertedInvoiceId + "' and QuickbookListId='" + itemExpense.TxnLineID + "'", con))
                                    {
                                        SqlCommand cmdGl = new SqlCommand();
                                        cmdGl.Connection = con;
                                        cmdGl.CommandType = CommandType.Text;
                                        cmdGl.CommandText = "SELECT * FROM [dbo].[GL_Accounts] where QuickbookListId ='" + itemExpense.ExpenseListID + "'";
                                        if (con.State == ConnectionState.Open)
                                            con.Close();
                                        con.Open();
                                        SqlDataReader rdrgl = cmdGl.ExecuteReader();
                                        if (rdrgl.HasRows)
                                        {
                                            while (rdrgl.Read())
                                            {
                                                string GL_Account_Id = rdrgl["GL_Account_Id"].ToString();
                                                objbill.GLAccountId = Convert.ToInt32(GL_Account_Id);
                                            }
                                        }
                                        cmditem.CommandType = CommandType.Text;
                                        cmditem.Parameters.AddWithValue("@InvoiceId", InsertedInvoiceId);
                                        cmditem.Parameters.AddWithValue("@LineType", "E");
                                        cmditem.Parameters.AddWithValue("@AppliedGL_Account_Id", objbill.GLAccountId);
                                        cmditem.Parameters.AddWithValue("@InventoryItem", itemExpense.ExpenseName);
                                        cmditem.Parameters.AddWithValue("@ExpenseItem", itemExpense.ExpenseName);
                                        cmditem.Parameters.AddWithValue("@ExpenseDepartmentId", DBNull.Value);
                                        cmditem.Parameters.AddWithValue("@InvoiceLineRef", itemExpense.Description);
                                        cmditem.Parameters.AddWithValue("@LineAmt", itemExpense.Amount);
                                        cmditem.Parameters.AddWithValue("@LineQty", DBNull.Value);
                                        cmditem.Parameters.AddWithValue("@UOM", DBNull.Value);
                                        cmditem.Parameters.AddWithValue("@QuickbookListId", itemExpense.TxnLineID);
                                        cmditem.Parameters.AddWithValue("@QuickbookFullName", itemExpense.ExpenseName);
                                        cmditem.Parameters.AddWithValue("@QuickbookStatus", 1);
                                        if (con.State == ConnectionState.Open)
                                            con.Close();
                                        con.Open();
                                        cmditem.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                                else
                                {
                                    //Insert Current Expases detail
                                    using (SqlCommand cmditem = new SqlCommand("Insert into [dbo].[AP_InvoiceLines] (InvoiceId,LineType,AppliedGL_Account_Id,InventoryItem,ExpenseItem,ExpenseDepartmentId,InvoiceLineRef,LineAmt,LineQty,UOM,QuickbookListId,QuickbookFullName,QuickbookStatus)output INSERTED.InvoiceLineId VALUES (@InvoiceId,@LineType,@AppliedGL_Account_Id,@InventoryItem,@ExpenseItem,@ExpenseDepartmentId,@InvoiceLineRef,@LineAmt,@LineQty,@UOM,@QuickbookListId,@QuickbookFullName,@QuickbookStatus)", con))
                                    {
                                        SqlCommand cmdGl = new SqlCommand();
                                        cmdGl.Connection = con;
                                        cmdGl.CommandType = CommandType.Text;
                                        cmdGl.CommandText = "SELECT * FROM [dbo].[GL_Accounts] where QuickbookListId ='" + itemExpense.ExpenseListID + "'";
                                        if (con.State == ConnectionState.Open)
                                            con.Close();
                                        con.Open();
                                        SqlDataReader rdrgl = cmdGl.ExecuteReader();
                                        if (rdrgl.HasRows)
                                        {
                                            while (rdrgl.Read())
                                            {
                                                string GL_Account_Id = rdrgl["GL_Account_Id"].ToString();
                                                objbill.GLAccountId = Convert.ToInt32(GL_Account_Id);
                                            }
                                        }
                                        cmditem.CommandType = CommandType.Text;
                                        cmditem.Parameters.AddWithValue("@InvoiceId", InsertedInvoiceId);
                                        cmditem.Parameters.AddWithValue("@LineType", "E");
                                        cmditem.Parameters.AddWithValue("@AppliedGL_Account_Id", objbill.GLAccountId);
                                        cmditem.Parameters.AddWithValue("@InventoryItem", itemExpense.ExpenseName);
                                        cmditem.Parameters.AddWithValue("@ExpenseItem", itemExpense.ExpenseName);
                                        cmditem.Parameters.AddWithValue("@ExpenseDepartmentId", DBNull.Value);
                                        if (itemExpense.Description != null)
                                            cmditem.Parameters.AddWithValue("@InvoiceLineRef", itemExpense.Description);
                                        else
                                            cmditem.Parameters.AddWithValue("@InvoiceLineRef", DBNull.Value);
                                        cmditem.Parameters.AddWithValue("@LineAmt", itemExpense.Amount);
                                        cmditem.Parameters.AddWithValue("@LineQty", DBNull.Value);
                                        cmditem.Parameters.AddWithValue("@UOM", DBNull.Value);
                                        cmditem.Parameters.AddWithValue("@QuickbookListId", itemExpense.TxnLineID);
                                        cmditem.Parameters.AddWithValue("@QuickbookFullName", itemExpense.ExpenseName);
                                        cmditem.Parameters.AddWithValue("@QuickbookStatus", 1);
                                        if (con.State == ConnectionState.Open)
                                            con.Close();
                                        con.Open();
                                        cmditem.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                            }

                            foreach (var itempayments in objbill.ListPayment)
                            {
                                SqlCommand cmdpaymentlines = new SqlCommand();
                                cmdpaymentlines.Connection = con;
                                cmdpaymentlines.CommandType = CommandType.Text;
                                cmdpaymentlines.CommandText = "SELECT * FROM [dbo].[AP_InvoicePayments] where QuickbookStatus=1 and InvoiceId ='" + InsertedInvoiceId + "' and QuickbookListId='" + itempayments.TxnLineID + "'";
                                if (con.State == ConnectionState.Open)
                                    con.Close();
                                con.Open();
                                SqlDataReader rdrpaymentlines = cmdpaymentlines.ExecuteReader();
                                if (rdrpaymentlines.HasRows)
                                {
                                    //Update Current Payment detail
                                    using (SqlCommand cmdpayment = new SqlCommand("Update [dbo].[AP_InvoicePayments] set InvoiceId=@InvoiceId,PaymentType=@PaymentType,RefNo=@RefNo,TxnDate=@TxnDate,Amount=@Amount,Note=@Note,LinkType=@LinkType,QuickbookListId=@QuickbookListId,QuickbookFullName=@QuickbookFullName,QuickbookStatus=@QuickbookStatus where QuickbookStatus=1 and InvoiceId ='" + InsertedInvoiceId + "' and QuickbookListId='" + itempayments.TxnLineID + "'", con))
                                    {
                                        cmdpayment.CommandType = CommandType.Text;
                                        cmdpayment.Parameters.AddWithValue("@InvoiceId", InsertedInvoiceId);
                                        cmdpayment.Parameters.AddWithValue("@PaymentType", itempayments.PaymentType);
                                        cmdpayment.Parameters.AddWithValue("@RefNo", itempayments.RefNumber);
                                        cmdpayment.Parameters.AddWithValue("@TxnDate", itempayments.TxnDate);
                                        cmdpayment.Parameters.AddWithValue("@Amount", itempayments.Amount);
                                        cmdpayment.Parameters.AddWithValue("@Note", DBNull.Value);
                                        cmdpayment.Parameters.AddWithValue("@LinkType", itempayments.LinkType);
                                        cmdpayment.Parameters.AddWithValue("@QuickbookListId", itempayments.TxnLineID);
                                        cmdpayment.Parameters.AddWithValue("@QuickbookFullName", DBNull.Value);
                                        cmdpayment.Parameters.AddWithValue("@QuickbookStatus", 1);
                                        if (con.State == ConnectionState.Open)
                                            con.Close();
                                        con.Open();
                                        cmdpayment.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                                else
                                {
                                    using (SqlCommand cmdpayment = new SqlCommand("Insert into [dbo].[AP_InvoicePayments] (InvoiceId,PaymentType,RefNo,TxnDate,Amount,LinkType,QuickbookListId,QuickbookFullName,QuickbookStatus)output INSERTED.InvoiceId VALUES (@InvoiceId,@PaymentType,@RefNo,@TxnDate,@Amount,@LinkType,@QuickbookListId,@QuickbookFullName,@QuickbookStatus)", con))
                                    {
                                        cmdpayment.CommandType = CommandType.Text;
                                        cmdpayment.Parameters.AddWithValue("@InvoiceId", InsertedInvoiceId);
                                        cmdpayment.Parameters.AddWithValue("@PaymentType", itempayments.PaymentType);
                                        cmdpayment.Parameters.AddWithValue("@RefNo", itempayments.RefNumber);
                                        cmdpayment.Parameters.AddWithValue("@TxnDate", Convert.ToDateTime(itempayments.TxnDate));
                                        cmdpayment.Parameters.AddWithValue("@Amount", itempayments.Amount);
                                        cmdpayment.Parameters.AddWithValue("@Note", DBNull.Value);
                                        cmdpayment.Parameters.AddWithValue("@LinkType", itempayments.LinkType);
                                        cmdpayment.Parameters.AddWithValue("@QuickbookListId", itempayments.TxnLineID);
                                        cmdpayment.Parameters.AddWithValue("@QuickbookFullName", DBNull.Value);
                                        cmdpayment.Parameters.AddWithValue("@QuickbookStatus", 1);
                                        if (con.State == ConnectionState.Open)
                                            con.Close();
                                        con.Open();
                                        cmdpayment.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                            }
                        }
                    }
                    InsertedInvoiceId = 0;
                }
                else
                {
                    SqlCommand cmdvendor = new SqlCommand();
                    cmdvendor.Connection = con;
                    cmdvendor.CommandType = CommandType.Text;
                    cmdvendor.CommandText = "SELECT * FROM [dbo].[AP_VendorMaster] where QuickbookListId ='" + objbill.VendorListID + "'";
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Open();
                    SqlDataReader rdrVendor = cmdvendor.ExecuteReader();
                    if (rdrVendor.HasRows)
                    {
                        while (rdrVendor.Read())
                        {
                            string VendorID = rdrVendor["VendorId"].ToString();
                            objbill.VendorId = Convert.ToInt32(VendorID);
                        }
                        SqlCommand cmdCC_GL_AccId = new SqlCommand();
                        cmdCC_GL_AccId.Connection = con;
                        cmdCC_GL_AccId.CommandType = CommandType.Text;
                        cmdCC_GL_AccId.CommandText = "SELECT * FROM [dbo].[GL_Accounts] where QuickbookListId ='" + objbill.CC_GL_AccId_ListID + "'";
                        if (con.State == ConnectionState.Open)
                            con.Close();
                        con.Open();
                        SqlDataReader rdrCC_GL_AccId = cmdCC_GL_AccId.ExecuteReader();
                        while (rdrCC_GL_AccId.Read())
                        {
                            string CC_GL_AccId = rdrCC_GL_AccId["GL_Account_Id"].ToString();
                            objbill.CC_GL_AccId = Convert.ToInt32(CC_GL_AccId);
                        }
                        //record not available then insert
                        using (SqlCommand cmdd = new SqlCommand("INSERT INTO [dbo].[AP_Invoices] (PropertyId ,VendorId,InvoiceNo,InvoiceDte,InvoiceRef,PO_No,GL_PostingDte,DueDte,InvoiceAmt,Approver,ApprovedDte,StatusId,Creator,CreatedDte,Notes,PaidByManualCheck,ManualCheckNo,ManualCheckDte,ManualCheckAmt,ExportedDte,ExportedBy,paymentDate,QuickbookListId,QuickbookFullName,QuickbookStatus,InvoiceType,CC_GL_AccId) output INSERTED.InvoiceId VALUES (@PropertyId,@VendorId,@InvoiceNo,@InvoiceDte,@InvoiceRef,@PO_No,@GL_PostingDte,@DueDte,@InvoiceAmt,@Approver,@ApprovedDte,@StatusId,@Creator,@CreatedDte,@Notes,@PaidByManualCheck,@ManualCheckNo,@ManualCheckDte,@ManualCheckAmt,@ExportedDte,@ExportedBy,@paymentDate,@QuickbookListId,@QuickbookFullName,@QuickbookStatus,@InvoiceType,@CC_GL_AccId)", con))
                        {
                            cmdd.CommandType = CommandType.Text;
                            cmdd.Parameters.AddWithValue("@PropertyId", PropertyId);
                            cmdd.Parameters.AddWithValue("@VendorId", objbill.VendorId);
                            cmdd.Parameters.AddWithValue("@InvoiceNo", objbill.TxnID);
                            cmdd.Parameters.AddWithValue("@InvoiceDte", Convert.ToDateTime(objbill.TxnDate));
                            cmdd.Parameters.AddWithValue("@InvoiceRef", objbill.RefNumber);
                            cmdd.Parameters.AddWithValue("@PO_No", DBNull.Value);
                            cmdd.Parameters.AddWithValue("@GL_PostingDte", Convert.ToDateTime(objbill.TxnDate));
                            if (objbill.DueDate != null)
                                cmdd.Parameters.AddWithValue("@DueDte", Convert.ToDateTime(objbill.DueDate));
                            else
                                cmdd.Parameters.AddWithValue("@DueDte", DBNull.Value);
                            cmdd.Parameters.AddWithValue("@InvoiceAmt", objbill.AmountDue);
                            cmdd.Parameters.AddWithValue("@Approver", DBNull.Value);
                            cmdd.Parameters.AddWithValue("@ApprovedDte", DBNull.Value);
                            if (objbill.IsPaid == "true")
                                cmdd.Parameters.AddWithValue("@StatusId", 8);
                            else if (objbill.IsPaid == "false")
                                cmdd.Parameters.AddWithValue("@StatusId", 7);
                            cmdd.Parameters.AddWithValue("@Creator", DBNull.Value);
                            cmdd.Parameters.AddWithValue("@CreatedDte", DateTime.Now);
                            cmdd.Parameters.AddWithValue("@Notes", objbill.Memo);
                            cmdd.Parameters.AddWithValue("@PaidByManualCheck", 0);
                            cmdd.Parameters.AddWithValue("@ManualCheckNo", DBNull.Value);
                            cmdd.Parameters.AddWithValue("@ManualCheckDte", DBNull.Value);
                            cmdd.Parameters.AddWithValue("@ManualCheckAmt", Convert.ToDecimal("0.00"));
                            cmdd.Parameters.AddWithValue("@ExportedDte", DBNull.Value);
                            cmdd.Parameters.AddWithValue("@ExportedBy", DBNull.Value);
                            if (objbill.PaymentDate != "")
                                cmdd.Parameters.AddWithValue("@paymentDate", Convert.ToDateTime(objbill.PaymentDate));
                            else
                                cmdd.Parameters.AddWithValue("@paymentDate", DBNull.Value);
                            cmdd.Parameters.AddWithValue("@QuickbookListId", objbill.TxnID);
                            cmdd.Parameters.AddWithValue("@QuickbookFullName", objbill.VendorName);
                            cmdd.Parameters.AddWithValue("@QuickbookStatus", 1);
                            cmdd.Parameters.AddWithValue("@InvoiceType", objbill.InvoiceType);
                            cmdd.Parameters.AddWithValue("@CC_GL_AccId", objbill.CC_GL_AccId);


                            if (con.State == ConnectionState.Open)
                                con.Close();
                            con.Open();
                            int InvoiceId = (int)cmdd.ExecuteScalar();
                            InsertedInvoiceId = InvoiceId;
                            con.Close();
                            //Insert Items Of Invoices
                            //foreach (var item in objbill.ListItems)
                            //{
                            //    using (SqlCommand cmditem = new SqlCommand("Insert into [dbo].[AP_InvoiceLines] (InvoiceId,LineType,AppliedGL_Account_Id,InventoryItem,ExpenseItem,ExpenseDepartmentId,InvoiceLineRef,LineAmt,LineQty,UOM,QuickbookListId,QuickbookFullName,QuickbookStatus)output INSERTED.InvoiceLineId VALUES (@InvoiceId,@LineType,@AppliedGL_Account_Id,@InventoryItem,@ExpenseItem,@ExpenseDepartmentId,@InvoiceLineRef,@LineAmt,@LineQty,@UOM,@QuickbookListId,@QuickbookFullName,@QuickbookStatus)", con))
                            //    {
                            //        cmditem.CommandType = CommandType.Text;
                            //        cmditem.Parameters.AddWithValue("@InvoiceId", InsertedInvoiceId);
                            //        cmditem.Parameters.AddWithValue("@LineType", "I");
                            //        cmditem.Parameters.AddWithValue("@AppliedGL_Account_Id", 0);
                            //        cmditem.Parameters.AddWithValue("@InventoryItem", item.ItemName);
                            //        cmditem.Parameters.AddWithValue("@ExpenseItem", item.ItemName);
                            //        cmditem.Parameters.AddWithValue("@ExpenseDepartmentId", DBNull.Value);
                            //        cmditem.Parameters.AddWithValue("@InvoiceLineRef", DBNull.Value);
                            //        cmditem.Parameters.AddWithValue("@LineAmt", item.Amount);
                            //        cmditem.Parameters.AddWithValue("@LineQty", item.Quantity);
                            //        cmditem.Parameters.AddWithValue("@UOM", DBNull.Value);
                            //        cmditem.Parameters.AddWithValue("@QuickbookListId", item.TxnLineID);
                            //        cmditem.Parameters.AddWithValue("@QuickbookFullName", item.ItemName);
                            //        cmditem.Parameters.AddWithValue("@QuickbookStatus", 1);
                            //        if (con.State == ConnectionState.Open)
                            //            con.Close();
                            //        con.Open();
                            //        cmditem.ExecuteNonQuery();
                            //        con.Close();
                            //    }
                            //}
                            //Insert Expenses Of Invoices

                            using (SqlCommand cmddeleteinvoicelines = new SqlCommand("delete from [dbo].[AP_InvoiceLines] where QuickbookStatus=0 and InvoiceId = '" + InsertedInvoiceId + "' and QuickbookListId is null", con))
                            {
                                if (con.State == ConnectionState.Open)
                                    con.Close();
                                con.Open();
                                cmddeleteinvoicelines.ExecuteNonQuery();
                                con.Close();
                            }

                            foreach (var itemExpense in objbill.ListExpense)
                            {
                                using (SqlCommand cmditem = new SqlCommand("Insert into [dbo].[AP_InvoiceLines] (InvoiceId,LineType,AppliedGL_Account_Id,InventoryItem,ExpenseItem,ExpenseDepartmentId,InvoiceLineRef,LineAmt,LineQty,UOM,QuickbookListId,QuickbookFullName,QuickbookStatus)output INSERTED.InvoiceLineId VALUES (@InvoiceId,@LineType,@AppliedGL_Account_Id,@InventoryItem,@ExpenseItem,@ExpenseDepartmentId,@InvoiceLineRef,@LineAmt,@LineQty,@UOM,@QuickbookListId,@QuickbookFullName,@QuickbookStatus)", con))
                                {
                                    SqlCommand cmdGl = new SqlCommand();
                                    cmdGl.Connection = con;
                                    cmdGl.CommandType = CommandType.Text;
                                    cmdGl.CommandText = "SELECT * FROM [dbo].[GL_Accounts] where QuickbookListId ='" + itemExpense.ExpenseListID + "'";
                                    if (con.State == ConnectionState.Open)
                                        con.Close();
                                    con.Open();
                                    SqlDataReader rdrgl = cmdGl.ExecuteReader();
                                    if (rdrgl.HasRows)
                                    {
                                        while (rdrgl.Read())
                                        {
                                            string GL_Account_Id = rdrgl["GL_Account_Id"].ToString();
                                            objbill.GLAccountId = Convert.ToInt32(GL_Account_Id);
                                        }
                                    }
                                    cmditem.CommandType = CommandType.Text;
                                    cmditem.Parameters.AddWithValue("@InvoiceId", InsertedInvoiceId);
                                    cmditem.Parameters.AddWithValue("@LineType", "E");
                                    cmditem.Parameters.AddWithValue("@AppliedGL_Account_Id", objbill.GLAccountId);
                                    cmditem.Parameters.AddWithValue("@InventoryItem", itemExpense.ExpenseName);
                                    cmditem.Parameters.AddWithValue("@ExpenseItem", itemExpense.ExpenseName);
                                    cmditem.Parameters.AddWithValue("@ExpenseDepartmentId", DBNull.Value);
                                    cmditem.Parameters.AddWithValue("@InvoiceLineRef", DBNull.Value);
                                    cmditem.Parameters.AddWithValue("@LineAmt", itemExpense.Amount);
                                    cmditem.Parameters.AddWithValue("@LineQty", DBNull.Value);
                                    cmditem.Parameters.AddWithValue("@UOM", DBNull.Value);
                                    cmditem.Parameters.AddWithValue("@QuickbookListId", itemExpense.TxnLineID);
                                    cmditem.Parameters.AddWithValue("@QuickbookFullName", itemExpense.ExpenseName);
                                    cmditem.Parameters.AddWithValue("@QuickbookStatus", 1);
                                    if (con.State == ConnectionState.Open)
                                        con.Close();
                                    con.Open();
                                    cmditem.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                            //Insert Payments Of Invoices
                            foreach (var itempayments in objbill.ListPayment)
                            {
                                //Insert Current Payment detail
                                using (SqlCommand cmdpayment = new SqlCommand("Insert into [dbo].[AP_InvoicePayments] (InvoiceId,PaymentType,RefNo,TxnDate,Amount,LinkType,QuickbookListId,QuickbookFullName,QuickbookStatus)output INSERTED.InvoiceId VALUES (@InvoiceId,@PaymentType,@RefNo,@TxnDate,@Amount,@LinkType,@QuickbookListId,@QuickbookFullName,@QuickbookStatus)", con))
                                {
                                    cmdpayment.CommandType = CommandType.Text;
                                    cmdpayment.Parameters.AddWithValue("@InvoiceId", InsertedInvoiceId);
                                    cmdpayment.Parameters.AddWithValue("@PaymentType", itempayments.PaymentType);
                                    cmdpayment.Parameters.AddWithValue("@RefNo", itempayments.RefNumber);
                                    cmdpayment.Parameters.AddWithValue("@TxnDate", itempayments.TxnDate);
                                    cmdpayment.Parameters.AddWithValue("@Amount", itempayments.Amount);
                                    cmdpayment.Parameters.AddWithValue("@Note", DBNull.Value);
                                    cmdpayment.Parameters.AddWithValue("@LinkType", itempayments.LinkType);
                                    cmdpayment.Parameters.AddWithValue("@QuickbookListId", itempayments.TxnLineID);
                                    cmdpayment.Parameters.AddWithValue("@QuickbookFullName", DBNull.Value);
                                    cmdpayment.Parameters.AddWithValue("@QuickbookStatus", 1);
                                    if (con.State == ConnectionState.Open)
                                        con.Close();
                                    con.Open();
                                    cmdpayment.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }

                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "InsertInvoiceQuickbook_to_system";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = objbill.InvoiceId;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }

            return InsertedInvoiceId;
        }
        // BillDetail.AmountDue, BillDetail.TxnID, BillDetail.TxnDate,objQuickbookLog.InvoiceId
        public static bool UpdateStatusInvoicePaymentsforCreditCardCharge(SqlConnection con, decimal? Amount, string txnID, DateTime? txnDate, int InvoiceID)
        {
            bool status = false;
            Amount = Amount * -1;
            SqlCommand cmdpaymentlines = new SqlCommand();
            cmdpaymentlines.Connection = con;
            cmdpaymentlines.CommandType = CommandType.Text;
            cmdpaymentlines.CommandText = "SELECT * FROM [dbo].[AP_InvoicePayments] where QuickbookStatus=1 and InvoiceId ='" + InvoiceID + "' and QuickbookListId='" + txnID + "'";
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            SqlDataReader rdrpaymentlines = cmdpaymentlines.ExecuteReader();
            if (rdrpaymentlines.HasRows)
            {
                //Update Current Payment detail
                using (SqlCommand cmdpayment = new SqlCommand("Update [dbo].[AP_InvoicePayments] set InvoiceId=@InvoiceId,PaymentType=@PaymentType,RefNo=@RefNo,TxnDate=@TxnDate,Amount=@Amount,Note=@Note,LinkType=@LinkType,QuickbookListId=@QuickbookListId,QuickbookFullName=@QuickbookFullName,QuickbookStatus=@QuickbookStatus where QuickbookStatus=1 and InvoiceId ='" + InvoiceID + "' and QuickbookListId='" + txnID + "'", con))
                {
                    cmdpayment.CommandType = CommandType.Text;
                    cmdpayment.Parameters.AddWithValue("@InvoiceId", InvoiceID);
                    cmdpayment.Parameters.AddWithValue("@PaymentType", "BillPaymentCreditCard");
                    cmdpayment.Parameters.AddWithValue("@RefNo", "");
                    cmdpayment.Parameters.AddWithValue("@TxnDate", txnDate);
                    cmdpayment.Parameters.AddWithValue("@Amount", Amount);
                    cmdpayment.Parameters.AddWithValue("@Note", DBNull.Value);
                    cmdpayment.Parameters.AddWithValue("@LinkType", "AMTTYPE");
                    cmdpayment.Parameters.AddWithValue("@QuickbookListId", "txnID");
                    cmdpayment.Parameters.AddWithValue("@QuickbookFullName", DBNull.Value);
                    cmdpayment.Parameters.AddWithValue("@QuickbookStatus", 1);
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Open();
                    cmdpayment.ExecuteNonQuery();
                    con.Close();
                    status = true;
                }
            }
            else
            {
                using (SqlCommand cmdpayment = new SqlCommand("Insert into [dbo].[AP_InvoicePayments] (InvoiceId,PaymentType,RefNo,TxnDate,Amount,LinkType,QuickbookListId,QuickbookFullName,QuickbookStatus)output INSERTED.InvoiceId VALUES (@InvoiceId,@PaymentType,@RefNo,@TxnDate,@Amount,@LinkType,@QuickbookListId,@QuickbookFullName,@QuickbookStatus)", con))
                {
                    cmdpayment.CommandType = CommandType.Text;
                    cmdpayment.Parameters.AddWithValue("@InvoiceId", InvoiceID);
                    cmdpayment.Parameters.AddWithValue("@PaymentType", "");
                    cmdpayment.Parameters.AddWithValue("@RefNo", "");
                    cmdpayment.Parameters.AddWithValue("@TxnDate", txnDate);
                    cmdpayment.Parameters.AddWithValue("@Amount", Amount);
                    cmdpayment.Parameters.AddWithValue("@Note", DBNull.Value);
                    cmdpayment.Parameters.AddWithValue("@LinkType", "AMTTYPE");
                    cmdpayment.Parameters.AddWithValue("@QuickbookListId", "txnID");
                    cmdpayment.Parameters.AddWithValue("@QuickbookFullName", DBNull.Value);
                    cmdpayment.Parameters.AddWithValue("@QuickbookStatus", 1);
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Open();
                    cmdpayment.ExecuteNonQuery();
                    con.Close();
                    status = true;
                }
            }
            return status = true;
        }
        public static ArrayList InsertXMLBlob_InvoicesQuery(SqlConnection con)
        {

            // Insert Invoice
            ArrayList req = new ArrayList();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT a.* ,[b].[Address1],[b].[Address2],[b].[City],[b].[State],[b].[PostalCode],[b].[QuickbookListId] as QBVendorListId,[b].[QuickbookFullName] as QBVendorName FROM [dbo].[AP_Invoices] as a inner join[dbo].[AP_VendorMaster] as b on a.[VendorId] = b.VendorId where a.QuickbookStatus = 0 and b.IsActive = 1 and a.QuickbookListId is null and a.PropertyId = " + PropertyId + " and a.InvoiceId not in (select distinct  InvoiceId from[dbo].[AP_InvoiceLines] where [AppliedGL_Account_Id] in (SELECT[GL_Account_Id] FROM [dbo].[GL_Accounts] where [QuickbookListId] is null and[GL_Account_Id] in (SELECT distinct  a.[AppliedGL_Account_Id] FROM [dbo].[AP_InvoiceLines] as a inner join[dbo].[AP_Invoices] as b on b.[InvoiceId] = a.[InvoiceId] inner join[dbo].[GL_Accounts] as c on c.[GL_Account_Id] = a.[AppliedGL_Account_Id] where a.[AppliedGL_Account_Id]  <> 0 )))";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string strrequestxml = "";
                    XmlDocument inputxmldoc;
                    strrequestxml = "";
                    inputxmldoc = null;
                    inputxmldoc = new XmlDocument();
                    inputxmldoc.AppendChild(inputxmldoc.CreateXmlDeclaration("1.0", null, null));
                    inputxmldoc.AppendChild(inputxmldoc.CreateProcessingInstruction("qbxml", "version=\"13.0\""));

                    XmlElement qbxml = inputxmldoc.CreateElement("QBXML");
                    inputxmldoc.AppendChild(qbxml);
                    XmlElement qbxmlmsgsrq = inputxmldoc.CreateElement("QBXMLMsgsRq");
                    qbxml.AppendChild(qbxmlmsgsrq);
                    qbxmlmsgsrq.SetAttribute("onError", "stopOnError");
                    XmlElement custaddrq = inputxmldoc.CreateElement("InvoiceAddRq");
                    qbxmlmsgsrq.AppendChild(custaddrq);
                    custaddrq.SetAttribute("requestID", rdr["InvoiceId"].ToString());

                    XmlElement customerAdd = inputxmldoc.CreateElement("InvoiceAdd");
                    custaddrq.AppendChild(customerAdd);

                    XmlElement CustomerRef = inputxmldoc.CreateElement("CustomerRef");
                    customerAdd.AppendChild(CustomerRef);
                    CustomerRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = rdr["QBVendorListId"].ToString();
                    CustomerRef.AppendChild(inputxmldoc.CreateElement("FullName")).InnerText = rdr["QBVendorName"].ToString();

                    customerAdd.AppendChild(inputxmldoc.CreateElement("TxnDate")).InnerText = Convert.ToDateTime(rdr["InvoiceDte"]).ToString("yyyy-MM-dd");
                    customerAdd.AppendChild(inputxmldoc.CreateElement("RefNumber")).InnerText = rdr["InvoiceRef"].ToString();

                    XmlElement customeraddress = inputxmldoc.CreateElement("BillAddress");
                    customerAdd.AppendChild(customeraddress);
                    customeraddress.AppendChild(inputxmldoc.CreateElement("Addr1")).InnerText = rdr["Address1"].ToString() + " " + rdr["Address2"].ToString();
                    customeraddress.AppendChild(inputxmldoc.CreateElement("City")).InnerText = rdr["City"].ToString();
                    customeraddress.AppendChild(inputxmldoc.CreateElement("State")).InnerText = rdr["State"].ToString();
                    customeraddress.AppendChild(inputxmldoc.CreateElement("PostalCode")).InnerText = rdr["PostalCode"].ToString();

                    customerAdd.AppendChild(inputxmldoc.CreateElement("PONumber")).InnerText = rdr["PO_No"].ToString();
                    customerAdd.AppendChild(inputxmldoc.CreateElement("DueDate")).InnerText = Convert.ToDateTime(rdr["DueDte"]).ToString("yyyy-MM-dd");
                    customerAdd.AppendChild(inputxmldoc.CreateElement("Memo")).InnerText = rdr["Notes"].ToString();
                    //customerAdd.AppendChild(inputxmldoc.CreateElement("IsPaid")).InnerText =1;
                    int InvoiceId = Convert.ToInt32(rdr["InvoiceId"]);
                    List<Item> objlistitem = GetItemsByInvoiceId(InvoiceId, con);
                    foreach (var item in objlistitem)
                    {
                        XmlElement InvoiceLineAdd = inputxmldoc.CreateElement("InvoiceLineAdd");
                        customerAdd.AppendChild(InvoiceLineAdd);
                        XmlElement ItemRef = inputxmldoc.CreateElement("ItemRef");
                        InvoiceLineAdd.AppendChild(ItemRef);
                        ItemRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = item.ListId;
                        ItemRef.AppendChild(inputxmldoc.CreateElement("FullName")).InnerText = item.ItemName;
                        InvoiceLineAdd.AppendChild(inputxmldoc.CreateElement("Quantity")).InnerText = Convert.ToString(item.Qty);
                        InvoiceLineAdd.AppendChild(inputxmldoc.CreateElement("Rate")).InnerText = Convert.ToString(item.Amount);
                    }
                    string input = inputxmldoc.OuterXml;
                    //step3: do the qbxmlrp request
                    strrequestxml = inputxmldoc.OuterXml;
                    // System.IO.File.AppendAllText(@"D:\Request.txt", strrequestxml + "\n\n");
                    req.Add(strrequestxml);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "InsertXMLBlob_InvoicesQuery";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }

            return req;
        }

        /// <summary>
        /// Insert logs for Gl Account is not available in Invoices    
        /// </summary>
        /// <param name="con"></param>
        /// <returns>return status (Log Inserted or not)</returns>
        public static bool InsertlogForGLAccountisnotavailableinInvoice(SqlConnection con)
        {
            // Insert Invoice Log
            bool InsertLog = false;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT a.* ,[b].[Address1],[b].[Address2],[b].[City],[b].[State],[b].[PostalCode],[b].[Fax],[b].[Phone1],[b].[QuickbookListId]  as QBVendorListId,[b].[QuickbookFullName] as QBVendorName FROM[dbo].[AP_Invoices] as a inner join[dbo].[AP_VendorMaster] as b on a.[VendorId] = b.VendorId where a.QuickbookStatus = 0 and b.QuickbookStatus = 1 and b.IsActive = 1 and a.QuickbookListId is null and a.statusid = 4 and a.PropertyId = " + PropertyId + " and a.InvoiceId  in (select distinct  InvoiceId from[dbo].[AP_InvoiceLines] where[AppliedGL_Account_Id] in (SELECT[GL_Account_Id] FROM [dbo].[GL_Accounts] where [QuickbookListId] is null and[GL_Account_Id] in (SELECT distinct  a.[AppliedGL_Account_Id] FROM [dbo].[AP_InvoiceLines] as a inner join[dbo].[AP_Invoices] as b on b.[InvoiceId] = a.[InvoiceId] inner join[dbo].[GL_Accounts] as c on c.[GL_Account_Id] = a.[AppliedGL_Account_Id] where a.[AppliedGL_Account_Id]  <> 0 )))";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    decimal InvoiceAmt = Convert.ToDecimal(rdr["InvoiceAmt"].ToString());
                    if (InvoiceAmt > 0)
                    {
                        QuickbookLog objQuickbookLog = new QuickbookLog();
                        objQuickbookLog.VendorId = 0;
                        objQuickbookLog.Status = "Quick Book GL-Accounts is not available in Invoice Items.";
                        objQuickbookLog.QuickbookListId = "";
                        objQuickbookLog.StatusDes = "Imported Not successfully";
                        objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                        objQuickbookLog.Process = "Inngenius to Quickbook";
                        objQuickbookLog.importdate = DateTime.Now;
                        objQuickbookLog.ResponseType = "Invoice";
                        objQuickbookLog.AccountSubTypeId = 0;
                        objQuickbookLog.InvoiceId = Convert.ToInt32(rdr["InvoiceId"].ToString());
                        objQuickbookLog.AccountId = 0;
                        objQuickbookLog.QuickbookFullName = "";
                        objQuickbookLog.VendorName = "";
                        //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
                        InsertLog = true;
                    }
                }
            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.ToString();
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "Inngenius to Quickbook";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "Invoice";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
                InsertLog = false;
            }
            return InsertLog;
        }

        /// <summary>
        /// Only Insert Approved Invoice and Imported GL Accounts Invoice
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        //public static ArrayList InsertXMLBlob_BillsQuery(SqlConnection con)
        //{
        //    // Insert Invoice
        //    ArrayList req = new ArrayList();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = con;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = "SELECT a.* ,[b].[Address1],[b].[Address2],[b].[City],[b].[State],[b].[PostalCode],[b].[Fax],[b].[Phone1],[b].[QuickbookListId] as QBVendorListId,[b].[QuickbookFullName] as QBVendorName FROM [dbo].[AP_Invoices] as a inner join[dbo].[AP_VendorMaster] as b on a.[VendorId] = b.VendorId where a.InvoiceType = 1 and a.QuickbookStatus = 0 and b.IsActive = 1 and a.QuickbookListId is null and a.statusid = 4 and a.PropertyId = " + PropertyId + " and a.InvoiceId not in (select distinct  InvoiceId from[dbo].[AP_InvoiceLines] where [AppliedGL_Account_Id] in (SELECT[GL_Account_Id] FROM [dbo].[GL_Accounts] where [QuickbookListId] is null and[GL_Account_Id] in (SELECT distinct  a.[AppliedGL_Account_Id] FROM [dbo].[AP_InvoiceLines] as a inner join[dbo].[AP_Invoices] as b on b.[InvoiceId] = a.[InvoiceId] inner join[dbo].[GL_Accounts] as c on c.[GL_Account_Id] = a.[AppliedGL_Account_Id] where a.[AppliedGL_Account_Id]  <> 0 )))";
        //    try
        //    {
        //        if (con.State == ConnectionState.Open)
        //            con.Close();
        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();
        //        while (rdr.Read())
        //        {
        //            decimal InvoiceAmt = Convert.ToDecimal(rdr["InvoiceAmt"].ToString());
        //            if (InvoiceAmt > 0)
        //            {
        //                string strrequestxml = "";
        //                XmlDocument inputxmldoc;
        //                strrequestxml = "";
        //                inputxmldoc = null;
        //                inputxmldoc = new XmlDocument();
        //                inputxmldoc.AppendChild(inputxmldoc.CreateXmlDeclaration("1.0", null, null));
        //                inputxmldoc.AppendChild(inputxmldoc.CreateProcessingInstruction("qbxml", "version=\"13.0\""));

        //                XmlElement qbxml = inputxmldoc.CreateElement("QBXML");
        //                inputxmldoc.AppendChild(qbxml);
        //                XmlElement qbxmlmsgsrq = inputxmldoc.CreateElement("QBXMLMsgsRq");
        //                qbxml.AppendChild(qbxmlmsgsrq);
        //                qbxmlmsgsrq.SetAttribute("onError", "stopOnError");
        //                XmlElement BillAddRq = inputxmldoc.CreateElement("BillAddRq");
        //                qbxmlmsgsrq.AppendChild(BillAddRq);
        //                BillAddRq.SetAttribute("requestID", rdr["InvoiceId"].ToString());
        //                XmlElement BillAdd = inputxmldoc.CreateElement("BillAdd");
        //                BillAddRq.AppendChild(BillAdd);
        //                XmlElement VendorRef = inputxmldoc.CreateElement("VendorRef");
        //                BillAdd.AppendChild(VendorRef);
        //                VendorRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = rdr["QBVendorListId"].ToString();
        //                BillAdd.AppendChild(inputxmldoc.CreateElement("TxnDate")).InnerText = Convert.ToDateTime(rdr["InvoiceDte"]).ToString("yyyy-MM-dd");
        //                BillAdd.AppendChild(inputxmldoc.CreateElement("DueDate")).InnerText = Convert.ToDateTime(rdr["DueDte"]).ToString("yyyy-MM-dd");
        //                BillAdd.AppendChild(inputxmldoc.CreateElement("RefNumber")).InnerText = rdr["InvoiceRef"].ToString();
        //                int InvoiceId = Convert.ToInt32(rdr["InvoiceId"]);
        //                string UploadedfileURL = GetUploadedFileNameByInvoiceId(InvoiceId, con);
        //                if (UploadedfileURL == null || UploadedfileURL == "")
        //                {
        //                    BillAdd.AppendChild(inputxmldoc.CreateElement("Memo")).InnerText = rdr["Notes"].ToString();
        //                }
        //                else
        //                {
        //                    BillAdd.AppendChild(inputxmldoc.CreateElement("Memo")).InnerText = rdr["Notes"].ToString() + " , " + UploadedfileURL.ToString();
        //                }
        //                List<GlAccount> objListExpense = GetGlAccountsByInvoiceId(InvoiceId, con);
        //                foreach (var item in objListExpense)
        //                {
        //                    XmlElement ExpenseLineAdd = inputxmldoc.CreateElement("ExpenseLineAdd");
        //                    BillAdd.AppendChild(ExpenseLineAdd);
        //                    XmlElement AccountRef = inputxmldoc.CreateElement("AccountRef");
        //                    ExpenseLineAdd.AppendChild(AccountRef);
        //                    AccountRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = item.ListId;
        //                    //AccountRef.AppendChild(inputxmldoc.CreateElement("FullName")).InnerText = item.ItemName;
        //                    ExpenseLineAdd.AppendChild(inputxmldoc.CreateElement("Amount")).InnerText = Convert.ToString(System.Math.Round(item.Amount, 2));
        //                    ExpenseLineAdd.AppendChild(inputxmldoc.CreateElement("Memo")).InnerText = item.Note;
        //                }
        //                //List<Item> objlistitem = GetItemsByInvoiceId(InvoiceId, con);
        //                //foreach (var item in objlistitem)
        //                //{
        //                //    XmlElement InvoiceLineAdd = inputxmldoc.CreateElement("ItemLineAdd");
        //                //    BillAdd.AppendChild(InvoiceLineAdd);
        //                //    XmlElement ItemRef = inputxmldoc.CreateElement("ItemRef");
        //                //    InvoiceLineAdd.AppendChild(ItemRef);
        //                //    ItemRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = item.ListId;
        //                //    //ItemRef.AppendChild(inputxmldoc.CreateElement("FullName")).InnerText = item.ItemName;
        //                //    InvoiceLineAdd.AppendChild(inputxmldoc.CreateElement("Quantity")).InnerText = Convert.ToString(item.Qty);
        //                //    InvoiceLineAdd.AppendChild(inputxmldoc.CreateElement("Amount")).InnerText = Convert.ToString(System.Math.Round(item.Amount, 2));
        //                //    //InvoiceLineAdd.AppendChild(inputxmldoc.CreateElement("Desc")).InnerText = Convert.ToString(item.Note);
        //                //}

        //                string input = inputxmldoc.OuterXml;
        //                //step3: do the qbxmlrp request
        //                strrequestxml = inputxmldoc.OuterXml;
        //                //System.IO.File.AppendAllText(@"D:\Request.txt", strrequestxml + "\n\n");
        //                req.Add(strrequestxml);
        //            }
        //        }
        //        con.Close();

        //    }
        //    catch (Exception ex)
        //    {
        //        QuickbookLog objQuickbookLog = new QuickbookLog();
        //        objQuickbookLog.VendorId = 0;
        //        objQuickbookLog.Status = ex.Message;
        //        objQuickbookLog.QuickbookListId = "";
        //        objQuickbookLog.StatusDes = "Imported Not successfully";
        //        objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
        //        objQuickbookLog.Process = "InsertXMLBlob_InvoicesQuery";
        //        objQuickbookLog.importdate = DateTime.Now;
        //        objQuickbookLog.ResponseType = "";
        //        objQuickbookLog.AccountSubTypeId = 0;
        //        objQuickbookLog.InvoiceId = 0;
        //        objQuickbookLog.AccountId = 0;
        //        objQuickbookLog.QuickbookFullName = "";
        //        objQuickbookLog.VendorName = "";
        //        //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
        //    }

        //    return req;
        //}


        /// <summary>
        /// Only Insert Approved Credit-Cardit Card Invoice and Imported GL Accounts Invoice
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        public static ArrayList InsertXMLBlob_VendorCreditCarditCardChargeBillsQuery(SqlConnection con)
        {

            // Insert Invoice
            ArrayList req = new ArrayList();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT a.* ,[b].[Address1],[b].[Address2],[b].[City],[b].[State],[b].[PostalCode],[b].[Fax],[b].[Phone1],[b].[QuickbookListId] as QBVendorListId,[b].[QuickbookFullName] as QBVendorName,g.QuickbookListId as GL_QuickbookListID,g.GL_Account_Name as GL_AccountName FROM [dbo].[AP_Invoices] as a inner join[dbo].[AP_VendorMaster] as b on a.[VendorId] = b.VendorId inner join [GL_Accounts] as g on a.CC_GL_AccId = g.GL_Account_Id  where a.InvoiceType = 2 and a.QuickbookStatus = 0 and b.IsActive = 1 and a.QuickbookListId is null and a.statusid = 4 and a.PropertyId = " + PropertyId + " and a.InvoiceId not in (select distinct  InvoiceId from[dbo].[AP_InvoiceLines] where [AppliedGL_Account_Id] in (SELECT[GL_Account_Id] FROM [dbo].[GL_Accounts] where [QuickbookListId] is null and[GL_Account_Id] in (SELECT distinct  a.[AppliedGL_Account_Id] FROM [dbo].[AP_InvoiceLines] as a inner join[dbo].[AP_Invoices] as b on b.[InvoiceId] = a.[InvoiceId] inner join[dbo].[GL_Accounts] as c on c.[GL_Account_Id] = a.[AppliedGL_Account_Id] where a.[AppliedGL_Account_Id]  <> 0 )))";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    decimal InvoiceAmt = Convert.ToDecimal(rdr["InvoiceAmt"].ToString());
                    if (InvoiceAmt > 0)
                    {
                        int InvoiceId = Convert.ToInt32(rdr["InvoiceId"]);
                        string strrequestxml = "";
                        XmlDocument inputxmldoc;
                        strrequestxml = "";
                        inputxmldoc = null;
                        inputxmldoc = new XmlDocument();
                        inputxmldoc.AppendChild(inputxmldoc.CreateXmlDeclaration("1.0", null, null));
                        inputxmldoc.AppendChild(inputxmldoc.CreateProcessingInstruction("qbxml", "version=\"13.0\""));

                        XmlElement qbxml = inputxmldoc.CreateElement("QBXML");
                        inputxmldoc.AppendChild(qbxml);
                        XmlElement qbxmlmsgsrq = inputxmldoc.CreateElement("QBXMLMsgsRq");
                        qbxml.AppendChild(qbxmlmsgsrq);
                        qbxmlmsgsrq.SetAttribute("onError", "stopOnError");
                        XmlElement BillAddRq = inputxmldoc.CreateElement("CreditCardChargeAddRq");
                        qbxmlmsgsrq.AppendChild(BillAddRq);
                        BillAddRq.SetAttribute("requestID", rdr["InvoiceId"].ToString());

                        XmlElement BillAdd = inputxmldoc.CreateElement("CreditCardChargeAdd");
                        BillAddRq.AppendChild(BillAdd);
                        //BillAdd.SetAttribute("defMacro", "MACROTYPE");

                        //GL_QuickbookListID
                        //GL_Account_Name
                        XmlElement AccountRef = inputxmldoc.CreateElement("AccountRef");
                        BillAdd.AppendChild(AccountRef);
                        AccountRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = rdr["GL_QuickbookListID"].ToString();
                        AccountRef.AppendChild(inputxmldoc.CreateElement("FullName")).InnerText = rdr["GL_AccountName"].ToString();


                        XmlElement VendorRef = inputxmldoc.CreateElement("PayeeEntityRef");
                        BillAdd.AppendChild(VendorRef);
                        VendorRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = rdr["QBVendorListId"].ToString();
                        BillAdd.AppendChild(inputxmldoc.CreateElement("TxnDate")).InnerText = Convert.ToDateTime(rdr["InvoiceDte"]).ToString("yyyy-MM-dd");
                        BillAdd.AppendChild(inputxmldoc.CreateElement("RefNumber")).InnerText = rdr["InvoiceRef"].ToString();
                        // BillAdd.AppendChild(inputxmldoc.CreateElement("Amount")).InnerText = rdr["InvoiceAmt"].ToString();


                        string UploadedfileURL = GetUploadedFileNameByInvoiceId(InvoiceId, con);
                        if (UploadedfileURL == null || UploadedfileURL == "")
                        {
                            BillAdd.AppendChild(inputxmldoc.CreateElement("Memo")).InnerText = rdr["Notes"].ToString();
                        }
                        else
                        {
                            BillAdd.AppendChild(inputxmldoc.CreateElement("Memo")).InnerText = rdr["Notes"].ToString() + " , " + UploadedfileURL.ToString();
                        }

                        //List<Item> objlistitem = GetItemsByInvoiceId(InvoiceId, con);
                        //foreach (var item in objlistitem)
                        //{
                        //    XmlElement InvoiceLineAdd = inputxmldoc.CreateElement("ItemLineAdd");
                        //    BillAdd.AppendChild(InvoiceLineAdd);
                        //    XmlElement ItemRef = inputxmldoc.CreateElement("ItemRef");
                        //    InvoiceLineAdd.AppendChild(ItemRef);
                        //    ItemRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = item.ListId;
                        //    //ItemRef.AppendChild(inputxmldoc.CreateElement("FullName")).InnerText = item.ItemName;
                        //    InvoiceLineAdd.AppendChild(inputxmldoc.CreateElement("Quantity")).InnerText = Convert.ToString(item.Qty);
                        //    InvoiceLineAdd.AppendChild(inputxmldoc.CreateElement("Amount")).InnerText = Convert.ToString(System.Math.Round(item.Amount, 2));
                        //}

                        List<GlAccount> objlistglaccount = GetGlAccountsByInvoiceId(InvoiceId, con);
                        foreach (var item in objlistglaccount)
                        {
                            XmlElement ExpenseLineAdd = inputxmldoc.CreateElement("ExpenseLineAdd");
                            BillAdd.AppendChild(ExpenseLineAdd);
                            XmlElement eAccountRef = inputxmldoc.CreateElement("AccountRef");
                            ExpenseLineAdd.AppendChild(eAccountRef);
                            eAccountRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = item.ListId;
                            eAccountRef.AppendChild(inputxmldoc.CreateElement("FullName")).InnerText = item.ItemName;
                            ExpenseLineAdd.AppendChild(inputxmldoc.CreateElement("Amount")).InnerText = Convert.ToString(System.Math.Round(item.Amount, 2));
                            ExpenseLineAdd.AppendChild(inputxmldoc.CreateElement("Memo")).InnerText = item.Note;
                        }
                        string input = inputxmldoc.OuterXml;
                        //step3: do the qbxmlrp request
                        strrequestxml = inputxmldoc.OuterXml;
                        // System.IO.File.AppendAllText(@"D:\Request.txt", strrequestxml + "\n\n");
                        req.Add(strrequestxml);
                    }
                }
                con.Close();

            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "InsertXMLBlob_InvoicesQuery";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }

            return req;
        }


        /// <summary>
        /// Only Insert Approved Invoice and Imported GL Accounts Invoice
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        public static ArrayList InsertXMLBlob_VendorCreditBillsQuery(SqlConnection con)
        {
            // Insert Invoice
            ArrayList req = new ArrayList();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT a.* ,[b].[Address1],[b].[Address2],[b].[City],[b].[State],[b].[PostalCode],[b].[Fax],[b].[Phone1],[b].[QuickbookListId] as QBVendorListId,[b].[QuickbookFullName] as QBVendorName FROM [dbo].[AP_Invoices] as a inner join[dbo].[AP_VendorMaster] as b on a.[VendorId] = b.VendorId where a.InvoiceType = 1 and a.QuickbookStatus = 0 and b.IsActive = 1 and a.QuickbookListId is null and a.statusid = 4 and a.PropertyId = " + PropertyId + " and a.InvoiceId not in (select distinct  InvoiceId from[dbo].[AP_InvoiceLines] where [AppliedGL_Account_Id] in (SELECT[GL_Account_Id] FROM [dbo].[GL_Accounts] where [QuickbookListId] is null and[GL_Account_Id] in (SELECT distinct  a.[AppliedGL_Account_Id] FROM [dbo].[AP_InvoiceLines] as a inner join[dbo].[AP_Invoices] as b on b.[InvoiceId] = a.[InvoiceId] inner join[dbo].[GL_Accounts] as c on c.[GL_Account_Id] = a.[AppliedGL_Account_Id] where a.[AppliedGL_Account_Id]  <> 0 )))";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    decimal InvoiceAmt = Convert.ToDecimal(rdr["InvoiceAmt"].ToString());
                    if (InvoiceAmt < 0)
                    {
                        string strrequestxml = "";
                        XmlDocument inputxmldoc;
                        strrequestxml = "";
                        inputxmldoc = null;
                        inputxmldoc = new XmlDocument();
                        inputxmldoc.AppendChild(inputxmldoc.CreateXmlDeclaration("1.0", null, null));
                        inputxmldoc.AppendChild(inputxmldoc.CreateProcessingInstruction("qbxml", "version=\"13.0\""));

                        XmlElement qbxml = inputxmldoc.CreateElement("QBXML");
                        inputxmldoc.AppendChild(qbxml);
                        XmlElement qbxmlmsgsrq = inputxmldoc.CreateElement("QBXMLMsgsRq");
                        qbxml.AppendChild(qbxmlmsgsrq);
                        qbxmlmsgsrq.SetAttribute("onError", "stopOnError");
                        XmlElement VendorCreditBillAddRq = inputxmldoc.CreateElement("VendorCreditAddRq");
                        qbxmlmsgsrq.AppendChild(VendorCreditBillAddRq);
                        VendorCreditBillAddRq.SetAttribute("requestID", rdr["InvoiceId"].ToString());
                        XmlElement VendorCreditAdd = inputxmldoc.CreateElement("VendorCreditAdd");
                        VendorCreditBillAddRq.AppendChild(VendorCreditAdd);
                        XmlElement VendorRef = inputxmldoc.CreateElement("VendorRef");
                        VendorCreditAdd.AppendChild(VendorRef);

                        VendorRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = rdr["QBVendorListId"].ToString();
                        VendorRef.AppendChild(inputxmldoc.CreateElement("FullName")).InnerText = rdr["QBVendorName"].ToString();
                        VendorCreditAdd.AppendChild(inputxmldoc.CreateElement("TxnDate")).InnerText = Convert.ToDateTime(rdr["InvoiceDte"]).ToString("yyyy-MM-dd");
                        int InvoiceId = Convert.ToInt32(rdr["InvoiceId"]);
                        string UploadedfileURL = GetUploadedFileNameByInvoiceId(InvoiceId, con);
                        if (UploadedfileURL == null || UploadedfileURL == "")
                        {
                            VendorCreditAdd.AppendChild(inputxmldoc.CreateElement("Memo")).InnerText = rdr["Notes"].ToString();
                        }
                        else
                        {
                            VendorCreditAdd.AppendChild(inputxmldoc.CreateElement("Memo")).InnerText = rdr["Notes"].ToString() + " , " + UploadedfileURL.ToString();
                        }

                        List<GlAccount> objlistglaccount = GetGlAccountsByInvoiceId(InvoiceId, con);
                        foreach (var item in objlistglaccount)
                        {
                            XmlElement ExpenseLineAdd = inputxmldoc.CreateElement("ExpenseLineAdd");
                            VendorCreditAdd.AppendChild(ExpenseLineAdd);
                            XmlElement eAccountRef = inputxmldoc.CreateElement("AccountRef");
                            ExpenseLineAdd.AppendChild(eAccountRef);
                            eAccountRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = item.ListId;
                            eAccountRef.AppendChild(inputxmldoc.CreateElement("FullName")).InnerText = item.ItemName;
                            ExpenseLineAdd.AppendChild(inputxmldoc.CreateElement("Amount")).InnerText = Convert.ToString(System.Math.Round(item.Amount, 2));
                            ExpenseLineAdd.AppendChild(inputxmldoc.CreateElement("Memo")).InnerText = item.Note;
                        }

                        //List<Item> objlistitem = GetItemsByInvoiceId(InvoiceId, con);
                        //foreach (var item in objlistitem)
                        //{
                        //    XmlElement InvoiceLineAdd = inputxmldoc.CreateElement("ItemLineAdd");
                        //    VendorCreditAdd.AppendChild(InvoiceLineAdd);
                        //    XmlElement ItemRef = inputxmldoc.CreateElement("ItemRef");
                        //    InvoiceLineAdd.AppendChild(ItemRef);
                        //    ItemRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = item.ListId;
                        //    //ItemRef.AppendChild(inputxmldoc.CreateElement("FullName")).InnerText = item.ItemName;
                        //    InvoiceLineAdd.AppendChild(inputxmldoc.CreateElement("Quantity")).InnerText = Convert.ToString(item.Qty);
                        //    InvoiceLineAdd.AppendChild(inputxmldoc.CreateElement("Amount")).InnerText = Convert.ToString(System.Math.Round(item.Amount, 2));
                        //}

                        string input = inputxmldoc.OuterXml;
                        //step3: do the qbxmlrp request
                        strrequestxml = inputxmldoc.OuterXml;
                        // System.IO.File.AppendAllText(@"D:\Request.txt", strrequestxml + "\n\n");
                        req.Add(strrequestxml);
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "InsertXMLBlob_InvoicesQuery";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }

            return req;
        }



        public static ArrayList InsertXMLBlob_ItemQuery(SqlConnection con)
        {
            // Insert Vendors
            ArrayList req = new ArrayList();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "select e.InvoiceLineId ,e.ItemName ,e.QuickbookListId, e.QuickbookFullName from (SELECT  a.[InvoiceLineId] as InvoiceLineId ,a.[InventoryItem] as ItemName ,(SELECT [QuickbookListId] FROM [dbo].[QBResponseStatus] where  [QuickbookFullName]= 'Payroll Expenses') as QuickbookListId,(SELECT [QuickbookFullName] FROM [dbo].[QBResponseStatus] where  [QuickbookFullName]= 'Payroll Expenses') as QuickbookFullName FROM [dbo].[AP_InvoiceLines] as a inner join [dbo].[AP_Invoices] as b on b.[InvoiceId] = a.[InvoiceId] where a.[InventoryItem] is not null and a.[InventoryItem] <> '' and b.PropertyId = "+ PropertyId +" union SELECT  a.[InvoiceLineId] as InvoiceLineId, a.[ExpenseItem] as ItemName,d.[QuickbookListId]  as QuickbookListId ,d.[QuickbookFullName] as QuickbookFullName FROM [dbo].[AP_InvoiceLines] as a inner join [dbo].[AP_Invoices] as b on b.[InvoiceId] = a.[InvoiceId] inner join [dbo].[GL_Accounts] as c on c.[GL_Account_Id] =a.[AppliedGL_Account_Id] left join [dbo].[QBResponseStatus] as d on d.[QuickbookFullName] = c.[GL_Account_Name] where a.[ExpenseItem] is not null and a.[ExpenseItem] <> '' and b.PropertyId = "+ PropertyId +" and d.[QuickbookListId] is not null and d.[QuickbookFullName] is not null) as e where  e.ItemName not in(SELECT [ItemName] FROM [dbo].[QBItemResponse])";
            cmd.CommandText = "select top 100 * from tblmstItems where QBItemStatus=0 and QBListItemId is null";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string strrequestxml = "";
                    XmlDocument inputxmldoc;
                    strrequestxml = "";
                    inputxmldoc = null;
                    inputxmldoc = new XmlDocument();
                    inputxmldoc.AppendChild(inputxmldoc.CreateXmlDeclaration("1.0", null, null));
                    inputxmldoc.AppendChild(inputxmldoc.CreateProcessingInstruction("qbxml", "version=\"13.0\""));

                    XmlElement qbxml = inputxmldoc.CreateElement("QBXML");
                    inputxmldoc.AppendChild(qbxml);
                    XmlElement qbxmlmsgsrq = inputxmldoc.CreateElement("QBXMLMsgsRq");
                    qbxml.AppendChild(qbxmlmsgsrq);
                    qbxmlmsgsrq.SetAttribute("onError", "stopOnError");
                    XmlElement custaddrq = inputxmldoc.CreateElement("ItemInventoryAddRq");
                    qbxmlmsgsrq.AppendChild(custaddrq);
                    custaddrq.SetAttribute("requestID", rdr["intItemId"].ToString());

                    XmlElement customerAdd = inputxmldoc.CreateElement("ItemInventoryAdd");
                    custaddrq.AppendChild(customerAdd);
                    string ItemName = Convert.ToString(rdr["txtItemName"]);
                    customerAdd.AppendChild(inputxmldoc.CreateElement("Name")).InnerText = ItemName.Length > 20 ? ItemName.Substring(0, 20) : ItemName;
                    customerAdd.AppendChild(inputxmldoc.CreateElement("Quantity")).InnerText = rdr["intQuantity"].ToString();
                    customerAdd.AppendChild(inputxmldoc.CreateElement("Rate")).InnerText = rdr["fltItemPrice"].ToString();

                    XmlElement IncomeAccountRef = inputxmldoc.CreateElement("IncomeAccountRef");
                    customerAdd.AppendChild(IncomeAccountRef);
                    IncomeAccountRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = "8000002B-1548860425".ToString();
                    IncomeAccountRef.AppendChild(inputxmldoc.CreateElement("FullName")).InnerText = "Income Test Account 15-02-2019".ToString();

                    XmlElement COGSAccountRef = inputxmldoc.CreateElement("COGSAccountRef");
                    customerAdd.AppendChild(COGSAccountRef);
                    COGSAccountRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = "80000029-1548149102".ToString();
                    COGSAccountRef.AppendChild(inputxmldoc.CreateElement("FullName")).InnerText = "Cost of Good Sales Test Account 15-02-2019".ToString();

                    XmlElement AssetAccountRef = inputxmldoc.CreateElement("AssetAccountRef");
                    customerAdd.AppendChild(AssetAccountRef);
                    AssetAccountRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = "8000002D-1548861151".ToString();
                    AssetAccountRef.AppendChild(inputxmldoc.CreateElement("FullName")).InnerText = "Asset Account Test 15-02-2019".ToString();

                    string input = inputxmldoc.OuterXml;
                    //string input = inputxmldoc.OuterXml;
                    //step3: do the qbxmlrp request
                    strrequestxml = inputxmldoc.OuterXml;
                    // System.IO.File.AppendAllText(@"D:\Request.txt", strrequestxml + "\n\n");
                    req.Add(strrequestxml);

                }
                con.Close();

            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "InsertXMLBlob_ItemQuery";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }
            return req;
        }


        public static List<GlAccount> GetGlAccountsByInvoiceId(int InvoiceId, SqlConnection con)
        {
            List<GlAccount> objlistitem = new List<GlAccount>();
            SqlCommand cmd = new SqlCommand();
            string cs1 = ConfigurationManager.ConnectionStrings["InnGeniusEntities"].ConnectionString;
            SqlConnection conReader = new SqlConnection(cs1);
            cmd.Connection = conReader;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select ga.QuickbookListId,ga.GL_Account_Name,al.LineAmt,al.LineQty,al.InvoiceLineRef from GL_Accounts ga inner join[dbo].[AP_InvoiceLines] al on ga.GL_Account_Id=al.AppliedGL_Account_Id where[InvoiceId] = " + InvoiceId + "  and al.LineType = 'E' and ga.PropertyId =" + PropertyId + "";
            try
            {
                if (conReader.State == ConnectionState.Open)
                    conReader.Close();
                conReader.Open();
                SqlDataReader rdr1 = cmd.ExecuteReader();
                while (rdr1.Read())
                {
                    GlAccount objitem = new GlAccount();
                    objitem.ListId = rdr1["QuickbookListId"].ToString();
                    objitem.ItemName = rdr1["GL_Account_Name"].ToString();
                    objitem.Qty = Convert.ToInt32(rdr1["LineQty"]);
                    objitem.Amount = Convert.ToDecimal(rdr1["LineAmt"]);
                    objitem.Note = rdr1["InvoiceLineRef"].ToString();
                    //InvoiceLineRef
                    objlistitem.Add(objitem);
                }
                conReader.Close();

            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "GetGlAccountsByInvoiceId";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }

            return objlistitem;
        }

        public static List<Item> GetItemsByInvoiceId(int InvoiceId, SqlConnection con)
        {
            // Insert Vendors
            List<Item> objlistitem = new List<Item>();
            SqlCommand cmd = new SqlCommand();
            string cs1 = ConfigurationManager.ConnectionStrings["InnGeniusEntities"].ConnectionString;
            SqlConnection conReader = new SqlConnection(cs1);
            cmd.Connection = conReader;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select  top 100 * from tblmstorders  where  QBInvoiceStatus=0 and QBInvoiceListId is NULL ";
            try
            {
                if (conReader.State == ConnectionState.Open)
                    conReader.Close();
                conReader.Open();
                SqlDataReader rdr1 = cmd.ExecuteReader();
                while (rdr1.Read())
                {
                    Item objitem = new Item();
                    objitem.ListId = rdr1["QBItemListID"].ToString();
                    objitem.ItemName = rdr1["ItemName"].ToString();
                    objitem.Qty = Convert.ToInt32(rdr1["LineQty"]);
                    objitem.Amount = Convert.ToDecimal(rdr1["LineAmt"]);
                    objitem.Note = rdr1["InvoiceLineRef"].ToString();
                    objlistitem.Add(objitem);
                }
                conReader.Close();
            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "GetItemsByInvoiceId";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }

            return objlistitem;
        }
        public static string GetUploadedFileNameByInvoiceId(int InvoiceId, SqlConnection con)
        {
            string FileName = null;
            try
            {

                SqlCommand cmd = new SqlCommand();
                string cs1 = ConfigurationManager.ConnectionStrings["InnGeniusEntities"].ConnectionString;
                SqlConnection conReader = new SqlConnection(cs1);
                cmd.Connection = conReader;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select [b].[docFileName] as filename from [dbo].[AP_Invoices] as a inner join [dbo].[AP_Documents] as b on a.[InvoiceId] = b.InvoiceId where a.InvoiceId=" + InvoiceId;
                if (conReader.State == ConnectionState.Open)
                    conReader.Close();
                conReader.Open();
                SqlDataReader rdr1 = cmd.ExecuteReader();
                while (rdr1.Read())
                {
                    FileName = "http://ogiapi.inngeniusonline.com/Media/InvoiceDocument/" + rdr1["filename"].ToString();
                }
                conReader.Close();
            }
            catch (Exception)
            {
                FileName = null;
            }
            return FileName;
        }
        public static bool InsertLogAccountNotAvailable(SqlConnection con)
        {
            // Insert Vendors
            bool IsAccountunavailable = false;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT [GL_Account_Id],[GL_Account_Name] FROM [dbo].[GL_Accounts] where [QuickbookListId] is null and[GL_Account_Id] in (SELECT  distinct a.[AppliedGL_Account_Id] FROM[dbo].[AP_InvoiceLines] as a inner join[dbo].[AP_Invoices] as b on b.[InvoiceId] = a.[InvoiceId] inner join[dbo].[GL_Accounts] as c on c.[GL_Account_Id] =a.[AppliedGL_Account_Id] where a.[AppliedGL_Account_Id]  <> 0 )";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    QuickbookLog objQuickbookLog = new QuickbookLog();
                    objQuickbookLog.VendorId = 0;
                    objQuickbookLog.Status = "Need to add Account in Quickbook";
                    objQuickbookLog.QuickbookListId = "";
                    objQuickbookLog.VendorName = "";
                    objQuickbookLog.QuickbookFullName = rdr["GL_Account_Name"].ToString();
                    objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                    objQuickbookLog.Process = "Quickbook to Inngenius";
                    objQuickbookLog.StatusDes = "Imported Not successfully";
                    objQuickbookLog.importdate = DateTime.Now;
                    objQuickbookLog.ResponseType = "Account";
                    objQuickbookLog.AccountSubTypeId = 0;
                    objQuickbookLog.InvoiceId = 0;
                    objQuickbookLog.AccountId = Convert.ToInt32(rdr["GL_Account_Id"]);
                    //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
                    string cs1 = ConfigurationManager.ConnectionStrings["InnGeniusEntities"].ConnectionString;
                    SqlConnection conReader = new SqlConnection(cs1);
                    using (SqlCommand cmdd = new SqlCommand("INSERT INTO [dbo].[qbresponsestatus] (propertyid,vendorid,vendorname,AccountId,AccountSubTypeId,InvoiceId,process,status,statusdes,importdate,QuickbookListId,QuickbookFullName,ResponseType) VALUES (@propertyid,@vendorid,@vendorname,@AccountId,@AccountSubTypeId,@InvoiceId,@process,@status,@statusdes,@importdate,@QuickbookListId,@QuickbookFullName,@ResponseType)", conReader))
                    {
                        cmdd.Connection = conReader;
                        cmdd.CommandType = CommandType.Text;
                        cmdd.Parameters.AddWithValue("@PropertyId", PropertyId);
                        cmdd.Parameters.AddWithValue("@vendorid", objQuickbookLog.VendorId);
                        cmdd.Parameters.AddWithValue("@vendorname", objQuickbookLog.VendorName);
                        cmdd.Parameters.AddWithValue("@AccountId", objQuickbookLog.AccountId);
                        cmdd.Parameters.AddWithValue("@AccountSubTypeId", objQuickbookLog.AccountSubTypeId);
                        cmdd.Parameters.AddWithValue("@InvoiceId", objQuickbookLog.InvoiceId);
                        cmdd.Parameters.AddWithValue("@process", objQuickbookLog.Process);
                        cmdd.Parameters.AddWithValue("@status", objQuickbookLog.Status);
                        cmdd.Parameters.AddWithValue("@statusdes", objQuickbookLog.StatusDes);
                        cmdd.Parameters.AddWithValue("@importdate", DateTime.Now);
                        cmdd.Parameters.AddWithValue("@QuickbookListId", objQuickbookLog.QuickbookListId);
                        cmdd.Parameters.AddWithValue("@QuickbookFullName", objQuickbookLog.QuickbookFullName);
                        cmdd.Parameters.AddWithValue("@ResponseType", objQuickbookLog.ResponseType);

                        if (conReader.State == ConnectionState.Open)
                            conReader.Close();
                        conReader.Open();
                        int rowsAffected = cmdd.ExecuteNonQuery();
                        conReader.Close();
                    }

                    IsAccountunavailable = true;
                }
                con.Close();

            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "InsertLogAccountNotAvailable";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }

            return IsAccountunavailable;
        }
        public static bool InsertQuickbookLog(SqlConnection con, QuickbookLog objQuickbookLog)
        {
            bool IsInsertedLog = false;
            try
            {
                using (SqlCommand cmdd = new SqlCommand("INSERT INTO [dbo].[qbresponsestatus] (propertyid,vendorid,vendorname,AccountId,AccountSubTypeId,InvoiceId,process,status,statusdes,importdate,QuickbookListId,QuickbookFullName,ResponseType) VALUES (@propertyid,@vendorid,@vendorname,@AccountId,@AccountSubTypeId,@InvoiceId,@process,@status,@statusdes,@importdate,@QuickbookListId,@QuickbookFullName,@ResponseType)", con))
                {
                    SqlParameter vendorname = new SqlParameter("@vendorname", DBNull.Value);
                    if (objQuickbookLog.VendorName != null)
                        vendorname = new SqlParameter("@vendorname", objQuickbookLog.VendorName);
                    SqlParameter QuickbookListId = new SqlParameter("@QuickbookListId", DBNull.Value);
                    if (objQuickbookLog.QuickbookListId != null)
                        QuickbookListId = new SqlParameter("@QuickbookListId", objQuickbookLog.QuickbookListId);
                    SqlParameter QuickbookFullName = new SqlParameter("@QuickbookFullName", DBNull.Value);
                    if (objQuickbookLog.QuickbookFullName != null)
                        QuickbookFullName = new SqlParameter("@QuickbookFullName", objQuickbookLog.QuickbookFullName);

                    cmdd.CommandType = CommandType.Text;
                    cmdd.Parameters.AddWithValue("@PropertyId", PropertyId);
                    cmdd.Parameters.AddWithValue("@vendorid", objQuickbookLog.VendorId);
                    cmdd.Parameters.Add(vendorname);
                    cmdd.Parameters.AddWithValue("@AccountId", objQuickbookLog.AccountId);
                    cmdd.Parameters.AddWithValue("@AccountSubTypeId", objQuickbookLog.AccountSubTypeId);
                    cmdd.Parameters.AddWithValue("@InvoiceId", objQuickbookLog.InvoiceId);
                    cmdd.Parameters.AddWithValue("@process", objQuickbookLog.Process);
                    cmdd.Parameters.AddWithValue("@status", objQuickbookLog.Status);
                    cmdd.Parameters.AddWithValue("@statusdes", objQuickbookLog.StatusDes);
                    cmdd.Parameters.AddWithValue("@importdate", DateTime.Now);
                    cmdd.Parameters.Add(QuickbookListId);
                    cmdd.Parameters.Add(QuickbookFullName);
                    //cmdd.Parameters.AddWithValue("@QuickbookListId", objQuickbookLog.QuickbookListId);
                    //cmdd.Parameters.AddWithValue("@QuickbookFullName", objQuickbookLog.QuickbookFullName);
                    cmdd.Parameters.AddWithValue("@ResponseType", objQuickbookLog.ResponseType);

                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Open();
                    int rowsAffected = cmdd.ExecuteNonQuery();
                    con.Close();
                    if (rowsAffected > 0)
                        IsInsertedLog = true;

                    return IsInsertedLog;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //public static bool InsertJEQuickbookLog(SqlConnection con, QuickbookLog objQuickbookLog)
        //{
        //    bool IsInsertedLog = false;
        //    try
        //    {
        //        using (SqlCommand cmdd = new SqlCommand("INSERT INTO [dbo].[qbresponsestatus] (propertyid,vendorid,vendorname,AccountId,AccountSubTypeId,InvoiceId,process,status,statusdes,importdate,QuickbookListId,QuickbookFullName,ResponseType,JETransactionDate) VALUES (@propertyid,@vendorid,@vendorname,@AccountId,@AccountSubTypeId,@InvoiceId,@process,@status,@statusdes,@importdate,@QuickbookListId,@QuickbookFullName,@ResponseType,@JETransactionDate)", con))
        //        {
        //            cmdd.CommandType = CommandType.Text;
        //            cmdd.Parameters.AddWithValue("@PropertyId", PropertyId);
        //            cmdd.Parameters.AddWithValue("@vendorid", objQuickbookLog.VendorId);
        //            cmdd.Parameters.AddWithValue("@vendorname", objQuickbookLog.VendorName);
        //            cmdd.Parameters.AddWithValue("@AccountId", objQuickbookLog.AccountId);
        //            cmdd.Parameters.AddWithValue("@AccountSubTypeId", objQuickbookLog.AccountSubTypeId);
        //            cmdd.Parameters.AddWithValue("@InvoiceId", objQuickbookLog.InvoiceId);
        //            cmdd.Parameters.AddWithValue("@process", objQuickbookLog.Process);
        //            cmdd.Parameters.AddWithValue("@status", objQuickbookLog.Status);
        //            cmdd.Parameters.AddWithValue("@statusdes", objQuickbookLog.StatusDes);
        //            cmdd.Parameters.AddWithValue("@importdate", DateTime.Now);
        //            cmdd.Parameters.AddWithValue("@QuickbookListId", objQuickbookLog.QuickbookListId);
        //            cmdd.Parameters.AddWithValue("@QuickbookFullName", objQuickbookLog.QuickbookFullName);
        //            cmdd.Parameters.AddWithValue("@ResponseType", objQuickbookLog.ResponseType);
        //            cmdd.Parameters.AddWithValue("@JETransactionDate", objQuickbookLog.JEtransactionDate);
        //            if (con.State == ConnectionState.Open)
        //                con.Close();
        //            con.Open();
        //            int rowsAffected = cmdd.ExecuteNonQuery();
        //            con.Close();
        //            if (rowsAffected > 0)
        //                IsInsertedLog = true;

        //            return IsInsertedLog;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        public static int InsertAccounttype_Quickbook_to_system(SqlConnection con, Account objAccount)
        {

            int AccountTypeId = 0;
            //check Account available for insert
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GLAccountType";
            cmd.Parameters.AddWithValue("@AccountName", objAccount.AccountType);
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows && rdr.Read())
                {
                    AccountTypeId = Convert.ToInt32(rdr["AccountTypeId"]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "InsertAccounttype_Quickbook_to_system";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }


            return AccountTypeId;
        }
        public static int InsertAccountSubtype_Quickbook_to_system(SqlConnection con, Account objAccount)
        {

            int AccountSubTypeId = 0;
            //check Account available for insert
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT [GL_Account_SubType_Id] FROM [dbo].[GL_Account_SubType] where [GL_Account_Type_Id] =" + objAccount.AccountTypeId + " and [GL_Account_SubType_Desc] ='" + objAccount.AccountType + "'";
            // cmd.CommandText = "SELECT [QuickbookFullName],[ResponseType] FROM [dbo].[QBResponseStatus] where [QuickbookFullName] = '" + objAccount.AccountType + "' and ResponseType = 'AccountSubType'";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows && rdr.Read())
                {
                    AccountSubTypeId = Convert.ToInt32(rdr["GL_Account_SubType_Id"]);
                }
                else
                {
                    // record not available then insert
                    using (SqlCommand cmdd = new SqlCommand("INSERT INTO [dbo].[GL_Account_SubType](GL_Account_Type_Id,GL_Account_SubType_Desc)  output INSERTED.GL_Account_SubType_Id VALUES (@GL_Account_Type_Id,@GL_Account_SubType_Desc)", con))
                    {
                        cmdd.CommandType = CommandType.Text;
                        cmdd.Parameters.AddWithValue("@GL_Account_Type_Id", objAccount.AccountTypeId);
                        cmdd.Parameters.AddWithValue("@GL_Account_SubType_Desc", objAccount.AccountType);
                        if (con.State == ConnectionState.Open)
                            con.Close();
                        con.Open();
                        AccountSubTypeId = (int)cmdd.ExecuteScalar();
                        con.Close();
                    }

                }

                con.Close();

            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "InsertAccountSubtype_Quickbook_to_system";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }


            return AccountSubTypeId;
        }
        public static int InsertAccount_Quickbook_to_system(SqlConnection con, Account objAccount)
        {

            int AccountId = 0;
            //check Account available for insert
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT [GL_Account_Name] FROM [dbo].[GL_Accounts] where[GL_Account_Type_Id] = " + objAccount.AccountTypeId + " and [GL_Account_SubType_Id] = " + objAccount.AccountSubTypeId + " and [IsActive] = 1 and [GL_Account_Name] ='" + objAccount.AccountName + "' and [PropertyId] = "+ PropertyId +"";
            cmd.CommandText = "SELECT [GL_Account_Name] FROM [dbo].[GL_Accounts] where [IsActive] = 1 and [GL_Account_Name] ='" + objAccount.AccountName + "' and [PropertyId] = " + PropertyId + "";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    using (SqlCommand cmddd = new SqlCommand("Update [dbo].[GL_Accounts] set GL_Account_Type_Id = @GL_Account_Type_Id,GL_Account_SubType_Id = @GL_Account_SubType_Id,IsActive=@IsActive,InActiveDateQB=@InActiveDateQB where PropertyId = " + PropertyId + "and [QuickbookStatus] = 1 and QuickbookListId = '" + objAccount.ListId + "' ", con))
                    {
                        cmddd.CommandType = CommandType.Text;
                        cmddd.Parameters.AddWithValue("@GL_Account_Type_Id", objAccount.AccountTypeId);
                        cmddd.Parameters.AddWithValue("@GL_Account_SubType_Id", objAccount.AccountSubTypeId);
                        cmddd.Parameters.AddWithValue("@IsActive", Convert.ToBoolean(objAccount.IsActive));
                        if (objAccount.IsActive == "false")
                            cmddd.Parameters.AddWithValue("@InActiveDateQB", DateTime.Now);
                        else
                            cmddd.Parameters.AddWithValue("@InActiveDateQB", DBNull.Value);

                        if (con.State == ConnectionState.Open)
                            con.Close();
                        con.Open();
                        cmddd.ExecuteNonQuery();
                        con.Close();
                    }

                    using (SqlCommand cmmd = new SqlCommand("UPDATE  [dbo].[GL_Accounts] SET QuickbookStatus = 1 , GL_Account_No = '" + objAccount.AccountNumber + "' , QuickbookListId = '" + objAccount.ListId + "', QuickbookFullName = '" + objAccount.AccountName + "'  where QuickbookStatus = 0 and PropertyId =" + PropertyId + " and QuickbookListId is null and GL_Account_Name = '" + objAccount.AccountName + "'", con))
                    {
                        if (con.State == ConnectionState.Open)
                            con.Close();
                        con.Open();
                        int rows = cmmd.ExecuteNonQuery();
                        con.Close();
                    }

                    AccountId = 0;
                }
                else
                {
                    // record not available then insert
                    using (SqlCommand cmdd = new SqlCommand("INSERT INTO [dbo].[GL_Accounts] (PropertyId,GL_Account_No,GL_Account_Name,GL_Account_Type_Id,GL_Account_SubType_Id,IsActive,QuickbookListId,QuickbookFullName,QuickbookStatus,InActiveDateQB)  output INSERTED.GL_Account_Id  VALUES (@PropertyId,@GL_Account_No,@GL_Account_Name,@GL_Account_Type_Id,@GL_Account_SubType_Id,@IsActive,@QuickbookListId,@QuickbookFullName,@QuickbookStatus,@InActiveDateQB)", con))
                    {
                        cmdd.CommandType = CommandType.Text;
                        cmdd.Parameters.AddWithValue("@PropertyId", PropertyId);
                        cmdd.Parameters.AddWithValue("@GL_Account_No", objAccount.AccountNumber);
                        cmdd.Parameters.AddWithValue("@GL_Account_Name", objAccount.AccountName);
                        cmdd.Parameters.AddWithValue("@GL_Account_Type_Id", objAccount.AccountTypeId);
                        cmdd.Parameters.AddWithValue("@GL_Account_SubType_Id", objAccount.AccountSubTypeId);
                        cmdd.Parameters.AddWithValue("@IsActive", Convert.ToBoolean(objAccount.IsActive));
                        cmdd.Parameters.AddWithValue("@QuickbookListId", objAccount.ListId);
                        cmdd.Parameters.AddWithValue("@QuickbookFullName", objAccount.AccountName);
                        cmdd.Parameters.AddWithValue("@QuickbookStatus", 1);
                        if (objAccount.IsActive == "false")
                            cmdd.Parameters.AddWithValue("@InActiveDateQB", DateTime.Now);
                        else
                            cmdd.Parameters.AddWithValue("@InActiveDateQB", DBNull.Value);
                        if (con.State == ConnectionState.Open)
                            con.Close();
                        con.Open();
                        AccountId = (int)cmdd.ExecuteScalar();
                        con.Close();
                    }

                }

                con.Close();

            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "InsertAccount_Quickbook_to_system";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }


            return AccountId;
        }
        public static bool CheckAccountNameExist(SqlConnection con, Account objAccount)
        {

            bool IsAccountNameExist = false;
            //check Account available for insert
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM [dbo].[GL_Accounts] where PropertyId = " + PropertyId + "and [QuickbookStatus] = 1 and QuickbookListId = '" + objAccount.ListId + "'";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows && rdr.Read())
                {
                    int GLAccountId = Convert.ToInt32(rdr["GL_Account_Id"]);
                    int GL_Account_SubType_Id = Convert.ToInt32(rdr["GL_Account_SubType_Id"]);
                    int? GL_Account_Type_Id = InsertAccounttype_Quickbook_to_system(con, objAccount);

                    using (SqlCommand cmddd = new SqlCommand("Update [dbo].[GL_Accounts] set GL_Account_No=@GL_Account_No,GL_Account_Name=@GL_Account_Name,IsActive=@IsActive,InActiveDateQB=@InActiveDateQB,QuickbookFullName=@QuickbookFullName,GL_Account_Type_Id=@GL_Account_Type_Id where PropertyId = " + PropertyId + "and [QuickbookStatus] = 1 and QuickbookListId = '" + objAccount.ListId + "'", con))
                    {
                        cmddd.CommandType = CommandType.Text;
                        cmddd.Parameters.AddWithValue("@GL_Account_Name", objAccount.AccountName);
                        cmddd.Parameters.AddWithValue("@GL_Account_No", objAccount.AccountNumber);
                        cmddd.Parameters.AddWithValue("@IsActive", Convert.ToBoolean(objAccount.IsActive));
                        cmddd.Parameters.AddWithValue("@QuickbookFullName", objAccount.AccountName);
                        cmddd.Parameters.AddWithValue("@GL_Account_Type_Id", GL_Account_Type_Id);
                        if (objAccount.IsActive == "false")
                            cmddd.Parameters.AddWithValue("@InActiveDateQB", DateTime.Now);
                        else
                            cmddd.Parameters.AddWithValue("@InActiveDateQB", DBNull.Value);

                        if (con.State == ConnectionState.Open)
                            con.Close();
                        con.Open();
                        cmddd.ExecuteNonQuery();
                        con.Close();
                    }

                    if (GL_Account_SubType_Id > 0)
                    {
                        using (SqlCommand cmdsubtype = new SqlCommand("Update [dbo].[GL_Account_SubType] set GL_Account_Type_Id=@GL_Account_Type_Id where GL_Account_SubType_Id = " + GL_Account_SubType_Id + "", con))
                        {
                            cmdsubtype.CommandType = CommandType.Text;
                            cmdsubtype.Parameters.AddWithValue("@GL_Account_Type_Id", GL_Account_Type_Id);
                            if (con.State == ConnectionState.Open)
                                con.Close();
                            con.Open();
                            cmdsubtype.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    IsAccountNameExist = true;
                }


                con.Close();

            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "CheckAccountNameExist";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }

            return IsAccountNameExist;
        }

        public static bool CheckAccountSubTypeNameExist(SqlConnection con, Account objAccount)
        {

            bool IsAccountTypeNameExist = false;
            //check Account available for insert
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM [dbo].[GL_Account_SubType] where [GL_Account_Type_Id] = " + objAccount.AccountTypeId + " and [GL_Account_SubType_Desc] = '" + objAccount.AccountType + "'";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    IsAccountTypeNameExist = true;
                }

                con.Close();

            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "CheckAccountSubTypeNameExist";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }

            return IsAccountTypeNameExist;
        }

        public static long InsertItem_Quickbook_to_system(SqlConnection con, Item objItem)
        {

            long ItemID = 0;
            //check Account available for insert
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT [ItemName] FROM [dbo].[QBItemResponse] where  ItemName  = '" + objItem.ItemName + "' and PropertyId = '" + PropertyId + "'";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    ItemID = 0;
                }
                else
                {
                    DateTime dt = new DateTime();
                    // record not available then insert
                    using (SqlCommand cmdd = new SqlCommand("INSERT INTO [dbo].[QBItemResponse](ItemName,QBItemListID,Status,PropertyId)  output INSERTED.QBItemID VALUES (@ItemName,@QBItemListID,@Status,@PropertyId)", con))
                    {
                        cmdd.CommandType = CommandType.Text;
                        cmdd.Parameters.AddWithValue("@ItemName", objItem.ItemName);
                        cmdd.Parameters.AddWithValue("@QBItemListID", objItem.ListId);
                        cmdd.Parameters.AddWithValue("@Status", objItem.Status);
                        cmdd.Parameters.AddWithValue("@PropertyId", PropertyId);
                        if (con.State == ConnectionState.Open)
                            con.Close();
                        con.Open();
                        ItemID = (long)cmdd.ExecuteScalar();
                        con.Close();
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = objItem.Status;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "InsertItem_Quickbook_to_system";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }
            return ItemID;
        }

        public static ArrayList InsertXMLBlob_JournalEntryQuery(SqlConnection con)
        {
            // Insert JournalEntry
            ArrayList req = new ArrayList();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select  distinct  CONVERT(varchar, itemdate, 101) as itemdate from PMSDataEntry p inner join GL_accounts g on p.GLAccountID = g.GL_Account_Id inner join GL_Account_Type ga on g.GL_Account_Type_Id = ga.GL_Account_Type_Id where FormId IN(select FormId from FB_FormSetUp where PropertyId = " + PropertyId + ") and(quickbooklistid is not null and quickbookfullname is not null) and(p.QuickBookStatus = 0 and p.QuickBookStatus is not null)";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string strrequestxml = "";
                    XmlDocument inputxmldoc;
                    strrequestxml = "";
                    inputxmldoc = null;
                    inputxmldoc = new XmlDocument();
                    inputxmldoc.AppendChild(inputxmldoc.CreateXmlDeclaration("1.0", null, null));
                    inputxmldoc.AppendChild(inputxmldoc.CreateProcessingInstruction("qbxml", "version=\"13.0\""));

                    XmlElement qbxml = inputxmldoc.CreateElement("QBXML");
                    inputxmldoc.AppendChild(qbxml);
                    XmlElement qbxmlmsgsrq = inputxmldoc.CreateElement("QBXMLMsgsRq");
                    qbxml.AppendChild(qbxmlmsgsrq);
                    qbxmlmsgsrq.SetAttribute("onError", "stopOnError");
                    XmlElement JournalEntryaddrq = inputxmldoc.CreateElement("JournalEntryAddRq");
                    qbxmlmsgsrq.AppendChild(JournalEntryaddrq);
                    Guid Id = Guid.NewGuid();
                    JournalEntryaddrq.SetAttribute("requestID", Id.ToString());

                    XmlElement JournalEntryAdd = inputxmldoc.CreateElement("JournalEntryAdd");
                    JournalEntryaddrq.AppendChild(JournalEntryAdd);

                    JournalEntryAdd.AppendChild(inputxmldoc.CreateElement("TxnDate")).InnerText = Convert.ToDateTime(rdr["itemdate"]).ToString("yyyy-MM-dd");
                    // JournalEntryAdd.AppendChild(inputxmldoc.CreateElement("RefNumber")).InnerText = "";
                    string JEdate = Convert.ToDateTime(rdr["itemdate"]).ToString("yyyy-MM-dd");
                    //lstJEdate.Add(JEdate);
                    List<JournalEntry> objlistJournalEntry = GetJournalEntryLine(JEdate);
                    foreach (var line in objlistJournalEntry)
                    {
                        if (!string.IsNullOrEmpty(line.DebitAccountQBListId))
                        {

                            XmlElement JournalDebitLine = inputxmldoc.CreateElement("JournalDebitLine");
                            JournalEntryAdd.AppendChild(JournalDebitLine);
                            XmlElement AccountRef = inputxmldoc.CreateElement("AccountRef");
                            JournalDebitLine.AppendChild(AccountRef);
                            //AccountRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = line.DebitAccountQBListId;
                            AccountRef.AppendChild(inputxmldoc.CreateElement("FullName")).InnerText = line.DebitAccountQBFullName;
                            Decimal debitamount = Convert.ToDecimal(line.DebitAccountQBAmount);
                            if (debitamount < 0)
                                debitamount = debitamount * -1;
                            JournalDebitLine.AppendChild(inputxmldoc.CreateElement("Amount")).InnerText = debitamount.ToString();
                            JournalDebitLine.AppendChild(inputxmldoc.CreateElement("Memo")).InnerText = line.Memo.ToString();
                        }

                        if (!string.IsNullOrEmpty(line.CreditAccountQBListId))
                        {
                            XmlElement JournalCreditLine = inputxmldoc.CreateElement("JournalCreditLine");
                            JournalEntryAdd.AppendChild(JournalCreditLine);

                            XmlElement CreditAccountRef = inputxmldoc.CreateElement("AccountRef");
                            JournalCreditLine.AppendChild(CreditAccountRef);
                            //CreditAccountRef.AppendChild(inputxmldoc.CreateElement("ListID")).InnerText = line.CreditAccountQBListId;
                            CreditAccountRef.AppendChild(inputxmldoc.CreateElement("FullName")).InnerText = line.CreditAccountQBFullName;
                            Decimal creditamount = Convert.ToDecimal(line.CreditAccountQBAmount);
                            if (creditamount < 0)
                                creditamount = creditamount * -1;
                            JournalCreditLine.AppendChild(inputxmldoc.CreateElement("Amount")).InnerText = creditamount.ToString();
                            JournalCreditLine.AppendChild(inputxmldoc.CreateElement("Memo")).InnerText = line.Memo.ToString();
                        }
                    }
                    string input = inputxmldoc.OuterXml;
                    //step3: do the qbxmlrp request
                    strrequestxml = inputxmldoc.OuterXml;
                    req.Add(strrequestxml);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.JEtransactionDate = DateTime.Now;
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "Inngenius to Quickbook";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "Journal Entry";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = Helper.InsertJEQuickbookLog(con, objQuickbookLog);
            }

            return req;
        }

        public static bool UpdateJE(SqlConnection con, QuickbookLog objQuickbookLog)
        {
            bool IsUpdateVendor = false;
            try
            {
                using (SqlCommand cmmd = new SqlCommand("update PMSDataEntry set QuickBookStatus=1 where FormId IN (select FormId from FB_FormSetUp where PropertyId=" + PropertyId + ") and itemdate= '" + objQuickbookLog.JEtransactionDate + "' and QuickBookStatus = 0", con))
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Open();
                    int rows = cmmd.ExecuteNonQuery();
                    con.Close();
                    if (rows > 0)
                        IsUpdateVendor = true;

                }
            }
            catch (Exception ex)
            {
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.JEtransactionDate = DateTime.Now;
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "Inngenius to Quickbook";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "Journal Entry";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                //bool IsInsertLog = Helper.InsertJEQuickbookLog(con, objQuickbookLog);
            }
            return IsUpdateVendor;
        }

        public static List<JournalEntry> GetJournalEntryLine(string transactiondate)
        {
            // Insert Vendors
            List<JournalEntry> objlistJournalEntry = new List<JournalEntry>();
            SqlCommand cmdd = new SqlCommand();
            string cs1 = ConfigurationManager.ConnectionStrings["InnGeniusEntities"].ConnectionString;
            SqlConnection conReader = new SqlConnection(cs1);
            cmdd.Connection = conReader;
            cmdd.CommandType = CommandType.Text;
            cmdd.CommandText = "select itemid,CONVERT(varchar, itemdate, 101) as itemdate,memo,case when ga.NormalBalance = 'D' then g.QuickbookListId end as DebitAccountQBListId ,case when ga.NormalBalance = 'D' then replace(replace(replace(p.TextValue,'(',''),')',''),',','') end as DebitAccountQBAmount ,case when ga.NormalBalance = 'D' then g.QuickbookFullName end as DebitAccountQBFullName ,case when ga.NormalBalance = 'C' then g.QuickbookListId end as CreditAccountQBListId ,case when ga.NormalBalance = 'C' then replace(replace(replace(p.TextValue,'(',''),')',''),',','') end as CreditAccountQBAmount ,case when ga.NormalBalance = 'C' then g.QuickbookFullName end as CreditAccountQBFullName from PMSDataEntry p inner join GL_accounts g on p.GLAccountID = g.GL_Account_Id inner join GL_Account_Type ga on g.GL_Account_Type_Id = ga.GL_Account_Type_Id where FormId IN(select FormId from FB_FormSetUp where PropertyId = " + PropertyId + ")  and (quickbooklistid is not null and quickbookfullname is not null) and (p.QuickBookStatus = 0 and p.QuickBookStatus is not null) order by itemid ";
            if (conReader.State == ConnectionState.Open)
                conReader.Close();
            conReader.Open();
            SqlDataReader rdr1 = cmdd.ExecuteReader();

            while (rdr1.Read())
            {
                string Comparedate = Convert.ToDateTime(rdr1["itemdate"]).ToString("yyyy-MM-dd");
                if (transactiondate == Comparedate)
                {
                    JournalEntry objJournalEntry = new JournalEntry();
                    if (!string.IsNullOrEmpty(rdr1["DebitAccountQBListId"].ToString()))
                    {
                        objJournalEntry.DebitAccountQBListId = rdr1["DebitAccountQBListId"].ToString();
                        objJournalEntry.DebitAccountQBFullName = rdr1["DebitAccountQBFullName"].ToString();
                        objJournalEntry.DebitAccountQBAmount = rdr1["DebitAccountQBAmount"].ToString();
                    }
                    if (!string.IsNullOrEmpty(rdr1["CreditAccountQBListId"].ToString()))
                    {
                        objJournalEntry.CreditAccountQBListId = rdr1["CreditAccountQBListId"].ToString();
                        objJournalEntry.CreditAccountQBFullName = rdr1["CreditAccountQBFullName"].ToString();
                        objJournalEntry.CreditAccountQBAmount = rdr1["CreditAccountQBAmount"].ToString();
                    }
                    objJournalEntry.Memo = rdr1["memo"].ToString();
                    objlistJournalEntry.Add(objJournalEntry);
                }
            }
            conReader.Close();

            return objlistJournalEntry;
        }

        public static bool Check_CustomeravailableFOrInsert(SqlConnection con)
        {
            bool IsCustomerstatus = false;
            //check vendor available for insert 
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select  * from tblmstCustomer where  QBCustStatus=0 and QBCustListId is null";
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    IsCustomerstatus = true;
                }
                con.Close();
            }

            
            catch (Exception ex)
            {
                QuickbookLog objQuickbookLog = new QuickbookLog();
                objQuickbookLog.VendorId = 0;
                objQuickbookLog.Status = ex.Message;
                objQuickbookLog.QuickbookListId = "";
                objQuickbookLog.StatusDes = "Imported Not successfully";
                objQuickbookLog.PropertyId = Convert.ToInt32(PropertyId);
                objQuickbookLog.Process = "Check_InvoiceavailableFOrInsert";
                objQuickbookLog.importdate = DateTime.Now;
                objQuickbookLog.ResponseType = "";
                objQuickbookLog.AccountSubTypeId = 0;
                objQuickbookLog.InvoiceId = 0;
                objQuickbookLog.AccountId = 0;
                objQuickbookLog.QuickbookFullName = "";
                objQuickbookLog.VendorName = "";
                bool IsInsertLog = InsertQuickbookLog(con, objQuickbookLog);
            }
            return IsCustomerstatus;
        }


        public static bool sednmailnotification(SqlConnection con, string Type)
        {
            bool issentmail = false;
            try
            {
                string Subject = string.Empty;
                string Body = string.Empty;
                string title = string.Empty;
                string content = string.Empty;
                if (Type.ToLower() == "chartofaccount")
                {
                    Subject = "Chart of Account Imported Successfully - Quickbook to Inngenius";
                    title = "Chart of Account Imported Successfully - Quickbook to Inngenius";
                    content = "Chart of Accounts data imported successfully quickbook to innegenius.";
                }
                else if (Type.ToLower() == "vendor")
                { 
                    Subject = "Vendor Imported Successfully - Quickbook to Inngenius";
                    title = "Vendor Imported Successfully - Quickbook to Inngenius";
                    content = "Vendors data imported successfully quickbook to innegenius.";
                }
                else if (Type.ToLower() == "fetchinvoice")
                {
                    Subject = "Invoice Imported Successfully - Quickbook to Inngenius";
                    title = "Invoice Imported Successfully - Quickbook to Inngenius";
                    content = "Invoices data imported successfully quickbook to innegenius.";
                }
                else if (Type.ToLower() == "insertinvoice")
                {
                    Subject = "Invoice Imported Successfully - Inngenius to Quickbook";
                    title = "Invoice Imported Successfully - Inngenius to Quickbook";
                    content = "Invoices data imported successfully innegenius to quickbook.";
                }
                else if (Type.ToLower() == "vendorcreditinvoice")
                {
                    Subject = "Vendor Credit Invoice Imported Successfully - Inngenius to Quickbook";
                    title = "Vendor Credit Invoice Imported Successfully - Inngenius to Quickbook";
                    content = "vendors credit invoices data imported successfully innegenius to quickbook.";
                }
                else if (Type.ToLower() == "fetchcreditcardchargeinvoice")
                {
                    Subject = "Credit Card Charge Invoice Imported Successfully - Quickbook to Inngenius";
                    title = "Credit Card Charge Invoice Imported Successfully - Quickbook to Inngenius";
                    content = "credit card charge invoices data imported successfully quickbook to innegenius.";
                }
                else if (Type.ToLower() == "insertcreditcardchargeinvoice")
                {
                    Subject = "Credit Card Charge Invoice Imported Successfully - Inngenius to Quickbook";
                    title = "Credit Card Charge Invoice Imported Successfully - Inngenius to Quickbook";
                    content = "credit card charge invoices data imported successfully innegenius to quickbook.";
                }
                else if (Type.ToLower() == "journalentry")
                {
                    Subject = "Journal Entry Data Imported Successfully - Inngenius to Quickbook";
                    title = "Journal Entry Data Imported Successfully - Inngenius to Quickbook";
                    content = "Journal Entry data imported successfully innegenius to quickbook.";
                }
                sendnotificationemail(con, Subject, title, content);
                issentmail = true;
            }
            catch (Exception)
            {
                issentmail = false;
            }
            return issentmail;
        }

        public static bool sendnotificationemail(SqlConnection con, string subject,string title,string content)
        {
            bool issendmail = false;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select Property_name,General_manager_email from Property_Master where PropertyID=" + PropertyId;
                try
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows && rdr.Read())
                    {
                        string ManagerEmailID = rdr["General_manager_email"].ToString();
                        string PropertyName = rdr["Property_name"].ToString(); 
                        if (ManagerEmailID != null)
                        {
                            string body = string.Empty;
                            using (StreamReader reader = new StreamReader(HostingEnvironment.MapPath("~/emailTemplate.html")))
                            {
                                body = reader.ReadToEnd();
                            }
                            body = body.Replace("{title}", title);
                            body = body.Replace("{date}", DateTime.Now.ToShortDateString());
                            body = body.Replace("{property}", PropertyName);
                            body = body.Replace("{content}", content);

                            string toEmail = ManagerEmailID;
                            string fromEmail = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["fromEmail"]);
                            string fromAddressName = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["fromAddressName"]);
                            string password = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["password"]);
                            System.Net.Mail.MailMessage msg = new MailMessage();
                            msg.To.Add(toEmail);
                            msg.From = new MailAddress(fromEmail, fromAddressName);
                            msg.Subject = subject;
                            msg.Body = body;
                            msg.IsBodyHtml = true;
                            SmtpClient client = new SmtpClient(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SMTP"]));
                            client.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Port"]);
                            client.EnableSsl = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["EnableSsl"]);
                            client.UseDefaultCredentials = true;
                            NetworkCredential cred = new System.Net.NetworkCredential(fromEmail, password);
                            client.Credentials = cred;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                            try
                            {
                                client.Send(msg);
                                msg.Dispose();
                            }

                            catch (System.Exception ex)
                            {
                                msg.Dispose();
                                issendmail = false;
                            }
                        }
                        issendmail = true;
                    }
                    con.Close();
                    
                }
                catch (Exception)
                {
                   
                }
            }
            catch (Exception)
            {
                issendmail = true;
            }
            return issendmail;
        }
    }
}