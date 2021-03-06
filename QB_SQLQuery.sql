USE [Inngenius]
GO
/****** Object:  Table [dbo].[AP_Invoices]    Script Date: 1/24/2019 3:32:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AP_Invoices](
	[InvoiceId] [int] IDENTITY(1,1) NOT NULL,
	[PropertyId] [int] NOT NULL,
	[VendorId] [int] NOT NULL,
	[InvoiceNo] [nvarchar](100) NOT NULL,
	[InvoiceDte] [smalldatetime] NOT NULL,
	[InvoiceRef] [nvarchar](50) NULL,
	[PO_No] [nvarchar](50) NULL,
	[GL_PostingDte] [smalldatetime] NOT NULL,
	[DueDte] [smalldatetime] NOT NULL,
	[InvoiceAmt] [money] NOT NULL,
	[Approver] [nvarchar](50) NULL,
	[ApprovedDte] [smalldatetime] NULL,
	[StatusId] [int] NULL,
	[Creator] [nvarchar](50) NULL,
	[CreatedDte] [smalldatetime] NULL,
	[Notes] [nvarchar](max) NULL,
	[PaidByManualCheck] [bit] NULL,
	[ManualCheckNo] [nvarchar](50) NULL,
	[ManualCheckDte] [smalldatetime] NULL,
	[ManualCheckAmt] [money] NULL,
	[ExportedDte] [smalldatetime] NULL,
	[ExportedBy] [nvarchar](50) NULL,
	[paymentDate] [datetime] NULL,
	[QuickbookListId] [nvarchar](50) NULL,
	[QuickbookFullName] [nvarchar](50) NULL,
	[QuickbookStatus] [int] NULL,
	[InvoiceType] [int] NULL,
	[CC_GL_AccId] [int] NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AP_VendorMaster]    Script Date: 1/24/2019 3:32:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AP_VendorMaster](
	[VendorId] [int] IDENTITY(1,1) NOT NULL,
	[PropertyId] [int] NOT NULL,
	[VendorName] [varchar](100) NOT NULL,
	[AccountNumber] [varchar](50) NULL,
	[Address1] [varchar](100) NULL,
	[Address2] [varchar](100) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[PostalCode] [varchar](20) NULL,
	[VendorContact] [varchar](50) NULL,
	[Phone1] [varchar](20) NULL,
	[Phone2] [varchar](20) NULL,
	[Fax] [varchar](20) NULL,
	[VendorEmail] [varchar](max) NULL,
	[CreateDate] [smalldatetime] NULL,
	[IsActive] [bit] NULL,
	[Comments] [varchar](max) NULL,
	[VendorNumber] [varchar](50) NULL,
	[MailingName] [varchar](100) NULL,
	[WebURL] [varchar](50) NULL,
	[IsNewVendor] [bit] NULL,
	[VCategory_Id] [int] NULL,
	[NeedsApproval] [bit] NULL,
	[Tax1099_1] [nvarchar](100) NULL,
	[Tax1099_2] [nvarchar](100) NULL,
	[SSN] [nvarchar](100) NULL,
	[TaxID] [nvarchar](100) NULL,
	[QuickbookListId] [nvarchar](50) NULL,
	[QuickbookFullName] [nvarchar](50) NULL,
	[QuickbookStatus] [int] NULL,
	[InActiveDateQB] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GL_Accounts]    Script Date: 1/24/2019 3:32:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GL_Accounts](
	[GL_Account_Id] [int] IDENTITY(1,1) NOT NULL,
	[PropertyId] [int] NULL,
	[GL_Account_No] [varchar](1000) NULL,
	[GL_Account_Name] [varchar](1000) NOT NULL,
	[GL_Account_Type_Id] [int] NULL,
	[GL_Account_SubType_Id] [int] NULL,
	[IsActive] [bit] NULL,
	[PostingType] [varchar](1) NULL,
	[NormalBalance] [varchar](1) NULL,
	[ParentAccountId] [int] NULL,
	[IsInvoiceAP_Account] [bit] NULL,
	[QuickbookListId] [nvarchar](50) NULL,
	[QuickbookFullName] [nvarchar](50) NULL,
	[QuickbookStatus] [int] NULL,
	[InActiveDateQB] [datetime] NULL,
 CONSTRAINT [PK_GL_Accounts] PRIMARY KEY CLUSTERED 
(
	[GL_Account_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QBItemResponse]    Script Date: 1/24/2019 3:32:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QBItemResponse](
	[QBItemID] [bigint] IDENTITY(1,1) NOT NULL,
	[PropertyId] [int] NULL,
	[ItemName] [nvarchar](100) NULL,
	[QBItemListID] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NULL,
	[importdate] [datetime] NULL,
 CONSTRAINT [PK_QBItemResponse] PRIMARY KEY CLUSTERED 
(
	[QBItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QBResponseStatus]    Script Date: 1/24/2019 3:32:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QBResponseStatus](
	[QBStatusId] [int] IDENTITY(1,1) NOT NULL,
	[PropertyId] [int] NOT NULL,
	[VendorId] [int] NULL,
	[VendorName] [varchar](50) NULL,
	[AccountId] [int] NULL,
	[AccountSubTypeId] [int] NULL,
	[InvoiceId] [bigint] NULL,
	[Process] [varchar](50) NULL,
	[Status] [nvarchar](max) NULL,
	[StatusDes] [varchar](50) NULL,
	[importdate] [datetime] NULL,
	[QuickbookListId] [nvarchar](50) NULL,
	[QuickbookFullName] [nvarchar](50) NULL,
	[ResponseType] [nvarchar](50) NULL,
	[JETransactionDate] [datetime] NULL,
 CONSTRAINT [PK_QBResponseStatus] PRIMARY KEY CLUSTERED 
(
	[QBStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QBWebConnector]    Script Date: 1/24/2019 3:32:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QBWebConnector](
	[QBConnectorID] [int] IDENTITY(1,1) NOT NULL,
	[PropertyID] [int] NULL,
	[PropertyCode] [varchar](50) NULL,
	[PropertyName] [nvarchar](max) NULL,
	[QBFilename] [nvarchar](max) NULL,
	[QBFilePath] [nvarchar](max) NULL,
	[QBUsername] [nvarchar](50) NULL,
	[QBPassword] [nvarchar](50) NULL,
	[FIleAppURL] [nvarchar](max) NULL,
	[FileAppSupportURL] [nvarchar](max) NULL,
	[FileAppDescription] [nvarchar](max) NULL,
	[FileownerGUID] [nvarchar](max) NULL,
	[FileIdGUID] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[ServerFilePath] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_QBWebConnector] PRIMARY KEY CLUSTERED 
(
	[QBConnectorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[AP_Invoices] ADD  CONSTRAINT [DF_AP_Invoices_PaidByManualCheck]  DEFAULT ((0)) FOR [PaidByManualCheck]
GO
ALTER TABLE [dbo].[AP_Invoices] ADD  CONSTRAINT [DF_AP_Invoices_QuickbookStatus]  DEFAULT ((0)) FOR [QuickbookStatus]
GO
ALTER TABLE [dbo].[AP_VendorMaster] ADD  CONSTRAINT [DF_AP_VendorMaster_IsNewVendor]  DEFAULT ((1)) FOR [IsNewVendor]
GO
ALTER TABLE [dbo].[AP_VendorMaster] ADD  CONSTRAINT [needsApproval_default]  DEFAULT ((0)) FOR [NeedsApproval]
GO
ALTER TABLE [dbo].[AP_VendorMaster] ADD  CONSTRAINT [DF_AP_VendorMaster_QuickbookStatus]  DEFAULT ((0)) FOR [QuickbookStatus]
GO
ALTER TABLE [dbo].[GL_Accounts] ADD  CONSTRAINT [DF_GL_Accounts_IsInvoiceAP_Account]  DEFAULT ((0)) FOR [IsInvoiceAP_Account]
GO
ALTER TABLE [dbo].[GL_Accounts] ADD  CONSTRAINT [DF_GL_Accounts_QuickbookStatus]  DEFAULT ((0)) FOR [QuickbookStatus]
GO
ALTER TABLE [dbo].[QBItemResponse] ADD  CONSTRAINT [DF_QBItemResponse_importdate]  DEFAULT (getdate()) FOR [importdate]
GO
ALTER TABLE [dbo].[QBWebConnector] ADD  CONSTRAINT [DF_QBWebConnector_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[QBWebConnector] ADD  CONSTRAINT [DF_QBWebConnector_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
