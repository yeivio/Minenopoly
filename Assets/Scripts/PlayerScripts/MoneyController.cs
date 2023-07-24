using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    private int playerMoney;

    public int DEFAULT_PLAYER_MONEY;



    void Awake()
    {
        playerMoney = DEFAULT_PLAYER_MONEY;
    }

    public void addMoney(int value){
        this.playerMoney += value;
    }

    public void removeMoney(int value){
        this.playerMoney -= value;
    }

    public int getMoney()
    {
        return this.playerMoney;
    }
    
}
