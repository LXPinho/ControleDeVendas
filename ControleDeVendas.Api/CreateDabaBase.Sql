USE [Vendas]
GO

/*
 * DROP TABLES
 *******************************************************************************************************/

/*
 * DROP PEDIDOOITENS
 */
/****** Object:  Table [dbo].[PedidoItens]    Script Date: 09/12/2023 22:24:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PedidoItens]') AND type in (N'U'))
DROP TABLE [dbo].[PedidoItens]
GO

/*
 * DROP PEDIDOO
 */
/****** Object:  Table [dbo].[Pedido]    Script Date: 09/12/2023 22:22:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pedido]') AND type in (N'U'))
DROP TABLE [dbo].[Pedido]
GO

/*
 * DROP PRODUTO
 */
 /****** Object:  Table [dbo].[Produto]    Script Date: 09/12/2023 22:21:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Produto]') AND type in (N'U'))
DROP TABLE [dbo].[Produto]
GO

 /*
  * DROP CLIENTE
  */
/****** Object:  Table [dbo].[Cliente]    Script Date: 09/12/2023 22:19:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cliente]') AND type in (N'U'))
DROP TABLE [dbo].[Cliente]
GO

/*
 * CREATE TABLES
 *******************************************************************************************************/
 
 /*
  * CREATE CLIENTE
  */
 /****** Object:  Table [dbo].[Cliente]    Script Date: 09/12/2023 22:26:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cliente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nchar](50) NOT NULL,
	[Telefone] [nchar](15) NOT NULL,
	[Email] [nchar](50) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

 /*
  * CREATE PRODUTO
  */
/****** Object:  Table [dbo].[Produto]    Script Date: 09/12/2023 22:27:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Produto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [nchar](50) NOT NULL,
	[ValorUnitario] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_Produto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

 /*
  * CREATE PEDIDO
  */
/****** Object:  Table [dbo].[Pedido]    Script Date: 09/12/2023 22:28:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Pedido](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCliente] [int] NOT NULL,
	[DataDaVenda] [date] NOT NULL,
 CONSTRAINT [PK_Pedido] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Pedido] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Cliente] ([Id])
GO

ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_Pedido]
GO

 /*
  * CREATE PEDIDO ITEM
  */
/****** Object:  Table [dbo].[PedidoItens]    Script Date: 09/12/2023 22:30:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PedidoItens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdPedido] [int] NOT NULL,
	[IdProduto] [int] NOT NULL,
	[Quantidade] [int] NOT NULL,
 CONSTRAINT [PK_PedidoItens] PRIMARY KEY CLUSTERED 
(
	[IdPedido] ASC,
	[IdProduto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PedidoItens]  WITH CHECK ADD  CONSTRAINT [FK_PedidoItens_Pedido] FOREIGN KEY([IdPedido])
REFERENCES [dbo].[Pedido] ([Id])
GO

ALTER TABLE [dbo].[PedidoItens] CHECK CONSTRAINT [FK_PedidoItens_Pedido]
GO

ALTER TABLE [dbo].[PedidoItens]  WITH CHECK ADD  CONSTRAINT [FK_PedidoItens_Produto] FOREIGN KEY([IdProduto])
REFERENCES [dbo].[Produto] ([Id])
GO

ALTER TABLE [dbo].[PedidoItens] CHECK CONSTRAINT [FK_PedidoItens_Produto]
GO

