using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{
    public Text message;
    private void Start()
    {
        bool bossDeath = false;
        bool charDeath = false;
        Boss boss = FindObjectOfType<Boss>();
        if (boss != null)
            bossDeath = boss.GetComponent<Zombie>().health<=0;
        Character m = Character.main;
        if (m != null)
            charDeath = m.health <= 0;
        if (bossDeath && !charDeath)
        {
            message.text = "Victory";
        }
        else if(!bossDeath && charDeath)
        {
            message.text = "Defeat";
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void Reload()
    {
        SceneManager.LoadScene(0);
    }
}
