using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class PropertyCard : GenericCard
{
    protected PlayerController owner; //Jugador Propietario de la carta
    [SerializeField] protected string cardValue; //Valor de la carta
    [SerializeField] protected int hotelValue; // Valor de comprar un Hotel
    [SerializeField] protected int houseValue; // Valor de comprar una casa
    [SerializeField] protected GameObject tmpCardNameText; // Texto Nombre de la carta
    [SerializeField] protected GameObject tmpValueNameText; // Texto valor de la carta
    [SerializeField] protected GameObject displayOwner; // Indicador del owner
    [SerializeField] private GameObject displayHouses; // Indicador del numero de casas
    [SerializeField] private GameObject displayHotels; // Indicador del numero de hoteles

    protected List<int> listAlquileres;

    private int houseNumber;
    private int hotelNumber;

    private int alquiler_hotel;
    protected String textureName;

    public override void setConfigCSV(string texture, string nombreCalle, string precioCompra,
        string precioDeCasa, string precioDeHotel, string alquiler_0, string alquiler_1, string alquiler_2,
        string alquiler_3, string alquiler_4, string alquiler_Hotel)
    {
        this.textureName = texture;
        this.cardName = nombreCalle;
        this.cardValue = precioCompra;
        listAlquileres = new List<int>();
        listAlquileres.Add(Int32.Parse(alquiler_0));
        listAlquileres.Add(Int32.Parse(alquiler_1));
        listAlquileres.Add(Int32.Parse(alquiler_2));
        listAlquileres.Add(Int32.Parse(alquiler_3));
        listAlquileres.Add(Int32.Parse(alquiler_4));
        this.alquiler_hotel = Int32.Parse(alquiler_Hotel);
        this.hotelValue = Int32.Parse(precioDeHotel);
        this.houseValue = Int32.Parse(precioDeCasa);

        this.houseNumber = 0;
        this.hotelNumber = 0;

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
        PlayerController player = jugador.GetComponent<PlayerController>();


        if (!this.hasOwner() || this.owner.getId() == player.getId())
            return;
        player.GetComponent<MoneyController>().removeMoney(this.getAlquiler());
    }

    public String getTextureName()
    {
        return this.textureName;
    }

    public bool hasOwner()
    {
        return this.owner != null;
    }

    public int getHotelPrice()
    {
        return this.hotelValue;
    }
    public int getHousePrice()
    {
        return this.houseValue;
    }

    public void setHotelNumber(int number)
    {
        this.hotelNumber = number;
        this.displayHotels.GetComponent<TextMeshProUGUI>().SetText(this.hotelNumber.ToString());
    }
    public void setHouseNumber(int number)
    {
        this.houseNumber = number;
        this.displayHouses.GetComponent<TextMeshProUGUI>().SetText(this.houseNumber.ToString());
    }

    public virtual int getAlquiler()
    {
        int totalCasa = 0;
        int totalHotel = 0;
        totalCasa = listAlquileres[this.houseNumber];
        totalHotel = alquiler_hotel * this.hotelNumber;
        return totalCasa + totalHotel;
    }
}

