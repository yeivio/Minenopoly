using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyInterface : MonoBehaviour
{
    private TableManager tableManager;
    public int ownerID;
    private void Awake()
    {
        tableManager = FindObjectOfType<TableManager>();
        ownerID = tableManager.getActivePlayer().getId();
    }

    public void onBuyButton()
    {
        
        tableManager.comprarCarta();
        Destroy(this.gameObject);
    }

    public void OnSkipButton()
    {
        
        tableManager.skipCompra();
        Destroy(this.gameObject);
    }

}
