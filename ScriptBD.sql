
/*SCRIPT PRUEBA CRISTIAN ARROYO
Generacion de la base de datos*/
CREATE DATABASE PruebaCristian
USE PruebaCristian

/* Generacion de tabla de cartas para el guardado de las mismas*/
CREATE TABLE Cartas (
Id INT IDENTITY(1,1) PRIMARY KEY,
Nombre VARCHAR (50) NOT NULL,
Imagen VARBINARY(MAX) NOT NULL,
IdTabla INT 
)

/*Generacion de tabla que contendra las 16 cartas al azar*/
CREATE TABLE Tabla (
Id INT IDENTITY(1,1) PRIMARY KEY,
IdCarta INT,
NumCarta INT
)