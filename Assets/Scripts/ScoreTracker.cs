using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    public int scoreMulti;
    public Transform player;
    public Text scoreText;

    int _score;

    public int Score
    {
        get
        {
            return _score;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameLogic.OnEnd.AddListener(HandleEndGame);
        GameLogic.OnStart.AddListener(HandleStartGame);
    }

    private void HandleStartGame()
    {
        _score = 0;
    }

    private void HandleEndGame()
    {
        _score = (int)Mathf.Ceil(player.position.x) * scoreMulti;
        scoreText.text = _score.ToString();
    }
}
