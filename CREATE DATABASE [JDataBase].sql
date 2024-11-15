USE [master]
GO

CREATE DATABASE [JDataBase]
GO

USE [JDataBase]
GO

CREATE TABLE [dbo].[tRol](
	[Consecutivo] [tinyint] IDENTITY(1,1) NOT NULL,
	[NombreRol] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tRol] PRIMARY KEY CLUSTERED 
(
	[Consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tUsuario](
	[Consecutivo] [bigint] IDENTITY(1,1) NOT NULL,
	[Identificacion] [varchar](20) NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
	[Correo] [varchar](80) NOT NULL,
	[Contrasenna] [varchar](255) NOT NULL,
	[Activo] [bit] NOT NULL,
	[ConsecutivoRol] [tinyint] NOT NULL,
	[UsaClaveTemp] [bit] NOT NULL,
	[Vigencia] [datetime] NOT NULL,
 CONSTRAINT [PK_tUsuario] PRIMARY KEY CLUSTERED 
(
	[Consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[tRol] ON 
GO
INSERT [dbo].[tRol] ([Consecutivo], [NombreRol]) VALUES (1, N'Administradores')
GO
INSERT [dbo].[tRol] ([Consecutivo], [NombreRol]) VALUES (2, N'Clientes')
GO
SET IDENTITY_INSERT [dbo].[tRol] OFF
GO

SET IDENTITY_INSERT [dbo].[tUsuario] ON 
GO
INSERT [dbo].[tUsuario] ([Consecutivo], [Identificacion], [Nombre], [Correo], [Contrasenna], [Activo], [ConsecutivoRol], [UsaClaveTemp], [Vigencia]) VALUES (7, N'208560001', N'Jose Daniel Villalobos', N'jvillalobos60001@ufide.ac.cr', N'2O/TarIM31mAlvkP2hOuDQ==', 1, 2, 0, CAST(N'2024-11-07T18:27:38.987' AS DateTime))
GO
INSERT [dbo].[tUsuario] ([Consecutivo], [Identificacion], [Nombre], [Correo], [Contrasenna], [Activo], [ConsecutivoRol], [UsaClaveTemp], [Vigencia]) VALUES (8, N'118010406', N'KAREN JIMENEZ ROMAN', N'kjimenez10406@ufide.ac.cr', N'L9q/+aRgNy1e4jpaIV3g9A==', 1, 2, 0, CAST(N'2024-11-14T19:51:09.953' AS DateTime))
GO
INSERT [dbo].[tUsuario] ([Consecutivo], [Identificacion], [Nombre], [Correo], [Contrasenna], [Activo], [ConsecutivoRol], [UsaClaveTemp], [Vigencia]) VALUES (9, N'901130505', N'SEBASTIAN CRUZ OSPINA', N'scruz30505@ufide.ac.cr', N'p8kRLW0mGyEPpItL1R3bzg==', 1, 1, 0, CAST(N'2024-11-14T19:38:39.897' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[tUsuario] OFF
GO

ALTER TABLE [dbo].[tUsuario] ADD  CONSTRAINT [UK_Correo] UNIQUE NONCLUSTERED 
(
	[Correo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tUsuario] ADD  CONSTRAINT [UK_Identificacion] UNIQUE NONCLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tUsuario]  WITH CHECK ADD  CONSTRAINT [FK_tUsuario_tRol] FOREIGN KEY([ConsecutivoRol])
REFERENCES [dbo].[tRol] ([Consecutivo])
GO
ALTER TABLE [dbo].[tUsuario] CHECK CONSTRAINT [FK_tUsuario_tRol]
GO

CREATE PROCEDURE [dbo].[ActualizarContrasenna]
	@Consecutivo			bigint,
	@Contrasenna			varchar(255),
	@UsaClaveTemp			bit,
	@Vigencia				datetime
AS
BEGIN

	UPDATE dbo.tUsuario
	   SET Contrasenna = @Contrasenna,
		   UsaClaveTemp = @UsaClaveTemp,
		   Vigencia = @Vigencia
	 WHERE Consecutivo = @Consecutivo
	
END
GO

CREATE PROCEDURE [dbo].[ActualizarPerfil]
	@Consecutivo		bigint,
	@Identificacion		varchar(20),
	@Nombre				varchar(255),
	@Correo				varchar(80),
	@ConsecutivoRol		tinyint
AS
BEGIN

	IF NOT EXISTS(SELECT 1 FROM dbo.tUsuario
			      WHERE (Identificacion = @Identificacion OR Correo = @Correo)
					 AND Consecutivo != @Consecutivo)
	BEGIN

		UPDATE dbo.tUsuario
		   SET Identificacion = @Identificacion,
			   Nombre = @Nombre,
			   Correo = @Correo,
			   ConsecutivoRol =	CASE WHEN @ConsecutivoRol != 0 
									 THEN @ConsecutivoRol 
									 ELSE ConsecutivoRol END
		 WHERE Consecutivo = @Consecutivo

	END
	
END
GO

CREATE PROCEDURE [dbo].[ConsultarRoles]
	
AS
BEGIN
	
	SELECT	Consecutivo,NombreRol
	FROM	dbo.tRol

END
GO

CREATE PROCEDURE [dbo].[ConsultarUsuario]
	@Consecutivo BIGINT
AS
BEGIN
	
	SELECT	U.Consecutivo,
			Identificacion,
			Nombre,
			Correo,
			Activo,
			ConsecutivoRol,
			R.NombreRol
	  FROM	dbo.tUsuario U
	  INNER JOIN dbo.tRol R ON U.ConsecutivoRol = R.Consecutivo
	  WHERE U.Consecutivo = @Consecutivo

END
GO

CREATE PROCEDURE [dbo].[ConsultarUsuarios]
	
AS
BEGIN
	
	SELECT	U.Consecutivo,
			Identificacion,
			Nombre,
			Correo,
			Activo,
			ConsecutivoRol,
			R.NombreRol
	  FROM	dbo.tUsuario U
	  INNER JOIN dbo.tRol R ON U.ConsecutivoRol = R.Consecutivo

END
GO

CREATE PROCEDURE [dbo].[CrearCuenta]
	@Identificacion varchar(20),
	@Nombre			varchar(255),
	@Correo			varchar(80),
	@Contrasenna	varchar(255)
AS
BEGIN

	DECLARE @EstadoActivo BIT = 1,
			@RolUsuario INT,
			@UsaClaveTemp BIT = 0

	SELECT	@RolUsuario = Consecutivo
	FROM	dbo.tRol
	WHERE	NombreRol = 'Clientes'

	IF NOT EXISTS(SELECT 1 FROM dbo.tUsuario
			      WHERE Identificacion = @Identificacion
					OR	Correo = @Correo)
	BEGIN
		INSERT INTO dbo.tUsuario (Identificacion,Nombre,Correo,Contrasenna,Activo,ConsecutivoRol,UsaClaveTemp,Vigencia)
		VALUES (@Identificacion,@Nombre,@Correo,@Contrasenna,@EstadoActivo,@RolUsuario,@UsaClaveTemp,GETDATE())
	END

END
GO

CREATE PROCEDURE [dbo].[IniciarSesion]
	@Correo			varchar(80),
	@Contrasenna	varchar(255)
AS
BEGIN
	
	SELECT	U.Consecutivo,
			Identificacion,
			Nombre,
			Correo,
			Activo,
			ConsecutivoRol,
			R.NombreRol,
			UsaClaveTemp,
			Vigencia
	  FROM	dbo.tUsuario U
	  INNER JOIN dbo.tRol R ON U.ConsecutivoRol = R.Consecutivo
	  WHERE	Correo = @Correo
		AND Contrasenna = @Contrasenna
		AND Activo = 1

END
GO

CREATE PROCEDURE [dbo].[ValidarUsuario]
	@Correo	varchar(80)
AS
BEGIN
	
	SELECT	U.Consecutivo,
			Identificacion,
			Nombre,
			Correo,
			Activo,
			ConsecutivoRol,
			R.NombreRol
	  FROM	dbo.tUsuario U
	  INNER JOIN dbo.tRol R ON U.ConsecutivoRol = R.Consecutivo
	  WHERE	Correo = @Correo

END
GO