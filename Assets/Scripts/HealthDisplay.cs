using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    Text healthDisplay;
    // Start is called before the first frame update
    void Start()
    {
        // Crazy stuff. . . just for the fun of it
        GameObject.FindGameObjectWithTag("Player").
            GetComponent<Player>().OnHealthUpdate.AddListener(UpdateText);

        healthDisplay = GetComponent<Text>();
    }

    private void UpdateText(int newHealth)
    {
        healthDisplay.text = "Health: " + newHealth.ToString();
    }
}
