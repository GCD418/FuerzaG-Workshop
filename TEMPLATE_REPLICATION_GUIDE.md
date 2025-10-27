# 🎨 Guía Completa: Replicar Plantilla Visual en Otro Proyecto

Esta guía te enseñará paso a paso cómo transferir toda la plantilla visual, menú, y diseño de este proyecto a un nuevo proyecto ASP.NET Core Razor Pages.

---

## 📋 Índice

1. [Requisitos Previos](#requisitos-previos)
2. [Estructura de la Plantilla](#estructura-de-la-plantilla)
3. [Paso 1: Crear el Proyecto Base](#paso-1-crear-el-proyecto-base)
4. [Paso 2: Instalar Bootstrap 5](#paso-2-instalar-bootstrap-5)
5. [Paso 3: Copiar Archivos Estáticos](#paso-3-copiar-archivos-estáticos)
6. [Paso 4: Copiar Layouts y Parciales](#paso-4-copiar-layouts-y-parciales)
7. [Paso 5: Configurar Páginas](#paso-5-configurar-páginas)
8. [Paso 6: Ajustes Finales](#paso-6-ajustes-finales)
9. [Checklist de Verificación](#checklist-de-verificación)

---

## ✅ Requisitos Previos

- .NET 8.0 o 9.0 SDK instalado
- Visual Studio Code o Visual Studio
- Acceso a este proyecto FuerzaG como referencia

---

## 📁 Estructura de la Plantilla

### Componentes Principales:

```
FuerzaG/
├── wwwroot/                          # Archivos estáticos
│   ├── css/
│   │   ├── style.css                 # ⭐ CSS principal del template
│   │   └── site.css                  # CSS personalizado
│   ├── js/
│   │   ├── main.js                   # ⭐ JavaScript del template
│   │   └── site.js                   # JavaScript personalizado
│   ├── img/                          # Imágenes
│   │   └── logo-fuerzaG.png          # ⭐ Logo
│   └── lib/                          # Librerías
│       ├── bootstrap/                # ⭐ Bootstrap 5
│       ├── animate/                  # Animaciones CSS
│       ├── owlcarousel/              # Carrusel
│       ├── easing/                   # Efectos de suavizado
│       ├── waypoints/                # Scroll triggers
│       └── counterup/                # Contador animado
├── Pages/
│   └── Shared/
│       ├── _Layout.cshtml             # ⭐ Layout principal
│       ├── _AuthLayout.cshtml         # Layout para login
│       ├── _ConfirmSaveModal.cshtml   # Modal de confirmación
│       └── _ConfirmEditModal.cshtml   # Modal de edición
```

---

## 🚀 Paso 1: Crear el Proyecto Base

### 1.1. Crear Nuevo Proyecto

```bash
# Navegar a tu carpeta de proyectos
cd ~/Documents/Proyectos

# Crear nuevo proyecto Razor Pages
dotnet new webapp -n MiNuevoProyecto

# Entrar al proyecto
cd MiNuevoProyecto
```

### 1.2. Verificar la Estructura

```bash
# Debería tener esta estructura básica
tree -L 2
```

Resultado esperado:
```
MiNuevoProyecto/
├── Pages/
├── wwwroot/
├── appsettings.json
├── Program.cs
└── MiNuevoProyecto.csproj
```

---

## 📦 Paso 2: Instalar Bootstrap 5

### Opción A: Usando libman (Recomendado)

#### 2.1. Crear archivo `libman.json`:

```bash
# En la raíz del proyecto
touch libman.json
```

#### 2.2. Contenido de `libman.json`:

```json
{
  "version": "1.0",
  "defaultProvider": "cdnjs",
  "libraries": [
    {
      "library": "bootstrap@5.3.2",
      "destination": "wwwroot/lib/bootstrap/",
      "files": [
        "dist/css/bootstrap.min.css",
        "dist/css/bootstrap.min.css.map",
        "dist/js/bootstrap.bundle.min.js",
        "dist/js/bootstrap.bundle.min.js.map"
      ]
    },
    {
      "library": "animate.css@4.1.1",
      "destination": "wwwroot/lib/animate/",
      "files": [
        "animate.min.css"
      ]
    },
    {
      "library": "OwlCarousel2@2.3.4",
      "destination": "wwwroot/lib/owlcarousel/",
      "files": [
        "dist/owl.carousel.min.js",
        "dist/assets/owl.carousel.min.css",
        "dist/assets/owl.theme.default.min.css"
      ]
    },
    {
      "library": "jquery.easing@1.4.1",
      "destination": "wwwroot/lib/easing/",
      "files": [
        "jquery.easing.min.js"
      ]
    },
    {
      "library": "waypoints@4.0.1",
      "destination": "wwwroot/lib/waypoints/",
      "files": [
        "lib/jquery.waypoints.min.js"
      ]
    },
    {
      "library": "Counter-Up@1.0.0",
      "destination": "wwwroot/lib/counterup/",
      "files": [
        "jquery.counterup.min.js"
      ]
    }
  ]
}
```

#### 2.3. Instalar libman y restaurar:

```bash
# Instalar libman CLI
dotnet tool install -g Microsoft.Web.LibraryManager.Cli

# Restaurar librerías
libman restore
```

### Opción B: Descargar Manualmente

Si no quieres usar libman, descarga:
1. **Bootstrap 5.3.2** desde https://getbootstrap.com
2. **Animate.css** desde https://animate.style
3. **Owl Carousel** desde https://owlcarousel2.github.io/OwlCarousel2/

---

## 📂 Paso 3: Copiar Archivos Estáticos

### 3.1. Copiar CSS

Desde el proyecto FuerzaG, copia:

```bash
# Desde el directorio del proyecto FuerzaG
cp ~/Documents/ucb/ArquiSoftware/Proyectos/FuerzaG/wwwroot/css/style.css \
   ~/Documents/Proyectos/MiNuevoProyecto/wwwroot/css/

# También puedes personalizar site.css si lo necesitas
cp ~/Documents/ucb/ArquiSoftware/Proyectos/FuerzaG/wwwroot/css/site.css \
   ~/Documents/Proyectos/MiNuevoProyecto/wwwroot/css/
```

**Archivos a copiar:**
- ✅ `wwwroot/css/style.css` - **OBLIGATORIO** (estilos del template)
- ⚪ `wwwroot/css/site.css` - Opcional (estilos personalizados)

### 3.2. Copiar JavaScript

```bash
# Copiar JavaScript principal
cp ~/Documents/ucb/ArquiSoftware/Proyectos/FuerzaG/wwwroot/js/main.js \
   ~/Documents/Proyectos/MiNuevoProyecto/wwwroot/js/

# Copiar JavaScript personalizado
cp ~/Documents/ucb/ArquiSoftware/Proyectos/FuerzaG/wwwroot/js/site.js \
   ~/Documents/Proyectos/MiNuevoProyecto/wwwroot/js/
```

**Archivos a copiar:**
- ✅ `wwwroot/js/main.js` - **OBLIGATORIO** (funcionalidad del template)
- ⚪ `wwwroot/js/site.js` - Opcional (JavaScript personalizado)

### 3.3. Copiar Imágenes

```bash
# Crear carpeta img si no existe
mkdir -p ~/Documents/Proyectos/MiNuevoProyecto/wwwroot/img

# Copiar logo (IMPORTANTE)
cp ~/Documents/ucb/ArquiSoftware/Proyectos/FuerzaG/wwwroot/img/logo-fuerzaG.png \
   ~/Documents/Proyectos/MiNuevoProyecto/wwwroot/img/

# Copiar otras imágenes que necesites
cp ~/Documents/ucb/ArquiSoftware/Proyectos/FuerzaG/wwwroot/img/*.jpg \
   ~/Documents/Proyectos/MiNuevoProyecto/wwwroot/img/
```

**Archivos importantes:**
- ✅ `logo-fuerzaG.png` o tu logo personalizado
- ⚪ Otras imágenes según necesites

---

## 📝 Paso 4: Copiar Layouts y Parciales

### 4.1. Copiar Layout Principal

```bash
# Copiar _Layout.cshtml
cp ~/Documents/ucb/ArquiSoftware/Proyectos/FuerzaG/Pages/Shared/_Layout.cshtml \
   ~/Documents/Proyectos/MiNuevoProyecto/Pages/Shared/
```

### 4.2. Copiar Layout de Autenticación

```bash
# Copiar _AuthLayout.cshtml (para páginas de login)
cp ~/Documents/ucb/ArquiSoftware/Proyectos/FuerzaG/Pages/Shared/_AuthLayout.cshtml \
   ~/Documents/Proyectos/MiNuevoProyecto/Pages/Shared/
```

### 4.3. Copiar Modales Compartidos

```bash
# Copiar modales de confirmación
cp ~/Documents/ucb/ArquiSoftware/Proyectos/FuerzaG/Pages/Shared/_ConfirmSaveModal.cshtml \
   ~/Documents/Proyectos/MiNuevoProyecto/Pages/Shared/

cp ~/Documents/ucb/ArquiSoftware/Proyectos/FuerzaG/Pages/Shared/_ConfirmEditModal.cshtml \
   ~/Documents/Proyectos/MiNuevoProyecto/Pages/Shared/
```

### 4.4. Copiar _ViewImports y _ViewStart

```bash
# Copiar _ViewImports.cshtml
cp ~/Documents/ucb/ArquiSoftware/Proyectos/FuerzaG/Pages/_ViewImports.cshtml \
   ~/Documents/Proyectos/MiNuevoProyecto/Pages/

# Copiar _ViewStart.cshtml
cp ~/Documents/ucb/ArquiSoftware/Proyectos/FuerzaG/Pages/_ViewStart.cshtml \
   ~/Documents/Proyectos/MiNuevoProyecto/Pages/
```

---

## 🔧 Paso 5: Configurar Páginas

### 5.1. Actualizar _Layout.cshtml

Abre `Pages/Shared/_Layout.cshtml` y personaliza:

#### **Cambiar el Logo:**

```html
<!-- Buscar esta sección (línea ~56) -->
<div class="logo">
    <a href="index.html">
        <img src="~/img/logo-fuerzaG.png" alt="Logo">
        <!--         👆 Cambiar por tu logo      -->
    </a>
</div>
```

#### **Actualizar Información de Contacto:**

```html
<!-- Buscar esta sección (línea ~67-96) -->
<div class="top-bar-text">
    <h3>Horarios de atención</h3>
    <p>Lun - Sab, 7:00 - 15:00</p>  <!-- 👈 Cambiar horario -->
</div>

<div class="top-bar-text">
    <h3>Contáctanos</h3>
    <p>+591 76433066</p>  <!-- 👈 Cambiar teléfono -->
</div>

<div class="top-bar-text">
    <h3>Email</h3>
    <p>info@fuerza-G.com</p>  <!-- 👈 Cambiar email -->
</div>
```

#### **Personalizar Menú de Navegación:**

```html
<!-- Buscar esta sección (línea ~117-123) -->
<div class="navbar-nav mr-auto">
    <a asp-page="/index" class="nav-item nav-link">
        <i class="fas fa-home me-1"></i> INICIO
    </a>
    <a asp-page="/Services/ServicePage" class="nav-item nav-link">Servicios</a>
    <a asp-page="/Owners/OwnerPage" class="nav-item nav-link">Clientes</a>
    <!-- 👆 Agregar/quitar/modificar según tus páginas -->
</div>
```

#### **Actualizar Footer:**

```html
<!-- Buscar esta sección (línea ~197-216) -->
<div class="footer-contact">
    <h2>Ponte En Contacto</h2>
    <p><i class="fa fa-map-marker-alt"></i>Calle 123, Cochabamba, BOL</p>  <!-- 👈 Cambiar -->
    <p><i class="fa fa-phone-alt"></i>+591 76433066</p>  <!-- 👈 Cambiar -->
    <p><i class="fa fa-envelope"></i>info@fuerza-G.com</p>  <!-- 👈 Cambiar -->
</div>

<!-- Copyright (línea ~254) -->
<p>&copy; <a href="#">FUERZA-G</a>, All Right Reserved.</p>  <!-- 👈 Cambiar -->
```

### 5.2. Crear Página de Inicio

Crea o modifica `Pages/Index.cshtml`:

```html
@page
@model IndexModel
@{
    ViewData["Title"] = "Inicio";
}

<section id="home" class="section bg-light">
    <div class="container">
        <div class="section-header text-center mb-5">
            <h2 class="fw-bold">Bienvenido a Mi Proyecto</h2>
            <p class="text-muted">Descripción de tu proyecto</p>
        </div>
        
        <div class="row">
            <!-- Tu contenido aquí -->
        </div>
    </div>
</section>
```

---

## 🎨 Paso 6: Ajustes Finales

### 6.1. Personalizar Colores

Edita `wwwroot/css/style.css` para cambiar los colores principales:

```css
/* Buscar estas variables (aproximadamente línea 1-50) */

/* Color principal (rojo en FuerzaG) */
.btn.btn-custom {
    background: #E81C2E;  /* 👈 Cambiar a tu color primario */
}

/* Color secundario (azul oscuro en FuerzaG) */
h1, h2, h3, h4, h5, h6 {
    color: #202C45;  /* 👈 Cambiar a tu color secundario */
}

/* Color de hover */
a:hover {
    color: #E81C2E;  /* 👈 Cambiar a tu color de hover */
}
```

### 6.2. Configurar Program.cs

Asegúrate de que `Program.cs` tenga la configuración correcta:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();  // ⭐ IMPORTANTE para archivos estáticos

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
```

### 6.3. Verificar appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

---

## ✅ Checklist de Verificación

### Archivos Copiados:

#### wwwroot/
- [ ] `css/style.css` ⭐ OBLIGATORIO
- [ ] `css/site.css`
- [ ] `js/main.js` ⭐ OBLIGATORIO
- [ ] `js/site.js`
- [ ] `img/logo-fuerzaG.png` (o tu logo) ⭐ OBLIGATORIO
- [ ] `img/*.jpg` (imágenes necesarias)

#### wwwroot/lib/ (vía libman o manual)
- [ ] `bootstrap/` ⭐ OBLIGATORIO
- [ ] `animate/` ⭐ OBLIGATORIO
- [ ] `owlcarousel/`
- [ ] `easing/`
- [ ] `waypoints/`
- [ ] `counterup/`

#### Pages/Shared/
- [ ] `_Layout.cshtml` ⭐ OBLIGATORIO
- [ ] `_AuthLayout.cshtml`
- [ ] `_ConfirmSaveModal.cshtml`
- [ ] `_ConfirmEditModal.cshtml`

#### Pages/
- [ ] `_ViewImports.cshtml` ⭐ OBLIGATORIO
- [ ] `_ViewStart.cshtml` ⭐ OBLIGATORIO

### Configuración:
- [ ] `libman.json` creado y ejecutado
- [ ] Logo cambiado en `_Layout.cshtml`
- [ ] Información de contacto actualizada
- [ ] Menú personalizado según tus páginas
- [ ] Colores personalizados en `style.css`
- [ ] `Program.cs` configurado correctamente

### Pruebas:
- [ ] Ejecutar `dotnet run` sin errores
- [ ] Página de inicio se ve correctamente
- [ ] Menú de navegación funciona
- [ ] Modales se abren/cierran correctamente
- [ ] Diseño responsive funciona en móvil
- [ ] Logo se muestra correctamente
- [ ] Footer muestra información correcta

---

## 🚀 Comandos Rápidos

### Para ejecutar el proyecto:

```bash
# Restaurar dependencias
dotnet restore

# Ejecutar el proyecto
dotnet run

# O ejecutar y abrir el navegador
dotnet watch run
```

### Para verificar archivos estáticos:

```bash
# Ver estructura de wwwroot
tree wwwroot

# Verificar que los archivos existen
ls -la wwwroot/css/
ls -la wwwroot/js/
ls -la wwwroot/img/
ls -la wwwroot/lib/
```

---

## 🎯 Ejemplo Completo de Script de Copia

Crea un archivo `copy-template.sh` en tu proyecto:

```bash
#!/bin/bash

# Variables
SOURCE_PROJECT="$HOME/Documents/ucb/ArquiSoftware/Proyectos/FuerzaG"
TARGET_PROJECT="$HOME/Documents/Proyectos/MiNuevoProyecto"

# Crear directorios necesarios
mkdir -p "$TARGET_PROJECT/wwwroot/css"
mkdir -p "$TARGET_PROJECT/wwwroot/js"
mkdir -p "$TARGET_PROJECT/wwwroot/img"
mkdir -p "$TARGET_PROJECT/Pages/Shared"

# Copiar CSS
cp "$SOURCE_PROJECT/wwwroot/css/style.css" "$TARGET_PROJECT/wwwroot/css/"
cp "$SOURCE_PROJECT/wwwroot/css/site.css" "$TARGET_PROJECT/wwwroot/css/"

# Copiar JavaScript
cp "$SOURCE_PROJECT/wwwroot/js/main.js" "$TARGET_PROJECT/wwwroot/js/"
cp "$SOURCE_PROJECT/wwwroot/js/site.js" "$TARGET_PROJECT/wwwroot/js/"

# Copiar imágenes
cp "$SOURCE_PROJECT/wwwroot/img/logo-fuerzaG.png" "$TARGET_PROJECT/wwwroot/img/"

# Copiar layouts
cp "$SOURCE_PROJECT/Pages/Shared/_Layout.cshtml" "$TARGET_PROJECT/Pages/Shared/"
cp "$SOURCE_PROJECT/Pages/Shared/_AuthLayout.cshtml" "$TARGET_PROJECT/Pages/Shared/"
cp "$SOURCE_PROJECT/Pages/Shared/_ConfirmSaveModal.cshtml" "$TARGET_PROJECT/Pages/Shared/"
cp "$SOURCE_PROJECT/Pages/Shared/_ConfirmEditModal.cshtml" "$TARGET_PROJECT/Pages/Shared/"

# Copiar ViewImports y ViewStart
cp "$SOURCE_PROJECT/Pages/_ViewImports.cshtml" "$TARGET_PROJECT/Pages/"
cp "$SOURCE_PROJECT/Pages/_ViewStart.cshtml" "$TARGET_PROJECT/Pages/"

echo "✅ Archivos copiados exitosamente!"
echo "📝 Recuerda:"
echo "   1. Ejecutar 'libman restore' para instalar Bootstrap y otras librerías"
echo "   2. Personalizar _Layout.cshtml con tu información"
echo "   3. Cambiar el logo en wwwroot/img/"
echo "   4. Ajustar colores en style.css"
```

### Ejecutar el script:

```bash
# Dar permisos de ejecución
chmod +x copy-template.sh

# Ejecutar
./copy-template.sh
```

---

## 🔍 Solución de Problemas Comunes

### 1. Las imágenes no se cargan

**Problema:** Ves rutas rotas o imágenes que no aparecen.

**Solución:**
```bash
# Verificar que las imágenes existen
ls -la wwwroot/img/

# Verificar permisos
chmod 644 wwwroot/img/*
```

### 2. El CSS no se aplica

**Problema:** La página se ve sin estilos.

**Solución:**
- Verificar que `style.css` existe en `wwwroot/css/`
- Verificar la referencia en `_Layout.cshtml`:
  ```html
  <link rel="stylesheet" href="~/css/style.css"/>
  ```
- Limpiar caché del navegador (Ctrl+F5)

### 3. Bootstrap no funciona

**Problema:** Modales, dropdowns no funcionan.

**Solución:**
```bash
# Reinstalar Bootstrap con libman
libman restore

# O verificar que existe
ls -la wwwroot/lib/bootstrap/dist/
```

### 4. JavaScript no funciona

**Problema:** Animaciones, menú móvil no funciona.

**Solución:**
- Verificar que jQuery está cargado antes de `main.js`
- Verificar el orden de scripts en `_Layout.cshtml`:
  ```html
  <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
  <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
  <script src="~/js/main.js"></script>
  ```

---

## 📚 Recursos Adicionales

### Documentación Oficial:
- [ASP.NET Core Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/)
- [Bootstrap 5](https://getbootstrap.com/docs/5.3/)
- [LibMan CLI](https://docs.microsoft.com/en-us/aspnet/core/client-side/libman/libman-cli)

### Templates Similares:
- [HTML Codex Templates](https://htmlcodex.com) - De donde viene este template
- [Start Bootstrap](https://startbootstrap.com)
- [Bootstrap Made](https://bootstrapmade.com)

---

## 💡 Consejos Finales

1. **Mantén los archivos organizados**: No mezcles código del template con tu código personalizado
2. **Usa libman**: Es más fácil actualizar librerías
3. **Personaliza gradualmente**: No cambies todo de una vez
4. **Haz commits frecuentes**: Antes y después de cada cambio grande
5. **Prueba en móvil**: Asegúrate de que el diseño sea responsive
6. **Documenta cambios**: Mantén notas de lo que personalizas

---

## 🎉 ¡Listo!

Ahora tienes todo listo para replicar esta plantilla en cualquier proyecto nuevo. 

### Pasos resumidos:
1. ✅ Crear proyecto nuevo
2. ✅ Instalar Bootstrap 5 y librerías (libman)
3. ✅ Copiar archivos estáticos (CSS, JS, imágenes)
4. ✅ Copiar layouts y vistas compartidas
5. ✅ Personalizar información (logo, contacto, colores)
6. ✅ Probar y ajustar

**¡Buena suerte con tu nuevo proyecto!** 🚀
