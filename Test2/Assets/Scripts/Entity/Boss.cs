using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    private Zombie zombie;
    public int currentForm;
    public List<BossForm> forms = new List<BossForm>();
    public UnityEvent onDeath;
    private void Start()
    {
        zombie = GetComponent<Zombie>();
        forms[currentForm].Apply(zombie);
    }
    
    public void NextForm()
    {
        currentForm++;
        if (currentForm >= forms.Count)
        {
            zombie.immortal = false;
            zombie.PlayDeath();
            onDeath?.Invoke();
            return;
        }
        else
        {
            zombie.death = false;
            zombie.immortal = true;
        }
        forms[currentForm].Apply(zombie);
    }
}
[System.Serializable]
public class BossForm
{
    public float health;
    public float range;
    public float speed;
    public float size;
    public void Apply(Zombie life)
    {
        life.health = health;
        life.maxHealth = health;
        life.attackRange = range;
        life.speed = speed;
        life.transform.localScale = Vector3.one * size;
    }
}
