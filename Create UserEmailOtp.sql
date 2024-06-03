/*
CREATE TABLE [dbo].[UserEmailOtps](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Pincode] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_UserEmailOtps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserEmailOtps] ADD  CONSTRAINT [DF_UserEmailOtps_Status]  DEFAULT ((0)) FOR [Status]
GO

ALTER TABLE [dbo].[UserEmailOtps] ADD  CONSTRAINT [DF_UserEmailOtps_Createdate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
*/