using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage;
    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 100;
    }
    private void OnTriggerEnter(Collider other)
    {
        DamageReceiver receiver = other.GetComponent<DamageReceiver>();
        if (receiver != null)
        {
            receiver.Damage(damage,transform);
        }
        Destroy(gameObject);
    }
}
