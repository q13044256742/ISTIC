USE [ISTIC]
GO

/****** Object:  Table [dbo].[module]    Script Date: 2018/2/6 14:37:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[module](
	[m_id] [varchar](200) NOT NULL,
	[m_name] [varchar](200) NOT NULL,
	[m_code] [varchar](200) NOT NULL,
	[m_sort] [varchar](200) NULL,
	[userGroup_id] [varchar](200) NULL,
	[state] [varchar](200) NULL,
	[m_extend_4] [varchar](200) NULL,
	[m_extend_5] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[m_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

