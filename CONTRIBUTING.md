# Guía de contribución

Gracias por colaborar con OSCORP Medical.

## Antes de empezar
1. Revisa la documentación existente en `docs/`.
2. Consulta los Issues abiertos para evitar duplicar trabajo.
3. Si el cambio es grande, acuerda primero su alcance con el equipo.
4. No publiques contraseñas, cadenas de conexión reales, datos personales ni archivos de configuración local.

## Flujo de trabajo
1. Actualiza tu copia local de la rama `main`.
2. Crea una rama descriptiva, por ejemplo `feature/gestion-citas`, `fix/validacion-horarios` o `docs/modelo-datos`.
3. Realiza un cambio pequeño y enfocado.
4. Ejecuta las pruebas disponibles. Si todavía no existe una suite automatizada, indícalo en la descripción del cambio.
5. Revisa `git diff` para comprobar que no incluiste archivos generados ni secretos.
6. Abre un Pull Request hacia `main`, explica el motivo, los cambios, las pruebas y cualquier limitación.

## Convención de commits
Usa mensajes breves y descriptivos:
- `feat: agregar gestión de horarios`
- `fix: validar disponibilidad de citas`
- `docs: documentar instalación`
- `test: agregar pruebas de reservas`
- `chore: configurar editorconfig`

## Criterios de revisión
- Mantener coherencia con los requisitos y el modelo de datos documentado.
- Evitar cambios no relacionados.
- No incluir credenciales ni datos médicos reales.
- Actualizar la documentación cuando cambie el comportamiento, la configuración o la base de datos.
- No declarar una función como terminada si no está implementada y probada.
