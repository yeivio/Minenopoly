using UnityEngine;
using System.Collections.Generic;
public class HouseBuildInterface : MonoBehaviour
{
    private List<GameObject> listObjects = new List<GameObject>(); //List spawned objects
    public GameObject prefab;
    private PlayerController activePlayer; //Jugador que activa la UI

    private float oldCanvasWidth, oldCanvasHeight;

    public void activarUI(PlayerController player)
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

            GameObject card = Instantiate(prefab, spawnPosition, Quaternion.identity, transform); // Instanciar objeto carta
            card.GetComponent<UICardController>().setCard(obj);
            listObjects.Add(card);
            contador++;
        }
        
        
    }

    public void desactivarUI()
    {
        foreach (GameObject obj in listObjects)
            Destroy(obj);
        this.gameObject.SetActive(false);
    }

}
