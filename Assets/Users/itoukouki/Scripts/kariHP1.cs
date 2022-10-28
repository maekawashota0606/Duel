using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kariHP1 : MonoBehaviour
{

    [SerializeField]
    private GameObject Player;
    public int _playerHp = 0;
    public bool hntei = false;
    public bool Dm = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Dm)
        {
            if(_playerHp > 0)
            {
                _playerHp -= 1;
                Dm = false;
            }
            if (_playerHp <= 0)
            {
                Destroy(Player);
                Dm = false;
            }
        }
        if (hntei)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Dm = true;
                hntei = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player1")
        {
            hntei = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player1")
        {
            hntei = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player1")
        {
            hntei = false;
        }
    }
}
