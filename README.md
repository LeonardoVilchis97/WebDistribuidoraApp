# WebDistribuidoraApp

Sistema web desarrollado en ASP.NET Core para la administración de productos y proveedores de una distribuidora.

## Tecnologías Utilizadas

- ASP.NET Core 6.0
- C#
- MVC (Model-View-Controller)
- Entity Framework / ADO.NET
- SQL Server
- HTML5
- CSS3
- Bootstrap 5
- jQuery

## IDE Utilizado

- Visual Studio 2022

## DBMS Utilizado

- Microsoft SQL Server

## Funcionalidades

- Gestión de productos
  - Alta, edición y baja de productos
  - Filtros por clave y tipo de producto
  - Validación de llaves foráneas al eliminar
- Gestión de proveedores por producto
  - Agregar proveedores a un producto
  - Editar proveedor asignado
  - Eliminar proveedor asignado

## Pasos para ejecutar el proyecto

### 1. Clonar repositorio

```
git clone https://github.com/LeonardoVilchis97/WebDistribuidoraApp.git
```

### 2. Crear base de datos

Crear una base de datos en SQL Server llamada:

```
Distribuidora
```

### 3. Configurar conexión SQL Server

Modificar el archivo:

```
appsettings.json
```

Configurar:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=Distribuidora;User Id=sa;Password=TU_PASSWORD;MultipleActiveResultSets=true;TrustServerCertificate=True;"
}
```

### 4. Ejecutar la aplicación

Desde Visual Studio 2022:

1. Abrir el archivo `WebDistribuidoraApp.sln`
2. Presionar `Ctrl + F5` para ejecutar sin depuración o `F5` para ejecutar con depuración

Desde consola:

```
dotnet run
```

### 5. Acceder a la aplicación

Abrir el navegador y navegar a:

```
https://localhost:7178
```

## Despliegue en IIS

Para publicar en IIS 7.5 o superior consultar el **Manual de Instalación** incluido en el repositorio.

Requisitos para el servidor:
- ASP.NET Core Hosting Bundle 6.0
- IIS 7.5 o superior
- SQL Server accesible desde el servidor web

## Autor

Leonardo Vilchis Martinez
