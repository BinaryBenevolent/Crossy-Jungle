using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] Grass grassPrefab;
    [SerializeField] Road roadPrefab;

    [SerializeField] int initialGrassCount = 5;
    [SerializeField] int horizontalSize;

    [SerializeField] int backRelativePos = -4;

    [SerializeField, Range(0,1)] float treeProbability;

    private void Start()
    {
        for (int zPos = backRelativePos; zPos < initialGrassCount; zPos++)
        {
            var grass = Instantiate(grassPrefab);

            grass.transform.position = new Vector3(0, 0, zPos);

            float treeProbability;
            if(zPos < -3)
            {
                treeProbability = 1;
            }
            else if(zPos < -2)
            {
                treeProbability = 0.5f;
            }
            else if( zPos < -1)
            {
                treeProbability = 0.3f;
            }
            else
            {
                treeProbability = 0;
            }


            grass.SetTreePercentage(treeProbability);

            grass.Generate(horizontalSize);
        }
    }
}
