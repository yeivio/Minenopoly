using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailCard : GenericCard
{
    [SerializeField] IDictionary<GameObject,int> jail;
    [SerializeField] private bool isGoToJail = false;
    [SerializeField] private GenericCard jailCard; // Carta Carcel
    private const int TURNOS = 3; //Num turnos que el jugador no puede jugar


    void Start(){
        jail = new Dictionary<GameObject,int>();

    }
    public void addPlayerJail(GameObject player){
        if(!this.jail.ContainsKey(player)){
            this.jail.Add(player, TURNOS);
        }
    }

    public void removePlayerJail(GameObject player){
        if(this.jail.ContainsKey(player)){
            this.jail.Remove(player);
        }
        
    }   

    public void updateJail(){
        foreach(var o in this.jail){
            jail[o.Key]--;
            if(o.Value == 0){
                removePlayerJail(o.Key);
            }
        }
    }

    public override void setConfigCSV(string texture, string nombreCalle, string precioCompra,
        string precioDeCasa, string precioDeHotel, string alquiler_0, string alquiler_1, string alquiler_2,
        string alquiler_3, string alquiler_4, string alquiler_Hotel)
    {
        var aux = Resources.Load<Texture>("Card/" + texture);
        try
        {
            //this.transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_MainTex", aux);
            this.transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_MainTex", aux);
        }
        catch (Exception e)
        {
            Debug.Log("Error al cargar textura en carta id:" + cardID.ToString() + ",e:" + e);
            this.GetComponent<Renderer>().material.SetTexture("_MainTex", aux);
        }
    }

    public override void cardAction(GameObject jugador)
    {
        if (isGoToJail)
        {
            this.addPlayerJail(jugador);
            jugador.transform.position = this.jailCard.obtenerLugarLibre();
            jugador.GetComponent<PlayerController>().setPosicionActual(this.jailCard);

        }

    }
}
