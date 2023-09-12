using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public float bulletDamage;
    private void FixedUpdate()
    {
        transform.position += direction.normalized * speed;
    }
}
