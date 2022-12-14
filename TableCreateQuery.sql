USE [ITEMSDB]
GO

/****** Object:  Table [dbo].[ItemsTable]    Script Date: 7/23/2021 12:46:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ItemsTable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) NULL,
	[description] [nvarchar](500) NULL,
	[priority] [nvarchar](500) NULL,
	[deadline] [nvarchar](500) NULL,
	[isCompleted] [bit] NULL,
	[start] [datetime] NULL,
	[end] [datetime] NULL,
	[attendees] [nvarchar](500) NULL
) ON [PRIMARY]
GO


