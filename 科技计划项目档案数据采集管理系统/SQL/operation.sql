USE [ISTIC]
GO

/****** Object:  Table [dbo].[operation]    Script Date: 2018/2/6 11:25:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[operation](
	[o_id] [varchar](200) NOT NULL,
	[o_view] [varchar](200) NULL,
	[o_edit] [varchar](200) NULL,
	[module_id] [varchar](2000) NULL,
PRIMARY KEY CLUSTERED 
(
	[o_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

