using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager
{

    private GameObject playerClass;
    private IList<GameObject> listPlayers;
    private int turn;

    public static event Action<PlayerController> onLastPlayer; //Evento para cuando acabe la partida (solo queda 1 jugador)


    /// <summary>
    /// Constructor de la clase PlayerManager, inicializa las listas de jugadores y los turnos de jugador.
    /// También se inicia con como mínimo un jugador
    /// </summary>
    public PlayerManager(){
        listPlayers = new List<GameObject>();
        turn = -1;
        playerClass = Resources.Load("Prefabs/Player") as GameObject;
    }

    /// <summary>
    /// Función que crea un jugador en el tablero con rotación 0,180,0,0 y en la posición 0,0,0
    /// </summary>
    /// <returns>El Objecto Jugador creado</returns>
    public GameObject registrarJugador(){
        GameObject aux = MonoBehaviour.Instantiate(playerClass, new Vector3(0,0,0), new Quaternion(0,180,0,0));
        aux.GetComponent<PlayerController>().setId(listPlayers.Count);
        listPlayers.Add(aux);
        turn++;
        return aux;
    }

    /// <summary>
    /// Función que crea un jugador en el tablero con rotación 0,180,0,0 y en la posición 0,0,0
    /// </summary>
    /// <returns>El Objecto Jugador creado</returns>
    public void destroyPlayer(PlayerController player)
    {
        GameObject.Destroy(player.gameObject);
        this.listPlayers.Remove(player.gameObject);
        if (this.listPlayers.Count == 1)
            onLastPlayer?.Invoke(this.listPlayers[0].GetComponent<PlayerController>());
    }

    /// <summary>
    /// Obtiene el siguiente jugador en orden de los turnos
    /// </summary>
    /// <returns>Objeto Jugador</returns>
    public GameObject getNextPlayer(){
        listPlayers[turn].GetComponent<PlayerController>().pasarASecundario();
        turn++;
        if (turn >= listPlayers.Count) //Final de la lista
        { 
            turn = 0;
        }
        listPlayers[turn].GetComponent<PlayerController>().pasarActivo();
        return this.listPlayers[turn];
    }

    public GameObject getFirstPlayer(){
        if (listPlayers.Count == 0)
            return null;
        return listPlayers[0];
    }

    public GameObject getPlayer(int index) {
        return this.listPlayers[index];
    }
    public IList<GameObject> getPlayers()
    {
        return listPlayers;
    }

    /// <summary>
    /// Función que devuelve el jugador que está ahora mismo activo
    /// </summary>
    /// <returns>GameObject que representa al jugador</returns>
    public PlayerController getJugadorActivo()
    {
        return this.getPlayer(this.turn).GetComponents<PlayerController>()[0];
    }

    public int getTurn()
    {
        return this.turn;
    }
}
