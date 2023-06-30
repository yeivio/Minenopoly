using System;
using TMPro;
using UnityEngine;

public class TaxesCard : GenericCard
{
    [SerializeField] private int value;
    [SerializeField] protected GameObject tmpValueNameText;
    
    public override void cardAction(GameObject jugador)
    {
        jugador.GetComponent<MoneyController>().removeMoney(value);
    }

    public override void setConfigCSV(string texture, string nombreCalle, string precioCompra, 
        string precioDeCasa, string precioDeHotel, string alquiler_0, string alquiler_1, string alquiler_2,
        string alquiler_3, string alquiler_4, string alquiler_Hotel)
    {
        value = Int16.Parse(precioCompra);
        var aux = Resources.Load<Texture>("Card/" + texture);
        this.tmpValueNameText.GetComponent<TextMeshProUGUI>().SetText(precioCompra);
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
}
