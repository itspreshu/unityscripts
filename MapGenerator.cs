/*
    Constant Random Generation of terrain by looking at the "Resources/Prefabs/Platforms/" path. The terrain must be a prefab. The player(s) has to have an empty object with collider and tag "GenerationPoint" to activate the script.

    Author: Luis Ponce (Preshu).

    Performance FIX: FixedUpdate now is destroying prefabs if there are more than 3 "Platform" tagged GameObjects.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    private GameObject[] platforms;
    private Object[] platformsToInstantiate;
    //Max platforms rendering. Default is 3
    public int MaxPlatforms = 3;

    private void Start()
    {
        //This will run once.
        platformsToInstantiate = Resources.LoadAll("Prefabs/Platforms/");
        Debug.Log(platformsToInstantiate);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "GenerationPoint")
        {
            int platformIndex = Random.Range(0, platformsToInstantiate.Length-1);
            Debug.Log("Next Platform is " + platformIndex.ToString());
            Instantiate(platformsToInstantiate[platformIndex], new Vector3(collision.transform.position.x + 5, collision.transform.position.y, collision.transform.position.z), Quaternion.identity);
        }
    }
    private void FixedUpdate()
    {
        platforms = GameObject.FindGameObjectsWithTag("Platform");
        if(platforms.Length >= MaxPlatforms)
        {
            Destroy(platforms[0]);
        }
    }
}
