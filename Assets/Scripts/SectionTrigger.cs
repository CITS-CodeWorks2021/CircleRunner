using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public ObjectPool pooler;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered!");
        if(collision.tag == "SectionTrigger")
        {
            Debug.Log("Triggered!");
            Vector3 newSpawnLoc = new Vector3();
            newSpawnLoc.x = transform.position.x + 20;
            pooler.SpawnRandom(newSpawnLoc);

            if (pooler.NumSpawned > 3) pooler.RemoveOldest();
        }

    }

}
