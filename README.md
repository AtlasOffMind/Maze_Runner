Introduccion:
 En este primer proyecto de programación se pidió desarrollar un juego al estilo "Maze Runner". Dada esta petición, utilizando Unity como entorno de desarrollo y diseño 3D, C# como lenguaje de programación, grandes cantidades de café como alimento fundamental en las madrugadas de programación, un desarrollador con ideas locas y pocas horas de sueño, nació este juego llamado "Maze Runners: Running in the Edge" ambientado levemente en la película Maze Runners y otras ideas por parte del desarrollador.

Guía de instalación:

 Descargar el "MazeRunner.rar", descomprimir en la carpeta que desee guardar el juego. Buscar el archivo MazeRunner.exe y dar doble click y jugar. 

Requisitos para la instalación

 Usar Windows como Sistema Operativo.

Descripcion del Codigo:

 La lógica del juego esta compuesta por un conjunto de clases en las que se implementan métodos de manera consecutiva necesarios para el control del  juego.
 Entre las clases principales se encuentran:
 
    1-	MazeCell:
         Es una clase destinada a guardar todos los componentes necesarios para crear un prefabricado acorde a la forma de una pequeña habitación en forma de cubo (celada) y además tener los métodos necesarios para poder ajustar y personalizar dichas celdas.
    2-	Player:
     Esta clase solo contiene las características de cada Token en específico y así acceder a ellas para crear personajes únicos.
    3-	Traps:
         Es una clase estática diseñada de esta forma para poder acceder a ella de manera mas sencilla y poder gestionar toda la lógica de las trampas de una forma más directa.
    4-	MazeGenerator:
         Esta es de por si la clase mas importante del juego mismo dado a que esta clase tiene varias funciones implementadas para ejecutarse en el principio del juego y básicamente gestione todo el comienzo. Entre las acciones que realiza en su método Start (método especial de Unity que se acciona a la hora de entrar en una nueva escena) se encuentran:
        a.	1er For:
             Este for se dedica a instanciar en la escena todas las copias de los prefabricados de las celdas necesarias para crear una matriz completa de objetos tipo MazeCell.
        b.	GenerateMaze:
             Este método utiliza la matriz creada con anterioridad para pararse sobre su primer elemento en la coordenada (de la matriz) <0,0> y lo que hace dicho de forma sencilla es preguntar si sus vecinos en 4 posibles direcciones (N, S, E, O) han sido visitados con anterioridad, si no es el caso escoge uno al azar y derrumba las paredes que lo conectan para así crear un camino. Este proceso se repite de manera recursiva hasta que no tenga más vecinos alrededor que no hayan sido visitados al menos una vez. De esta manera aseguramos crear “proceduralmente” un laberinto completo, totalmente aleatorio para cada partida y sin posibilidades de espacios sin sentido o inalcanzables.
        c.	SetEntranceAndExit:
             Como su nombre lo indica establece las entradas y salidas del juego cambiándoles el color para identificarlas y guardándolas en listas de tipo MazeCell para ser utilizadas más tarde por otras clases.
        d.	Traps.Iniciate:
             Este es un método que hace iniciar las instanciaciones necesarias en la clase Traps para ser usadas adecuadamente luego.
        e.	PuttingTraps:
             Un método dedicado solamente a la colocación de los 3 tipos de trampas diferentes en posiciones aleatorias del juego utilizando rangos preestablecidos por el desarrollador.
        f.	PlacingPlayer:
             Este método coloca a todos los jugadores en las respectivas posiciones de entrada.
        g.	TM.PlayerSelect:
             Este método se encuentra en la clase TurnManagement, de la cual hablaremos ahora, este método recibe una lista de tipo Player y selecciona al 1er token que le toca jugar y lo activa como el único al cual se le permite moverse hasta que se acabe su turno.
    5-	TurnManagement:
         Esta clase gestiona toda la lógica de los turnos recibiendo una lista de tipo Player donde se encuentran todos los jugadores en juego. Esta clase activa y le pasa a cada token los componentes necesarios para jugar sin crear conflictos con los otros scripts del juego mientras esta en ejecución. 
    6-	Motion:
         Esta clase es la destinada a desplazar a los personajes por cada una de las celdas dentro del laberinto y evitando que atraviesen paredes. Esta clase desplaza, deselecciona y salta a los tokens según criterios correspondientes dentro de ella durante la ejecución del juego. 

                                    
                                                                    ¡Gracias por jugar!


Pd: Espero que haya disfrutado tanto el proyecto como yo a la hora de realizarlo y verlo completado dado a lo arduo que fue hacer el proyecto en 3D y con 0 conocimiento de Unity, solo pido que no tenga demasiado en cuenta el apartado visual dado a que aún lo puedo mejorar mas tenga en cuenta que esto es lo máximo que he podido lograr antes de la fecha límite.

Pd de la posdata: Espero para el momento de la exposición poder lograr mejorarlo al grado que me gustaría, espere con ansias futuras actualizaciones, no me pierda de vista.
