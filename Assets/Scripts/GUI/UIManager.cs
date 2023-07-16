using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject InterfazCompra;
    [SerializeField] private GameObject InterfazMovimiento;
    [SerializeField] private GameObject InterfazCompraCasa;
    List<GameObject> UIList = new List<GameObject>();

    private void Start()
    {
        UIList.Add(InterfazCompra);
        UIList.Add(InterfazMovimiento);
        UIList.Add(InterfazCompraCasa);

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

    public void activarUICompraCasa(PlayerController player)
    {
        this.InterfazCompraCasa.GetComponent<HouseBuildInterface>().activarUI(player);
    }
    public void desactivarUICompraCasa()
    {
        this.InterfazCompraCasa.GetComponent<HouseBuildInterface>().desactivarUI();
    }

    public void desactivarTodaUI()
    {
        desactivarUICompra();
        desactivarUICompraCasa();
        desactivarUIMovimiento();
    }

}
