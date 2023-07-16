using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyInterface : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textoOwner;
    [SerializeField] private TextMeshProUGUI textoPrecio;
    [SerializeField] private TextMeshProUGUI textoDineroJugador;

    
    public void activarUI(PropertyCard carta)
    {
        string textoCarta;
        if (carta.getOwner() == null)
            textoCarta = "Nadie";
        else
            textoCarta = carta.getOwner().getId().ToString();
        
        this.textoOwner.text = "Owner: "+textoCarta;
        this.gameObject.SetActive(true);
    }

    public void desactivarUI()
    {
        this.gameObject.SetActive(false);
    }
}
