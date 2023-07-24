using UnityEngine;
using System.Collections.Generic;

public class TableManager : MonoBehaviour
{
    public CardManager cardManager; //Manejador de las cartas del tablero
    private PlayerManager playerManager; //Manejador de todos los objetos Jugador
    private UIManager uiManager;
    private GameObject mainCamera; // Instancia de la cámara actual
    private GameObject jugadorActivo; //Instancia del jugador que le toca jugar
    [SerializeField] private int NUM_PLAYERS = 2;

    /// <summary>
    /// Función constructor privada para cargar las variables necesarias para la clase
    /// </summary>
    private void Start(){
        playerManager = new PlayerManager(); //Inicializar el player manager
        uiManager = this.GetComponent<UIManager>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera"); //Obtener la camara principal
        try
        {
            mainCamera.gameObject.GetComponent<CameraManager>()
                .setNewActivePlayer(jugadorActivo.GetComponent<PlayerController>()); //Establecer la posición de la camra en el primer jugador
        }
        catch
        {
            mainCamera.gameObject.GetComponent<CameraManager>().setDefaultPosition();
        }
        spawnearJugador(NUM_PLAYERS);
        uiManager.activarUIMovimiento(this.jugadorActivo.GetComponent<PlayerController>());
    }

    public void spawnearJugador(int players)
    {
        for(int i = 0; i<players; i++)
            jugadorActivo = playerManager.registrarJugador();
        mainCamera.gameObject.GetComponent<CameraManager>().setNewActivePlayer(jugadorActivo.GetComponent<PlayerController>());
        
    }

    /// <summary>
    /// Función que empieza el turno del jugador establecido como jugador activo. esto es lanzar dados, moverse,
    /// hacer una acción de viviendas y por último cambiar al siguiente jugador activo
    /// </summary>
    public void empezarTurno()
    {
        jugadorActivo.GetComponent<PlayerController>().mover(lanzarDados()); // Mover el jugador un número aleatorio de posiciones
        mainCamera.gameObject.GetComponent<CameraManager>().setNewActivePlayer(jugadorActivo.GetComponent<PlayerController>());
    }


    /// <summary>
    /// Función para pasar al siguiente jugador activo.
    /// </summary>
    public void siguienteTurno(){
        jugadorActivo = playerManager.getNextPlayer();
        //jugadorActivo.transform.position = playerManager.getJugadorActivo().getPosicionEnCarta().obtenerLugarLibre();
        mainCamera.gameObject.GetComponent<CameraManager>().setNewActivePlayer(jugadorActivo.GetComponent<PlayerController>());
        uiManager.activarUIMovimiento(jugadorActivo.GetComponent<PlayerController>());
        
    }


    public void skipCompra()
    {
        jugadorActivo = playerManager.getNextPlayer();
        mainCamera.gameObject.GetComponent<CameraManager>().setNewActivePlayer(jugadorActivo.GetComponent<PlayerController>());
        uiManager.activarUIMovimiento(jugadorActivo.GetComponent<PlayerController>());
    }

    public void construirCasa()
    {
        //@TODO
    }
   

    public void comprarCarta()
    {
        if (!jugadorActivo.GetComponent<PlayerController>().comprarCarta())
        {
            Debug.Log("No se ha podido comprar la carta");
        }
        this.siguienteTurno();
    }

    public void buildStructures(List<UICardController> listObjects)
    {
        foreach (UICardController obj in listObjects)
        {
            cardManager.buildStructures(obj.getCard(),obj.getNumHouses(), obj.getNumHotel());
        }
    }


    /// <summary>
    /// Función que simula lanzar dos dados de 6 caras y devuelve la suma de ambos
    /// </summary>
    /// <returns>Integer con la suma de ambos resultados</returns>
    public int lanzarDados(){
        System.Random rnd = new System.Random();
        return rnd.Next(1,6) + rnd.Next(1,6);
    }

    public PlayerController getActivePlayer()
    {
        return this.jugadorActivo.GetComponent<PlayerController>();
    }
}
