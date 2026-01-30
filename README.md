# Invoice Management - Prueba Técnica

## InvoiceManagementFrontEnd

Este proyecto contiene la aplicación frontend desarrollada en Angular. Permite la gestión de facturas, clientes, productos, métodos de pago y usuarios a través de una interfaz web.

**Tecnologías utilizadas:**
- Angular 19
- TypeScript
- HTML/CSS

**Estructura principal:**
- `src/app/core/`: Servicios y lógica central de la app
- `src/app/features/`: Módulos de funcionalidades (facturas, clientes, etc.)
- `src/app/shared/`: Componentes y utilidades reutilizables


## InvoiceManagementBackEnd

Este proyecto contiene la API backend desarrollada en .NET (C#), implementando una arquitectura limpia y modular basada en DDD (Domain-Driven Design). Expone endpoints REST para la gestión de facturas, clientes, productos, métodos de pago y usuarios.

**Tecnologías utilizadas:**
- .NET 9 (C#)
- Entity Framework Core
- SQL Server

**Arquitectura:**
- DDD (Domain-Driven Design): Separación clara de capas de dominio, aplicación, infraestructura y presentación.

**Estructura principal:**
- `InvoiceManagement.API/`: Proyecto principal de la API
- `InvoiceManagement.Application/`: Lógica de negocio y servicios
- `InvoiceManagement.Domain/`: Entidades y lógica de dominio
- `InvoiceManagement.Infrastructure/`: Persistencia y acceso a datos

## Base de Datos

En la carpeta `InvoiceManagementDatabase/` se incluyen:
- `InvoiceManagement.mdf` y `InvoiceManagement_log.ldf`: Archivos de base de datos SQL Server local.
- `initial.sql`: Script de creación y datos iniciales.
- `InvoiceManagement.bak`: Backup de la base de datos.
- **Diagrama:** El archivo `DIAGRAMBD` contiene el diagrama de la base de datos.

## Pasos para levantar el proyecto

### 1. Clonar el repositorio y restaurar dependencias
- Frontend: Ejecutar `npm install` en la carpeta `InvoiceManagementFrontEnd`.
- Backend: Restaurar paquetes NuGet desde Visual Studio o con `dotnet restore` en la carpeta `InvoiceManagementBackEnd`.

### 2. Configurar la base de datos
- Montar el archivo `InvoiceManagement.mdf` en tu instancia local de SQL Server.
- O restaurar desde `InvoiceManagement.bak` si lo prefieres.
- Verifica la cadena de conexión en `InvoiceManagementBackEnd/src/InvoiceManagement.API/appsettings.json` y adáptala a tu entorno local.

### 3. Levantar el backend
- Desde Visual Studio: Ejecutar el proyecto `InvoiceManagement.API`.
- O desde terminal:
  ```sh
  cd InvoiceManagementBackEnd/src/InvoiceManagement.API
  dotnet run
  ```

### 4. Levantar el frontend
- Desde terminal:
  ```sh
  cd InvoiceManagementFrontEnd
  npm start
  ```
  o
  ```sh
  ng serve
  ```

### 5. Acceso
- Frontend: http://localhost:4200
- Backend API: http://localhost:5055

---

## Notas importantes sobre la base de datos

- Si tienes problemas con los archivos `.mdf` o `.ldf`, simplemente ejecuta las migraciones con:
  ```
  dotnet ef database update --project src/InvoiceManagement.Infrastructure --startup-project src/InvoiceManagement.API
  ```
- También puedes ejecutar el script `initial.sql` para crear los usuarios y métodos de pagos.

### Usuario de prueba
- **Usuario:** admin@gmail.com
- **Contraseña:** 123qwe


