using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CardManager : MonoBehaviour
{
    [SerializeField] private int numCartas; // Numero de cartas m�ximas en el tablero
    private GenericCard[] listCard; //lista de todos los objetos cartas que existen
    private const string ROUTE_CSV = @"C:\Users\logan\Desktop\Monopoly\Assets\Resources\tablero.csv"; //Lista del fichero de configuraci�n para las cartas

    private Dictionary<String, int> cardDictionary = new Dictionary<String, int>();

    void Start(){
        listCard = new GenericCard[numCartas];
        foreach (var p in GameObject.FindGameObjectsWithTag("Card")) { 
            listCard[p.GetComponent<GenericCard>().getId()] = p.GetComponent<GenericCard>();
        }
        loadCSV();
    }

    public GenericCard getFirstCard(){
        return listCard[0].GetComponent<GenericCard>();
    }

    public GenericCard getCard(int place){
        return listCard[place].GetComponent<GenericCard>();
    }    

    public GenericCard getNextCard(GenericCard actualCard){
        if(actualCard.getId() + 1 == listCard.Length)
            return getFirstCard();
        return getCard(actualCard.getId()+1);
    }

    public void loadCSV(){
        //var reader = new StreamReader(File.OpenRead(ROUTE_CSV));
        var reader = new StreamReader(File.OpenRead("Assets/Resources/tablero.csv"));
        while (!reader.EndOfStream)
        {
            
            var line = reader.ReadLine();
            String[] values = line.Split(',');
            int indice = 0;
            string[] clonedValues = (string[])values.Clone();
            foreach (string cadena in clonedValues)
            {
                if(string.IsNullOrEmpty(cadena))
                    values[indice] = new String("");
                indice++;
            }
            var auxcard = this.getCard(Int32.Parse(values[0])) as GenericCard;
            auxcard.setConfigCSV(values[1], values[2],
                values[3], values[4], values[5], values[6],
                values[7], values[8], values[9], values[10], values[11]);
            //TODO Muy poco eficiente guardar todas las texturas, habr�a que hacer solo si es una propertyCard
            this.addToCardDictionary(values[1]);
        }
    } 

    public int addToCardDictionary(String textureName)
    {
        if (String.IsNullOrEmpty(textureName))
            return -1;
        if (cardDictionary.ContainsKey(textureName))
            cardDictionary[textureName] += 1;
        else
            cardDictionary[textureName] = 1;
        return cardDictionary[textureName];
    }

    public int getHouseNumber(string key)
    {
        return this.cardDictionary[key];
    }

    public void buildStructures(PropertyCard propertyCard, int numHouses, int numHotels)
    {
        ((PropertyCard)this.listCard[propertyCard.getId()]).setHouseNumber(numHouses);
        ((PropertyCard)this.listCard[propertyCard.getId()]).setHotelNumber(numHotels);
    }
    
    public JailCard getJailCard()
    {
        foreach (JailCard card in FindObjectsOfType<JailCard>())
            if (card.isGoToJailCard())
                return card;
        return null;
    }
    
}
