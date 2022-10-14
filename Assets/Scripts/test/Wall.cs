using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private Vector3 normal = Vector3.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spiner"))
        {
            //Debug.Log("Hit");
            collision.gameObject.GetComponent<Spiner>().Reflect(normal);
        }
    }
}
