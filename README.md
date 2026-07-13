## 1. Descripción del Proyecto
El proyecto consiste en un videojuego interactivo en 3D en primera persona. El jugador debe navegar por un escenario, enfrentarse a enemigos con inteligencia artificial de persecución, gestionar su munición mediante recargas manuales, alternar entre múltiples armas y curar sus puntos de vida recolectando botiquines en el mapa con el fin de alcanzar una zona de meta.

---

## 2. Características del Juego e Implementación de Scripts

* **Controlador en Primera Persona (`PrimeraPersona.cs`)**: Gestiona las físicas de movimiento tridimensional y el comportamiento de la cámara simulando la perspectiva de ojos del jugador.
* **Sistema de Disparo (`Disparar.cs`)**: Implementa el combate por proyección de rayos (*Raycast*). Maneja la cadencia de fuego, el consumo y recarga de munición, efectos visuales de destello en el cañón (*Muzzle flash*) e interfaz de usuario (UI) para la munición.
* **Selección Dinámica de Armas (`SelectorArmas.cs`)**: Permite cambiar entre diferentes armamentos en tiempo de ejecución de forma fluida (alternando a través de la tecla `T`).
* **Inteligencia Artificial de Enemigos (`EnemigoIA.cs` / `Enemigo.cs`)**: Los enemigos calculan rutas dinámicas persiguiendo al jugador, además de reaccionar al daño y realizar ataques cuerpo a cuerpo.
* **Sistema de Vida y Curación (`Vida.cs` / `Botiquin.cs`)**: Controla los puntos de salud de la entidad del jugador y enemigos, gestionando la interfaz de vida y el trigger de curación al colisionar con kits médicos.
* **Generación Dinámica (`SpawnerEnemigos.cs`)**: Spawnea oleadas de enemigos periódicamente para añadir dificultad al escenario.
* **Condición de Victoria (`Meta.cs`)**: Trigger de fin de nivel que detecta al jugador y gestiona la victoria del juego reproduciendo sonidos específicos.

---

## 3. Herramientas Tecnológicas y Motores Utilizados

Para la creación del proyecto se utilizaron las siguientes herramientas del ecosistema de desarrollo de videojuegos:

* **Unity Editor (Versión 6000)**: Motor gráfico principal usado para el diseño de niveles, la composición tridimensional de la escena (`SampleScene.unity`) y la simulación física.
* **C# y .NET Standard 2.1**: Lenguaje de programación nativo del motor usado para el desarrollo lógico de scripts.
* **Universal Render Pipeline (URP)**: Pipeline gráfico moderno de Unity empleado para lograr una iluminación dinámica optimizada tanto para plataformas móviles como PC.
* **Unity NavMesh (Navegación)**: Mapeo de superficies transitables usado para calcular las rutas y el pathfinding dinámico de la IA de los enemigos (`NavMesh-Piso.asset`).
* **Unity Input System**: Mapeo de teclas y acciones para asegurar un control fluido del jugador (`InputSystem_Actions.inputactions`).
* **Visual Studio / VS Code**: Entornos de desarrollo utilizados para la edición y depuración del código fuente C#.
