# OSCORP Medical - ASP.NET Core MVC

Proyecto inicial de aplicación web C# basado en el informe OSCORP y en el esquema SQL Server existente.

## Requisitos
- Visual Studio 2022/2026 con carga de trabajo ASP.NET y desarrollo web.
- .NET 8 SDK.
- SQL Server y SQL Server Management Studio.

## Configuración
1. Crear la base de datos ejecutando `database/script_oscorp_db.sql` en SQL Server **solo en una base de datos de pruebas**. El script original elimina y vuelve a crear `oscorp_db`.
2. Abrir `OSCORP.Web.csproj` en Visual Studio.
3. Revisar `appsettings.json` y cambiar `OscorpConnection` para que coincida con el servidor SQL Server local.
4. Restaurar paquetes NuGet y ejecutar el proyecto (F5).

## Funcionalidades implementadas
- Login con roles Paciente, Doctor y Administrador.
- Registro de pacientes.
- Listado de horarios disponibles.
- Reserva transaccional de citas y validación para evitar doble reserva.
- Creación de horarios futuros por el administrador, con validación de cruces.
- Agenda del doctor.
- Panel de administración con resumen y consulta de horarios.
- Comprobante imprimible/guardable como PDF desde el navegador.

## Cuentas de prueba del SQL original
Los horarios de ejemplo del SQL están fechados en octubre de 2026; agrega horarios futuros desde el panel de administración para probar reservas actuales. El SQL incluye contraseñas de ejemplo en texto plano: doctores `pass123`; pacientes `client123`. En el primer inicio de sesión válido, la aplicación reemplaza esa contraseña por un hash seguro. Cambia o elimina estas cuentas antes de desplegar el sistema.

## Notas importantes
- El documento limita el alcance a la reserva de espacios; el comprobante no cobra ni registra pagos. El campo `monto` de la tabla existente no se usa.
- El registro público crea únicamente pacientes. Las cuentas de Doctor y Administrador deben crearse por un procedimiento administrativo seguro.
- No se implementan SMS, WhatsApp, correo automático, historia clínica, pagos ni sincronización con calendarios externos.
- El bloqueo de reserva usa una transacción `Serializable` y además respeta el índice único existente de `citas_medicas.id_horario`.
- Este paquete es una base funcional de desarrollo. Antes de producción se requiere configurar HTTPS, secretos fuera del repositorio, política de contraseñas, copias de seguridad y pruebas de integración.
