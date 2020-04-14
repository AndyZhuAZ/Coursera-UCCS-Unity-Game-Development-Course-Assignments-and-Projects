using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float MinImpulseForce = 1f;
        float MaxImpulseForce = 2f;
        float angle = Random.Range(0, 2 * Mathf.PI);
        Vector2 direction = new Vector2(
            Mathf.Cos(angle),Mathf.Sin(angle));
        float magnitude = Random.Range(MinImpulseForce, MaxImpulseForce);
        GetComponent<Rigidbody2D>().AddForce(direction*magnitude,ForceMode2D.Impulse);
    }

    // destroy by collision
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     GameObject.Destroy(gameObject,0.0f);
    // }

    //destroy by camera
    private void OnBecameInvisible()
    {
        // throw new NotImplementedException();
        GameObject.Destroy(gameObject,0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
