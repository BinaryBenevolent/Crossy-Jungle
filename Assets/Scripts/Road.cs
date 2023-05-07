using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : Terrain
{
    [SerializeField] Elephant elephantPrefab;

    [SerializeField] float minElephantSpawnInterval;
    [SerializeField] float maxElephantSpawnInterval;

    float timer;

    Vector3 elephantSpawnPosition;
    Quaternion elephantRotation;

    private void Start()
    {
        if(Random.value > 0.5f)
        {
            elephantSpawnPosition = new Vector3(horizontalSize / 2 + 3, 0, this.transform.position.z);
            elephantRotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            elephantSpawnPosition = new Vector3(-(horizontalSize / 2 + 3), 0, this.transform.position.z);
            elephantRotation = Quaternion.Euler(0, 90, 0);
        }
    }

    private void Update()
    {
        if(timer < 0)
        {
            timer = Random.Range(minElephantSpawnInterval, maxElephantSpawnInterval);

            var elephant = Instantiate(elephantPrefab, elephantSpawnPosition, elephantRotation);

            elephant.SetUpDistanceLimit(horizontalSize + 6);

            return;
        } 

        timer -= Time.deltaTime;
    }
}
