using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private const int ThrustForce = 3;

    private bool preFrameThrustInput = false;

    private Rigidbody2D _shipRigidbody2D;
    private Vector2 _thrustDirection;
    private CircleCollider2D _shipCollider2D;

    private const int rotateDegreesPerSecond = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        _shipRigidbody2D = GetComponent<Rigidbody2D>();
        _thrustDirection = new Vector2(x:1,y:0);
        _shipCollider2D = GetComponent<CircleCollider2D>();
    }

    /// <summary>
    /// Frame-rate independent MonoBehaviour.FixedUpdate message for physics calculations.
    /// </summary>
    void FixedUpdate()
    {
        if (Input.GetAxis("Thrust") > 0)
        {
            if (!preFrameThrustInput)
            {
                preFrameThrustInput = true;
                _shipRigidbody2D.AddForce(ThrustForce * _thrustDirection,ForceMode2D.Impulse);
            }
        }
        else
        {
            preFrameThrustInput = false;
        }
    }
    
    /// <summary>
    /// OnBecameInvisible is called when the renderer is no longer visible by any camera.
    /// </summary>
    private void OnBecameInvisible()
    {
        Vector2 vector2 = transform.position;
        if (vector2.x > ScreenUtils.ScreenLeft || vector2.x < ScreenUtils.ScreenRight)
        {
            vector2.x = -vector2.x;
        }
        if (vector2.y > ScreenUtils.ScreenTop || vector2.y < ScreenUtils.ScreenBottom)
        {
            vector2.y = -vector2.y;
            //I don't know why need change x ,but it work.
            //If I don't change x ,ship will position wrong side.
            vector2.x = -vector2.x;
        }

        transform.position = vector2;
    }

    // Update is called once per frame
    void Update()
    {
        float rotationInput = Input.GetAxis("Rotate");
        if (rotationInput != 0f)
        {
            // calculate rotation amount and apply rotation
            float rotationAmount = rotateDegreesPerSecond * Time.deltaTime;
            if (rotationInput < 0) {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);
            
            //calculate new force direction
            float rad = Mathf.Deg2Rad * transform.eulerAngles.z;
            _thrustDirection = new Vector2(-Mathf.Sin(rad),Mathf.Cos(rad));
            
            //remove old force and add new direction force
            if (_shipRigidbody2D.velocity != Vector2.zero)
            {
                _shipRigidbody2D.velocity = Vector2.zero;
                _shipRigidbody2D.AddForce(ThrustForce * _thrustDirection,ForceMode2D.Impulse);
            }
        }
    }
    
}
