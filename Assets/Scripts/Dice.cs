using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice
{    
    public int numberGenerated;

    public int generateThrow(){
        return numberGenerated = Random.Range(1, 7);
    }
}
