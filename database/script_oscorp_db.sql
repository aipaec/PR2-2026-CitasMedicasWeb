USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'oscorp_db')
BEGIN
    ALTER DATABASE oscorp_db SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE oscorp_db;
END
GO

CREATE DATABASE oscorp_db;
GO
USE oscorp_db;
GO
CREATE TABLE usuarios (
    id_usuario   INT IDENTITY(1,1) PRIMARY KEY,
    nombre       VARCHAR(100) NOT NULL,
    correo       VARCHAR(100) NOT NULL UNIQUE,
    contrasena   VARCHAR(255) NOT NULL,
    tipo_usuario VARCHAR(20) NOT NULL CHECK (tipo_usuario IN ('Paciente', 'Doctor', 'Administrador'))
);
GO

CREATE TABLE especialidades (
    id_especialidad INT IDENTITY(1,1) PRIMARY KEY,
    nombre          VARCHAR(100) NOT NULL UNIQUE,
    descripcion     VARCHAR(255) NULL
);
GO

CREATE TABLE medicamentos (
    id_medicamento INT IDENTITY(1,1) PRIMARY KEY,
    nombre         VARCHAR(100) NOT NULL,
    descripcion    VARCHAR(255) NULL
);
GO
CREATE TABLE pacientes (
    id_paciente INT PRIMARY KEY,
    CONSTRAINT fk_pacientes_usuarios FOREIGN KEY (id_paciente)
        REFERENCES usuarios(id_usuario) ON DELETE CASCADE
);
GO
CREATE TABLE doctores (
    id_doctor       INT PRIMARY KEY,
    id_especialidad INT NOT NULL,
    CONSTRAINT fk_doctores_usuarios FOREIGN KEY (id_doctor)
        REFERENCES usuarios(id_usuario) ON DELETE CASCADE,
    CONSTRAINT fk_doctores_especialidades FOREIGN KEY (id_especialidad)
        REFERENCES especialidades(id_especialidad)
);
GO
CREATE TABLE horarios (
    id_horario INT IDENTITY(1,1) PRIMARY KEY,
    id_doctor  INT NOT NULL,
    fecha      DATE NOT NULL,
    hora_inicio TIME NOT NULL,
    hora_fin    TIME NOT NULL,
    estado     VARCHAR(20) NOT NULL DEFAULT 'Disponible' CHECK (estado IN ('Disponible', 'Ocupado', 'Cancelado')),
    CONSTRAINT fk_horarios_doctores FOREIGN KEY (id_doctor) 
        REFERENCES doctores(id_doctor)
);
GO

CREATE TABLE citas_medicas (
    id_cita         INT IDENTITY(1,1) PRIMARY KEY,
    id_paciente     INT NOT NULL,
    id_horario      INT NOT NULL UNIQUE, 
    fecha           DATE NOT NULL,
    hora            TIME NOT NULL,
    motivo_consulta VARCHAR(255) NOT NULL,
    estado          VARCHAR(20) NOT NULL DEFAULT 'Pendiente' CHECK (estado IN ('Pendiente', 'Completada', 'Cancelada')),
    CONSTRAINT fk_citas_pacientes FOREIGN KEY (id_paciente) 
        REFERENCES pacientes(id_paciente),
    CONSTRAINT fk_citas_horarios FOREIGN KEY (id_horario) 
        REFERENCES horarios(id_horario)
);
GO
CREATE TABLE comprobantes (
    id_comprobante INT IDENTITY(1,1) PRIMARY KEY,
    id_cita        INT NOT NULL UNIQUE,
    fecha_emision  DATETIME NOT NULL DEFAULT GETDATE(),
    tipo           VARCHAR(50) NOT NULL CHECK (tipo IN ('Factura', 'Recibo', 'Nota de Credito')),
    monto          DECIMAL(10, 2) NOT NULL CHECK (monto >= 0),
    CONSTRAINT fk_comprobantes_citas FOREIGN KEY (id_cita) 
        REFERENCES citas_medicas(id_cita)
);
GO

CREATE TABLE tratamientos (
    id_tratamiento INT IDENTITY(1,1) PRIMARY KEY,
    id_cita        INT NOT NULL,
    descripcion    VARCHAR(MAX) NOT NULL,
    fecha_inicio   DATE NOT NULL,
    fecha_final    DATE NOT NULL,
    CONSTRAINT fk_tratamientos_citas FOREIGN KEY (id_cita) 
        REFERENCES citas_medicas(id_cita)
);
GO

CREATE TABLE tratamiento_medicamentos (
    id_tratamiento INT NOT NULL,
    id_medicamento INT NOT NULL,
    dosis          VARCHAR(50) NOT NULL,
    frecuencia     VARCHAR(50) NOT NULL,
    PRIMARY KEY (id_tratamiento, id_medicamento),
    CONSTRAINT fk_tm_tratamientos FOREIGN KEY (id_tratamiento) 
        REFERENCES tratamientos(id_tratamiento) ON DELETE CASCADE,
    CONSTRAINT fk_tm_medicamentos FOREIGN KEY (id_medicamento) 
        REFERENCES medicamentos(id_medicamento)
);
GO
INSERT INTO usuarios (nombre, correo, contrasena, tipo_usuario) VALUES
('Dr. Roberto Gomez', 'roberto.gomez@oscorp.com', 'pass123', 'Doctor'),
('Dra. Maria Delgado', 'maria.delgado@oscorp.com', 'pass123', 'Doctor'),
('Dr. Carlos Mendoza', 'carlos.mendoza@oscorp.com', 'pass123', 'Doctor'),
('Dra. Ana Torres', 'ana.torres@oscorp.com', 'pass123', 'Doctor'),
('Dr. Luis Paredes', 'luis.paredes@oscorp.com', 'pass123', 'Doctor'),
('Juan Perez', 'juan.perez@gmail.com', 'client123', 'Paciente'),
('Sofia Rodriguez', 'sofia.rodriguez@gmail.com', 'client123', 'Paciente'),
('Mateo Fernandez', 'mateo.fernandez@gmail.com', 'client123', 'Paciente'),
('Lucia Morales', 'lucia.morales@gmail.com', 'client123', 'Paciente'),
('Diego Sanchez', 'diego.sanchez@gmail.com', 'client123', 'Paciente');

