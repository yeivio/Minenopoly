using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoneyController : MonoBehaviour
{
    private int playerMoney;

    public int DEFAULT_PLAYER_MONEY;

    public static event Action<PlayerController> onBankrupt;

    void Awake()
    {
        playerMoney = DEFAULT_PLAYER_MONEY;
    }

    public void addMoney(int value){
        this.playerMoney += value;
    }

    public void removeMoney(int value){
        this.playerMoney -= value;
        if (playerMoney < 0)
            onBankrupt?.Invoke(this.gameObject.GetComponent<PlayerController>());
    }

    public int getMoney()
    {
        return this.playerMoney;
    }
    
}
