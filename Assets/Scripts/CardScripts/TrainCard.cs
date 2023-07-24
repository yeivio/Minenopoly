using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class TrainCard : PropertyCard
{
    public override void setConfigCSV(string texture, string nombreCalle, string precioCompra,
        string precioDeCasa, string precioDeHotel, string alquiler_0, string alquiler_1, string alquiler_2,
        string alquiler_3, string alquiler_4, string alquiler_Hotel)
    {
        this.textureName = texture;
        this.cardName = nombreCalle;
        this.cardValue = precioCompra;
        listAlquileres = new List<int>();
        listAlquileres.Add(Int32.Parse(alquiler_1));
        listAlquileres.Add(Int32.Parse(alquiler_2));
        listAlquileres.Add(Int32.Parse(alquiler_3));
        listAlquileres.Add(Int32.Parse(alquiler_4));

        var aux = Resources.Load<Texture>("Card/" + texture);
        //Texto visual
        this.tmpCardNameText.GetComponent<TextMeshProUGUI>().SetText(cardName);
        this.tmpValueNameText.GetComponent<TextMeshProUGUI>().SetText(cardValue);
        this.displayOwner.GetComponent<MeshRenderer>().material.color = Color.white;

        try
        {
            this.transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_MainTex", aux);
        }
        catch (Exception e)
        {
            Debug.Log("Error al cargar textura en carta id:" + cardID.ToString() + ",e:" + e);
            this.GetComponent<Renderer>().material.SetTexture("_MainTex", aux);
        }
    }

    public override int getAlquiler()
    {
        int numCartas = this.owner.getPropertiesOwned()[this.textureName].Count;
        if (numCartas == 0)
            return 0;
        return this.listAlquileres[this.owner.getPropertiesOwned()[this.textureName].Count-1];
    }
}

