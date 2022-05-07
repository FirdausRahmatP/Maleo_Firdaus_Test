using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : Singleton<Spawner>
{
    public List<Wave> waves = new List<Wave>();
    public List<Transform> spawnPoints = new List<Transform>();
    public Transform bossPoint;
    public List<Zombie> normalZombie = new List<Zombie>();
    public Zombie specialZombie;
    public Zombie bossZombie;
    public float currentHealthBonus;
    public Wave currentWave;
    public int zombieSpawned;
    public int currentZombie;
    public UnityEvent<Wave> onWaveStarted;
    private void Start()
    {
        StartCoroutine(StartSpawn());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Wave1");
            StopAllCoroutines();
            StartCoroutine(StartWave(waves[0]));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Wave2");
            StopAllCoroutines();
            StartCoroutine(StartWave(waves[1]));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Wave3");
            StopAllCoroutines();
            StartCoroutine(StartWave(waves[2]));
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (var item in FindObjectsOfType<Zombie>())
            {
                item.Damage(item.health);
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnBoss();
        }
    }
    IEnumerator StartSpawn()
    {
        foreach (var wave in waves)
        {
            yield return StartCoroutine(StartWave(wave));
        }
    }
    IEnumerator StartWave(Wave wave)
    {
        onWaveStarted?.Invoke(wave);
        currentWave = wave;
        yield return new WaitForSeconds(wave.startDelay);
        bool spawning = true;

        //Spawning
        while (spawning)
        {
            if (GetCurrentZombie() < wave.normalZombieLimit)
            {
                SpawnNormal(wave.notSpawnRange);
            }
            if (zombieSpawned >= wave.normalZombieStop)
            {
                spawning = false;
                yield return null;
            }
            yield return new WaitForSeconds(wave.normalZombieSpawnInterval);
        }
        zombieSpawned = 0;
        while (GetCurrentZombie() > 0)
        {
            yield return new WaitForSeconds(2);
        }
        //Special Wave
        foreach (var special in wave.specialWave)
        {
            SpawnSpecial(special);
        }
        while (GetCurrentZombie() > 0)
        {
            yield return new WaitForSeconds(2);
        }
        //Boss
        if (wave.spawnBoss)
        {
            SpawnBoss();
        }
        currentHealthBonus += wave.addHealth;
        zombieSpawned = 0;
    }
    public void SpawnBoss()
    {
        Zombie b = SpawnZombie(bossPoint.position, bossPoint.rotation, bossZombie);
        b.GetComponent<Boss>().onDeath.AddListener(UIController.Instance.OnGameEnd);
    }
    public void SpawnNormal(float notSpawnRange)
    {
        List<Transform> spawnPool = new List<Transform>();
        foreach (var item in spawnPoints)
        {
            if(Vector3.Distance(item.transform.position,Character.main.transform.position) >= notSpawnRange)
            {
                spawnPool.Add(item);
            }
        }
        int random = Random.Range(0, spawnPool.Count);
        Transform spawnPoint = spawnPool[random];
        int random2 = Random.Range(0, normalZombie.Count);
        Zombie zombie = normalZombie[random2];
        SpawnZombie(spawnPoint.position,spawnPoint.rotation, zombie);
    }
    public void SpawnSpecial(SpecialWave special)
    {
        Transform spawnPoint = spawnPoints[special.spawnPoint - 1];
        Zombie zombie = null;
        for (int i = 0; i < special.count; i++)
        {
            switch (special.type)
            {
                case ZombieType.Normal:
                    int random2 = Random.Range(0, normalZombie.Count);
                    zombie = normalZombie[random2];
                    break;
                case ZombieType.Special:
                    zombie = specialZombie;
                    break;
                case ZombieType.Boss:
                    zombie = bossZombie;
                    break;
            }
            Vector3 offset = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            SpawnZombie(spawnPoint.position+offset, spawnPoint.rotation, zombie);
        }
    }
    public Zombie SpawnZombie(Vector3 spawnPoint,Quaternion rotation,Zombie zombie)
    {
        Zombie z = Instantiate(zombie, spawnPoint, rotation);
        z.health += currentHealthBonus;
        z.maxHealth = z.health;
        zombieSpawned++;
        return z;
    }
    public int GetCurrentZombie()
    {
        currentZombie = 0;
        foreach (var item in FindObjectsOfType<Zombie>())
        {
            if (!item.death)
            {
                currentZombie++;
            }
        }
        return currentZombie;
    }
}
