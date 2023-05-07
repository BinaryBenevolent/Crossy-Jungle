using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerraSpawner : MonoBehaviour
{
    [SerializeField] private Terra terra;
    [SerializeField] private Player player;

    [SerializeField] private float initialTimer = 10f;

    private float timer;

    private void Start()
    {
        timer = initialTimer;
        terra.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (timer <= 0 && terra.gameObject.activeInHierarchy == false)
        {
            terra.gameObject.SetActive(true);
            terra.transform.position = player.transform.position + new Vector3(0, 0, 20);
            player.SetNotMoveable(true);
        }
        timer -= Time.deltaTime;
    }
    public void ResetTimer()
    {
        timer = initialTimer;
    }
}
