using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Object_Fruit_Drop : MonoBehaviour
{
    Rigidbody rb;
    float speed = 5.0f;
    float lifeTime = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0,-speed,0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 50 * Time.deltaTime);
        if (lifeTime > 0)
            lifeTime -= Time.deltaTime;
        else
            NetworkObject.Destroy(gameObject);
    }
}
