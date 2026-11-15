# Visión y alcance — OSCORP Medical

## 1. Visión

OSCORP Medical busca ofrecer una aplicación web que facilite la organización de citas médicas y la administración de información relacionada con pacientes, médicos, especialidades y horarios.

## 2. Problema

La coordinación manual de turnos puede generar demoras, duplicación de reservas y dificultades para consultar la disponibilidad de los profesionales. El sistema propone centralizar esa información para apoyar la programación y seguimiento de citas.

## 3. Objetivo general

Desarrollar un producto mínimo viable (MVP) web para gestionar pacientes, médicos, especialidades, horarios y citas médicas, con persistencia en SQL Server.

## 4. Usuarios previstos

- **Paciente:** consulta opciones de atención y gestiona sus citas, según las funciones que se implementen.
- **Doctor:** administra o consulta sus horarios y la información de las citas asignadas, según permisos.
- **Administrador:** gestiona usuarios y catálogos del sistema.

Los permisos y pantallas definitivos deben corresponder a la implementación y al documento de requisitos aprobado.

## 5. Alcance funcional identificado

El script SQL actual contempla:
- Usuarios con tipos Paciente, Doctor y Administrador.
- Especialidades médicas.
- Registro de pacientes y doctores asociados a usuarios.
- Horarios de atención y sus estados.
- Citas médicas y sus estados.
- Comprobantes asociados a citas.
- Tratamientos y medicamentos relacionados.

La existencia de tablas en la base de datos no demuestra por sí sola que todas estas funciones estén implementadas en la interfaz web.

## 6. Fuera del alcance confirmado

No se da por confirmada ninguna integración externa, despliegue público, pasarela de pagos, envío de notificaciones, firma digital ni conexión con sistemas hospitalarios. Deben añadirse solo si el equipo las define y desarrolla.

## 7. Criterios generales de aceptación

- Los roles deben tener permisos diferenciados.
- No se deben asignar dos citas al mismo horario.
- Los datos deben persistir correctamente en SQL Server.
- Las entradas deben validarse antes de guardarse.
- Las operaciones deben respetar las relaciones y restricciones de la base de datos.
- La instalación y ejecución deben poder repetirse siguiendo instrucciones documentadas.

## 8. Estado

Documento elaborado a partir del README, el script de base de datos, el documento de requisitos y la estructura actual del código fuente publicado en `src/OSCORP.Web/`. Debe seguir revisándose conforme avance el desarrollo y se compruebe el funcionamiento de la aplicación.
