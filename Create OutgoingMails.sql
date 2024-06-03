/*
CREATE TABLE [dbo].[OutgoingMails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Message] [varchar](640) NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL
 CONSTRAINT [PK_OutgoingMails] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OutgoingMails] ADD CONSTRAINT [DF_OutgoingMails_UserId] DEFAULT ((0)) FOR [UserId]
GO

ALTER TABLE [dbo].[OutgoingMails] ADD CONSTRAINT [DF_OutgoingMails_Status] DEFAULT ((0)) FOR [Status]
GO

ALTER TABLE [dbo].[OutgoingMails] ADD CONSTRAINT [DF_OutgoingMails_CreatedDate] DEFAULT (getdate()) FOR [CreatedDate]
GO
*/