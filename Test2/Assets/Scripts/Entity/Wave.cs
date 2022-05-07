using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Wave",menuName ="Data/Wave",order = 0)]
public class Wave : ScriptableObject
{
    public float startDelay;
    public float normalZombieSpawnInterval;
    public float notSpawnRange;
    public int normalZombieLimit;
    public int normalZombieStop;
    public List<SpecialWave> specialWave = new List<SpecialWave>();
    public float addHealth;
    public bool spawnBoss;
}
[System.Serializable]
public class SpecialWave
{
    public ZombieType type;
    public int count;
    public int spawnPoint;
}
public enum ZombieType
{
    Normal,
    Special,
    Boss
}
