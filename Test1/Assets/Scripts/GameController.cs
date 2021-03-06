using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public int score;
    void Start()
    {
        UIController.SetScore(score);
        FindObjectOfType<GameField>().InitGameField(64, 64);

        int blockCount = 128;

        while(blockCount > 0)
        {
            int rdX = Random.Range(0, 64);
            int rdY = Random.Range(0, 64);
            if (FindObjectOfType<GameField>().IsCellBlocked(rdX, rdY))
                continue;

            FindObjectOfType<GameField>().BlockCell(rdX, rdY);
            blockCount--;
        }

        int rewardCount = 16;

        while(rewardCount> 0)
        {
            int rdX = Random.Range(0, 64);
            int rdY = Random.Range(0, 64);

            if (FindObjectOfType<GameField>().IsCellBlocked(rdX, rdY))
                continue;

            FindObjectOfType<GameField>().CreateReward(rdX, rdY);
            rewardCount--;
        }

        FindObjectOfType<GameField>().InitAICharacter(0, 0);


        score = 0;

        GameObject firstReward = FindObjectOfType<GameField>().CreateReward(6, 9);

        Vector3 rewardPosition = firstReward.transform.position;

        AICharacter aiCharacter = FindObjectOfType<AICharacter>();

        aiCharacter.PathFind(aiCharacter.transform.position, rewardPosition);
    }
}
