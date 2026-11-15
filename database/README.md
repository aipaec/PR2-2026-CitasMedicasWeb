# Base de datos

## Archivo disponible

- `script_oscorp_db.sql`: crea la base `oscorp_db`, tablas, restricciones y registros de demostración para SQL Server.

## Ejecución en desarrollo

1. Instala y abre una instancia compatible de Microsoft SQL Server.
2. Abre el archivo en SQL Server Management Studio o una herramienta compatible.
3. Verifica que estás conectado a una instancia de desarrollo.
4. Lee el inicio del script antes de ejecutarlo.
5. Ejecuta el script y comprueba que se creó `oscorp_db` y sus tablas.

## Advertencias

- El script elimina y vuelve a crear la base `oscorp_db` si ya existe. Esto borra los datos de esa base. No ejecutarlo en producción ni sobre datos que deban conservarse.
- Incluye cuentas y contraseñas ficticias de demostración almacenadas como texto plano. No reutilizar esas credenciales ni ese método en un sistema real.
- Las fechas y reservas de ejemplo son datos semilla, no disponibilidad actual.
- Realizar copias de seguridad antes de cualquier operación destructiva.

## Conexión desde la aplicación

La cadena de conexión debe configurarse mediante secretos de desarrollo o variables de entorno y no debe guardarse con credenciales reales en Git. La tecnología y el mecanismo exactos deben documentarse cuando se publique el proyecto .NET.
