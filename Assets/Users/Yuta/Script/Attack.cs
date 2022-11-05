using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public static bool Offset_P1 = false;
    public static bool Offset_P2 = false;

    private void Start()
    {
        Offset_P1 = false;
        Offset_P2 = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            Debug.Log("UŒ‚‚ª‘ŠE‚³‚ê‚½");
            Offset_P1 = true;
            Offset_P2 = true;
            Destroy(this.gameObject);
        }
    }
}
