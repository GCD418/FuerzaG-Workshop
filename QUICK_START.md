# 🚀 Guía Rápida: Replicar Template FuerzaG

## ⚡ TL;DR (Resumen Ultra Rápido)

### Opción 1: Script Automático (Recomendado)

```bash
# 1. Ejecutar el script de copia
./copy-template.sh

# 2. Ir al proyecto nuevo
cd /ruta/a/tu/proyecto

# 3. Instalar librerías
libman restore

# 4. Ejecutar
dotnet run
```

---

### Opción 2: Manual

```bash
# 1. Copiar archivos esenciales
cp -r FuerzaG/wwwroot/css/style.css TuProyecto/wwwroot/css/
cp -r FuerzaG/wwwroot/js/main.js TuProyecto/wwwroot/js/
cp -r FuerzaG/wwwroot/img/logo-fuerzaG.png TuProyecto/wwwroot/img/
cp -r FuerzaG/Pages/Shared/_Layout.cshtml TuProyecto/Pages/Shared/
cp -r FuerzaG/Pages/_ViewImports.cshtml TuProyecto/Pages/
cp -r FuerzaG/Pages/_ViewStart.cshtml TuProyecto/Pages/

# 2. Copiar libman.json
cp FuerzaG/libman.json.example TuProyecto/libman.json

# 3. Instalar librerías
cd TuProyecto
libman restore

# 4. Ejecutar
dotnet run
```

---

## 📋 Archivos OBLIGATORIOS

### ✅ Mínimo Indispensable

| Archivo | Ubicación | Propósito |
|---------|-----------|-----------|
| `style.css` | `wwwroot/css/` | Estilos del template |
| `main.js` | `wwwroot/js/` | JavaScript del template |
| `_Layout.cshtml` | `Pages/Shared/` | Layout principal |
| `_ViewImports.cshtml` | `Pages/` | Configuración de vistas |
| `_ViewStart.cshtml` | `Pages/` | Layout por defecto |
| `libman.json` | Raíz del proyecto | Gestión de librerías |
| Tu logo | `wwwroot/img/` | Logo de tu proyecto |

### ⚪ Opcionales pero Recomendados

| Archivo | Ubicación | Propósito |
|---------|-----------|-----------|
| `site.css` | `wwwroot/css/` | Estilos personalizados |
| `site.js` | `wwwroot/js/` | JavaScript personalizado |
| `_AuthLayout.cshtml` | `Pages/Shared/` | Layout para login |
| `_ConfirmSaveModal.cshtml` | `Pages/Shared/` | Modal de confirmación |
| `_ConfirmEditModal.cshtml` | `Pages/Shared/` | Modal de edición |

---

## 🎨 Personalización Rápida

### 1. Cambiar Logo

**Archivo:** `Pages/Shared/_Layout.cshtml` (línea ~56)

```html
<img src="~/img/tu-logo.png" alt="Logo">
```

### 2. Cambiar Colores

**Archivo:** `wwwroot/css/style.css` (líneas ~35-50)

```css
.btn.btn-custom {
    background: #TU_COLOR_PRIMARIO;  /* Cambia #E81C2E */
}

h1, h2, h3, h4, h5, h6 {
    color: #TU_COLOR_SECUNDARIO;  /* Cambia #202C45 */
}
```

### 3. Actualizar Contacto

**Archivo:** `Pages/Shared/_Layout.cshtml` (líneas ~67-96)

```html
<!-- Horario -->
<p>Lun - Vie, 9:00 - 18:00</p>

<!-- Teléfono -->
<p>+XX XXX XXXXXXX</p>

<!-- Email -->
<p>tu@email.com</p>
```

### 4. Modificar Menú

**Archivo:** `Pages/Shared/_Layout.cshtml` (líneas ~117-123)

```html
<div class="navbar-nav mr-auto">
    <a asp-page="/Index" class="nav-item nav-link">Inicio</a>
    <a asp-page="/TuPagina" class="nav-item nav-link">Tu Sección</a>
    <!-- Agregar más links según necesites -->
</div>
```

---

## 🔧 Comandos Útiles

### Instalar libman (primera vez)
```bash
dotnet tool install -g Microsoft.Web.LibraryManager.Cli
```

### Restaurar librerías
```bash
libman restore
```

### Limpiar y reconstruir
```bash
dotnet clean
dotnet build
```

### Ejecutar en modo desarrollo
```bash
dotnet watch run
```

### Ver archivos copiados
```bash
tree wwwroot
tree Pages/Shared
```

---

## ✅ Checklist Post-Instalación

- [ ] Logo cambiado en `_Layout.cshtml`
- [ ] Información de contacto actualizada
- [ ] Colores personalizados en `style.css`
- [ ] Menú actualizado con tus páginas
- [ ] libman restore ejecutado
- [ ] Proyecto compila sin errores (`dotnet build`)
- [ ] Página de inicio se ve correctamente
- [ ] Menú responsive funciona en móvil
- [ ] Footer con información correcta

---

## 🐛 Problemas Comunes

### "No se encuentra style.css"
```bash
# Verificar que existe
ls -la wwwroot/css/style.css

# Si no existe, copiar desde FuerzaG
cp FuerzaG/wwwroot/css/style.css wwwroot/css/
```

### "Bootstrap no funciona"
```bash
# Reinstalar con libman
libman restore

# Verificar instalación
ls -la wwwroot/lib/bootstrap/
```

### "Las imágenes no cargan"
```bash
# Verificar permisos
chmod 644 wwwroot/img/*

# Verificar ruta en HTML
# Debe ser: ~/img/nombre.png
```

### "JavaScript no funciona"
```bash
# Verificar orden de scripts en _Layout.cshtml
# 1. jQuery
# 2. Bootstrap
# 3. main.js
```

---

## 📚 Documentación Completa

Para más detalles, consulta:
- 📖 `TEMPLATE_REPLICATION_GUIDE.md` - Guía completa paso a paso
- 🎨 `BOOTSTRAP5_MIGRATION_GUIDE.md` - Guía de Bootstrap 5
- 🤖 `copy-template.sh` - Script automático de copia

---

## 💡 Tips Pro

1. **Usa el script:** Ahorra tiempo con `./copy-template.sh`
2. **Personaliza gradualmente:** No cambies todo de golpe
3. **Prueba en móvil:** Verifica diseño responsive
4. **Mantén backups:** Haz commits antes de cambios grandes
5. **Documenta cambios:** Anota qué personalizas

---

## 📞 ¿Necesitas Ayuda?

1. Revisa `TEMPLATE_REPLICATION_GUIDE.md` para guía detallada
2. Verifica la consola del navegador (F12) para errores
3. Compara con el proyecto FuerzaG original

---

**¡Buena suerte! 🚀**
