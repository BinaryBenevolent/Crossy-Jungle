using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private List<Terrain> terrainList;  

    [SerializeField] private Grass grassPrefab;
    [SerializeField] private Road roadPrefab;

    [SerializeField] private int initialGrassCount = 5;
    [SerializeField] private int horizontalSize;

    [SerializeField] private int backViewDistance = -4;
    [SerializeField] private int forwardViewDistance = 15; 

    [SerializeField, Range(0,1)] private float treeProbability;

    private Dictionary<int, Terrain> activeTerrainDict = new Dictionary<int, Terrain>(20);

    [SerializeField] private int travelDistance;

    private void Start()
    {
        // Create Initial Grass
        for (int zPos = backViewDistance; zPos < initialGrassCount; zPos++)
        {
            var terrain = Instantiate(terrainList[0]);

            terrain.transform.position = new Vector3(0, 0, zPos);

            if(terrain is Grass grass)
            {
                float treeProbability;
                if (zPos < -3)
                {
                    treeProbability = 1;
                }
                else if (zPos < -2)
                {
                    treeProbability = 0.5f;
                }
                else if (zPos < -1)
                {
                    treeProbability = 0.3f;
                }
                else
                {
                    treeProbability = 0;
                }

                grass.SetTreePercentage(treeProbability);
            }
            terrain.Generate(horizontalSize);

            activeTerrainDict[zPos] = terrain;
        }

        for (int zPos = initialGrassCount; zPos < forwardViewDistance; zPos++)
        {
            var terrain = SpawnRandomTerrain(zPos);

            terrain.Generate(horizontalSize);

            activeTerrainDict[zPos] = terrain;
        }

        SpawnRandomTerrain(0);
    }

    private Terrain SpawnRandomTerrain(int zPos)
    {
        Terrain terrainCheck = null;

        int randomIndex;
        Terrain terrain = null;
    
        for(int z = -1; z >= -3; z--)
        {
            var checkPos = zPos + z;

            if(terrainCheck == null)
            {
                terrainCheck = activeTerrainDict[checkPos];
                continue;
            }
            else if (terrainCheck.GetType() != activeTerrainDict[checkPos].GetType())
            {
                randomIndex = Random.Range(0, terrainList.Count);
                terrain = Instantiate(terrainList[randomIndex]);
                terrain.transform.position = new Vector3(0, 0, zPos);

                return terrain;
            }
            else
            {
                continue;
            }
        }

        var candidateTerrain = new List<Terrain>(terrainList);

        for (int i = 0; i < candidateTerrain.Count; i++)
        {
            if(terrainCheck.GetType() == candidateTerrain[i].GetType())
            {
                candidateTerrain.Remove(candidateTerrain[i]);
                break;
            }
        }

        randomIndex = Random.Range(0, candidateTerrain.Count);
        terrain = Instantiate(candidateTerrain[randomIndex]);
        terrain.transform.position = new Vector3(0, 0, zPos);

        return terrain;
    }

    private void Update()
    {
        
    }

    public void UpdateTravelDistance(Vector3 targetPosition)
    {
        if (targetPosition.z > travelDistance)
        {
            travelDistance = Mathf.CeilToInt(targetPosition.z);
        }
    }
}