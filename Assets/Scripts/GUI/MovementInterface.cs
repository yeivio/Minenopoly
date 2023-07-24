using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovementInterface : MonoBehaviour
{
    private TableManager tableManager;
    private MoneyController activePlayer;
    [SerializeField] private GameObject botonComprarCasa;
    [SerializeField] private TextMeshProUGUI textoDinero;

    private void Awake()
    {
        
        tableManager = FindObjectOfType<TableManager>();
        if (tableManager.getActivePlayer().GetComponent<PlayerController>().hasAllHouses())
            this.botonComprarCasa.SetActive(true);
        else
            this.botonComprarCasa.SetActive(false);

        this.activePlayer = tableManager.getActivePlayer().GetComponent<MoneyController>();
    }

    public void OnStartMovementButton()
    {
        tableManager.empezarTurno();
        Destroy(this.gameObject);
    }

    public void OnNextPlayerButton()
    {

        tableManager.siguienteTurno();
        Destroy(this.gameObject);
    }

    public void OnBuyHouseButton()
    {
        tableManager.construirCasa();
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if(activePlayer != null)
            textoDinero.SetText("Dinero:" + activePlayer.getMoney());
    }
}
