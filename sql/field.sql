USE [OPDT]
GO
SET IDENTITY_INSERT [dbo].[Field] ON 

INSERT [dbo].[Field] ([Id], [Order], [CreatedAt], [ModifiedAt], [Name], [Code], [Description], [Active]) VALUES (1, 1, CAST(N'2021-03-01T17:30:01.057' AS DateTime), NULL, N'Nông nghiệp', N'NN', NULL, 1)
INSERT [dbo].[Field] ([Id], [Order], [CreatedAt], [ModifiedAt], [Name], [Code], [Description], [Active]) VALUES (2, 2, NULL, CAST(N'2021-03-01T20:23:04.720' AS DateTime), N'Giáo dục', N'GD', NULL, 1)
INSERT [dbo].[Field] ([Id], [Order], [CreatedAt], [ModifiedAt], [Name], [Code], [Description], [Active]) VALUES (3, 3, CAST(N'2021-03-02T09:48:34.077' AS DateTime), NULL, N'Y tế', N'YT', NULL, 1)
SET IDENTITY_INSERT [dbo].[Field] OFF
