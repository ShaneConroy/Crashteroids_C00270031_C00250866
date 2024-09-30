using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speed = 1;
    private readonly float maxY = -5;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public void Move()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        if (transform.position.y < maxY)
        {
            Destroy(gameObject);
        }
    }
}
