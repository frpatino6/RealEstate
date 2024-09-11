# Real Estate API

Esta es una API para gestionar propiedades, propietarios y sus imágenes, desarrollada con **.NET 5+** siguiendo la arquitectura **Clean Architecture** y el patrón **CQRS**. El enfoque utilizado es **Code First**, lo que significa que las entidades y el contexto de base de datos están definidos en el código, y la base de datos se genera a partir de ellos.

## Requisitos

- .NET 5 o superior
- SQL Server
- Git
- Docker (opcional, si deseas usar contenedores para la base de datos)

## Configuración

### Clonar el repositorio

Primero, clona este repositorio en tu máquina local:

```bash
git clone https://github.com/tu-usuario/nombre-repositorio.git
cd nombre-repositorio
```
## Configurar el archivo appsettings.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=RealEstateDB;User Id=sa;Password=Str0ngP@ssw0rd!;TrustServerCertificate=True;"
  }
}
```
## Ejecutar migraciones (Code First)
Una vez que hayas configurado tu cadena de conexión, debes aplicar las migraciones para crear la base de datos.

Abre la terminal en la carpeta raíz del proyecto.
Ejecuta los siguientes comandos para aplicar las migraciones y actualizar la base de datos:
```bash
dotnet ef migrations add InitialCreate --project RealEstate.Infrastructure --startup-project RealEstate.API
dotnet ef database update --project RealEstate.Infrastructure --startup-project RealEstate.API
```
Esto generará la base de datos en tu servidor de SQL Server especificado.

## Ejecutar la API
Una vez que la base de datos esté configurada, puedes ejecutar la API:
```bash
cd RealEstate.API
dotnet run
```
La API estará disponible en http://localhost:5000 o https://localhost:5001.

#Pruebas
Este proyecto incluye pruebas unitarias para validar la lógica del negocio. Para ejecutar las pruebas:

```bash
dotnet test
```

## Endpoints
Algunos de los endpoints disponibles son:

Propiedades

POST /api/property - Crear una propiedad.
GET /api/property/{id} - Obtener una propiedad por ID.
GET /api/property - Listar propiedades con filtros.
Propietarios

POST /api/owner - Crear un propietario.
GET /api/owner/{id} - Obtener un propietario por ID.
GET /api/owner - Listar propietarios.
Imágenes de propiedades

POST /api/property/{id}/images - Agregar una imagen a una propiedad.
GET /api/property/{id}/images - Obtener imágenes de una propiedad.
Estructura del proyecto
Este proyecto sigue la Clean Architecture con la siguiente estructura de carpetas:

RealEstate.Domain: Contiene las entidades y las interfaces del dominio.
RealEstate.Application: Contiene la lógica de negocio, comandos y consultas.
RealEstate.Infrastructure: Implementaciones de repositorios y acceso a datos.
RealEstate.API: Controladores y configuración de la API.
