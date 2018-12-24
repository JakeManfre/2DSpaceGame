using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    float horizontal;
    float vertical;

    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        // FIXME: Move all input to a seperate input manager script!
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // move right
        if (horizontal > 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0));
        }
        // move left
        if (horizontal < 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-speed, 0));
        }
        // move up
        if (vertical > 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, speed));
        }
        // move down
        if (vertical < 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -speed));
        }
    }
}
