using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UICardController : MonoBehaviour
{

    private PropertyCard card;
    public GameObject cardNameText;

    [SerializeField] private GameObject houseText;
    [SerializeField] private GameObject hotelText;

    private static string DEFAULT_HOUSE_TEXT = "Casa:";
    private static string DEFAULT_HOTEL_TEXT= "Hotel:";
    private int numHouse;
    private int numHotel;

    public void setCard(PropertyCard card)
    {
        this.card = card;
        var aux = Resources.Load<Sprite>("Card/" + card.getTextureName());
        this.GetComponent<Image>().sprite = aux;

        this.cardNameText.GetComponent<TextMeshProUGUI>().SetText(card.getCardName());

    }

    public PropertyCard getCard(){  return this.card;   }
    public int getNumHouses(){ return this.numHouse;   }
    public int getNumHotel(){  return this.numHotel;   }

    public void increaseNumberHouse()
    {
        this.numHouse++;
        this.houseText.GetComponent<TextMeshProUGUI>().SetText(DEFAULT_HOUSE_TEXT + numHouse.ToString());
    }

    public void decreaseNumberHouse()
    {
        if(this.numHouse > 0)
            this.numHouse--;
        this.houseText.GetComponent<TextMeshProUGUI>().SetText(DEFAULT_HOUSE_TEXT + numHouse.ToString());
    }

    public void increaseNumberHotel()
    {
        this.numHotel++;
        this.hotelText.GetComponent<TextMeshProUGUI>().SetText(DEFAULT_HOTEL_TEXT + numHotel.ToString());
    }

    public void decreaseNumberHotel()
    {
        if (this.numHotel > 0)
            this.numHotel--;
        this.hotelText.GetComponent<TextMeshProUGUI>().SetText(DEFAULT_HOTEL_TEXT + numHotel.ToString());
    }

    public int getTotalPrice()
    {
        return this.card.getHousePrice() * this.numHotel + 
            this.card.getHousePrice() * this.numHouse;
    }
}
