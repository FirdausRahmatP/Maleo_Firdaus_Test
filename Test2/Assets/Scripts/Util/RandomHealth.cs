using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHealth : MonoBehaviour
{
    public List<float> healths = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        Life life = GetComponent<Life>();
        int random = Random.Range(0, healths.Count);
        life.health = healths[random];
        life.maxHealth = healths[random];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
