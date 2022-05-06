using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public ParticleSystem explosion;
    void OnTriggerEnter(Collider otherCollider)
    {
        if(otherCollider.tag == "Player")
        {
            GameController.Instance.score++;
            explosion.transform.parent = null;
            explosion.gameObject.SetActive(true);
            explosion.Play();
            UIController.SetScore(GameController.Instance.score);
            Destroy(gameObject);
        }
    }
}
