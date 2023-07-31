using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovementInterface : MonoBehaviour
{
    private TableManager tableManager;
    private MoneyController activePlayer;
    [SerializeField] private GameObject botonComprarCasa;
    [SerializeField] private GameObject botonMover;
    [SerializeField] private GameObject botonTrade;
    [SerializeField] private TextMeshProUGUI textoDinero;
    public int ownerID;

    private void Awake()
    {
        
        tableManager = FindObjectOfType<TableManager>();
        if (tableManager.getActivePlayer().GetComponent<PlayerController>().hasAllHouses())
            this.botonComprarCasa.SetActive(true);
        else
            this.botonComprarCasa.SetActive(false);

        this.activePlayer = tableManager.getActivePlayer().GetComponent<MoneyController>();
        ownerID = tableManager.getActivePlayer().getId();

        if (FindObjectOfType<UIManager>().hasAlreadyMoved())
            botonMover.SetActive(false);

        if (tableManager.getActivePlayer().GetComponent<PlayerController>().getPropertiesOwned().Count > 0)
            botonTrade.SetActive(false);

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
