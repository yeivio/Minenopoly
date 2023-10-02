using System;
using UnityEngine;

public class BuyInterface : MonoBehaviour
{
    private TableManager tableManager;
    public int ownerID;
    public static event Action onCreation;

    private void Awake()
    {
        tableManager = FindObjectOfType<TableManager>();
        ownerID = tableManager.getActivePlayer().getId();
        onCreation?.Invoke();
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
