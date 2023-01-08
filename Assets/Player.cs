using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool pendingAction = false;
    public bool carrying = false;
    public GameObject carry;

    public void Move(Vector3 targetPosition)
    {
        GetComponentInChildren<SpriteRenderer>().flipX = transform.position.x > targetPosition.x;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 6 * Time.deltaTime);
    }

    public void Carry(GameObject target)
    {
        carrying = true;
        carry = target;
    }

    public void Drop()
    {
        carrying = false;
    }
}
