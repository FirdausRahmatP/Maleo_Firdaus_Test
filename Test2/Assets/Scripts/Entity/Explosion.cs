using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Damage explosion;
    public GameObject effect;
    public void Detonate(float delay)
    {
        Invoke("Explode", delay);
    }
    public void Explode()
    {
        Damage g = Instantiate(explosion, transform.position, Quaternion.identity);
        g.owner = gameObject.GetComponent<Life>();
        g.transform.forward = Vector3.up;
        GameObject e = Instantiate(effect, transform.position, Quaternion.identity);
        e.AddComponent<DestroyDelay>().delay = 2;
        Destroy(gameObject);
    }
}
