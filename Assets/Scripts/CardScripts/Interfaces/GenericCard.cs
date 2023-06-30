using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.NetworkInformation;

public abstract class GenericCard : MonoBehaviour
{
    protected TableManager tableManager;
    protected Material image;

    [SerializeField] protected int cardID;
    [SerializeField] protected string cardName;
    [SerializeField] private bool isActionOnly;

    public static float OFFSET = 0.1f; //Pequeño offset para que no atraviese el pj la pieza

    private Dictionary<Vector3,Boolean> posicionesEspera; //Diccionario que relaciona el estado de las posiciones de espera en las cartas

    [SerializeField] GameObject posicionActiva;
    [SerializeField] GameObject posicionEspera1,posicionEspera2,posicionEspera3;

    public abstract void setConfigCSV(String texture, String nombreCalle, String precioCompra,
        String precioDeCasa, String precioDeHotel, String alquiler_0, String alquiler_1,
        String alquiler_2, String alquiler_3, String alquiler_4, String alquiler_Hotel);
    public abstract void cardAction(GameObject jugador);


    void Start(){
        posicionesEspera = new Dictionary<Vector3,Boolean>(){{posicionEspera1.transform.position,true},
                                                            {posicionEspera2.transform.position,true},
                                                            {posicionEspera3.transform.position,true}};
    }

    /// <summary>
    /// Función con la que obtener el id de cada objeto carta
    /// </summary>
    /// <returns></returns>
    public int getId(){
        return cardID;
    }

    /// <summary>
    /// Función que devuelve la posición donde se puede colocar un jugador activo
    /// </summary>
    /// <returns>Vector3 con la posición en la que puede estar</returns>
    public Vector3 getActiveLocation(){
        Vector3 aux = this.posicionActiva.transform.position; 
        aux.Set(aux.x, aux.y + OFFSET, aux.z); //OFFSET
        return aux;
    }

    /// <summary>
    /// Busca en las posiciones de espera una posicion libre dónde se puede quedar el jugador
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Vector3 obtenerLugarLibre(){
        foreach (var i in posicionesEspera.Keys){ 
            if(posicionesEspera[i]){
                posicionesEspera[i] = false;
                return i;
            }
        }

        throw new Exception("No hay sitios dónde posicionar al jugador");
    }

    /// <summary>
    /// Liberar una posición de espera
    /// </summary>
    /// <param name="pos"> Posición que se quiere liberar</param>
    public void liberarLugar(Vector3 pos){
        foreach(var(key,value) in new Dictionary<Vector3, Boolean>(posicionesEspera))
            if(key == pos)
                posicionesEspera[key] = true;
    }

    /// <summary>
    /// Comprobar si una carta es de solo acción. Esto es que no se necesita intervención del jugador para nada
    /// </summary>
    /// <returns>True si es una carta de solo acción, false para lo contrario</returns>
    public bool getIsActionOnly()
    {
        return this.isActionOnly;
    }
}
