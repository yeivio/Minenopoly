using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject InterfazCompra;
    [SerializeField] private GameObject InterfazMovimiento;

    private void Start()
    {
        
    }

    public void activarUICompra(PropertyCard carta)
    {
        this.InterfazCompra.GetComponent<BuyInterface>().activarUI(carta);
    }


    public void desactivarUICompra()
    {
        this.InterfazCompra.GetComponent<BuyInterface>().desactivarUI();
    }


    public void activarUIMovimiento(PlayerController player) 
    {
        if (player == null)
            this.InterfazMovimiento.GetComponent<MovementInterface>().activarUI();
        else
            this.InterfazMovimiento.GetComponent<MovementInterface>().activarUI(player.gameObject);
    }

    public void desactivarUIMovimiento()
    {
        this.InterfazMovimiento.GetComponent<MovementInterface>().desactivarUI();
    }
}
