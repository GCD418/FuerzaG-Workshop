#!/bin/bash

# 🎨 Script de Copia de Template FuerzaG
# Este script copia automáticamente todos los archivos necesarios 
# de la plantilla FuerzaG a un nuevo proyecto

# Colores para output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # Sin color

# Banner
echo -e "${BLUE}"
echo "╔═══════════════════════════════════════════════════╗"
echo "║   🎨 Replicador de Plantilla FuerzaG             ║"
echo "║   Copia automática de archivos visuales          ║"
echo "╚═══════════════════════════════════════════════════╝"
echo -e "${NC}"

# Solicitar rutas
echo -e "${YELLOW}📁 Configuración de Rutas${NC}"
read -p "Ruta del proyecto FuerzaG origen (default: ~/Documents/ucb/ArquiSoftware/Proyectos/FuerzaG): " SOURCE_PROJECT
SOURCE_PROJECT=${SOURCE_PROJECT:-~/Documents/ucb/ArquiSoftware/Proyectos/FuerzaG}
SOURCE_PROJECT="${SOURCE_PROJECT/#\~/$HOME}"

read -p "Ruta del proyecto destino: " TARGET_PROJECT
if [ -z "$TARGET_PROJECT" ]; then
    echo -e "${RED}❌ Error: Debes especificar la ruta del proyecto destino${NC}"
    exit 1
fi
TARGET_PROJECT="${TARGET_PROJECT/#\~/$HOME}"

# Verificar que el proyecto origen existe
if [ ! -d "$SOURCE_PROJECT" ]; then
    echo -e "${RED}❌ Error: El proyecto origen no existe en $SOURCE_PROJECT${NC}"
    exit 1
fi

# Verificar que el proyecto destino existe
if [ ! -d "$TARGET_PROJECT" ]; then
    echo -e "${RED}❌ Error: El proyecto destino no existe en $TARGET_PROJECT${NC}"
    exit 1
fi

echo -e "${GREEN}✅ Rutas verificadas${NC}"
echo ""

# Función para crear directorios
create_directories() {
    echo -e "${BLUE}📂 Creando estructura de directorios...${NC}"
    
    mkdir -p "$TARGET_PROJECT/wwwroot/css"
    mkdir -p "$TARGET_PROJECT/wwwroot/js"
    mkdir -p "$TARGET_PROJECT/wwwroot/img"
    mkdir -p "$TARGET_PROJECT/wwwroot/lib"
    mkdir -p "$TARGET_PROJECT/Pages/Shared"
    
    echo -e "${GREEN}✅ Directorios creados${NC}"
}

# Función para copiar CSS
copy_css() {
    echo -e "${BLUE}🎨 Copiando archivos CSS...${NC}"
    
    if [ -f "$SOURCE_PROJECT/wwwroot/css/style.css" ]; then
        cp "$SOURCE_PROJECT/wwwroot/css/style.css" "$TARGET_PROJECT/wwwroot/css/"
        echo -e "${GREEN}  ✓ style.css copiado${NC}"
    else
        echo -e "${RED}  ✗ style.css no encontrado${NC}"
    fi
    
    if [ -f "$SOURCE_PROJECT/wwwroot/css/site.css" ]; then
        cp "$SOURCE_PROJECT/wwwroot/css/site.css" "$TARGET_PROJECT/wwwroot/css/"
        echo -e "${GREEN}  ✓ site.css copiado${NC}"
    else
        echo -e "${YELLOW}  ⚠ site.css no encontrado (opcional)${NC}"
    fi
}

# Función para copiar JavaScript
copy_js() {
    echo -e "${BLUE}📜 Copiando archivos JavaScript...${NC}"
    
    if [ -f "$SOURCE_PROJECT/wwwroot/js/main.js" ]; then
        cp "$SOURCE_PROJECT/wwwroot/js/main.js" "$TARGET_PROJECT/wwwroot/js/"
        echo -e "${GREEN}  ✓ main.js copiado${NC}"
    else
        echo -e "${RED}  ✗ main.js no encontrado${NC}"
    fi
    
    if [ -f "$SOURCE_PROJECT/wwwroot/js/site.js" ]; then
        cp "$SOURCE_PROJECT/wwwroot/js/site.js" "$TARGET_PROJECT/wwwroot/js/"
        echo -e "${GREEN}  ✓ site.js copiado${NC}"
    else
        echo -e "${YELLOW}  ⚠ site.js no encontrado (opcional)${NC}"
    fi
}

# Función para copiar imágenes
copy_images() {
    echo -e "${BLUE}🖼️  Copiando imágenes...${NC}"
    
    if [ -f "$SOURCE_PROJECT/wwwroot/img/logo-fuerzaG.png" ]; then
        cp "$SOURCE_PROJECT/wwwroot/img/logo-fuerzaG.png" "$TARGET_PROJECT/wwwroot/img/"
        echo -e "${GREEN}  ✓ logo-fuerzaG.png copiado${NC}"
    else
        echo -e "${YELLOW}  ⚠ logo-fuerzaG.png no encontrado${NC}"
    fi
    
    # Preguntar si copiar todas las imágenes
    read -p "¿Copiar todas las imágenes del template? (y/n): " COPY_ALL_IMAGES
    if [ "$COPY_ALL_IMAGES" = "y" ] || [ "$COPY_ALL_IMAGES" = "Y" ]; then
        cp "$SOURCE_PROJECT/wwwroot/img/"*.jpg "$TARGET_PROJECT/wwwroot/img/" 2>/dev/null
        echo -e "${GREEN}  ✓ Imágenes JPG copiadas${NC}"
    fi
}

