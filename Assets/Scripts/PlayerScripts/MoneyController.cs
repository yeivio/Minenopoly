using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    private int playerMoney;

    public int DEFAULT_PLAYER_MONEY = 12000;



    void Awake()
    {
        playerMoney = DEFAULT_PLAYER_MONEY;
    }

    public void addMoney(int value){
        playerMoney += value;
    }

    public void removeMoney(int value){
        playerMoney -= value;
    }

    public int getMoney()
    {
        return this.playerMoney;
    }
    
}
