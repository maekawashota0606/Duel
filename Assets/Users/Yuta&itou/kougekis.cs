using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kougekis : MonoBehaviour
{
    [SerializeField]
    private GameObject Pl;
    public int _playerHp = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Attack") && other.gameObject.name == "Attck_Test_P2(Clone)")
        {
            if (_playerHp > 0)
            {
                _playerHp -= 1;
            }
            if (_playerHp <= 0)
            {
                Destroy(Pl);
            }
        }
    }
}
