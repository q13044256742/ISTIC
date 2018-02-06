USE [ISTIC]
GO

/****** Object:  Table [dbo].[user_list]    Script Date: 2018/2/6 11:24:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[user_list](
	[ul_id] [varchar](200) NOT NULL,
	[login_name] [varchar](200) NOT NULL,
	[login_password] [varchar](200) NOT NULL,
	[belong_unit] [varchar](200) NOT NULL,
	[belong_department] [varchar](200) NOT NULL,
	[real_name] [varchar](200) NOT NULL,
	[email] [varchar](200) NULL,
	[telephone] [varchar](200) NULL,
	[cellphone] [varchar](200) NULL,
	[ip_address] [varchar](200) NULL,
	[remark] [varchar](2000) NULL,
	[belong_user_group_id] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[ul_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

