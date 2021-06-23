using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float camSpeed;

    Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        GameLogic.OnEnd.AddListener(HandleGameEnd);
    }

    // Update is called once per frame
    void Update()
    {
        // A simple movement
        // Can we get this to accelerate? :)
        if(GameLogic.isPlaying) transform.Translate(camSpeed, 0, 0);
    }

    void HandleGameEnd()
    {
        transform.position = startPosition;
    }
}
