using TMPro;
using UnityEngine;
using System;

public class MovementInterface : MonoBehaviour
{
    private TableManager tableManager;
    private MoneyController activePlayer;
    [SerializeField] private GameObject botonComprarCasa;
    [SerializeField] private GameObject botonMover;
    [SerializeField] private GameObject botonTrade;
    [SerializeField] private TextMeshProUGUI textoDinero;
    public int ownerID;
    public static event Action onCreation;

    private void Awake()
    {
        Debug.Log("aparesco");
        tableManager = FindObjectOfType<TableManager>();

        if (tableManager.getActivePlayer().GetComponent<PlayerController>().hasAllHouses()) //Boton comprar casa
            this.botonComprarCasa.SetActive(true);
        else
            this.botonComprarCasa.SetActive(false);

        this.activePlayer = tableManager.getActivePlayer().GetComponent<MoneyController>();
        ownerID = tableManager.getActivePlayer().getId();

        if (FindObjectOfType<UIManager>().hasAlreadyMoved()) //Ya ha movido
            botonMover.SetActive(false);

        if (tableManager.getActivePlayer().GetComponent<PlayerController>().getPropertiesOwned().Count > 0)
            botonTrade.SetActive(false);
        onCreation?.Invoke();
    }

    public void OnStartMovementButton()
    {
        tableManager.empezarTurno();
        Destroy(this.gameObject);
    }

    public void OnNextPlayerButton()
    {
        tableManager.siguienteTurno();
        Debug.Log("Ahora me elimino");
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
