using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovementInterface : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textoDinero;

    private void Start()
    {

    }

    public void activarUI(GameObject player)
    {
        textoDinero.SetText("Dinero:" + player.GetComponent<MoneyController>().getMoney());
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
