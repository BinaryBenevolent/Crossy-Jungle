using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Elephant : MonoBehaviour
{
    [SerializeField, Range(0,5)] float speed = 3;

    Vector3 initialPosition;

    float distanceLimit = float.MaxValue;

    public void SetUpDistanceLimit(float distance)
    {
        this.distanceLimit = distance;
    }

    private void Start()
    {
        initialPosition = this.transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(Vector3.Distance(initialPosition, this.transform.position) > distanceLimit)
        {
            Destroy(this.gameObject);
        }
    }
}
