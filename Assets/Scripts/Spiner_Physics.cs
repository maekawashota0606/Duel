using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiner_Physics : MonoBehaviour
{
    private Rigidbody2D _rb = null;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(new Vector2(0, 30000));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
