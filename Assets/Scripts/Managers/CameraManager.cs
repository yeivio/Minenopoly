using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour
{
    public PlayerController playerTarget = null;

    private static float DISTANCE_TO_PLAYER = 8.431273f; //Default distance from the player
    private int numside; //Side of the table the player is located
    private static Vector3 DEFAULT_POSITION = new Vector3(16.11f, 5.16f, 4.153445f); //Default starting position of the camera
    Dictionary<int, int> playersSettings= new Dictionary<int, int>(); //Saves the side of the table the player is located when switching

    private void Start()
    {
        numside = 0;
        PlayerController.onStartedMovement += startMovement;
        PlayerController.onFinishedMovement += stopMovement;
        PlayerController.onRotatedMovement += startRotateCamera;

    }

    private void startMovement(PlayerController player)
    {        
        StartCoroutine(startMovementCoroutine());
    }

    private void stopMovement(PlayerController player)
    {
        StopAllCoroutines();
        playersSettings[player.getId()] = numside;
    }

    private void startRotateCamera()
    {
        this.numside++;
        if (numside >= 4)
            numside = 0;
    }

    private IEnumerator startMovementCoroutine()
    {
        while (true) {
            this.transform.LookAt(playerTarget.gameObject.transform);
            this.transform.position = setPositionCamera();
            yield return null;
        }
        
    }

    private IEnumerator startRotationCoroutine()
    {
        
        Vector3 startPosition = this.transform.position;
        Vector3 endPosition = this.transform.position + setPositionCamera();
        float duration = 3f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / duration;
            this.transform.position = Vector3.Lerp(startPosition, endPosition, percentageComplete);
            yield return null;
        }
        StartCoroutine(startMovementCoroutine());
    }

    public void setNewActivePlayer(PlayerController player){
        playerTarget = player;
        if (!playersSettings.ContainsKey(player.getId()))
            playersSettings.Add(player.getId(), 0);
        else
            numside = playersSettings[player.getId()];

        this.transform.position = setPositionCamera();
    }

    private Vector3 setPositionCamera()
    {
        Vector3 newPosition = this.playerTarget.transform.position;
        switch (numside)
        {
            case 0:
                newPosition.x = playerTarget.transform.position.x + DISTANCE_TO_PLAYER;
                newPosition.y = DEFAULT_POSITION.y;
                break;
            case 1:
                newPosition.z = playerTarget.transform.position.z - DISTANCE_TO_PLAYER;
                newPosition.y = DEFAULT_POSITION.y;
                break;
            case 2:
                newPosition.x = playerTarget.transform.position.x - DISTANCE_TO_PLAYER;
                newPosition.y = DEFAULT_POSITION.y;
                break;
            case 3:
                newPosition.z = playerTarget.transform.position.z + DISTANCE_TO_PLAYER;
                newPosition.y = DEFAULT_POSITION.y;
                break;
        }

        return newPosition;
    }

}
