# Urban Ecommerce 🛍️

Una plataforma de comercio electrónico moderna y escalable construida con **ASP.NET Core Blazor WebAssembly**, ofreciendo una experiencia de usuario fluida con procesamiento del lado del servidor y cliente.

---

## 📋 Tabla de Contenidos

- [Características](#características)
- [Arquitectura](#arquitectura)
- [Requisitos Previos](#requisitos-previos)
- [Instalación](#instalación)
- [Configuración](#configuración)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Tecnologías Utilizadas](#tecnologías-utilizadas)
- [Documentación de API](#documentación-de-api)
- [Características Principales](#características-principales)
- [Contribuciones](#contribuciones)

---

## ✨ Características

### Gestión de Productos
- 📦 Catálogo dinámico de productos con filtrado por categorías
- 🏷️ Sistema de ofertas y precios especiales
- 🎨 Visualización de imágenes y detalles de productos
- 🆕 Indicador automático de productos nuevos (últimos 30 días)

### Carrito de Compras
- 🛒 Carrito persistente con almacenamiento local
- ➕ Agregar/eliminar productos dinámicamente
- 💰 Cálculo automático de totales y descuentos
- 🔄 Sincronización en tiempo real

### Sistema de Usuarios
- 👤 Registro e inicio de sesión seguro
- 🔐 Autenticación basada en JWT
- 📋 Gestión de perfiles de usuario
- 🛡️ Autorización por roles

### Gestión de Ventas
- 📊 Panel de control (Dashboard)
- 💳 Procesamiento de pedidos
- 📈 Reportes de ventas y estadísticas
- 📝 Historial de transacciones

### Panel Administrativo
- 🎛️ Gestión de categorías
- 📦 Administración de productos
- 👥 Gestión de usuarios
- 📊 Análisis de ventas en tiempo real

---

## 🏗️ Arquitectura

Urban Ecommerce implementa una **arquitectura de tres capas** con separación clara de responsabilidades:

### Capas del Proyecto

```
┌─────────────────────────────────────────────────────────┐
│          Client (Blazor WebAssembly)                    │
│  - Interfaz de usuario interactiva                      │
│  - Componentes Razor                                    │
│  - Servicios del cliente                                │
│  - Lógica de presentación                               │
└─────────────────────────────────────────────────────────┘
                            │
                    (HTTP/JSON)
                            │
┌─────────────────────────────────────────────────────────┐
│          Server (ASP.NET Core)                          │
│  - API REST Controllers                                 │
│  - Lógica de negocio (Servicios)                        │
│  - Acceso a datos (Repositorios)                        │
│  - Autenticación y autorización                         │
└─────────────────────────────────────────────────────────┘
                            │
                    (Entity Framework Core)
                            │
┌─────────────────────────────────────────────────────────┐
│          Shared (Modelos DTO)                           │
│  - Contratos de datos compartidos                       │
│  - DTOs (Data Transfer Objects)                         │
│  - Modelos de respuesta                                 │
└─────────────────────────────────────────────────────────┘
                            │
                    (SQL Server)
                            │
┌─────────────────────────────────────────────────────────┐
│          Base de Datos                                  │
│  - SQL Server                                           │
│  - Tablas de dominio                                    │
└─────────────────────────────────────────────────────────┘
```

### Patrones Implementados

- **Repository Pattern**: Acceso a datos abstarcido
- **Service Layer**: Lógica de negocio centralizada
- **DTO Pattern**: Separación entre modelos internos y externos
- **Dependency Injection**: Inyección de dependencias en toda la aplicación
- **Authentication State Provider**: Gestión de autenticación personalizada

---

## 📋 Requisitos Previos

- **.NET 7.0 o superior** (se recomienda .NET 9.0)
- **SQL Server** (Express o Superior)
- **Visual Studio 2022** o **VS Code** con extensiones C#
- **Node.js** (opcional, para herramientas frontend adicionales)
- **Git** para control de versiones

---

## 🚀 Instalación

### 1. Clonar el Repositorio

```bash
git clone https://github.com/M4NDIT0/UrbanEcommerce.git
cd UrbanEcommerce
```

### 2. Restaurar Dependencias

```powershell
dotnet restore
```

### 3. Configurar la Base de Datos

#### Opción A: Update-Database (Recomendado)

```powershell
# Navegar a la carpeta del Server
cd Server

# Aplicar migraciones
dotnet ef database update
```

#### Opción B: Crear manualmente

1. Crear una base de datos en SQL Server llamada `DbtiendaBlazor`
2. Ejecutar los scripts de inicialización (si existen en la carpeta `Scripts`)

### 4. Compilar y Ejecutar

```powershell
# Desde la raíz del proyecto
dotnet build

# Ejecutar la aplicación
dotnet run --project .\Server\BlazorEcommerce.Server.csproj
```

La aplicación estará disponible en `https://localhost:7182`

---

## ⚙️ Configuración

### Archivo de Configuración: `appsettings.json`

```json
{
  "ConnectionStrings": {
    "CadenaSQL": "Server=YOUR_SERVER;Database=DbtiendaBlazor;Trusted_Connection=true;Encrypt=false;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Variables de Entorno Recomendadas

```powershell
# Development
$env:ASPNETCORE_ENVIRONMENT = "Development"

# Production (opcional)
$env:ASPNETCORE_ENVIRONMENT = "Production"
```

---

## 📂 Estructura del Proyecto

```
UrbanEcommerce/
│
├── Client/                           # Proyecto Blazor WebAssembly
│   ├── Pages/
│   │   ├── Tienda/                  # Componentes de tienda
│   │   │   ├── Catalogo.razor       # Catálogo de productos
│   │   │   ├── Cart.razor           # Carrito de compras
│   │   │   └── ...
│   │   ├── Autorizacion/            # Páginas de autenticación
│   │   │   └── Login.razor
│   │   ├── Productos.razor          # Gestión de productos
│   │   └── ...
│   ├── Servicios/                   # Servicios del cliente
│   │   ├── IProductoServicio.cs
│   │   ├── ProductoServicio.cs
│   │   ├── ICarritoServicio.cs
│   │   ├── CarritoServicio.cs
│   │   └── ...
│   ├── Extensiones/                 # Extensiones personalizadas
│   │   ├── AutenticacionExtension.cs
│   │   └── SesionStorageExtension.cs
│   ├── Shared/                      # Componentes compartidos
│   │   ├── MainLayout.razor
│   │   ├── NavMenu.razor
│   │   └── ...
│   ├── wwwroot/                     # Activos estáticos
│   │   ├── css/
│   │   ├── js/
│   │   └── images/
│   └── Program.cs                   # Configuración del cliente
│
├── Server/                          # Proyecto ASP.NET Core
│   ├── Controllers/                 # API Controllers
│   │   ├── ProductoController.cs
│   │   ├── CategoriaController.cs
│   │   ├── PersonaController.cs
│   │   ├── VentaController.cs
│   │   └── DashboardController.cs
│   ├── Servicios/                   # Lógica de negocio
│   │   ├── IProductoServicio.cs
│   │   ├── ProductoServicio.cs
│   │   ├── ICategoriaServicio.cs
│   │   └── ...
│   ├── Repositorios/                # Acceso a datos
│   │   ├── IGenericoRepositorio.cs
│   │   ├── GenericoRepositorio.cs
│   │   ├── IVentaRepositorio.cs
│   │   └── VentaRepositorio.cs
│   ├── Modelos/                     # Entidades de base de datos
│   │   ├── DbtiendaBlazorContext.cs
│   │   ├── Producto.cs
│   │   ├── Categoria.cs
│   │   ├── Persona.cs
│   │   ├── Venta.cs
│   │   ├── DetalleVenta.cs
│   │   └── ...
│   ├── Utilidades/                  # Funciones auxiliares
│   │   └── AutoMapperProfile.cs
│   └── Program.cs                   # Configuración del servidor
│
├── Shared/                          # Proyecto de clases compartidas
│   ├── ProductoDTO.cs
│   ├── CategoriaDTO.cs
│   ├── PersonaDTO.cs
│   ├── VentaDTO.cs
│   ├── DetalleVentaDTO.cs
│   ├── CarritoDTO.cs
│   ├── ResponseDTO.cs
│   ├── LoginDTO.cs
│   ├── SesionDTO.cs
│   └── ...
│
├── BlazorEcommerce.sln              # Solution file
└── README.md                        # Este archivo
```

---

## 🛠️ Tecnologías Utilizadas

### Backend
| Tecnología | Versión | Propósito |
|-----------|---------|----------|
| ASP.NET Core | 7.0 | Framework web server-side |
| Entity Framework Core | 7.0.5 | ORM para acceso a datos |
| SQL Server | Latest | Base de datos relacional |
| AutoMapper | 12.0.1 | Mapeo de objetos |
| C# | 11 | Lenguaje de programación |

### Frontend
| Tecnología | Versión | Propósito |
|-----------|---------|----------|
| Blazor WebAssembly | 7.0.5 | Framework SPA interactivo |
| Bootstrap | Latest | Framework CSS responsive |
| MudBlazor | 6.11.2 | Componentes Material Design |
| Blazored.LocalStorage | 4.3.0 | Almacenamiento local |
| Blazored.SessionStorage | 2.3.0 | Almacenamiento de sesión |
| Blazored.Toast | 4.1.0 | Notificaciones Toast |
| SweetAlert2 | 5.5.0 | Diálogos personalizados |

### Patrones de Desarrollo
- Clean Architecture
- Repository Pattern
- Dependency Injection
- SOLID Principles
- Async/Await Pattern

---

## 📡 Documentación de API

### Endpoints Principales

#### Productos
```
GET     /api/producto/catalogo          - Obtener catálogo de productos
GET     /api/producto/{id}              - Obtener producto por ID
POST    /api/producto                   - Crear nuevo producto
PUT     /api/producto/{id}              - Actualizar producto
DELETE  /api/producto/{id}              - Eliminar producto
```

#### Categorías
```
GET     /api/categoria                  - Listar todas las categorías
GET     /api/categoria/{id}             - Obtener categoría por ID
POST    /api/categoria                  - Crear nueva categoría
PUT     /api/categoria/{id}             - Actualizar categoría
DELETE  /api/categoria/{id}             - Eliminar categoría
```

#### Usuarios (Personas)
```
POST    /api/persona/registro           - Registrar nuevo usuario
POST    /api/persona/login              - Iniciar sesión
GET     /api/persona/{id}               - Obtener datos del usuario
PUT     /api/persona/{id}               - Actualizar datos del usuario
```

#### Ventas
```
POST    /api/venta                      - Crear nueva venta
GET     /api/venta/{id}                 - Obtener venta por ID
GET     /api/venta/usuario/{idUsuario}  - Listar ventas del usuario
```

#### Dashboard
```
GET     /api/dashboard/estadisticas     - Obtener estadísticas generales
GET     /api/dashboard/ventasmes        - Ventas por mes
GET     /api/dashboard/topproductos     - Productos más vendidos
```

### Formato de Respuesta

```json
{
  "esCorrecto": true,
  "mensaje": "Operación exitosa",
  "resultado": {},
  "errores": []
}
```

---

## 🎯 Características Principales Detalladas

### 1. Catálogo de Productos
- Búsqueda y filtrado avanzado
- Indicador visual de productos nuevos
- Sistema de ofertas con precios especiales
- Galería de imágenes

### 2. Carrito Persistente
- Sincronización con base de datos
- Historial de carritos
- Recuperación automática del último carrito

### 3. Sistema de Autenticación
- Registro seguro de usuarios
- Inicio de sesión con JWT
- Recuperación de contraseña
- Perfil de usuario personalizado

### 4. Procesamiento de Órdenes
- Detalles de compra completos
- Seguimiento de órdenes
- Confirmación por email (configurable)

### 5. Panel Administrativo
- Gestión de inventario
- Reportes de ventas
- Análisis de comportamiento del cliente
- Control de acceso basado en roles

---

## 🔒 Seguridad

- ✅ **Autenticación JWT**: Tokens seguros para API
- ✅ **Validación de entrada**: Protección contra inyección SQL
- ✅ **HTTPS**: Comunicación encriptada
- ✅ **CORS configurado**: Acceso controlado a recursos
- ✅ **SQL Parameterizado**: A través de Entity Framework Core

---

## 📊 Diagrama de Base de Datos

```
┌─────────────┐
│ Categoría   │
├─────────────┤
│ IdCategoria │◄─────┐
│ Nombre      │      │
│ Descripción │      │
└─────────────┘      │
                     │
┌─────────────────────┴──────┐
│ Producto                   │
├────────────────────────────┤
│ IdProducto                 │
│ Nombre                     │
│ Descripción                │
│ IdCategoria (FK)           │
│ Precio                     │
│ PrecioOferta               │
│ Cantidad                   │
│ Imagen                     │
│ FechaCreacion              │
└────────┬────────────────────┘
         │
         │
┌────────┴────────────────────┐
│ DetalleVenta               │
├────────────────────────────┤
│ IdDetalleVenta             │
│ IdProducto (FK)            │
│ IdVenta (FK)               │
│ Cantidad                   │
│ PrecioUnitario             │
│ Subtotal                   │
└────────┬────────────────────┘
         │
┌────────┴────────────────────┐
│ Venta                      │
├────────────────────────────┤
│ IdVenta                    │
│ IdPersona (FK)             │
│ FechaVenta                 │
│ Total                      │
│ Estado                     │
└────────┬────────────────────┘
         │
┌────────┴────────────────────┐
│ Persona                    │
├────────────────────────────┤
│ IdPersona                  │
│ Nombre                     │
│ Apellido                   │
│ Email                      │
│ Contrasena                 │
│ Rol                        │
│ FechaRegistro              │
└────────────────────────────┘
```

---

## 🚀 Mejoras Futuras

- [ ] Implementar Stripe/PayPal para pagos en línea
- [ ] Sistema de calificaciones y reseñas
- [ ] Recomendaciones de productos por IA
- [ ] Soporte multiidioma (i18n)
- [ ] Progressive Web App (PWA)
- [ ] Sistema de notificaciones en tiempo real (SignalR)
- [ ] Integración con redes sociales
- [ ] Descuentos por código promocional

---

## 📝 Licencia

Este proyecto está bajo licencia **MIT**. Consulta el archivo `LICENSE` para más detalles.

---

## 👥 Autor

**Armando Nuñez**  
GitHub: [@M4NDIT0](https://github.com/M4NDIT0)

---

## 🤝 Contribuir

Las contribuciones son bienvenidas. Por favor:

1. **Fork** el repositorio
2. **Crea una rama** para tu feature (`git checkout -b feature/AmazingFeature`)
3. **Commit** tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. **Push** a la rama (`git push origin feature/AmazingFeature`)
5. **Abre un Pull Request**

---

## 📧 Contacto

Para preguntas o sugerencias:
- Email: [josearmandonunezarteaga80@gmail.com]
- Issues: [GitHub Issues](https://github.com/M4NDIT0/UrbanEcommerce/issues)

---

## 📚 Recursos Útiles

- [Documentación de Blazor](https://learn.microsoft.com/en-us/aspnet/core/blazor)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [MudBlazor Components](https://www.mudblazor.com/)
- [ASP.NET Core Security](https://learn.microsoft.com/en-us/aspnet/core/security)

---

**Última actualización**: Noviembre 2025  
**Estado**: En desarrollo 🔄
