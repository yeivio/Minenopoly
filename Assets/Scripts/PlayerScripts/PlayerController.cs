using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CardManager cardManager; //CardManager
    private GenericCard actualCard; //Carta posición actual
    private MoneyController moneyController; //Controlador del dinero del jugador
    private int playerId; //id del jugador
    private static int duration = 1; //Velocidad de movimiento
    public GameObject cameraLocation;
    private Dictionary<String, List<PropertyCard>> listCardBought = new Dictionary<String, List<PropertyCard>>(); //Cartas compradas
    public int CustomNumMov = 0;

   

    [SerializeField] private Color colorPlayer;
    Color[] listaColores = { Color.red, Color.black , Color.blue, Color.green, Color.grey, Color.magenta };

    public static event Action<PlayerController> onFinishedMovement; //Evento para cuando acabe los movimientos
    void Awake()
    {
        cardManager = FindObjectOfType<CardManager>(); //Obtener la instancia del CardManager
        this.moneyController = this.GetComponent<MoneyController>();

        this.transform.position = cardManager.getFirstCard().getActiveLocation(); //Posicionar jugador en primera carta
        this.actualCard = cardManager.getFirstCard(); //Establecer variable posicion en primera carta


        System.Random random = new System.Random(); //@TODO Numeros aleatorios
        int numeroAleatorio = random.Next(0, 6);
        this.colorPlayer = listaColores[numeroAleatorio];
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
        }          
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

    public Vector3 getCameraPlacement()
    {
        return cameraLocation.transform.position;
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

}
