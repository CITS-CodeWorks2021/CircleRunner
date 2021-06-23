using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagers : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if this is the player
        // If it is, tell it to take damage
        if (collision.tag == "Player") collision.GetComponent<Player>().TakeDamage();
    }
}
