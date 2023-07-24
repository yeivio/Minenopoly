using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] private GameObject buyUI;
    [SerializeField] private GameObject movementUI;
    [SerializeField] private GameObject houseBuildUI;
    List<GameObject> UIList = new List<GameObject>();

    private void Awake()
    {
        PlayerController.onFinishedMovement += onPlayerEndMovement;
    }

    public void activarUICompra(PropertyCard carta)
    {
        UIList.Add(Instantiate(buyUI, new Vector2(), Quaternion.identity, transform)); // Instanciar objeto carta
    }

    public void activarUIMovimiento(PlayerController player) 
    {
        UIList.Add(Instantiate(movementUI, new Vector2(), Quaternion.identity, transform)); 
    }

    public void activarUICompraCasa(PlayerController player)
    {
        UIList.Add(Instantiate(houseBuildUI, new Vector2(), Quaternion.identity, transform)); 
    }

    public void desactivarTodaUI()
    {
        foreach (GameObject obj in UIList)
            Destroy(obj);
    }


    public void onPlayerEndMovement(PlayerController player)
    {
        GenericCard card = player.getPosicionEnCarta();


        if (card is PropertyCard && !(card as PropertyCard).hasOwner())
        {
            this.activarUICompra((PropertyCard)card);
        }
        else
        {
            this.activarUIMovimiento(player);
        }


    }
}
