using UnityEngine;
public class PlayerModelMenu : MonoBehaviour
{

    [SerializeField] private GameObject gameModel; //Modelo del jugador
    Color[] listaColores = { Color.red, Color.black, Color.blue, Color.green, Color.grey, Color.magenta };


    // Start is called before the first frame update
    void Start()
    {
        System.Random random = new System.Random();
        int numeroAleatorio = random.Next(0, 6);
        gameModel.GetComponent<Renderer>().material.color = listaColores[numeroAleatorio];

    }

}
