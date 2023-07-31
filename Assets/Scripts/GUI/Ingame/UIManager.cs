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

    private bool playerMoved; //Check if the player already moved on this turn

    private GameObject ultimaInterfaz; //Last UI 

    private void Awake()
    {
        playerMoved = false;
        
        HouseBuildInterface.OnCancelled += cancelarUI;
        PlayerController.onStartedMovement += setAlreadyMoved;
        PlayerController.onFinishedMovement += onPlayerEndMovement;
        TableManager.onRoundFinished += resetAlreadyMoved; 
    }

    public void activarUICompra(PropertyCard carta)
    {
        desactivarTodaUI();
        GameObject interfaz = Instantiate(buyUI, new Vector2(), Quaternion.identity, transform);
        UIList.Add(interfaz); // Instanciar objeto carta
        ultimaInterfaz = interfaz;
    }

    public void activarUIMovimiento(PlayerController player) 
    {
        desactivarTodaUI();
        GameObject interfaz = Instantiate(movementUI, new Vector2(), Quaternion.identity, transform);
        UIList.Add(interfaz);
        ultimaInterfaz = interfaz;

    }

    public void activarUICompraCasa(PlayerController player)
    {
        desactivarTodaUI();
        UIList.Add(Instantiate(houseBuildUI, new Vector2(), Quaternion.identity, transform)); 
    }

    public void cancelarUI()
    {
        activarUIMovimiento(null);
    }


    public void desactivarTodaUI()
    {
        UIList.Clear();
    }

    public void onPlayerEndMovement(PlayerController player)
    {
        GenericCard card = player.getPosicionEnCarta();
        if (card is JailCard && player.getJailed())
            return;

        if (card is PropertyCard && !(card as PropertyCard).hasOwner())
        {
            this.activarUICompra((PropertyCard)card);
        }
        else
        {
            this.activarUIMovimiento(player);
        }


    }

    private void setAlreadyMoved(PlayerController player)
    {
        this.playerMoved = true;

    }
    public bool hasAlreadyMoved()
    {
        return this.playerMoved;
    }
    private void resetAlreadyMoved()
    {
        this.playerMoved = false;
    }
}
