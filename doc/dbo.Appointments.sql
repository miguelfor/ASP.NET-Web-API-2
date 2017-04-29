CREATE TABLE [dbo].[Appointments] (
    [ID]        INT      IDENTITY (1, 1) NOT NULL,
    [IdDoctor]  INT      NOT NULL,
    [IdPatient] INT      NOT NULL,
    [Date]      DATETIME NOT NULL,
    [Status]    INT      NOT NULL,
    CONSTRAINT [PK_dbo.Appointments] PRIMARY KEY CLUSTERED ([ID] ASC)
);

