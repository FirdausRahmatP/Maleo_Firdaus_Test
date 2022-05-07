using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public bool destroy;
    public Life owner;
    public float damageZombie;
    public float damageCharacter;
    public float speed = 100;
    public float distance;
    private void Update()
    {
        Vector3 forward = transform.forward * Time.deltaTime * speed;
        transform.position += forward;
        distance -= forward.magnitude;
        if(distance <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        DamageReceiver receiver = other.GetComponent<DamageReceiver>();
        if (receiver != null && owner.GetType() != receiver.life.GetType())
        {
            float d = receiver.life.GetType() == typeof(Zombie) ? damageZombie : damageCharacter;
            receiver.Damage(d,transform);
            if(destroy)
                Destroy(gameObject);
        }
    }
}
