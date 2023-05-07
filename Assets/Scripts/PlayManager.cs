using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayManager : MonoBehaviour
{
    [SerializeField] private List<Terrain> terrainList;

    [SerializeField] private List<Coin> coinList;

    [SerializeField] private Grass grassPrefab;
    [SerializeField] private Road roadPrefab;

    [SerializeField] private int initialGrassCount = 5;
    [SerializeField] private int horizontalSize;

    [SerializeField] private int backViewDistance = -4;
    [SerializeField] private int forwardViewDistance = 15; 

    private Dictionary<int, Terrain> activeTerrainDict = new Dictionary<int, Terrain>(20);

    [SerializeField] private int travelDistance;
    [SerializeField] private int coin;

    public UnityEvent<int,int> OnUpdateTerrainLimit;

    public UnityEvent<int> OnScoreUpdate;

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
            SpawnRandomTerrain(zPos);
        }

        OnUpdateTerrainLimit.Invoke(horizontalSize, travelDistance + backViewDistance);
    }

    private Terrain SpawnRandomTerrain(int zPos)
    {
        Terrain comparatorTerrain = null;

        int randomIndex;
    
        for(int z = -1; z >= -3; z--)
        {
            var checkPos = zPos + z;

            if(comparatorTerrain == null)
            {
                comparatorTerrain = activeTerrainDict[checkPos];
                continue;
            }
            else if (comparatorTerrain.GetType() != activeTerrainDict[checkPos].GetType())
            {
                randomIndex = Random.Range(0, terrainList.Count);

                return SpawnTerrain(terrainList[randomIndex], zPos);
            }
            else
            {
                continue;
            }
        }

        var candidateTerrain = new List<Terrain>(terrainList);

        for (int i = 0; i < candidateTerrain.Count; i++)
        {
            if (comparatorTerrain.GetType() == candidateTerrain[i].GetType())
            {
                candidateTerrain.Remove(candidateTerrain[i]);
                break;
            }
        }

        randomIndex = Random.Range(0, candidateTerrain.Count);

        return SpawnTerrain(candidateTerrain[randomIndex], zPos);        
    }

    public Terrain SpawnTerrain(Terrain terrain, int zPos)
    {
        terrain = Instantiate(terrain);
        terrain.transform.position = new Vector3(0, 0, zPos);
        terrain.Generate(horizontalSize);
        activeTerrainDict[zPos] = terrain;
        SpawnCoin(horizontalSize, zPos);

        return terrain;
    }

    public Coin SpawnCoin(int horizontalSize,int zPos, float probability = 0.2f)
    {
        if(probability == 0)
            return null;

        List <Vector3> spawnPosCandidateList = new List<Vector3>();

        for(int x = -horizontalSize/2; x <= horizontalSize/2; x++)
        {
            var spawnPos = new Vector3(x, 0, zPos);

            if(Tree.AllPositions.Contains(spawnPos) == false)
                spawnPosCandidateList.Add(spawnPos);
        }

        if(probability >= Random.value)
        {
            var index = Random.Range(0, coinList.Count);
            var xPosIndex = Random.Range(0, spawnPosCandidateList.Count);
            return Instantiate(coinList[index], spawnPosCandidateList[xPosIndex], Quaternion.identity);
        }

        return null;
    }

    public void UpdateTravelDistance(Vector3 targetPosition)
    {
        if (targetPosition.z > travelDistance)
        {
            travelDistance = Mathf.CeilToInt(targetPosition.z);
            UpdateTerrain();

            OnScoreUpdate.Invoke(GetScore());
        }
    }

    public void AddCoin(int value = 1)
    {
        this.coin += value;
    }

    private int GetScore()
    {
        return travelDistance + coin;
    }

    public void UpdateTerrain()
    {
        var destroyPos = travelDistance - 1 + backViewDistance;
        Destroy(activeTerrainDict[destroyPos].gameObject);
        activeTerrainDict.Remove(destroyPos);

        var spawnPos = travelDistance - 1 + forwardViewDistance;

        SpawnRandomTerrain(spawnPos);

        OnUpdateTerrainLimit.Invoke(horizontalSize, travelDistance + backViewDistance);
    }
}