using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingCard : GenericCard
{
    public override void cardAction(GameObject jugador)
    {
        return;
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
}
