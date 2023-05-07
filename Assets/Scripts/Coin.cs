using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    [SerializeField] int value = 1;

    [SerializeField, Range(0,100)] float rotationSpeed = 1;

    public int Value { get => value; }

    public void Collected()
    {
        this.transform.DOJump(this.transform.position, 1f, 1, 0.3f).onComplete = SelfDestruct;
    }

    private void SelfDestruct()
    {
        Destroy(this.gameObject);
    }

    private void Update()
    {
        transform.Rotate(0, 360 * Time.deltaTime * rotationSpeed, 0);
    }
}
