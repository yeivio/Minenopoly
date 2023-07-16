using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class PropertyCard : GenericCard
{
    protected PlayerController owner; //Jugador Propietario de la carta
    [SerializeField] protected string cardValue; 

    [SerializeField] protected GameObject tmpCardNameText; // Texto Nombre de la carta
    [SerializeField] protected GameObject tmpValueNameText; // Texto valor de la carta
    [SerializeField] protected GameObject displayOwner; // Indicador del owner

    private String textureName;
    public override void setConfigCSV(string texture, string nombreCalle, string precioCompra,
        string precioDeCasa, string precioDeHotel, string alquiler_0, string alquiler_1, string alquiler_2,
        string alquiler_3, string alquiler_4, string alquiler_Hotel)
    {
        this.textureName = texture;
        this.cardName = nombreCalle;
        this.cardValue = precioCompra;
        var aux = Resources.Load<Texture>("Card/" + texture);
        //Texto visual
        this.tmpCardNameText.GetComponent<TextMeshProUGUI>().SetText(cardName);
        this.tmpValueNameText.GetComponent<TextMeshProUGUI>().SetText(cardValue);
        this.displayOwner.GetComponent<MeshRenderer>().material.color = Color.white;

        try
        {
            this.transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_MainTex", aux);
        }
        catch(Exception e)
        {
            Debug.Log("Error al cargar textura en carta id:"+ cardID.ToString() +",e:" + e);
            this.GetComponent<Renderer>().material.SetTexture("_MainTex", aux);
        }


    }
    public bool comprar(PlayerController jugador)
    {
        if(this.owner  == null)
        {
            this.owner = jugador;
            this.displayOwner.GetComponent<MeshRenderer>().material.color = jugador.getColor();
            return true;
        }
        return false;
    }
    public int getCardValue()
    {
        return Int32.Parse(cardValue);
    }

    public string getCardName()
    {
        return this.cardName;
    }
    public PlayerController getOwner()
    {
        return this.owner;
    }

    public override void cardAction(GameObject jugador)
    {
        throw new NotImplementedException();
    }

    public String getTextureName()
    {
        return this.textureName;
    }

    public bool hasOwner()
    {
        return this.owner != null;
    }
    
}
