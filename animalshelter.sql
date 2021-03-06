USE [animal_shelter]
GO
/****** Object:  Table [dbo].[animals]    Script Date: 12/6/2016 4:38:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[animals](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[gender] [varchar](255) NULL,
	[admittance_date] [datetime] NULL,
	[breed] [varchar](255) NULL,
	[type_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[types]    Script Date: 12/6/2016 4:38:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[types](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
