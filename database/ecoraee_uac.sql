-- ============================================
-- BASE DE DATOS: EcoRAEE UAC
-- Universidad Andina del Cusco - 2026
-- ============================================

CREATE DATABASE IF NOT EXISTS ecoraee_uac
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE ecoraee_uac;

-- TABLA: Campañas Ambientales
CREATE TABLE Campanas (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nombre VARCHAR(200) NOT NULL,
  Fecha DATE NOT NULL,
  Lugar VARCHAR(200) NOT NULL,
  Responsable VARCHAR(150) NOT NULL,
  Descripcion TEXT,
  Estado ENUM('Planificada','En Curso','Finalizada') DEFAULT 'Planificada',
  FechaCreacion DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- TABLA: Participantes
CREATE TABLE Participantes (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Nombres VARCHAR(150) NOT NULL,
  Apellidos VARCHAR(150) NOT NULL,
  TipoParticipante ENUM('Estudiante','Docente','Ciudadano') NOT NULL,
  DNI VARCHAR(20),
  Email VARCHAR(150),
  Telefono VARCHAR(20),
  CampanaId INT NOT NULL,
  FechaRegistro DATETIME DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (CampanaId) REFERENCES Campanas(Id) ON DELETE CASCADE
);

-- TABLA: Material Educativo
CREATE TABLE MaterialEducativo (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  Titulo VARCHAR(200) NOT NULL,
  Tipo ENUM('Infografia','Video','PDF','Noticia') NOT NULL,
  Descripcion TEXT,
  UrlArchivo VARCHAR(500),
  FechaPublicacion DATE NOT NULL,
  CampanaId INT,
  FOREIGN KEY (CampanaId) REFERENCES Campanas(Id) ON DELETE SET NULL
);

-- TABLA: Recolección RAEE
CREATE TABLE RecoleccionRaee (
  Id INT AUTO_INCREMENT PRIMARY KEY,
  TipoResiduo VARCHAR(150) NOT NULL,
  CantidadKg DECIMAL(10,2) NOT NULL,
  LugarRecoleccion VARCHAR(200) NOT NULL,
  Fecha DATE NOT NULL,
  CampanaId INT NOT NULL,
  Observaciones TEXT,
  FOREIGN KEY (CampanaId) REFERENCES Campanas(Id) ON DELETE CASCADE
);

-- DATOS DE PRUEBA
INSERT INTO Campanas (Nombre, Fecha, Lugar, Responsable, Descripcion, Estado)
VALUES
  ('Campaña RAEE Semestre 2026-I', '2026-04-15', 'Campus Principal UAC',
   'Mtro. Ing. Luis Monzón', 'Primera campaña de recolección del semestre', 'Finalizada'),
  ('Sensibilización Digital RAEE', '2026-05-20', 'Facultad de Ingeniería',
   'Comisión RSU', 'Charlas y recolección de residuos electrónicos', 'EnCurso');
