using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terra : MonoBehaviour
{
    [SerializeField, Range(0, 50)] float speed = 25;

    private void Update()
    {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
