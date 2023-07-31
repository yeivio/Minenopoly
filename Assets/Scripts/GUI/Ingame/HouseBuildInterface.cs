using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;
public class HouseBuildInterface : MonoBehaviour
{
    private List<UICardController> listObjects = new List<UICardController>(); //List spawned objects
    public GameObject Instantiateprefab; //Visual card for buying
    public GameObject totalPriceText; //Total Price text
    private PlayerController activePlayer; //Jugador que activa la UI

    private float oldCanvasWidth, oldCanvasHeight;

    private static string DEFAULT_TEXT = "Precio Total:";
    private int total_price;
    public int ownerID;

    private TableManager tableManager;

    public static event Action OnCancelled;

    private void Awake()
    {
        tableManager = FindObjectOfType<TableManager>();
        activePlayer = tableManager.getActivePlayer();
        loadUI(activePlayer);

        ownerID = tableManager.getActivePlayer().getId();

    }


    private void LateUpdate()
    {
        total_price = 0;
        //@TODO   Debería ser con eventos
        foreach (UICardController aux in listObjects)
            total_price += aux.getTotalPrice();
        this.totalPriceText.GetComponent<TextMeshProUGUI>().SetText(DEFAULT_TEXT + total_price);
    }

    private void loadUI(PlayerController player)
    {
        this.gameObject.SetActive(true);

        this.activePlayer = player;
        string texture = ((PropertyCard)player.getPosicionEnCarta()).getTextureName();

        oldCanvasWidth = GetComponent<RectTransform>().rect.width;
        oldCanvasHeight = GetComponent<RectTransform>().rect.height;

        IDictionary<string, List<PropertyCard>> listProperties = activePlayer.getPropertiesOwned(); // Keys are textures

        float position = oldCanvasWidth / (listProperties[texture].Count + 1);
        int contador = 1;
        foreach (PropertyCard obj in listProperties[texture])
        {
            float offset = contador * position;
            Vector3 spawnPosition = new Vector3(offset, oldCanvasHeight / 2, 0f); // Posición de instanciación

            GameObject card = Instantiate(Instantiateprefab, spawnPosition, Quaternion.identity, transform); // Instanciar objeto carta
            card.GetComponent<UICardController>().setCard(obj);
            listObjects.Add(card.GetComponent<UICardController>());
            contador++;
        }
        
        
    }


    public void finishBuildStage()
    {
        if (this.total_price > activePlayer.GetComponent<MoneyController>().getMoney())
        {
            Debug.LogError("Demasiado caro");
            return;
        }
        
        tableManager.buildStructures(listObjects);
        Destroy(this);
    }

    public void cancelBuildStage()
    {
        OnCancelled?.Invoke();
        Destroy(this);
    }
}
