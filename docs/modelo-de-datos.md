# Modelo de datos — OSCORP Medical

## 1. Fuente

Modelo documentado a partir de `database/script_oscorp_db.sql`. El script crea la base `oscorp_db`, sus tablas, restricciones y datos de demostración.

## 2. Entidades y atributos principales

| Tabla | Clave primaria | Atributos y relaciones principales |
|---|---|---|
| usuarios | id_usuario | nombre, correo (único), contrasena, tipo_usuario |
| especialidades | id_especialidad | nombre (único), descripcion |
| medicamentos | id_medicamento | nombre, descripcion |
| pacientes | id_paciente | FK a usuarios.id_usuario |
| doctores | id_doctor | FK a usuarios.id_usuario; FK a especialidades.id_especialidad |
| horarios | id_horario | FK a doctores.id_doctor; fecha, hora_inicio, hora_fin, estado |
| citas_medicas | id_cita | FK a pacientes.id_paciente; FK única a horarios.id_horario; fecha, hora, motivo_consulta, estado |
| comprobantes | id_comprobante | FK única a citas_medicas.id_cita; fecha_emision, tipo, monto |
| tratamientos | id_tratamiento | FK a citas_medicas.id_cita; descripcion, fecha_inicio, fecha_final |
| tratamiento_medicamentos | (id_tratamiento, id_medicamento) | tabla puente; dosis y frecuencia |

## 3. Relaciones

- Un usuario puede tener un registro de paciente o de doctor, según su tipo.
- Cada doctor pertenece a una especialidad.
- Un doctor puede tener varios horarios.
- Un horario puede estar asociado como máximo a una cita, debido a la restricción UNIQUE sobre `citas_medicas.id_horario`.
- Una cita pertenece a un paciente y a un horario.
- Una cita puede tener como máximo un comprobante, por la restricción UNIQUE de `comprobantes.id_cita`.
- Una cita puede tener varios tratamientos.
- Un tratamiento puede incluir varios medicamentos y un medicamento puede aparecer en varios tratamientos, mediante `tratamiento_medicamentos`.

## 4. Reglas y restricciones presentes en SQL

- El correo de usuario es único.
- El tipo de usuario admite Paciente, Doctor o Administrador.
- El estado del horario admite Disponible, Ocupado o Cancelado.
- El estado de la cita admite Pendiente, Completada o Cancelada.
- El monto del comprobante no puede ser negativo.
- El tipo de comprobante admite Factura, Recibo o Nota de Credito.
- La tabla puente de tratamiento y medicamento utiliza una clave primaria compuesta.

## 5. Observaciones importantes

- El script comienza eliminando y recreando `oscorp_db`. Ejecutarlo de nuevo puede borrar los datos existentes de esa base. Usar únicamente en un entorno de desarrollo o prueba y hacer una copia de seguridad antes.
- Los datos de usuarios incluidos son demostrativos. El campo `contrasena` recibe cadenas en texto plano; no es un mecanismo aceptable para producción. La aplicación debe aplicar hashing seguro y el esquema debe revisarse para almacenar hashes.
- Las fechas de ejemplo son de octubre de 2026 y deben tratarse como datos semilla, no como disponibilidad vigente.
- El esquema no muestra por sí solo reglas completas para impedir horarios superpuestos ni garantizar que la fecha/hora de una cita coincida con el horario referenciado. Esas validaciones deben definirse e implementarse.

## 6. Diagrama

El diagrama entidad-relación debe añadirse en `docs/diagramas/` y mantenerse sincronizado con el script SQL.
