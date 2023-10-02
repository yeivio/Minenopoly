using UnityEngine;
using System.Collections.Generic;
using System;

public class TableManager : MonoBehaviour
{
    public CardManager cardManager; //Manejador de las cartas del tablero
    private PlayerManager playerManager; //Manejador de todos los objetos Jugador
    private UIManager uiManager;
    private GameObject mainCamera; // Instancia de la cámara actual
    private GameObject jugadorActivo; //Instancia del jugador que le toca jugar
    [SerializeField] private int NUM_PLAYERS = 2;

    public static event Action onRoundFinished;

    /// <summary>
    /// Función constructor privada para cargar las variables necesarias para la clase
    /// </summary>
    private void Start(){
        playerManager = new PlayerManager(); //Inicializar el player manager
        uiManager = this.GetComponent<UIManager>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera"); //Obtener la camara principal

        spawnearJugador(NUM_PLAYERS);
        uiManager.activarUIMovimiento(this.jugadorActivo.GetComponent<PlayerController>());

        PlayerController.onJailCard += siguienteTurno; //Evento jugador cae en ir a carcel 
        PlayerManager.onLastPlayer += finishGame;
        MoneyController.onBankrupt += playerBankrupt;
        
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
        onRoundFinished?.Invoke();
        jugadorActivo = playerManager.getNextPlayer();
        while (cardManager.getJailCard().onJail(jugadorActivo))
            jugadorActivo = playerManager.getNextPlayer();
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
        uiManager.activarUICompraCasa(this.jugadorActivo.GetComponent<PlayerController>());
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

    public void finishGame(PlayerController player)
    {
        //@TODO
    }


    /// <summary>
    /// Función que simula lanzar dos dados de 6 caras y devuelve la suma de ambos
    /// </summary>
    /// <returns>Integer con la suma de ambos resultados</returns>
    public int lanzarDados(){
        System.Random rnd = new System.Random();
        return rnd.Next(1,6) + rnd.Next(1,6);
    }

    private void playerBankrupt(PlayerController player)
    {
        this.playerManager.destroyPlayer(player);

    }
    public PlayerController getActivePlayer()
    {
        return this.jugadorActivo.GetComponent<PlayerController>();
    }
}
