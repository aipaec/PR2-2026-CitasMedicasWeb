# Automatización CI

Esta carpeta queda preparada para incorporar GitHub Actions.

No se agrega todavía un workflow de compilación porque el repositorio revisado no contiene una solución `.sln` ni proyectos `.csproj`, y no se conoce la versión objetivo del SDK .NET. Crear un pipeline con esos datos inventados produciría una validación que no representa el proyecto real.

Cuando se publique el código:
1. Confirmar la versión de .NET en el proyecto.
2. Añadir un workflow que restaure dependencias.
3. Compilar la solución en modo Release.
4. Ejecutar los proyectos de pruebas.
5. Publicar los resultados como artefactos cuando sea útil.

El workflow debe fallar si la compilación o las pruebas fallan.
