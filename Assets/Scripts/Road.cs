using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : Terrain
{
    [SerializeField] private Elephant elephantPrefab;

    [SerializeField] private float minElephantSpawnInterval;
    [SerializeField] private float maxElephantSpawnInterval;

    private float timer;

    private Vector3 elephantSpawnPosition;
    private Quaternion elephantRotation;

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
