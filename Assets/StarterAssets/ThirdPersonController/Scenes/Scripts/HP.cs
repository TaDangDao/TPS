using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    private float maxHp=100;
    private float hp=100;
    
    
    private void OnTriggerEnter(Collider other)
    {
       
        {
            if (other.GetComponent<Bullet>() != null)
            {
                hp = hp - 33;
            }
        }
        if (hp < 0)
        {
            Destroy(gameObject);
        }
    }


}
