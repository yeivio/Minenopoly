using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UICardController : MonoBehaviour
{

    private PropertyCard card;
    public GameObject cardNameText;

    [SerializeField] private GameObject house_button_1;
    [SerializeField] private GameObject house_button_2;
    [SerializeField] private GameObject house_button_3;
    [SerializeField] private GameObject house_button_4;
    [SerializeField] private GameObject hotel_button;
    [SerializeField] private GameObject clear_button;

    private GameObject[] listButtons;

    private int lastSelection;
    private int currentSelection;

    public static event Action<int, UICardController> onValueChange;

    private void Start()
    {
        listButtons = new GameObject[5]; // 5 is 4 houses + 1 hotel
        listButtons[0] = house_button_1;
        listButtons[1] = house_button_2;
        listButtons[2] = house_button_3;
        listButtons[3] = house_button_4;
        listButtons[4] = hotel_button;
        this.clear_button.SetActive(false);
    }

    public void setCard(PropertyCard card)
    {
        this.card = card;
        var aux = Resources.Load<Sprite>("Card/" + card.getTextureName());
        this.GetComponent<Image>().sprite = aux;

        this.cardNameText.GetComponent<TextMeshProUGUI>().SetText(card.getCardName());

    }

    public PropertyCard getCard(){  return this.card;   }
    public int getNumHouses(){
        if (currentSelection == 5)
            return 0;
        return currentSelection;
    }
    public int getNumHotel(){
        if (currentSelection == 5)
            return 1;
        return 0;
    }

    public void selectedHouse(int numberHouse)
    {
        this.lastSelection = this.currentSelection;
        this.currentSelection = numberHouse;
        if(this.lastSelection != currentSelection)
        {
            onValueChange?.Invoke(getTotalPrice(), this);
            this.clear_button.SetActive(true);
        }

    }

    public void clearSelection()
    {
        this.clear_button.SetActive(false);
        this.lastSelection = 0;
        this.currentSelection = 0;
    }


    public int getTotalPrice()
    {
        if (this.getNumHotel() > 0)
            return this.card.getHousePrice() * 4 + this.card.getHotelPrice();
        return this.card.getHousePrice() * this.getNumHouses();
    }
}
