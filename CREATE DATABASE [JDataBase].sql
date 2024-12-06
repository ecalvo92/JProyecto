USE [master]
GO

CREATE DATABASE [JDataBase]
GO

USE [JDataBase]
GO

CREATE TABLE [dbo].[tCarrito](
	[Consecutivo] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsecutivoUsuario] [bigint] NOT NULL,
	[ConsecutivoProducto] [bigint] NOT NULL,
	[Unidades] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_tCarrito] PRIMARY KEY CLUSTERED 
(
	[Consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tError](
	[Consecutivo] [bigint] IDENTITY(1,1) NOT NULL,
	[ConsecutivoUsuario] [bigint] NOT NULL,
	[Mensaje] [varchar](max) NOT NULL,
	[Origen] [varchar](50) NOT NULL,
	[FechaHora] [datetime] NOT NULL,
 CONSTRAINT [PK_tError] PRIMARY KEY CLUSTERED 
(
	[Consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[tProducto](
	[Consecutivo] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
	[Inventario] [int] NOT NULL,
	[Imagen] [varchar](50) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_tProducto] PRIMARY KEY CLUSTERED 
(
	[Consecutivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
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

SET IDENTITY_INSERT [dbo].[tCarrito] ON 
GO
INSERT [dbo].[tCarrito] ([Consecutivo], [ConsecutivoUsuario], [ConsecutivoProducto], [Unidades], [Fecha]) VALUES (1, 8, 5, 3, CAST(N'2024-12-05T20:48:07.250' AS DateTime))
GO
INSERT [dbo].[tCarrito] ([Consecutivo], [ConsecutivoUsuario], [ConsecutivoProducto], [Unidades], [Fecha]) VALUES (2, 8, 3, 1, CAST(N'2024-12-05T20:47:41.067' AS DateTime))
GO
INSERT [dbo].[tCarrito] ([Consecutivo], [ConsecutivoUsuario], [ConsecutivoProducto], [Unidades], [Fecha]) VALUES (3, 8, 4, 1, CAST(N'2024-12-05T20:41:17.833' AS DateTime))
GO
INSERT [dbo].[tCarrito] ([Consecutivo], [ConsecutivoUsuario], [ConsecutivoProducto], [Unidades], [Fecha]) VALUES (4, 8, 6, 1, CAST(N'2024-12-05T20:41:21.200' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[tCarrito] OFF
GO

SET IDENTITY_INSERT [dbo].[tError] ON 
GO
INSERT [dbo].[tError] ([Consecutivo], [ConsecutivoUsuario], [Mensaje], [Origen], [FechaHora]) VALUES (2, 0, N'Could not find stored procedure ''ValidarUsuario2''.', N'/api/Login/RecuperarAcceso', CAST(N'2024-11-21T20:48:20.157' AS DateTime))
GO
INSERT [dbo].[tError] ([Consecutivo], [ConsecutivoUsuario], [Mensaje], [Origen], [FechaHora]) VALUES (3, 7, N'Could not find stored procedure ''ConsultarUsuarios2''.', N'/api/Usuario/ConsultarUsuarios', CAST(N'2024-11-21T20:53:17.820' AS DateTime))
GO
INSERT [dbo].[tError] ([Consecutivo], [ConsecutivoUsuario], [Mensaje], [Origen], [FechaHora]) VALUES (4, 7, N'Could not find stored procedure ''ConsultarUsuarios2''.', N'/api/Usuario/ConsultarUsuarios', CAST(N'2024-11-21T20:54:41.153' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[tError] OFF
GO

SET IDENTITY_INSERT [dbo].[tProducto] ON 
GO
INSERT [dbo].[tProducto] ([Consecutivo], [Nombre], [Precio], [Inventario], [Imagen], [Activo]) VALUES (3, N'XBox 360.3', CAST(225000.00 AS Decimal(18, 2)), 3, N'/products/', 1)
GO
INSERT [dbo].[tProducto] ([Consecutivo], [Nombre], [Precio], [Inventario], [Imagen], [Activo]) VALUES (4, N'XBox 360.4', CAST(50000.00 AS Decimal(18, 2)), 1, N'/products/', 1)
GO
INSERT [dbo].[tProducto] ([Consecutivo], [Nombre], [Precio], [Inventario], [Imagen], [Activo]) VALUES (5, N'XBox 360.5', CAST(225000.00 AS Decimal(18, 2)), 3, N'/products/', 1)
GO
INSERT [dbo].[tProducto] ([Consecutivo], [Nombre], [Precio], [Inventario], [Imagen], [Activo]) VALUES (6, N'XBox 360.6', CAST(50000.00 AS Decimal(18, 2)), 1, N'/products/', 1)
GO
SET IDENTITY_INSERT [dbo].[tProducto] OFF
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
INSERT [dbo].[tUsuario] ([Consecutivo], [Identificacion], [Nombre], [Correo], [Contrasenna], [Activo], [ConsecutivoRol], [UsaClaveTemp], [Vigencia]) VALUES (7, N'208560001', N'JOSE DANIEL VILLALOBOS OROZCO', N'jvillalobos60001@ufide.ac.cr', N'2O/TarIM31mAlvkP2hOuDQ==', 1, 1, 0, CAST(N'2024-11-07T18:27:38.987' AS DateTime))
GO
INSERT [dbo].[tUsuario] ([Consecutivo], [Identificacion], [Nombre], [Correo], [Contrasenna], [Activo], [ConsecutivoRol], [UsaClaveTemp], [Vigencia]) VALUES (8, N'118010406', N'KAREN JIMENEZ ROMAN', N'kjimenez10406@ufide.ac.cr', N'L9q/+aRgNy1e4jpaIV3g9A==', 1, 2, 0, CAST(N'2024-11-14T19:51:09.953' AS DateTime))
GO
INSERT [dbo].[tUsuario] ([Consecutivo], [Identificacion], [Nombre], [Correo], [Contrasenna], [Activo], [ConsecutivoRol], [UsaClaveTemp], [Vigencia]) VALUES (9, N'901130505', N'SEBASTIAN CRUZ OSPINA', N'scruz30505@ufide.ac.cr', N'p8kRLW0mGyEPpItL1R3bzg==', 1, 2, 0, CAST(N'2024-11-14T19:38:39.897' AS DateTime))
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

ALTER TABLE [dbo].[tCarrito]  WITH CHECK ADD  CONSTRAINT [FK_tCarrito_tProducto] FOREIGN KEY([ConsecutivoProducto])
REFERENCES [dbo].[tProducto] ([Consecutivo])
GO
ALTER TABLE [dbo].[tCarrito] CHECK CONSTRAINT [FK_tCarrito_tProducto]
GO

ALTER TABLE [dbo].[tCarrito]  WITH CHECK ADD  CONSTRAINT [FK_tCarrito_tUsuario] FOREIGN KEY([ConsecutivoUsuario])
REFERENCES [dbo].[tUsuario] ([Consecutivo])
GO
ALTER TABLE [dbo].[tCarrito] CHECK CONSTRAINT [FK_tCarrito_tUsuario]
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

CREATE PROCEDURE [dbo].[ActualizarEstado]
	@Consecutivo		bigint
AS
BEGIN

	UPDATE dbo.tUsuario
    SET Activo =	CASE WHEN Activo = 1 
	   				     THEN 0
						 ELSE 1 END
	WHERE Consecutivo = @Consecutivo
	
END
GO

CREATE PROCEDURE [dbo].[ActualizarEstadoProducto]
	@Consecutivo		bigint
AS
BEGIN

	UPDATE dbo.tProducto
    SET Activo =	CASE WHEN Activo = 1 
	   				     THEN 0
						 ELSE 1 END
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

CREATE PROCEDURE [dbo].[ActualizarProducto]
	@Consecutivo	BIGINT,
	@Nombre			varchar(50),
	@Precio			decimal(18,2),
	@Inventario		int
AS
BEGIN

	UPDATE dbo.tProducto
	SET Nombre = @Nombre,
        Precio = @Precio,
		Inventario = @Inventario
	WHERE Consecutivo = @Consecutivo

END
GO

CREATE PROCEDURE [dbo].[ConsultarCarrito]
	@ConsecutivoUsuario BIGINT
AS
BEGIN
	
	SELECT	C.Consecutivo,
			C.ConsecutivoProducto,
			P.Nombre,
			C.Unidades,
			P.Precio,
			C.Unidades * P.Precio 'Total',
			C.Fecha
	  FROM	dbo.tCarrito C
	  INNER JOIN dbo.tProducto P ON C.ConsecutivoProducto = P.Consecutivo
	  WHERE C.ConsecutivoUsuario = @ConsecutivoUsuario

END
GO

CREATE PROCEDURE [dbo].[ConsultarProducto]
	@Consecutivo BIGINT
AS
BEGIN
	
	SELECT	Consecutivo,
			Nombre,
			Precio,
			Inventario,
			Imagen + CONVERT(VARCHAR,Consecutivo) + '.png' 'Imagen',
			(CASE WHEN Activo = 1 THEN 'Activo' ELSE 'Inactivo' END) 'Estado'
	  FROM	dbo.tProducto
	  WHERE Consecutivo = @Consecutivo

END
GO

CREATE PROCEDURE [dbo].[ConsultarProductos]
	
AS
BEGIN
	
	SELECT	Consecutivo,
			Nombre,
			Precio,
			Inventario,
			Imagen + CONVERT(VARCHAR,Consecutivo) + '.png' 'Imagen',
			(CASE WHEN Activo = 1 THEN 'Activo' ELSE 'Inactivo' END) 'Estado'
	  FROM	dbo.tProducto

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
			(CASE WHEN Activo = 1 THEN 'Activo' ELSE 'Inactivo' END) 'Estado',
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

CREATE PROCEDURE [dbo].[RegistrarCarrito]
	@ConsecutivoUsuario		BIGINT,
	@ConsecutivoProducto	BIGINT,
	@Unidades				INT
AS
BEGIN

	IF (SELECT COUNT(*) FROM tCarrito
				  WHERE ConsecutivoUsuario = @ConsecutivoUsuario
					AND ConsecutivoProducto = @ConsecutivoProducto) = 0
	BEGIN

		INSERT INTO dbo.tCarrito (ConsecutivoUsuario,ConsecutivoProducto,Unidades,Fecha)
		VALUES (@ConsecutivoUsuario, @ConsecutivoProducto, @Unidades, GETDATE())

	END
	ELSE 
	BEGIN

		UPDATE	dbo.tCarrito
		SET		Unidades = @Unidades,
				Fecha = GETDATE()
		WHERE ConsecutivoUsuario = @ConsecutivoUsuario
		  AND ConsecutivoProducto = @ConsecutivoProducto 
		
	END

END
GO

CREATE PROCEDURE [dbo].[RegistrarError]
	@Consecutivo	bigint,
	@Mensaje		varchar(max),
	@Origen			varchar(50)
AS
BEGIN

	INSERT INTO dbo.tError(ConsecutivoUsuario,Mensaje,Origen,FechaHora)
    VALUES (@Consecutivo,@Mensaje,@Origen,GETDATE())

END
GO

CREATE PROCEDURE [dbo].[RegistrarProducto]
	@Nombre		varchar(50),
	@Precio		decimal(18,2),
	@Inventario	int,
	@Imagen		varchar(50)
AS
BEGIN

	INSERT INTO dbo.tProducto (Nombre,Precio,Inventario,Imagen,Activo)
	VALUES (@Nombre,@Precio,@Inventario,@Imagen,1)

	SELECT @@IDENTITY Consecutivo

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