CREATE DATABASE ProyectoInventarioBorrar;
GO

USE ProyectoInventarioBorrar;
GO

CREATE TABLE Almacen(
	id int not null identity(1,1) PRIMARY KEY,
	nombre nvarchar(100) not null,
	direccion nvarchar(255) not null,
	telefono nvarchar(15)
);

CREATE TABLE Rol(
	id int not null identity(1,1) PRIMARY KEY,
	nombre nvarchar(100) not null,
	estado bit DEFAULT 1 NOT NULL
);

CREATE TABLE Persona (
  id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
  nombre nvarchar(80) NOT NULL,
  apellido nvarchar(80) NOT NULL,
  ci nvarchar(20),
  telefono nvarchar(20) NOT NULL,)


CREATE TABLE Usuario (
  id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
  username nvarchar(100) UNIQUE NOT NULL,
  contrasena nvarchar(255) NOT NULL,
  horarioLaboral nvarchar(30) NOT NULL,
  rol_id int NOT NULL FOREIGN KEY REFERENCES Rol(id),
  almacen_id int NOT NULL FOREIGN KEY REFERENCES Almacen(id),
  persona_id int NOT NULL FOREIGN KEY REFERENCES Persona(id),
  fecha datetime DEFAULT CURRENT_TIMESTAMP NOT NULL,
  estado bit DEFAULT 1 NOT NULL
)

CREATE TABLE Zapato(
	id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	codigo  nvarchar(255) NOT NULL,
	nombre  nvarchar(255) NOT NULL,
	modelo  nvarchar(255) NOT NULL,
	talla  nvarchar(255) NOT NULL,
	color  nvarchar(255) NOT NULL,
	stock int NOT NULL,
	precio DECIMAL(12, 2) NOT NULL,
	img nvarchar(255) NOT NULL,
	usuario_id  int NOT NULL FOREIGN KEY REFERENCES Usuario(id),
	fecha datetime DEFAULT CURRENT_TIMESTAMP NOT NULL,
	estado bit DEFAULT 1 NOT NULL
)

CREATE TABLE Reporte(
	id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	observaciones  nvarchar(255) NOT NULL,
	usuario_id  int NOT NULL FOREIGN KEY REFERENCES Usuario(id),
	fecha datetime DEFAULT CURRENT_TIMESTAMP NOT NULL,
)

 --INSERTS INICIALES
INSERT INTO Almacen(nombre, direccion, telefono) VALUES ('Almacen Perez','Calle la calle del barrio', '75342333')
INSERT INTO Rol (nombre) VALUES ('Administrador');

INSERT INTO Persona (nombre,apellido,telefono,ci) VALUES ('Dante', 'Arias' ,'658234434','154343412'); 
INSERT INTO Usuario (username, contrasena, horarioLaboral, rol_id, almacen_id, persona_id, estado)
VALUES ('admin', 'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=', 'Ma�ana',1, 1, 1, 1);

