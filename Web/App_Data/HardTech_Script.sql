USE [master]
GO
/****** Object:  Database [DBHardTech]    Script Date: 23/6/2023 19:07:45 ******/
CREATE DATABASE [DBHardTech]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HardTech', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\HardTech.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HardTech_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\HardTech_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DBHardTech] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBHardTech].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBHardTech] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBHardTech] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBHardTech] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBHardTech] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBHardTech] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBHardTech] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBHardTech] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBHardTech] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBHardTech] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBHardTech] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBHardTech] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBHardTech] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBHardTech] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBHardTech] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBHardTech] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DBHardTech] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBHardTech] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBHardTech] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBHardTech] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBHardTech] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBHardTech] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBHardTech] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBHardTech] SET RECOVERY FULL 
GO
ALTER DATABASE [DBHardTech] SET  MULTI_USER 
GO
ALTER DATABASE [DBHardTech] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBHardTech] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBHardTech] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBHardTech] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DBHardTech] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DBHardTech] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DBHardTech', N'ON'
GO
ALTER DATABASE [DBHardTech] SET QUERY_STORE = OFF
GO
USE [DBHardTech]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 23/6/2023 19:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Descripcion] [varchar](250) NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CuentaPago]    Script Date: 23/6/2023 19:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CuentaPago](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoPago] [int] NULL,
	[NumCuenta] [nvarchar](16) NULL,
	[FechaExp] [date] NOT NULL,
	[CodSeguridad] [nvarchar](3) NULL,
	[IdUsuario] [int] NULL,
 CONSTRAINT [PK_CuentaPago] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Direccion]    Script Date: 23/6/2023 19:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Direccion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NULL,
	[Provincia] [varchar](50) NULL,
	[Canton] [varchar](50) NULL,
	[Distrito] [varchar](50) NULL,
	[DireccionExacta] [varchar](450) NULL,
 CONSTRAINT [PK_Direccion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Escala]    Script Date: 23/6/2023 19:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Escala](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Valor] [int] NULL,
 CONSTRAINT [PK_Escala] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estado]    Script Date: 23/6/2023 19:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estado](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
 CONSTRAINT [PK_Estado] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Evaluacion]    Script Date: 23/6/2023 19:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evaluacion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdEvaluador] [int] NULL,
	[IdEvaluado] [int] NULL,
	[IdEscala] [int] NULL,
	[Comentario] [varchar](250) NULL,
 CONSTRAINT [PK_Evaluacion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 23/6/2023 19:07:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factura](
	[IdFactura] [int] IDENTITY(1,1) NOT NULL,
	[IdCuentaPago] [int] NULL,
	[Fecha] [date] NOT NULL,
	[IdUsuario] [int] NULL,
	[IVA] [int] NULL,
	[Total] [float] NULL,
	[IdOrden] [int] NULL,
 CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED 
(
	[IdFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Foto]    Script Date: 23/6/2023 19:07:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Foto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdProducto] [int] NOT NULL,
	[Media] [varbinary](max) NULL,
 CONSTRAINT [PK_Foto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mensaje]    Script Date: 23/6/2023 19:07:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mensaje](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdProducto] [int] NULL,
	[Mensaje] [varchar](480) NULL,
	[IdUsuario] [int] NULL,
 CONSTRAINT [PK_Mensaje] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orden]    Script Date: 23/6/2023 19:07:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orden](
	[IdOrden] [int] IDENTITY(1,1) NOT NULL,
	[IdEstado] [int] NULL,
	[FechaInicio] [date] NULL,
	[SubTotal] [float] NULL,
 CONSTRAINT [PK_Orden] PRIMARY KEY CLUSTERED 
(
	[IdOrden] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrdenDetalle]    Script Date: 23/6/2023 19:07:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdenDetalle](
	[IdOrden] [int] NOT NULL,
	[IdProducto] [int] NOT NULL,
	[Cantidad] [int] NULL,
	[FechaEntrega] [date] NULL,
	[IdEstado] [int] NULL,
 CONSTRAINT [PK_OrdenDetalle] PRIMARY KEY CLUSTERED 
(
	[IdOrden] ASC,
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 23/6/2023 19:07:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[IdProducto] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Descripcion] [varchar](2000) NULL,
	[Precio] [float] NULL,
	[Cantidad] [int] NULL,
	[IdCategoria] [int] NULL,
	[IdProveedor] [int] NULL,
	[IdEstado] [int] NULL,
	[VentasR] [int] NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Respuesta]    Script Date: 23/6/2023 19:07:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Respuesta](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdMensaje] [int] NULL,
	[Respuesta] [varchar](480) NULL,
	[IdProveedor] [int] NULL,
 CONSTRAINT [PK_Respuesta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoPago]    Script Date: 23/6/2023 19:07:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoPago](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
 CONSTRAINT [PK_TipoPago] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoUsuario]    Script Date: 23/6/2023 19:07:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoUsuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
 CONSTRAINT [PK_TipoUsuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 23/6/2023 19:07:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Apellido1] [varchar](50) NULL,
	[Apellido2] [varchar](50) NULL,
	[Cedula] [nvarchar](9) NULL,
	[Telefono] [nvarchar](8) NULL,
	[Correo] [varchar](250) NULL,
	[Clave] [varchar](250) NULL,
	[IdEstado] [int] NULL,
	[Foto] [varbinary](max) NULL,
	[Genero] [char](1) NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario_Tipo]    Script Date: 23/6/2023 19:07:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario_Tipo](
	[IdUsuario] [int] NOT NULL,
	[IdTipoUsuario] [int] NOT NULL,
 CONSTRAINT [PK_Usuario_Tipo] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC,
	[IdTipoUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CuentaPago]  WITH CHECK ADD  CONSTRAINT [FK_CuentaPago_TipoPago] FOREIGN KEY([IdTipoPago])
REFERENCES [dbo].[TipoPago] ([Id])
GO
ALTER TABLE [dbo].[CuentaPago] CHECK CONSTRAINT [FK_CuentaPago_TipoPago]
GO
ALTER TABLE [dbo].[CuentaPago]  WITH CHECK ADD  CONSTRAINT [FK_CuentaPago_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[CuentaPago] CHECK CONSTRAINT [FK_CuentaPago_Usuario]
GO
ALTER TABLE [dbo].[Direccion]  WITH CHECK ADD  CONSTRAINT [FK_Direccion_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[Direccion] CHECK CONSTRAINT [FK_Direccion_Usuario]
GO
ALTER TABLE [dbo].[Evaluacion]  WITH CHECK ADD  CONSTRAINT [FK_Evaluacion_Escala] FOREIGN KEY([IdEscala])
REFERENCES [dbo].[Escala] ([Id])
GO
ALTER TABLE [dbo].[Evaluacion] CHECK CONSTRAINT [FK_Evaluacion_Escala]
GO
ALTER TABLE [dbo].[Evaluacion]  WITH CHECK ADD  CONSTRAINT [FK_Evaluacion_Usuario] FOREIGN KEY([IdEvaluador])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[Evaluacion] CHECK CONSTRAINT [FK_Evaluacion_Usuario]
GO
ALTER TABLE [dbo].[Evaluacion]  WITH CHECK ADD  CONSTRAINT [FK_Evaluacion_Usuario1] FOREIGN KEY([IdEvaluado])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[Evaluacion] CHECK CONSTRAINT [FK_Evaluacion_Usuario1]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_CuentaPago] FOREIGN KEY([IdCuentaPago])
REFERENCES [dbo].[CuentaPago] ([Id])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_CuentaPago]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Orden] FOREIGN KEY([IdOrden])
REFERENCES [dbo].[Orden] ([IdOrden])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Orden]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Usuario]
GO
ALTER TABLE [dbo].[Foto]  WITH CHECK ADD  CONSTRAINT [FK_Foto_Producto] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Producto] ([IdProducto])
GO
ALTER TABLE [dbo].[Foto] CHECK CONSTRAINT [FK_Foto_Producto]
GO
ALTER TABLE [dbo].[Mensaje]  WITH CHECK ADD  CONSTRAINT [FK_Mensaje_Producto] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Producto] ([IdProducto])
GO
ALTER TABLE [dbo].[Mensaje] CHECK CONSTRAINT [FK_Mensaje_Producto]
GO
ALTER TABLE [dbo].[Mensaje]  WITH CHECK ADD  CONSTRAINT [FK_Mensaje_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[Mensaje] CHECK CONSTRAINT [FK_Mensaje_Usuario]
GO
ALTER TABLE [dbo].[Orden]  WITH CHECK ADD  CONSTRAINT [FK_Orden_Estado] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Estado] ([Id])
GO
ALTER TABLE [dbo].[Orden] CHECK CONSTRAINT [FK_Orden_Estado]
GO
ALTER TABLE [dbo].[OrdenDetalle]  WITH CHECK ADD  CONSTRAINT [FK_OrdenDetalle_Estado] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Estado] ([Id])
GO
ALTER TABLE [dbo].[OrdenDetalle] CHECK CONSTRAINT [FK_OrdenDetalle_Estado]
GO
ALTER TABLE [dbo].[OrdenDetalle]  WITH CHECK ADD  CONSTRAINT [FK_OrdenDetalle_Orden] FOREIGN KEY([IdOrden])
REFERENCES [dbo].[Orden] ([IdOrden])
GO
ALTER TABLE [dbo].[OrdenDetalle] CHECK CONSTRAINT [FK_OrdenDetalle_Orden]
GO
ALTER TABLE [dbo].[OrdenDetalle]  WITH CHECK ADD  CONSTRAINT [FK_OrdenDetalle_Producto] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Producto] ([IdProducto])
GO
ALTER TABLE [dbo].[OrdenDetalle] CHECK CONSTRAINT [FK_OrdenDetalle_Producto]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Categoria] FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Categoria] ([Id])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Categoria]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Estado] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Estado] ([Id])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Estado]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Usuario] FOREIGN KEY([IdProveedor])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Usuario]
GO
ALTER TABLE [dbo].[Respuesta]  WITH CHECK ADD  CONSTRAINT [FK_Respuesta_Mensaje] FOREIGN KEY([IdMensaje])
REFERENCES [dbo].[Mensaje] ([Id])
GO
ALTER TABLE [dbo].[Respuesta] CHECK CONSTRAINT [FK_Respuesta_Mensaje]
GO
ALTER TABLE [dbo].[Respuesta]  WITH CHECK ADD  CONSTRAINT [FK_Respuesta_Usuario] FOREIGN KEY([IdProveedor])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[Respuesta] CHECK CONSTRAINT [FK_Respuesta_Usuario]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Estado] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Estado] ([Id])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Estado]
GO
ALTER TABLE [dbo].[Usuario_Tipo]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Tipo_TipoUsuario] FOREIGN KEY([IdTipoUsuario])
REFERENCES [dbo].[TipoUsuario] ([Id])
GO
ALTER TABLE [dbo].[Usuario_Tipo] CHECK CONSTRAINT [FK_Usuario_Tipo_TipoUsuario]
GO
ALTER TABLE [dbo].[Usuario_Tipo]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Tipo_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[Usuario_Tipo] CHECK CONSTRAINT [FK_Usuario_Tipo_Usuario]
GO
USE [master]
GO
ALTER DATABASE [DBHardTech] SET  READ_WRITE 
GO
