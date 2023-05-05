using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;

    public virtual void Generate(int size)
    {
        if (size == 0)
            return;

        if ((float) size % 2 == 0)
            size -= 1;

        int moveLimit = Mathf.FloorToInt((float) size / 2);

        for (int i = -moveLimit; i <= moveLimit; i++)
        {
            SpawnTile(i);
        }

        int grayedOutTile = 1;

        for (int i = 1; i <= grayedOutTile; i++)
        {
            DarkenObject(SpawnTile(-moveLimit - i));
            DarkenObject(SpawnTile(moveLimit + i));
        }
    }

    private GameObject SpawnTile(int xPos)
    {
        var go = Instantiate(tilePrefab, transform);
        go.transform.localPosition = new Vector3(xPos, 0, 0);

        return go;
    }

    private void DarkenObject(GameObject go)
    {
        var renderers = go.GetComponentInChildren<MeshRenderer>(includeInactive : true);

        renderers.material.color *= Color.gray;
    }
}
