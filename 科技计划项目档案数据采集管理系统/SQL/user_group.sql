USE [ISTIC]
GO

/****** Object:  Table [dbo].[user_group]    Script Date: 2018/2/6 11:25:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[user_group](
	[ug_id] [varchar](200) NOT NULL,
	[ug_name] [varchar](200) NOT NULL,
	[ug_code] [varchar](200) NOT NULL,
	[ug_note] [varchar](2000) NULL,
	[ug_sort] [varchar](200) NULL,
	[ug_extend_1] [varchar](2000) NULL,
PRIMARY KEY CLUSTERED 
(
	[ug_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

