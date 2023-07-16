using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{    
    public PlayerController playerTarget = null;
    public Vector3 default_position;

    void LateUpdate(){
        if(playerTarget != null)
            this.gameObject.transform.position = playerTarget.getCameraPlacement();
    }

    public void setDefaultPosition()
    {
        this.gameObject.transform.position = default_position;
    }

    public void setNewActivePlayer(PlayerController player){
        playerTarget = player;
        this.gameObject.transform.position = player.getCameraPlacement();
    }


}