INSERT INTO especialidades (nombre, descripcion) VALUES
('Medicina General', 'Atención médica primaria y preventiva'),
('Pediatría', 'Atención médica para niños y adolescentes'),
('Cardiología', 'Enfermedades del corazón y sistema circulatorio'),
('Dermatología', 'Cuidado y tratamiento de la piel'),
('Neurología', 'Tratamiento de trastornos del sistema nervioso'),
('Ginecología', 'Salud del sistema reproductor femenino'),
('Oftalmología', 'Salud visual y enfermedades de los ojos'),
('Traumatología', 'Lesiones en huesos y músculos'),
('Psiquiatría', 'Salud mental y trastornos emocionales'),
('Odontología', 'Salud bucal y dental');

INSERT INTO pacientes (id_paciente) VALUES (6), (7), (8), (9), (10);

INSERT INTO doctores (id_doctor, id_especialidad) VALUES (1, 1), (2, 2), (3, 3), (4, 4), (5, 5);

INSERT INTO horarios (id_doctor, fecha, hora_inicio, hora_fin, estado) VALUES
(1, '2026-10-01', '08:00', '09:00', 'Ocupado'),
(1, '2026-10-01', '09:00', '10:00', 'Disponible'),
(2, '2026-10-01', '10:00', '11:00', 'Ocupado'),
(2, '2026-10-01', '11:00', '12:00', 'Disponible'),
(3, '2026-10-02', '14:00', '15:00', 'Ocupado'),
(3, '2026-10-02', '15:00', '16:00', 'Disponible'),
(4, '2026-10-03', '08:00', '09:00', 'Ocupado'),
(4, '2026-10-03', '09:00', '10:00', 'Disponible'),
(5, '2026-10-04', '11:00', '12:00', 'Ocupado'),
(5, '2026-10-04', '12:00', '13:00', 'Disponible');

INSERT INTO citas_medicas (id_paciente, id_horario, fecha, hora, motivo_consulta, estado) VALUES
(6, 1, '2026-10-01', '08:00', 'Chequeo general de rutina', 'Completada'),
(7, 3, '2026-10-01', '10:00', 'Fiebre alta y dolor de garganta', 'Completada'),
(8, 5, '2026-10-02', '14:00', 'Evaluación de presión arterial', 'Completada'),
(9, 7, '2026-10-03', '08:00', 'Aparición de sarpullido en los brazos', 'Completada'),
(10, 9, '2026-10-04', '11:00', 'Migrañas frecuentes e insomnio', 'Pendiente');

INSERT INTO comprobantes (id_cita, fecha_emision, tipo, monto) VALUES
(1, '2026-10-01 08:50:00', 'Factura', 150.00),
(2, '2026-10-01 10:45:00', 'Recibo', 180.00),
(3, '2026-10-02 14:50:00', 'Factura', 250.00),
(4, '2026-10-03 08:55:00', 'Factura', 200.00),
(5, '2026-10-04 11:30:00', 'Recibo', 220.00);

INSERT INTO tratamientos (id_cita, descripcion, fecha_inicio, fecha_final) VALUES
(1, 'Suplementación vitamínica y dieta balanceada', '2026-10-01', '2026-10-30'),
(2, 'Reposo absoluto e hidratación', '2026-10-01', '2026-10-07'),
(3, 'Monitoreo diario de presión y reducción de sodio', '2026-10-02', '2026-11-02'),
(4, 'Uso de crema dermatológica e antihistamínicos', '2026-10-03', '2026-10-17'),
(5, 'Terapia de relajación y ajuste de hábitos de sueño', '2026-10-04', '2026-11-04');

INSERT INTO medicamentos (nombre, descripcion) VALUES
('Paracetamol 500mg', 'Analgésico y antipirético'),
('Ibuprofeno 400mg', 'Antiinflamatorio no esteroideo'),
('Amoxicilina 500mg', 'Antibiótico de amplio espectro'),
('Enalapril 10mg', 'Antihipertensivo'),
('Loratadina 10mg', 'Antihistamínico para alergias'),
('Omeprazol 20mg', 'Inhibidor de la bomba de protones'),
('Metformina 850mg', 'Antidiabético oral'),
('Losartan 50mg', 'Antihipertensivo'),
('Clonazepam 0.5mg', 'Ansiolítico'),
('Complejo B', 'Suplemento vitamínico');

INSERT INTO tratamiento_medicamentos (id_tratamiento, id_medicamento, dosis, frecuencia) VALUES
(1, 10, '1 tableta', 'Cada 24 horas'),
(2, 1, '1 tableta', 'Cada 8 horas'),
(2, 3, '1 cápsula', 'Cada 8 horas'),
(3, 4, '1 tableta', 'Cada 12 horas'),
(4, 5, '1 tableta', 'Cada 24 horas');
GO
