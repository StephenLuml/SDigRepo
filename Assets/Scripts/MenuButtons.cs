using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField]
    private MapToLoadSO mapToLoad;

    public static Action onPlayClinkSound;

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void SetMapString(string mapName)
    {
        mapToLoad.SetMap(mapName);
    }

    public void LogMessage(string message)
    {
        Debug.Log(message);
    }

    public void PlayClinkSound()
    {
        onPlayClinkSound?.Invoke();
    }
}
