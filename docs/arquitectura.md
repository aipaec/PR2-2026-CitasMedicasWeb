# Arquitectura — OSCORP Medical

## 1. Estado de la arquitectura

El README declara C#, ASP.NET Core MVC, HTML, CSS, JavaScript, Bootstrap y SQL Server como tecnologías utilizadas o planificadas. En la revisión de la estructura del repositorio no se encontró una solución `.sln`, un proyecto `.csproj` ni código de aplicación bajo `src/`. Por tanto, esta documentación describe la arquitectura lógica prevista y no afirma que ya exista una implementación comprobada.

## 2. Vista lógica propuesta

- **Presentación:** vistas web y recursos de interfaz (HTML, CSS, JavaScript y Bootstrap).
- **Aplicación:** controladores y servicios que procesan las operaciones y aplican reglas de negocio.
- **Persistencia:** acceso a datos y consultas contra SQL Server.
- **Base de datos:** esquema `oscorp_db`, definido en `database/script_oscorp_db.sql`.

## 3. Módulos funcionales previstos

1. Usuarios y autenticación.
2. Pacientes.
3. Doctores y especialidades.
4. Horarios.
5. Citas médicas.
6. Comprobantes.
7. Tratamientos y medicamentos.

## 4. Flujo general previsto

1. El usuario accede a la aplicación.
2. El sistema identifica el rol y autoriza las operaciones correspondientes.
3. El usuario realiza una consulta o registra una operación.
4. La capa de aplicación valida los datos y las reglas.
5. La capa de persistencia guarda o consulta la información en SQL Server.
6. La aplicación presenta el resultado.

## 5. Seguridad y configuración

- Las credenciales deben configurarse fuera del repositorio.
- Las contraseñas de usuarios deben almacenarse mediante un mecanismo de hashing seguro; el script de demostración actual contiene valores de ejemplo en texto plano y no debe usarse así en producción.
- Las operaciones deben validar autorización en el servidor, no solo ocultar botones en la interfaz.
- Las cadenas de conexión no deben incluirse en archivos versionados con credenciales reales.

## 6. Pendiente de completar con la implementación

- Nombre y ubicación de la solución y proyectos .NET.
- Versión exacta de .NET y paquetes NuGet.
- Diagrama de componentes y despliegue.
- Patrón real de acceso a datos (por ejemplo, Entity Framework Core o ADO.NET).
- Configuración de autenticación y autorización implementada.
- Entorno de ejecución y servidor de despliegue.

Actualizar este documento cuando se incorpore el código para que describa la arquitectura real.
