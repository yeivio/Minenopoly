using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
    private static int MAX_JOIN_PLAYERS = 2; //Max players to add (2 start + 2 joined)
    private List<GameObject> listPlayers;
    [SerializeField] private GameObject playerObject; // player selection menu
    [SerializeField] private Button removePlayerButton; 
    [SerializeField] private Button addPlayerButton;
    [SerializeField] private GameObject playerCanvas;
    ColorManager colorManager;


    private void Start()
    {
        colorManager = new ColorManager();
        this.listPlayers = new List<GameObject>();
        this.removePlayerButton.enabled = false;
    }

    public void addPlayer()
    {
        this.removePlayerButton.enabled = true;
        Instantiate(this.playerObject).transform.SetParent(playerCanvas.transform); // Child to playerCanvas
        if (MAX_JOIN_PLAYERS == this.listPlayers.Count)
            this.addPlayerButton.enabled = false;
    }

    public void removePlayer()
    {
        this.addPlayerButton.enabled = true;
        this.listPlayers.RemoveAt(this.listPlayers.Count);
        if(listPlayers.Count == 0)
            this.removePlayerButton.enabled = false;
    }

    public void changeColor(GameObject obj)
    {
        Color32 newColor = colorManager.getRandomColor();
        while (obj.GetComponent<Image>().color == newColor && !colorNotUsed(newColor, obj))
            newColor = colorManager.getRandomColor();
        obj.GetComponent<Image>().color = newColor;

    }

    private bool colorNotUsed(Color otherColor, GameObject obj)
    {
        return this.listPlayers.TrueForAll(g => g.GetComponent<Image>().color != otherColor);
    }
}
