USE [Inngenius]
GO
/****** Object:  Table [dbo].[AP_Documents]    Script Date: 1/30/2019 6:16:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AP_Documents](
	[docID] [int] IDENTITY(1,1) NOT NULL,
	[docFileName] [nvarchar](150) NOT NULL,
	[docNote] [nvarchar](1024) NULL,
	[docImage] [varbinary](max) NULL,
	[InvoiceId] [int] NULL,
	[docTempGuid] [nvarchar](512) NULL,
 CONSTRAINT [PK_AP_Documents_1] PRIMARY KEY CLUSTERED 
(
	[docID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AP_InvoiceLines]    Script Date: 1/30/2019 6:16:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AP_InvoiceLines](
	[InvoiceLineId] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[LineType] [varchar](1) NOT NULL,
	[AppliedGL_Account_Id] [int] NULL,
	[InventoryItem] [varchar](250) NULL,
	[ExpenseItem] [varchar](500) NULL,
	[ExpenseDepartmentId] [int] NULL,
	[InvoiceLineRef] [varchar](100) NULL,
	[LineAmt] [money] NULL,
	[LineQty] [int] NULL,
	[UOM] [varchar](15) NULL,
	[QuickbookListId] [nvarchar](50) NULL,
	[QuickbookFullName] [nvarchar](50) NULL,
	[QuickbookStatus] [int] NULL,
 CONSTRAINT [PK_InvoiceLines] PRIMARY KEY CLUSTERED 
(
	[InvoiceLineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AP_InvoicePayments]    Script Date: 1/30/2019 6:16:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AP_InvoicePayments](
	[InvoicePaymentId] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NULL,
	[PaymentType] [nvarchar](max) NULL,
	[RefNo] [nvarchar](max) NULL,
	[TxnDate] [datetime] NULL,
	[Amount] [decimal](18, 2) NULL,
	[Note] [nvarchar](max) NULL,
	[LinkType] [nvarchar](max) NULL,
	[QuickbookListId] [nvarchar](50) NULL,
	[QuickbookFullName] [nvarchar](max) NULL,
	[QuickbookStatus] [int] NULL,
 CONSTRAINT [PK_AP_InvoicePayments] PRIMARY KEY CLUSTERED 
(
	[InvoicePaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GL_Account_SubType]    Script Date: 1/30/2019 6:16:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GL_Account_SubType](
	[GL_Account_SubType_Id] [int] IDENTITY(1,1) NOT NULL,
	[GL_Account_Type_Id] [varchar](50) NOT NULL,
	[GL_Account_SubType_Desc] [varchar](50) NULL,
	[QB_AccountType] [varchar](20) NULL,
	[PeachTreeTypeId] [int] NULL,
	[DisplayOrder] [int] NULL,
 CONSTRAINT [PK_GL_Account_Type] PRIMARY KEY CLUSTERED 
(
	[GL_Account_SubType_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PMSDataEntry]    Script Date: 1/30/2019 6:16:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PMSDataEntry](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[FormId] [int] NOT NULL,
	[DataEntryControlID] [int] NOT NULL,
	[ItemDate] [datetime] NOT NULL,
	[GLAccountID] [int] NULL,
	[NumericValueDR] [numeric](18, 4) NULL,
	[NumericValueCR] [numeric](18, 4) NULL,
	[NumericValue] [numeric](18, 4) NULL,
	[TextValue] [nvarchar](max) NULL,
	[CommittedDate] [datetime] NULL,
	[OldDRValue] [numeric](18, 4) NULL,
	[OldCRValue] [numeric](18, 4) NULL,
	[OldNumericValue] [numeric](18, 4) NULL,
	[OldTextValue] [nvarchar](max) NULL,
	[QuickbookStatus] [int] NULL,
	[Memo] [nvarchar](max) NULL,
 CONSTRAINT [PK_PMSDataEntry] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Property_Master]    Script Date: 1/30/2019 6:16:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Property_Master](
	[PropertyID] [int] IDENTITY(13,1) NOT NULL,
	[Property_code] [nvarchar](50) NULL,
	[Property_name] [nvarchar](500) NULL,
	[LicensedOn] [datetime] NULL,
	[TotalRooms] [int] NULL,
	[Franchise_name] [nvarchar](500) NULL,
	[Organisation_name] [nvarchar](500) NULL,
	[Opened_date] [datetime] NULL,
	[ProjectID] [nvarchar](50) NULL,
	[Address] [nvarchar](1000) NULL,
	[City_town] [nvarchar](100) NULL,
	[State] [nvarchar](100) NULL,
	[Zipcode] [nvarchar](20) NULL,
	[Business_phone1] [nvarchar](20) NULL,
	[Business_phone2] [nvarchar](20) NULL,
	[Business_fax] [nvarchar](20) NULL,
	[General_manager_name] [nvarchar](100) NULL,
	[General_manager_email] [nvarchar](500) NULL,
	[Management_company] [nvarchar](100) NULL,
	[General_manager_cellphone] [nvarchar](20) NULL,
	[Operation_Week_start_day] [nvarchar](20) NULL,
	[Operation_Week_day_from] [nvarchar](20) NULL,
	[Operation_Week_day_to] [nvarchar](20) NULL,
	[Operation_Week_end_from] [nvarchar](20) NULL,
	[Operation_Week_end_to] [nvarchar](20) NULL,
	[Operation_Default_expense_account] [nvarchar](50) NULL,
	[Operation_Default_lunch_allotment] [decimal](18, 2) NULL,
	[Operation_Pay_frequency] [nvarchar](50) NULL,
	[Operation_Paid_leave] [bit] NULL,
	[Operation_Travel] [bit] NULL,
	[Operation_ServiceTax] [bit] NULL,
	[Operation_InvoiceApproval] [bit] NULL,
	[Operation_Vendor_validations] [nvarchar](1000) NULL,
	[Operation_PMSReport_emailbox] [nvarchar](100) NULL,
	[Operation_PMSReport_email_fromaddress] [nvarchar](100) NULL,
	[Operation_PMSReport_email_password] [nvarchar](50) NULL,
	[Operation_PMSReport_poll_email] [bit] NULL,
	[Operation_Frontdesk_Checklist_form_name] [nvarchar](100) NULL,
	[Accounting_EIN_FederalID] [nvarchar](50) NULL,
	[Accounting_System] [nvarchar](50) NULL,
	[Accounting_Closing_date] [datetime] NULL,
	[Accounting_Advance_deposit_ledger] [decimal](18, 2) NULL,
	[Accounting_Guest_ledger] [decimal](18, 2) NULL,
	[Accounting_City_ledger] [decimal](18, 2) NULL,
	[Accounting_Closing_balance] [decimal](18, 2) NULL,
	[Accounting_FICA_perc_labor_calculation] [decimal](18, 2) NULL,
	[Accounting_Taxes_perc_labor_calculation] [decimal](18, 2) NULL,
	[Accounting_Benefit_perc] [decimal](18, 2) NULL,
	[Accounting_Current_fiscal_year] [nvarchar](10) NULL,
	[General_manager_alternate_email] [nvarchar](500) NULL,
	[MHPOR] [decimal](18, 2) NULL,
	[LeaveType] [nvarchar](100) NULL,
	[IsEnable] [bit] NULL,
 CONSTRAINT [PK_Property_Master] PRIMARY KEY CLUSTERED 
(
	[PropertyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AP_InvoiceLines] ADD  CONSTRAINT [DF_AP_InvoiceLines_QuickbookStatus]  DEFAULT ((0)) FOR [QuickbookStatus]
GO
ALTER TABLE [dbo].[AP_InvoicePayments] ADD  CONSTRAINT [DF_AP_InvoicePayments_QuickbookStatus]  DEFAULT ((0)) FOR [QuickbookStatus]
GO
ALTER TABLE [dbo].[PMSDataEntry] ADD  CONSTRAINT [DF_PMSDataEntry_QuickbookStatus]  DEFAULT ((0)) FOR [QuickbookStatus]
GO
ALTER TABLE [dbo].[AP_Documents]  WITH CHECK ADD  CONSTRAINT [FK_AP_Documents_AP_Invoices] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[AP_Invoices] ([InvoiceId])
GO
ALTER TABLE [dbo].[AP_Documents] CHECK CONSTRAINT [FK_AP_Documents_AP_Invoices]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'PMS entry ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PMSDataEntry', @level2type=N'COLUMN',@level2name=N'ItemID'
GO
