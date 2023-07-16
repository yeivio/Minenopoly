using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovementInterface : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textoDinero;
    [SerializeField] private GameObject botonComprarCasa;
    private MoneyController activePlayer;

    private void Update()
    {
        if(activePlayer != null)
            textoDinero.SetText("Dinero:" + activePlayer.getMoney());
    }
    public void activarUI(GameObject player)
    {
        if (player.GetComponent<PlayerController>().hasAllHouses())
            this.botonComprarCasa.SetActive(true);
        
        else
            this.botonComprarCasa.SetActive(false);

        //textoDinero.SetText("Dinero:" + player.GetComponent<MoneyController>().getMoney());
        this.activePlayer = player.GetComponent<MoneyController>();
        this.gameObject.SetActive(true);
    }

    public void activarUI()
    {
        this.gameObject.SetActive(true);
    }

    public void desactivarUI()
    {
        this.gameObject.SetActive(false);
    }
}
