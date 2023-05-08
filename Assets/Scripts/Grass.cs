using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Grass : Terrain
{
    [SerializeField] private List<GameObject> treePrefabList;
    [SerializeField, Range(0,1)] private float treeProbability;

    public void SetTreePercentage(float newProbability)
    {
        this.treeProbability = Mathf.Clamp01(newProbability);
    }

    public override void Generate(int size)
    {
        base.Generate(size);

        var limit = Mathf.FloorToInt((float)size / 2);
        var treeCount = Mathf.FloorToInt((float)size * treeProbability);

        List<int> emptyPosition = new List<int>();
        
        for (int i = -limit; i <= limit; i++)
        {
            emptyPosition.Add(i);
        }

        for (int i = 0; i < treeCount; i++)
        {
            var randomIndex = Random.Range(0, emptyPosition.Count - 1);
            var pos = emptyPosition[randomIndex];
        
            emptyPosition.RemoveAt(randomIndex);

            SpawnRandomTree(pos);
        }

        for(int i = 1; i < grayedOutTile; i++)
        {
            DarkenObject(SpawnRandomTree(-limit - i));
            DarkenObject(SpawnRandomTree(limit + i));
        }
    }

    private GameObject SpawnRandomTree(int xPos)
    {
        var randomIndex = Random.Range(0, treePrefabList.Count);
        var prefab = treePrefabList[randomIndex];

        var tree = Instantiate(
            prefab,
            new Vector3(xPos, 0, this.transform.position.z),
            Quaternion.identity,
            transform);
        return tree;
    }

    private void DarkenObject(GameObject go)
    {
        var renderers = go.GetComponentInChildren<MeshRenderer>(includeInactive: true);

        renderers.material.color *= new Color32(220,220,220,30);
    }
}
