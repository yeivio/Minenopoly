using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CardManager cardManager; 
    private GenericCard actualCard; 
    private MoneyController moneyController; 
    [SerializeField] private int playerId; //id del jugador
    private static int duration = 1; //Velocidad de movimiento
    private Dictionary<String, List<PropertyCard>> listCardBought; //Cartas compradas
    public int CustomNumMov = 0; // Custom input for custom number of player movements
    [SerializeField] private GameObject gameModel; //Modelo del jugador
    private ColorManager colorManager;


    private bool isJailed; //Player is jailed status


    [SerializeField] private Color colorPlayer;
    

    public static event Action<PlayerController> onFinishedMovement; //Evento para cuando acabe los movimientos
    public static event Action<PlayerController> onStartedMovement; //Evento para cuando empiece los movimientos
    public static event Action onRotatedMovement; //Evento para cuando rote el jugador

    public static event Action onJailCard; //Evento para cuando acabe los movimientos

    void Awake()
    {
        cardManager = FindObjectOfType<CardManager>(); //Obtener la instancia del CardManager
        this.moneyController = this.GetComponent<MoneyController>();

        this.transform.position = cardManager.getFirstCard().getActiveLocation(); //Posicionar jugador en primera carta
        this.actualCard = cardManager.getFirstCard(); //Establecer variable posicion en primera carta
        this.listCardBought = new Dictionary<String, List<PropertyCard>>();
        this.isJailed = false;
        colorManager = new ColorManager();
        this.colorPlayer = colorManager.getRandomColor();
        gameModel.GetComponent<Renderer>().material.color = this.colorPlayer;
    }

    public void mover(int numMovements) {
        StartCoroutine(iMovimiento(numMovements));
    }

    private IEnumerator iMovimiento(int numMovements) {
        float timeElapsed; //Variable dedicada para Lerp
        Vector3 startPosition; //Posicion desde la posición inicial
        Vector3 targetPosition; // Siguiente carta a la que debe ir
        int startNumber = actualCard.getId();

        if (CustomNumMov > 0) //@TODO Importante cambiar
            numMovements = CustomNumMov;

        if (actualCard.getId() == 0)
            actualCard.cardAction(this.gameObject);

        onStartedMovement?.Invoke(this);

        while (numMovements != 0) {
            timeElapsed = 0;
            startPosition = this.transform.position; //Obtener el lugar de dónde debe empezar
            targetPosition = cardManager.getNextCard(actualCard).getActiveLocation(); //Lugar dónde debe acabar(Siguiente carta)
            while (timeElapsed < duration) {
                transform.position = Vector3.Lerp(startPosition,
                                                    targetPosition,
                                                    timeElapsed / duration);
                timeElapsed += Time.deltaTime;

                yield return null; //Null es siguiente frame, se cambia por la condicion que se tiene que dar
            }
            numMovements--; //Restar un movimiento
            actualCard = cardManager.getNextCard(actualCard); //Actualizar la carta en la que está 
            if ((actualCard.getId() % 10) == 0)
                onRotatedMovement?.Invoke();
        }
        if (this.actualCard is JailCard && (this.actualCard as JailCard).isGoToJailCard())
            onJailCard?.Invoke();
        actualCard.cardAction(this.gameObject);
        onFinishedMovement?.Invoke(this);
        
    }
    public Color getColor()
    {
        return this.colorPlayer;
    }
    public void setId(int id)
    {
        this.playerId = id;
    }
    public int getId() {
        return playerId;
    }

    /// <summary>
    /// Devuelve la carta en la que se encuentra el jugador actualmente
    /// </summary>
    /// <returns>Objeto GenericCard con el objeto en el que se encuentra el jugador</returns>
    public GenericCard getPosicionEnCarta()
    {
        return this.actualCard;
    }

    public void setPosicionActual(GenericCard actualCard)
    {
        this.actualCard = actualCard;
    }

    public bool comprarCarta()
    {
        if (this.getPosicionEnCarta() is not PropertyCard
                || this.moneyController.getMoney() < ((PropertyCard)this.getPosicionEnCarta()).getCardValue()
                || isCardAlreadyBought((PropertyCard) this.getPosicionEnCarta()))
            return false;
        if (!((PropertyCard)this.getPosicionEnCarta()).comprar(this)) 
            return false; //Can't buy the card
        PropertyCard card = (PropertyCard) this.getPosicionEnCarta();
        try
        {
            this.listCardBought[card.getTextureName()].Add((PropertyCard)this.getPosicionEnCarta());
        }
        catch (KeyNotFoundException)
        {
            this.listCardBought.Add(card.getTextureName(), new List<PropertyCard> { (PropertyCard)this.getPosicionEnCarta() });
        }
        finally
        {
            this.moneyController.removeMoney(card.getCardValue());
        }
        return true;
    }

    private bool isCardAlreadyBought(PropertyCard card)
    {
            return card.hasOwner()
                &&( this.listCardBought[card.getTextureName()].Count > 0
                || this.listCardBought[card.getTextureName()].Exists(c => c.getId() == card.getId()));
    }

    public void pasarASecundario()
    {
        this.transform.position = this.actualCard.obtenerLugarLibre();
    }

    public void pasarActivo()
    {
        this.actualCard.liberarLugar(this.transform.position);
        this.transform.position = this.actualCard.getActiveLocation();
    }

    public bool hasAllHouses()
    {
        bool found = false;
        foreach (KeyValuePair<string, List<PropertyCard>> entry in listCardBought)
            if (!found) {
                found = entry.Value.Count == this.cardManager.getHouseNumber(entry.Key);
            }
        return found;
    }

    public IDictionary<String, List<PropertyCard>> getPropertiesOwned()
    {
        return new Dictionary<String, List<PropertyCard>>(this.listCardBought);
    }

    public bool getJailed()
    {
        return this.isJailed;
    }
    public void setJailed(bool state)
    {
        this.isJailed = state;
    }

}
