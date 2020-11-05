CREATE TABLE [dbo].[Enrolments] (
[Id] INT IDENTITY (1, 1) NOT NULL,
[CourseId] INT NOT NULL,
[Start] DATE NOT NULL,
PRIMARY KEY CLUSTERED ([Id] ASC),
FOREIGN KEY ([CourseId]) REFERENCES [dbo].[Course] ([Id])
);

