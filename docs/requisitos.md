# Requisitos del sistema — OSCORP Medical

## Documento de referencia

El documento de requisitos y casos de uso existente se conserva en:

[`requisitos/Documento de Requisitos y Casos de Uso.docx`](requisitos/Documento%20de%20Requisitos%20y%20Casos%20de%20Uso.docx)

Este archivo Markdown funciona como índice y registro de trazabilidad; no reemplaza ni modifica el documento original.

## Requisitos identificados en el alcance y el esquema SQL

Los siguientes puntos se desprenden del alcance descrito y de las entidades presentes en `database/script_oscorp_db.sql`. Deben contrastarse con los casos de uso aprobados:

- RF-01: gestionar usuarios y distinguir los tipos Paciente, Doctor y Administrador.
- RF-02: registrar pacientes asociados a una cuenta de usuario.
- RF-03: registrar doctores y asociarlos a una especialidad.
- RF-04: mantener un catálogo de especialidades.
- RF-05: registrar horarios de atención de los doctores.
- RF-06: representar citas con estados Pendiente, Completada y Cancelada.
- RF-07: asociar cada cita a un paciente y a un horario.
- RF-08: registrar comprobantes vinculados a citas.
- RF-09: registrar tratamientos y relacionarlos con medicamentos.

## Requisitos no funcionales iniciales

- RNF-01: persistencia de datos en Microsoft SQL Server.
- RNF-02: validación de entradas y manejo controlado de errores.
- RNF-03: control de acceso según el tipo de usuario.
- RNF-04: no almacenar secretos en el repositorio.
- RNF-05: documentar los pasos de instalación y ejecución una vez confirmada la solución .NET.

## Trazabilidad pendiente

Cuando se publique el código fuente, el equipo debe vincular cada requisito con su pantalla, controlador/servicio, tabla relacionada y prueba correspondiente. No se asignan estados de implementación sin verificar el código.
