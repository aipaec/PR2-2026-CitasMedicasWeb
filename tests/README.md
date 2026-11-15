# Pruebas

Esta carpeta está destinada a guardar las pruebas del sistema OSCORP Medical.

## Estado actual

El repositorio contiene el código fuente de la aplicación ASP.NET Core MVC en `src/OSCORP.Web/`. Sin embargo, todavía no se ha publicado un proyecto de pruebas automatizadas.

## Cobertura recomendada

Cuando se implementen las pruebas, se recomienda verificar:

- Validación de datos de usuarios y roles.
- Disponibilidad y no duplicación de horarios.
- Creación, cancelación y cambio de estado de citas.
- Relaciones entre citas, comprobantes, tratamientos y medicamentos.
- Autorización de operaciones según el rol del usuario.

## Próximos pasos

Crear un proyecto de pruebas compatible con .NET 8, registrar los comandos necesarios para ejecutarlas y documentar los resultados reales.

Por el momento, las pruebas automatizadas están pendientes. No se debe considerar que los módulos están verificados mediante pruebas hasta que estas se ejecuten y se registren sus resultados.
