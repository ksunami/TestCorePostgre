Instrucciones
1. Para ejecutar la aplicación localmente:

Asegúrate de tener Docker y Docker Compose instalados.
Clona este repositorio y navega al directorio raíz del proyecto.
Construye los contenedores con Docker Compose:
   docker-compose up --build

La API estará disponible en http://localhost:5000.

2. Para ejecutar las pruebas unitarias:
   Desde la linea de comandos, situado en la carpeta de la solucion ejecuta el siguiente comando:
   dotnet test
   
3. Para ejecutar las pruebas con Docker Compose:
   - Asegúrate de que Docker esté corriendo.
   - Ejecuta el siguiente comando para levantar los servicios de Docker Compose y probar la integración de la API REST con PostgreSQL:
       docker-compose up
   - La prueba de verificación del controlador usará la base de datos PostgreSQL configurada en Docker.
