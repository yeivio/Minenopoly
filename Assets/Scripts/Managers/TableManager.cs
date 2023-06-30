using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using Unity.VisualScripting;

public class TableManager : MonoBehaviour
{
    public CardManager cardManager; //Manejador de las cartas del tablero
    private PlayerManager playerManager; //Manejador de todos los objetos Jugador
    public UIManager UIManager;

    private GameObject mainCamera; // Instancia de la cámara actual
    private GameObject jugadorActivo; //Instancia del jugador que le toca jugar

    public int MAX_JUGADORES;

    void Start(){
        load(); //iniciar variables
    }

    //void Update() { Debug.Log(this.playerManager.getTurn()); }

    /// <summary>
    /// Función constructor privada para cargar las variables necesarias para la clase
    /// </summary>
    private void load(){
        playerManager = new PlayerManager(); //Inicializar el player manager

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

        this.UIManager.activarUIMovimiento(null);
    }

    public void spawnearJugador()
    {
        if (playerManager.getPlayers().Count != MAX_JUGADORES) //Se puede generar otro jugador
            jugadorActivo = playerManager.registrarJugador();
        mainCamera.gameObject.GetComponent<CameraManager>().setNewActivePlayer(jugadorActivo.GetComponent<PlayerController>());
        this.UIManager.activarUIMovimiento(jugadorActivo.GetComponent<PlayerController>());
    }

    /// <summary>
    /// Función que empieza el turno del jugador establecido como jugador activo. esto es lanzar dados, moverse,
    /// hacer una acción de viviendas y por último cambiar al siguiente jugador activo
    /// </summary>
    public void empezarTurno()
    {
        this.UIManager.desactivarUIMovimiento();
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
    }

    
    public void skipCompra()
    {
        jugadorActivo = playerManager.getNextPlayer();
        UIManager.desactivarUICompra();
        UIManager.activarUIMovimiento(jugadorActivo.GetComponent<PlayerController>());
        mainCamera.gameObject.GetComponent<CameraManager>().setNewActivePlayer(jugadorActivo.GetComponent<PlayerController>());
    }
   

    public void comprarCarta()
    {
        if (jugadorActivo.GetComponent<PlayerController>().comprarCarta())// Comprar carta
        { 
            UIManager.activarUICompra((PropertyCard)jugadorActivo.GetComponent<PlayerController>().getPosicionEnCarta()); // Actualizar UI
        }
        else
        {
            Debug.LogWarning("No se ha podido ejecutar la compra");
        }

        UIManager.desactivarUICompra();
        UIManager.activarUIMovimiento(null);
    }

    


    /// <summary>
    /// Función que simula lanzar dos dados de 6 caras y devuelve la suma de ambos
    /// </summary>
    /// <returns>Integer con la suma de ambos resultados</returns>
    public int lanzarDados(){
        System.Random rnd = new System.Random();
        return rnd.Next(1,6) + rnd.Next(1,6);
    }
}
