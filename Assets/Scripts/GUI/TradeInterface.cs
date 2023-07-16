using UnityEngine;
using System.Collections.Generic;
public class TradeInterface : MonoBehaviour
{
    public int activeNumber;
    private int numberOfObjects = 2; // Número de objetos a instanciar
    private List<GameObject> listObjects = new List<GameObject>();
    public GameObject prefab;

    private float oldCanvasWidth, oldCanvasHeight;

   void Start()
    {
        oldCanvasWidth = GetComponent<RectTransform>().rect.width;
        oldCanvasHeight = GetComponent<RectTransform>().rect.height;
        float position = oldCanvasWidth / (numberOfObjects+1); 

        for (int i = 1; i <= numberOfObjects; i++)
        {
            float offset = i * position;
            Vector3 spawnPosition = new Vector3(offset, oldCanvasHeight/2, 0f); // Posición de instanciación
            GameObject aux = Instantiate(prefab, spawnPosition, Quaternion.identity, transform); // Instanciar objeto
            listObjects.Add(aux);
        }


    }


    private void Update()
    {
        if(activeNumber != numberOfObjects 
            || GetComponent<RectTransform>().rect.width != oldCanvasWidth
            || GetComponent<RectTransform>().rect.height != oldCanvasHeight)
        {
            oldCanvasWidth = GetComponent<RectTransform>().rect.width;
            oldCanvasHeight = GetComponent<RectTransform>().rect.height;
            numberOfObjects = activeNumber;
            foreach (GameObject a in listObjects)
                Destroy(a);
            this.Start();

        }
    }

    public void activarUI(GameObject player)
    {
        this.gameObject.SetActive(true);
    }

    public void activarUI()
    {
        this.gameObject.SetActive(true);
    }

    public void desactivarUI()
    {
        this.gameObject.SetActive(false);
    }

}
