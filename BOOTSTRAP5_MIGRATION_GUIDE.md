# 🚀 Guía de Migración a Bootstrap 5

Esta guía te ayudará a migrar cualquier proyecto de Bootstrap 4 a Bootstrap 5 de manera efectiva.

## 📋 Tabla de Contenidos

1. [Cambios Principales](#cambios-principales)
2. [Pasos de Migración](#pasos-de-migración)
3. [Cambios en HTML](#cambios-en-html)
4. [Cambios en JavaScript](#cambios-en-javascript)
5. [Problemas Comunes](#problemas-comunes)
6. [Checklist de Verificación](#checklist-de-verificación)

---

## 🔄 Cambios Principales

### Bootstrap 4 → Bootstrap 5

| Componente | Bootstrap 4 | Bootstrap 5 |
|-----------|-------------|-------------|
| **Atributo de Toggle** | `data-toggle` | `data-bs-toggle` |
| **Atributo de Target** | `data-target` | `data-bs-target` |
| **Atributo de Dismiss** | `data-dismiss` | `data-bs-dismiss` |
| **Botón de Cerrar** | `<button class="close">` | `<button class="btn-close">` |
| **jQuery Dependency** | ✅ Requerido | ❌ No requerido |
| **Prefijo de datos** | `data-` | `data-bs-` |

---

## 📝 Pasos de Migración

### 1️⃣ Actualizar Referencias de Bootstrap

#### En el Layout Principal (`_Layout.cshtml` o `index.html`):

**❌ ANTES (Bootstrap 4):**
```html
<!-- CSS -->
<link href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" rel="stylesheet">

<!-- JS -->
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.bundle.min.js"></script>
```

**✅ DESPUÉS (Bootstrap 5):**
```html
<!-- CSS - Usando versión local -->
<link href="~/lib/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet">

<!-- O usando CDN -->
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">

<!-- JS - Usando versión local -->
<script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>

<!-- O usando CDN -->
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
```

### 2️⃣ Actualizar Componentes Modales

#### Modal con Botón de Apertura:

**❌ ANTES:**
```html
<button type="button" class="btn btn-danger"
        data-toggle="modal"
        data-target="#deleteModal">
    Eliminar
</button>
```

**✅ DESPUÉS:**
```html
<button type="button" class="btn btn-danger"
        data-bs-toggle="modal"
        data-bs-target="#deleteModal">
    Eliminar
</button>
```

#### Botón de Cerrar Modal:

**❌ ANTES:**
```html
<div class="modal-header">
    <h5 class="modal-title">Título del Modal</h5>
    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
        <span aria-hidden="true">&times;</span>
    </button>
</div>
```

**✅ DESPUÉS:**
```html
<div class="modal-header">
    <h5 class="modal-title">Título del Modal</h5>
    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
</div>
```

**✅ Para Modal con fondo oscuro:**
```html
<div class="modal-header bg-dark text-white">
    <h5 class="modal-title">Título del Modal</h5>
    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
</div>
```

#### Botones de Acción en Footer:

**❌ ANTES:**
```html
<div class="modal-footer">
    <button type="button" class="btn btn-secondary" data-dismiss="modal">
        Cancelar
    </button>
    <button type="submit" class="btn btn-primary">
        Confirmar
    </button>
</div>
```

**✅ DESPUÉS:**
```html
<div class="modal-footer">
    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
        Cancelar
    </button>
    <button type="submit" class="btn btn-primary">
        Confirmar
    </button>
</div>
```

### 3️⃣ Actualizar Componentes Collapse/Navbar

**❌ ANTES:**
```html
<button class="navbar-toggler" type="button" 
        data-toggle="collapse" 
        data-target="#navbarNav">
    <span class="navbar-toggler-icon"></span>
</button>
```

**✅ DESPUÉS:**
```html
<button class="navbar-toggler" type="button" 
        data-bs-toggle="collapse" 
        data-bs-target="#navbarNav">
    <span class="navbar-toggler-icon"></span>
</button>
```

### 4️⃣ Actualizar Componentes Dropdown

**❌ ANTES:**
```html
<div class="dropdown">
    <button class="btn btn-primary dropdown-toggle" 
            data-toggle="dropdown">
        Opciones
    </button>
    <div class="dropdown-menu">
        <a class="dropdown-item" href="#">Opción 1</a>
    </div>
</div>
```

**✅ DESPUÉS:**
```html
<div class="dropdown">
    <button class="btn btn-primary dropdown-toggle" 
            data-bs-toggle="dropdown">
        Opciones
    </button>
    <div class="dropdown-menu">
        <a class="dropdown-item" href="#">Opción 1</a>
    </div>
</div>
```

---

## 💻 Cambios en JavaScript

### Manejo de Modales

**❌ ANTES (Bootstrap 4 con jQuery):**
```javascript
// Mostrar modal
$('#myModal').modal('show');

// Ocultar modal
$('#myModal').modal('hide');

// Escuchar evento
$('#myModal').on('hidden.bs.modal', function () {
    // código
});
```

**✅ DESPUÉS (Bootstrap 5 - Vanilla JS):**
```javascript
// Obtener o crear instancia del modal
const modal = new bootstrap.Modal(document.getElementById('myModal'));

// Mostrar modal
modal.show();

// Ocultar modal
modal.hide();

// Obtener instancia existente
const existingModal = bootstrap.Modal.getInstance(document.getElementById('myModal'));

// Escuchar evento
document.getElementById('myModal').addEventListener('hidden.bs.modal', function () {
    // código
});
```

### Ejemplo Completo de Modal Controlado por JavaScript:

```javascript
document.addEventListener('DOMContentLoaded', function () {
    const openModalBtn = document.getElementById('openModalBtn');
    const confirmBtn = document.getElementById('confirmBtn');
    const modalElement = document.getElementById('confirmModal');
    
    // Crear instancia del modal
    const modal = new bootstrap.Modal(modalElement);
    
    // Abrir modal
    openModalBtn.addEventListener('click', function () {
        modal.show();
    });
    
    // Confirmar y cerrar
    confirmBtn.addEventListener('click', function () {
        // Realizar acción
        console.log('Confirmado');
        
        // Cerrar modal
        modal.hide();
    });
    
    // Escuchar cuando el modal se cierra
    modalElement.addEventListener('hidden.bs.modal', function () {
        console.log('Modal cerrado');
    });
});
```

---

## 🐛 Problemas Comunes y Soluciones

### 1. Modales no se abren

**Problema:** Los modales no responden al hacer clic.

**Solución:**
- Verifica que todos los atributos usen el prefijo `data-bs-*`
- Asegúrate de que Bootstrap 5 JS esté cargado correctamente
- Verifica que no haya conflictos con Bootstrap 4

### 2. Botón de cerrar (×) no funciona

**Problema:** El botón "×" no cierra el modal.

**Solución:**
```html
<!-- Cambiar esto -->
<button type="button" class="close" data-dismiss="modal">
    <span aria-hidden="true">&times;</span>
</button>

<!-- Por esto -->
<button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
```

### 3. Dropdown no funciona

**Problema:** Los menús desplegables no se abren.

**Solución:**
- Cambiar `data-toggle="dropdown"` a `data-bs-toggle="dropdown"`
- Verificar que no haya versiones mixtas de Bootstrap

### 4. Collapse/Accordion no funciona

**Problema:** Los elementos colapsables no se expanden/contraen.

**Solución:**
```html
<!-- Asegúrate de usar data-bs-toggle y data-bs-target -->
<button data-bs-toggle="collapse" data-bs-target="#collapseExample">
    Toggle
</button>
```

### 5. Tooltips y Popovers no funcionan

**Problema:** Los tooltips no aparecen.

**Solución (Bootstrap 5 requiere inicialización manual):**
```javascript
// Inicializar todos los tooltips
const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl));

// Inicializar todos los popovers
const popoverTriggerList = document.querySelectorAll('[data-bs-toggle="popover"]');
const popoverList = [...popoverTriggerList].map(popoverTriggerEl => new bootstrap.Popover(popoverTriggerEl));
```

---

## ✅ Checklist de Verificación

Usa esta lista para asegurarte de que no olvidas nada:

### Archivos HTML/Razor

- [ ] Actualizar referencias CSS de Bootstrap (CDN o local)
- [ ] Actualizar referencias JS de Bootstrap (CDN o local)
- [ ] Eliminar referencias duplicadas de Bootstrap 4
- [ ] Cambiar `data-toggle` → `data-bs-toggle`
- [ ] Cambiar `data-target` → `data-bs-target`
- [ ] Cambiar `data-dismiss` → `data-bs-dismiss`
- [ ] Actualizar botones de cerrar: `class="close"` → `class="btn-close"`
- [ ] Eliminar `<span>&times;</span>` de botones de cerrar
- [ ] Agregar `btn-close-white` en modales con fondo oscuro

### Archivos JavaScript

- [ ] Reemplazar `$('#modal').modal('show')` con `new bootstrap.Modal()`
- [ ] Actualizar eventos: `.on('hidden.bs.modal')` → `.addEventListener('hidden.bs.modal')`
- [ ] Inicializar tooltips manualmente si se usan
- [ ] Inicializar popovers manualmente si se usan
- [ ] Eliminar dependencias de jQuery (si ya no se necesitan)

### Componentes Específicos

- [ ] Actualizar todos los modales
- [ ] Actualizar navbar/collapse
- [ ] Actualizar dropdowns
- [ ] Actualizar tabs
- [ ] Actualizar accordions
- [ ] Actualizar toasts
- [ ] Actualizar offcanvas (nuevo en Bootstrap 5)

### Pruebas

- [ ] Probar apertura/cierre de modales
- [ ] Probar menú responsive (navbar collapse)
- [ ] Probar dropdowns
- [ ] Probar tooltips y popovers
- [ ] Probar tabs y accordions
- [ ] Verificar en diferentes navegadores
- [ ] Verificar en diferentes dispositivos/resoluciones

---

## 🔍 Búsqueda Rápida de Elementos a Actualizar

Usa estos comandos para encontrar elementos que necesitan actualización:

### En VSCode:
1. **Buscar con Ctrl+Shift+F** (o Cmd+Shift+F en Mac)
2. Buscar los siguientes patrones (uno a la vez):

```
data-toggle=
data-target=
data-dismiss=
class="close"
bootstrap/4.
```

### En terminal (Linux/Mac):
```bash
# Buscar data-toggle
grep -r "data-toggle" ./Pages

# Buscar data-target
grep -r "data-target" ./Pages

# Buscar data-dismiss
grep -r "data-dismiss" ./Pages

# Buscar class="close"
grep -r 'class="close"' ./Pages
```

---

## 📚 Recursos Adicionales

- [Documentación Oficial de Bootstrap 5](https://getbootstrap.com/docs/5.3/getting-started/introduction/)
- [Guía de Migración Oficial](https://getbootstrap.com/docs/5.3/migration/)
- [Bootstrap 5 vs Bootstrap 4](https://getbootstrap.com/docs/5.3/migration/#v5)

---

## 💡 Consejos Finales

1. **No mezcles versiones**: Asegúrate de eliminar completamente Bootstrap 4 antes de usar Bootstrap 5
2. **Prueba incrementalmente**: Actualiza un componente a la vez y prueba
3. **Usa el navegador**: Las herramientas de desarrollo del navegador (F12) te ayudarán a detectar errores
4. **Backup primero**: Haz un commit o backup antes de empezar la migración
5. **Lee los warnings**: Bootstrap 5 muestra advertencias útiles en la consola

---

## 🎯 Resumen de Cambios en Este Proyecto

### Archivos Modificados:

1. **`Pages/Shared/_Layout.cshtml`**
   - ✅ Cambiado CSS de Bootstrap 4.4.1 CDN → Bootstrap 5 local
   - ✅ Cambiado JS de Bootstrap 4.4.1 CDN → Bootstrap 5 local
   - ✅ Eliminado Bootstrap 5.3.2 CDN duplicado
   - ✅ Actualizado navbar toggler (`data-toggle` → `data-bs-toggle`)
   - ✅ Actualizado dropdown de usuario (`data-toggle` → `data-bs-toggle`)

2. **`Pages/Owners/OwnerPage.cshtml`**
   - ✅ Actualizado botón de modal (`data-toggle` → `data-bs-toggle`)
   - ✅ Actualizado target de modal (`data-target` → `data-bs-target`)
   - ✅ Cambiado botón de cerrar (`class="close"` → `class="btn-close btn-close-white"`)
   - ✅ Actualizado dismiss de modal (`data-dismiss` → `data-bs-dismiss`)

3. **`Pages/Technicians/TechnicianPage.cshtml`**
   - ✅ Actualizado botón de modal (`data-toggle` → `data-bs-toggle`)
   - ✅ Actualizado target de modal (`data-target` → `data-bs-target`)
   - ✅ Cambiado botón de cerrar (`class="close"` → `class="btn-close btn-close-white"`)
   - ✅ Actualizado dismiss de modal (`data-dismiss` → `data-bs-dismiss`)

4. **`Pages/Services/ServicePage.cshtml`**
   - ✅ Actualizado botón de modal (`data-toggle` → `data-bs-toggle`)
   - ✅ Actualizado target de modal (`data-target` → `data-bs-target`)
   - ✅ Cambiado botón de cerrar (`class="close"` → `class="btn-close btn-close-white"`)
   - ✅ Actualizado dismiss de modal (`data-dismiss` → `data-bs-dismiss`)

5. **`Pages/UserAccounts/AccountPage.cshtml`**
   - ✅ Actualizado botón de modal (`data-toggle` → `data-bs-toggle`)
   - ✅ Actualizado target de modal (`data-target` → `data-bs-target`)
   - ✅ Cambiado botón de cerrar (`class="close"` → `class="btn-close btn-close-white"`)
   - ✅ Actualizado dismiss de modal (`data-dismiss` → `data-bs-dismiss`)

### Archivos que YA estaban correctos:

- ✅ `Pages/Shared/_ConfirmSaveModal.cshtml` - Ya usaba sintaxis Bootstrap 5
- ✅ `Pages/Shared/_ConfirmEditModal.cshtml` - Ya usaba sintaxis Bootstrap 5
- ✅ Archivos JavaScript - Ya usaban API Bootstrap 5 (`new bootstrap.Modal()`)

---

**¡Migración Completada! 🎉**

Tu proyecto ahora usa Bootstrap 5 de manera consistente y sin conflictos.
