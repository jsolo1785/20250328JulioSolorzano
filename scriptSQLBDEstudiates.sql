USE [rEstudiante]
GO
/****** Object:  Table [dbo].[estudiantes]    Script Date: 28/03/2025 16:31:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[estudiantes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [varchar](50) NULL,
	[nombre] [varchar](250) NULL,
	[apllidos] [varchar](250) NULL,
	[fechaNacimiento] [date] NULL,
	[edad] [int] NULL,
	[correo] [varchar](50) NULL,
	[usercreate] [int] NULL,
	[datecreate] [datetime] NULL,
	[userupdate] [int] NULL,
	[dateupdate] [datetime] NULL,
 CONSTRAINT [PK_estudiantes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[materias]    Script Date: 28/03/2025 16:31:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[materias](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codestudiante] [varchar](50) NULL,
	[codigo] [varchar](50) NULL,
	[materia] [varchar](150) NULL,
	[instructor] [varchar](50) NULL,
	[horario] [varchar](50) NULL,
	[ubicacion] [varchar](50) NULL,
	[usercreate] [int] NULL,
	[datecreate] [datetime] NULL,
	[userupdate] [int] NULL,
	[dateupdate] [datetime] NULL,
 CONSTRAINT [PK_materias] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[spCRUDEstudiante]    Script Date: 28/03/2025 16:31:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spCRUDEstudiante]
    @bandera int,  
    @id INT = NULL,
    @codigo VARCHAR(50) = NULL,
    @nombre VARCHAR(250) = NULL,
    @apllidos VARCHAR(250) = NULL,
    @fechaNacimiento DATE = NULL,
    @edad INT = NULL,
    @correo VARCHAR(50) = NULL,
    @usercreate INT = NULL,
    @userupdate INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @bandera = 1
    BEGIN
 SELECT [id]
      ,[codigo]
      ,[nombre]
      ,[apllidos]
      ,convert(char(10),[fechaNacimiento],121) as [fechaNacimiento]
      ,[edad]
      ,[correo]
      ,[usercreate]
      ,[datecreate]
      ,[userupdate]
      ,[dateupdate]

		FROM estudiantes
    END

    IF @bandera = 2
    BEGIN
    
	 SELECT [id]
      ,[codigo]
      ,[nombre]
      ,[apllidos]
      ,convert(char(10),[fechaNacimiento],121) as [fechaNacimiento]
      ,[edad]
      ,[correo]
      ,[usercreate]
      ,[datecreate]
      ,[userupdate]
      ,[dateupdate]

	FROM estudiantes WHERE codigo like  '%'+@codigo+'%'
    END

    IF @bandera = 3
    BEGIN
        INSERT INTO estudiantes (codigo, nombre, apllidos, fechaNacimiento, edad, correo, usercreate, datecreate, userupdate, dateupdate)
        VALUES (@codigo, @nombre, @apllidos, @fechaNacimiento, @edad, @correo, @usercreate, GETDATE(),@usercreate, GETDATE());
    END

    IF @bandera = 4
    BEGIN
        UPDATE estudiantes
        SET codigo = @codigo,
            nombre = @nombre,
            apllidos = @apllidos,
            fechaNacimiento = @fechaNacimiento,
            edad = @edad,
            correo = @correo,
            userupdate = @userupdate,
            dateupdate = GETDATE()
        WHERE id = @id
    END

    IF @bandera = 5
    BEGIN
        DELETE FROM estudiantes WHERE codigo like  '%'+@codigo+'%'
    END
END;

GO
/****** Object:  StoredProcedure [dbo].[spCRUDMaterias]    Script Date: 28/03/2025 16:31:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spCRUDMaterias]
    @bandera int,  
	@codestudiante varchar(50) = NULL,
    @id INT = NULL,
    @codigo VARCHAR(50) = NULL,
    @materia VARCHAR(150) = NULL,
    @instructor VARCHAR(50) = NULL,
    @horario VARCHAR(50) = NULL,
    @ubicacion VARCHAR(50) = NULL,
    @usercreate INT = NULL,
    @userupdate INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @bandera = 1
    BEGIN
   SELECT  [id]
      ,[codestudiante]
      ,[codigo]
      ,[materia]
      ,[instructor]
      ,[horario]
      ,[ubicacion]
      ,[usercreate]
      ,[datecreate]
      ,[userupdate]
      ,[dateupdate]
	  
	  FROM materias
    END

    IF @bandera = 2
    BEGIN
    SELECT  [id]
      ,[codestudiante]
      ,[codigo]
      ,[materia]
      ,[instructor]
      ,[horario]
      ,[ubicacion]
      ,[usercreate]
      ,[datecreate]
      ,[userupdate]
      ,[dateupdate]
	  
	  FROM materias WHERE codestudiante  like '%' + @codestudiante + '%'
    END

   IF @bandera = 3
    BEGIN
        INSERT INTO materias (codestudiante,codigo, materia, instructor, horario, ubicacion, usercreate, datecreate)
        VALUES (@codestudiante,@codigo, @materia, @instructor, @horario, @ubicacion, @usercreate, GETDATE())
    END

    IF @bandera = 4
    BEGIN
        UPDATE materias
        SET codestudiante = @codestudiante,
		    codigo = @codigo,
            materia = @materia,
            instructor = @instructor,
            horario = @horario,
            ubicacion = @ubicacion,
            userupdate = @userupdate,
            dateupdate = GETDATE()
        WHERE id = @id
    END

    IF @bandera = 5
    BEGIN
        DELETE FROM materias WHERE codestudiante = @codestudiante
    END
END;
GO
