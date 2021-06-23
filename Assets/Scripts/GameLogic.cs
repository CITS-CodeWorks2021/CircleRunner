using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameLogic : MonoBehaviour
{
    // Something folks can look at to see if they should do anything
    public static bool isPlaying;

    // Something for folks to listen to
    public static UnityEvent OnStart, OnEnd;

    public GameObject startPanel, gamePanel, endPanel;

    private void Start()
    {
        if (OnStart == null) OnStart = new UnityEvent();
        if (OnEnd == null) OnEnd = new UnityEvent();

        OnEnd.AddListener(EndGame);

        startPanel.SetActive(true);
        gamePanel.SetActive(false);
        endPanel.SetActive(false);
    }

    public void StartGame()
    {
        OnStart.Invoke();
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        endPanel.SetActive(false);
        isPlaying = true;
    }

    void EndGame()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(false);
        endPanel.SetActive(true);
        isPlaying = false;
    }
}
