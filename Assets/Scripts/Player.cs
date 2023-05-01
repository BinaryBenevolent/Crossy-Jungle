using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField, Range(0,1)] float moveDuration = 0.1f;
    [SerializeField, Range(0, 1)] float jumpHeight = 0.3f;
    bool isMoving;
    private void Update()
    {
        if (DOTween.IsTweening(transform))
            return;

        Vector3 direction = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction += Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction += Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction += Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction += Vector3.left;
        }

        if(direction == Vector3.zero)
        {
            return;
        }

        Move(direction);
    }

    private void Move(Vector3 direction)
    {
        transform.DOJump(transform.position + direction, jumpHeight, 1, moveDuration);

        transform.forward = direction;
    }
}