# Función para copiar layouts
copy_layouts() {
    echo -e "${BLUE}📄 Copiando layouts y vistas compartidas...${NC}"
    
    if [ -f "$SOURCE_PROJECT/Pages/Shared/_Layout.cshtml" ]; then
        cp "$SOURCE_PROJECT/Pages/Shared/_Layout.cshtml" "$TARGET_PROJECT/Pages/Shared/"
        echo -e "${GREEN}  ✓ _Layout.cshtml copiado${NC}"
    else
        echo -e "${RED}  ✗ _Layout.cshtml no encontrado${NC}"
    fi
    
    if [ -f "$SOURCE_PROJECT/Pages/Shared/_AuthLayout.cshtml" ]; then
        cp "$SOURCE_PROJECT/Pages/Shared/_AuthLayout.cshtml" "$TARGET_PROJECT/Pages/Shared/"
        echo -e "${GREEN}  ✓ _AuthLayout.cshtml copiado${NC}"
    else
        echo -e "${YELLOW}  ⚠ _AuthLayout.cshtml no encontrado (opcional)${NC}"
    fi
    
    if [ -f "$SOURCE_PROJECT/Pages/Shared/_ConfirmSaveModal.cshtml" ]; then
        cp "$SOURCE_PROJECT/Pages/Shared/_ConfirmSaveModal.cshtml" "$TARGET_PROJECT/Pages/Shared/"
        echo -e "${GREEN}  ✓ _ConfirmSaveModal.cshtml copiado${NC}"
    else
        echo -e "${YELLOW}  ⚠ _ConfirmSaveModal.cshtml no encontrado (opcional)${NC}"
    fi
    
    if [ -f "$SOURCE_PROJECT/Pages/Shared/_ConfirmEditModal.cshtml" ]; then
        cp "$SOURCE_PROJECT/Pages/Shared/_ConfirmEditModal.cshtml" "$TARGET_PROJECT/Pages/Shared/"
        echo -e "${GREEN}  ✓ _ConfirmEditModal.cshtml copiado${NC}"
    else
        echo -e "${YELLOW}  ⚠ _ConfirmEditModal.cshtml no encontrado (opcional)${NC}"
    fi
}

# Función para copiar ViewImports y ViewStart
copy_view_files() {
    echo -e "${BLUE}📝 Copiando archivos de configuración de vistas...${NC}"
    
    if [ -f "$SOURCE_PROJECT/Pages/_ViewImports.cshtml" ]; then
        cp "$SOURCE_PROJECT/Pages/_ViewImports.cshtml" "$TARGET_PROJECT/Pages/"
        echo -e "${GREEN}  ✓ _ViewImports.cshtml copiado${NC}"
    else
        echo -e "${RED}  ✗ _ViewImports.cshtml no encontrado${NC}"
    fi
    
    if [ -f "$SOURCE_PROJECT/Pages/_ViewStart.cshtml" ]; then
        cp "$SOURCE_PROJECT/Pages/_ViewStart.cshtml" "$TARGET_PROJECT/Pages/"
        echo -e "${GREEN}  ✓ _ViewStart.cshtml copiado${NC}"
    else
        echo -e "${RED}  ✗ _ViewStart.cshtml no encontrado${NC}"
    fi
}

# Función para copiar libman.json
copy_libman() {
    echo -e "${BLUE}📦 Configurando libman.json...${NC}"
    
    if [ -f "$SOURCE_PROJECT/libman.json" ]; then
        read -p "¿Copiar libman.json existente? (y/n): " COPY_LIBMAN
        if [ "$COPY_LIBMAN" = "y" ] || [ "$COPY_LIBMAN" = "Y" ]; then
            cp "$SOURCE_PROJECT/libman.json" "$TARGET_PROJECT/"
            echo -e "${GREEN}  ✓ libman.json copiado${NC}"
        fi
    else
        read -p "¿Crear libman.json nuevo con Bootstrap 5? (y/n): " CREATE_LIBMAN
        if [ "$CREATE_LIBMAN" = "y" ] || [ "$CREATE_LIBMAN" = "Y" ]; then
            cat > "$TARGET_PROJECT/libman.json" << 'EOF'
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
    }
  ]
}
EOF
            echo -e "${GREEN}  ✓ libman.json creado${NC}"
        fi
    fi
}

# Ejecutar funciones
create_directories
copy_css
copy_js
copy_images
copy_layouts
copy_view_files
copy_libman

# Resumen final
echo ""
echo -e "${BLUE}╔═══════════════════════════════════════════════════╗${NC}"
echo -e "${BLUE}║           📊 RESUMEN DE LA COPIA                  ║${NC}"
echo -e "${BLUE}╚═══════════════════════════════════════════════════╝${NC}"
echo ""
echo -e "${GREEN}✅ Archivos copiados exitosamente${NC}"
echo ""
echo -e "${YELLOW}📝 PASOS SIGUIENTES:${NC}"
echo ""
echo "1. Instalar libman CLI (si no lo tienes):"
echo -e "   ${BLUE}dotnet tool install -g Microsoft.Web.LibraryManager.Cli${NC}"
echo ""
echo "2. Restaurar librerías en el proyecto destino:"
echo -e "   ${BLUE}cd $TARGET_PROJECT${NC}"
echo -e "   ${BLUE}libman restore${NC}"
echo ""
echo "3. Personalizar _Layout.cshtml:"
echo "   - Cambiar logo"
echo "   - Actualizar información de contacto"
echo "   - Modificar menú de navegación"
echo ""
echo "4. Personalizar colores en style.css"
echo ""
echo "5. Ejecutar el proyecto:"
echo -e "   ${BLUE}dotnet run${NC}"
echo ""
echo -e "${GREEN}¡Buena suerte con tu nuevo proyecto! 🚀${NC}"
