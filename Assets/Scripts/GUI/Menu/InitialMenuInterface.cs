using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InitialMenuInterface : MonoBehaviour
{
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject optionButton;
    [SerializeField] private GameObject playButton;

    private static string TABLERO_INICIAL= "Tablero";

    public void playButtonPressed()
    {
        this.exitButton.SetActive(false);
        this.optionButton.SetActive(false);
        this.playButton.SetActive(false);
        StartCoroutine(LoadAsynchronously());
        return;
    }

    public void optionButtonPressed()
    {
        return;
    }

    public void exitButtonPressed()
    {
        Application.Quit();
    }

    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(TABLERO_INICIAL);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            yield return null;
        }
    }

}
