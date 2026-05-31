# 🚀 SGE — Sistema de Gestión de Expedientes

<div align="center">

![.NET](https://img.shields.io/badge/.NET-10.0-purple?style=for-the-badge&logo=.net)
![C#](https://img.shields.io/badge/C%23-Programming-239120?style=for-the-badge&logo=c-sharp)
![Architecture](https://img.shields.io/badge/Clean%20Architecture-Implemented-blue?style=for-the-badge)
![Status](https://img.shields.io/badge/Status-In%20Development-success?style=for-the-badge)

Sistema de gestión de expedientes y trámites desarrollado en **C# y .NET**, aplicando principios de **Clean Architecture**, separación de responsabilidades y patrones de diseño.

</div>

---

# 📖 Descripción

**SGE** es un sistema orientado a la administración de expedientes y trámites administrativos.

El proyecto fue desarrollado utilizando una arquitectura desacoplada y organizada en capas, permitiendo:

- Mayor mantenibilidad
- Escalabilidad
- Reutilización de código
- Separación clara de responsabilidades
- Facilidad para testing y evolución futura

El sistema implementa casos de uso independientes, repositorios y persistencia desacoplada mediante infraestructura propia.

---

# 🏗️ Arquitectura

El proyecto sigue una estructura inspirada en **Clean Architecture**.

```text
SGE
│
├── 📁 SGE.Aplicacion
├── 📁 SGE.Dominio
├── 📁 SGE.Infraestructura
└── 📁 SGE.Consola
```

---

# 📂 Capas del Proyecto

## 🧠 SGE.Dominio

Contiene las entidades principales y reglas de negocio del sistema.

### Responsabilidades

- Modelado del dominio
- Reglas de negocio
- Entidades
- Validaciones
- Objetos del sistema

### Ejemplos

- Expedientes
- Trámites
- Estados
- Validaciones de dominio

---

## ⚙️ SGE.Aplicacion

Contiene la lógica de aplicación mediante **Use Cases**.

### Responsabilidades

- Coordinar operaciones
- Aplicar reglas del negocio
- Gestionar autorizaciones
- Conectar dominio e infraestructura

### Ejemplos

- Crear expediente
- Eliminar expediente
- Crear trámite
- Validaciones de permisos

---

## 💾 SGE.Infraestructura

Implementa persistencia y acceso a datos.

### Responsabilidades

- Repositorios
- Lectura y escritura de archivos
- Persistencia TXT
- Acceso externo

### Tecnologías

- Archivos `.txt`
- Repositories Pattern

---

## 🖥️ SGE.Consola

Capa de presentación e interacción con el usuario.

### Responsabilidades

- Inyeccion de dependencias
- Manejos de errores
- Ejecución de casos de uso

---

# ✨ Características

## 📌 Funcionalidades actuales

- ✅ Gestión de expedientes
- ✅ Gestión de trámites
- ✅ Persistencia de datos
- ✅ Arquitectura por capas
- ✅ Casos de uso desacoplados
- ✅ Repository Pattern
- ✅ Validaciones de negocio
- ✅ Separación entre dominio e infraestructura

---

# 🧩 Tecnologías Utilizadas

| Tecnología | Uso |
|---|---|
| C# | Lenguaje principal |
| .NET | Framework |
| Clean Architecture | Arquitectura |
| Repository Pattern | Persistencia |
| SOLID Principles | Diseño |
| TXT Storage | Persistencia de datos |

---

# 📁 Estructura del Proyecto

```text
SGE/
│
├── 📁 SGE.Aplicacion/
│   ├── 📁 Autorizacion/
│   ├── 📁 Comun/
│   ├── 📁 Expedientes/
│   └── 📁 Tramites/
│
├── 📁 SGE.Dominio/
│   ├── 📁 Comun/
│   ├── 📁 Expedientes/
│   └── 📁 Tramites/
│
├── 📁 SGE.Infraestructura/
│
└── 📁 SGE.Consola/
```

---

# 🧠 Conceptos Aplicados

## 🏛️ Clean Architecture

El proyecto separa responsabilidades entre capas para lograr:

- Bajo acoplamiento
- Alta cohesión
- Escalabilidad
- Facilidad de testing
- Independencia de frameworks

---

## 📦 Repository Pattern

Se implementa el patrón Repository para abstraer el acceso a datos.

### Ejemplos

```csharp
IExpedienteRepository
ITramiteRepository
```

---

## ⚡ Casos de Uso

Cada acción importante del sistema se implementa como un caso de uso independiente.

### Ejemplos

```text
CrearExpedienteUseCase
EliminarExpedienteUseCase
CrearTramiteUseCase
```

---

# 🚀 Instalación

## 1️⃣ Clonar el repositorio

```bash
git clone https://github.com/EOA2020/SGE.git
```

---

## 2️⃣ Abrir la solución

Abrir el proyecto utilizando:

- Visual Studio
- Rider
- VS Code + extensión C#

---

## 3️⃣ Ejecutar el proyecto

Seleccionar `SGE.Consola` como proyecto principal y ejecutar.

---

# 📚 Objetivos del Proyecto

Este proyecto busca aplicar:

- Arquitectura limpia
- Principios SOLID
- Programación orientada a objetos
- Separación de responsabilidades
- Modelado de dominio
- Persistencia desacoplada

---

# 📈 Posibles Mejoras Futuras

- 🔹 Base de datos SQL
- 🔹 API REST
- 🔹 Interfaz web
- 🔹 JWT/Auth
- 🔹 Entity Framework
- 🔹 Tests unitarios
- 🔹 Docker
- 🔹 Logging
- 🔹 Validaciones avanzadas

---

# 👥 Equipo de Desarrollo

Este proyecto fue desarrollado por:

- 👨‍💻 [**Sergio Ariel Paredes**](https://github.com/EOA2020)
- 👩‍💻 [**Cristal Milagros Andrade**](https://github.com/andradecristal)
- 👨‍💻 [**Elias Nahuel Lopez**](https://github.com/nauelo)

Proyecto desarrollado con fines académicos y de aprendizaje.


## Repositorio oficial

👉 https://github.com/EOA2020/SGE

---

# 📄 Licencia

Proyecto de uso educativo y académico.

---

<div align="center">

### ⭐ Si te gusta el proyecto, podés darle una estrella al repositorio ⭐

</div>