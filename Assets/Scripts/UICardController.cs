using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UICardController : MonoBehaviour
{

    private String texture;
    private PropertyCard card;
    public GameObject cardName;
    private UIManager uiManager;

    public void click()
    {
        uiManager = FindObjectOfType<UIManager>();  //Obtener la instancia de la interfaz
        uiManager.desactivarTodaUI();

    }

    public void setCard(PropertyCard card)
    {
        this.card = card;
        this.setTexture(card.getTextureName());
        this.cardName.GetComponent<TextMeshProUGUI>().SetText(card.getCardName());
    }

    private void setTexture(string texture)
    {
        this.texture = texture;
        var aux = Resources.Load<Sprite>("Card/" + texture);
        this.GetComponent<Image>().sprite = aux;
    }
}
