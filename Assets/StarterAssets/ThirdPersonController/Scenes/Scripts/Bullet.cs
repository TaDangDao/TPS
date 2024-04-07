using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody bullet;
    [SerializeField] private Transform hitTarget;
    [SerializeField] private Transform missTarget;
    void Awake()
    {
        bullet = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Start()
    {
        float speed = 50f;
        bullet.velocity = transform.forward * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Target>() != null) {
        Instantiate(hitTarget,transform.position, Quaternion.identity);
        }
        else
        {
        Instantiate(missTarget,transform.position, Quaternion.identity);

        }
        Destroy(gameObject);
       
    }
}
