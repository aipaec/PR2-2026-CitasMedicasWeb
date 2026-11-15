# Modelo de Datos y Entidad-Relación — Sistema OSCORP

* **Proyecto:** Sistema de Gestión de Citas Médicas (`PR2-2026-CitasMedicasWeb`)
* **Base de Datos:** `oscorp_db` (Microsoft SQL Server)
* **Notación Conceptual:** Modelo Entidad-Relación (Peter Chen) con restricciones Min-Max
* **Integrantes:**
  * Julio Lara Laura
  * Yury Sebastián Vasquez
  * Jhoel Ricardo Pita Silva
  *(Nota: Agregar tu nombre completo en esta lista antes de hacer commit)*

---

## 1. Herramientas y Estándares de Modelado

* **Diagrams.net (Draw.io):** Construcción gráfica del diagrama Entidad-Relación en formato vectorial/XML (ubicado en `docs/diagramas/`).
* **Notación Peter Chen:** Estándar conceptual utilizado para representar:
  * **Rectángulos:** Entidades fuertes y asociativas.
  * **Rombos:** Relaciones entre entidades.
  * **Óvalos:** Atributos simples, claves primarias (subrayadas) y atributos asociativos.
  * **Triángulos / Círculos de Especialización:** Jerarquía de herencia disjunta/total desde `Usuarios`.
* **Notación Min-Max:** Especificación explícita de cardinalidad mínima y máxima `(min, max)` para validar las reglas de negocio antes del paso a tablas.

---

## 2. Reglas de Negocio y Participación (Min-Max)

### 2.1. Jerarquía de Usuarios por Especialización
* **Usuario:** Centraliza las credenciales de acceso y datos base de identificación (`id_usuario`, `nombre`, `correo`, `contrasena`, `tipo_usuario`).
* **Especialización (`Paciente`, `Doctor`, `Administrador`):** Implementada mediante herencia *Table-Per-Type* (TPT), donde `pacientes.id_paciente` y `doctores.id_doctor` actúan simultáneamente como clave primaria (`PK`) y clave foránea (`FK`) hacia `usuarios(id_usuario)` con borrado en cascada.

### 2.2. Gestión Médica y Agenda
* **Doctor – Pertenece – Especialidad:**
  * **Participación:** `Doctor (1,1)` — `Especialidad (0,N)`
  * **Regla:** Todo doctor registrado debe estar asociado obligatoriamente a una única especialidad médica. Una especialidad puede agrupar a cero, uno o múltiples doctores.
* **Doctor – Tiene – Horario:**
  * **Participación:** `Doctor (0,N)` — `Horario (1,1)`
  * **Regla:** Un doctor puede habilitar múltiples bloques horarios en su agenda, pero cada bloque horario pertenece exclusivamente a un único doctor.

### 2.3. Reserva e Historial de Citas
* **Paciente – Realiza – CitaMedica:**
  * **Participación:** `Paciente (0,N)` — `CitaMedica (1,1)`
  * **Regla:** Un paciente puede agendar múltiples citas médicas a lo largo del tiempo. Toda cita médica pertenece estrictamente a un único paciente.
* **CitaMedica – Reserva – Horario:**
  * **Participación:** `CitaMedica (1,1)` — `Horario (0,1)`
  * **Regla:** Relación `1:1` restringida mediante clave única (`UNIQUE`). Cada cita médica ocupa exactamente un bloque horario disponible, y un bloque horario solo puede estar vinculado a una cita activa.

### 2.4. Facturación y Prescripción Clínica
* **CitaMedica – Genera – Comprobante:**
  * **Participación:** `CitaMedica (0,1)` — `Comprobante (1,1)`
  * **Regla:** Relación `1:1`. Una cita médica genera como máximo un comprobante de pago (Factura, Recibo o Nota de Crédito), y todo comprobante corresponde obligatoriamente a una única cita.
* **CitaMedica – Tiene – Tratamiento:**
  * **Participación:** `CitaMedica (0,N)` — `Tratamiento (1,1)`
  * **Regla:** A partir de una consulta médica se pueden prescribir uno o varios tratamientos clínicos. Cada tratamiento se origina en una única cita.
* **Tratamiento – Incluye – Medicamento:**
  * **Participación:** `Tratamiento (0,N)` — `Medicamento (0,N)`
  * **Regla:** Relación muchos a muchos (`N:M`) resuelta mediante la entidad asociativa `tratamiento_medicamentos`, la cual incorpora los atributos propios de la receta (`dosis` y `frecuencia`).

---

## 3. Diccionario de Entidades y Atributos (Esquema Implementado)

La siguiente matriz refleja la estructura exacta implementada en `database/01_schema.sql`:

| Entidad (Tabla) | Tipo | Clave Primaria (PK) | Claves Foráneas (FK) | Atributos Descriptivos y Restricciones |
| :--- | :--- | :--- | :--- | :--- |
| **`usuarios`** | Fuerte | `id_usuario` | — | `nombre`, `correo` (UNIQUE), `contrasena`, `tipo_usuario` (`Paciente`, `Doctor`, `Administrador`) |
| **`especialidades`** | Fuerte | `id_especialidad` | — | `nombre` (UNIQUE), `descripcion` (NULL) |
| **`medicamentos`** | Fuerte | `id_medicamento` | — | `nombre`, `descripcion` (NULL) |
| **`pacientes`** | Subtipo (Herencia) | `id_paciente` | `id_paciente` $\rightarrow$ `usuarios` | Hereda atributos base de `usuarios` |
| **`doctores`** | Subtipo (Herencia) | `id_doctor` | `id_doctor` $\rightarrow$ `usuarios`<br>`id_especialidad` $\rightarrow$ `especialidades` | Hereda atributos base de `usuarios` y vincula especialidad |
| **`horarios`** | Fuerte | `id_horario` | `id_doctor` $\rightarrow$ `doctores` | `fecha`, `hora_inicio`, `hora_fin`, `estado` (`Disponible`, `Ocupado`, `Cancelado`) |
| **`citas_medicas`** | Fuerte | `id_cita` | `id_paciente` $\rightarrow$ `pacientes`<br>`id_horario` $\rightarrow$ `horarios` (UNIQUE) | `fecha`, `hora`, `motivo_consulta`, `estado` (`Pendiente`, `Completada`, `Cancelada`) |
| **`comprobantes`** | Fuerte | `id_comprobante` | `id_cita` $\rightarrow$ `citas_medicas` (UNIQUE) | `fecha_emision`, `tipo` (`Factura`, `Recibo`, `Nota de Credito`), `monto` ($\ge 0$) |
| **`tratamientos`** | Fuerte | `id_tratamiento` | `id_cita` $\rightarrow$ `citas_medicas` | `descripcion`, `fecha_inicio`, `fecha_final` |
| **`tratamiento_medicamentos`** | Asociativa (`N:M`) | `(id_tratamiento, id_medicamento)` | `id_tratamiento` $\rightarrow$ `tratamientos`<br>`id_medicamento` $\rightarrow$ `medicamentos` | `dosis`, `frecuencia` |

---

## 4. Diagrama Entidad-Relación

El archivo editable y su exportación visual se encuentran en:
* **Diagrama ER (Modelo Chen / Relacional):** `docs/diagramas/modelo-er-oscorp.drawio` *(verificar que el nombre de archivo coincida exactamente con el subido en `docs/diagramas/`)*
