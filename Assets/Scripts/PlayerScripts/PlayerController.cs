using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CardManager cardManager; //CardManager
    [SerializeField] private GenericCard actualCard; //Carta posición actual
    [SerializeField] private MoneyController moneyController; //Controlador del dinero del jugador

    private int playerId; //id del jugador
    private static int duration = 1; //Velocidad de movimiento

    [SerializeField] private CameraManager camaraManager;
    [SerializeField] private UIManager uiManager;


    public GameObject cameraLocation;

    void Awake()
    {
        cardManager = FindObjectOfType<CardManager>(); //Obtener la instancia del CardManager
        camaraManager = FindObjectOfType<CameraManager>(); //Obtener la instancia de la camara
        uiManager = FindObjectOfType<UIManager>();  //Obtener la instancia de la interfaz

        this.transform.position = cardManager.getFirstCard().getActiveLocation(); //Posicionar jugador en primera carta
        this.actualCard = cardManager.getFirstCard(); //Establecer variable posicion en primera carta
        
    }

    public void mover(int numMovements) {

        StartCoroutine(iMovimiento(numMovements));

        if (actualCard is PropertyCard)
            uiManager.activarUICompra((PropertyCard)actualCard);
        else
        {
            if (actualCard.getIsActionOnly()) { actualCard.cardAction(this.gameObject);  }
                uiManager.activarUIMovimiento(this);
        }
        
    }

    private IEnumerator iMovimiento(int numMovements) {
        float timeElapsed; //Variable dedicada para Lerp
        Vector3 startPosition; //Posicion desde la posición inicial
        Vector3 targetPosition; // Siguiente carta a la que debe ir

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
        if (this.getPosicionEnCarta() is PropertyCard)
            return ((PropertyCard)this.getPosicionEnCarta()).comprar(this);
        return false;
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
}
