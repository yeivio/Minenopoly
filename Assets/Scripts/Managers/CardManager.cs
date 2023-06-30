using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

public class CardManager : MonoBehaviour
{
    private const int GET_MAX_PLAYER_POSITIONS= 4; //Máximo de casillas en cada carta
    [SerializeField] private int numCartas; // Numero de cartas máximas en el tablero
    private GameObject[] listCard; //lista de todos los objetos cartas que existen
    private const string ROUTE_CSV = @"C:\Users\logan\Desktop\Monopoly\Assets\Resources\tablero.csv"; //Lista del fichero de configuración para las cartas

    public GameObject a;

    void Start(){
        listCard = new GameObject[numCartas];
        foreach (var p in GameObject.FindGameObjectsWithTag("Card")) { 
            listCard[p.GetComponent<GenericCard>().getId()] = p;
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
        }
    } 

    public int getMaxPlayerPosition()
    {
        return GET_MAX_PLAYER_POSITIONS;
    }

}
