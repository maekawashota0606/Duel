using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerEnter2D : Spiner" + gameObject.name + "->" + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Attack"))
        {
            Debug.Log("�U�������E���ꂽ");
            Destroy(this.gameObject);
        }
    }
}
