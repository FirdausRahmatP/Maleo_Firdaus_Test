using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AICharacter : MonoBehaviour
{
    private Queue<Vector3> path = new Queue<Vector3>();
    private Vector3 targetPosition;
    public float speed = 5;
    public bool isMoving;

    void Awake()
    {
        targetPosition = transform.position;
    }
    public void PathFind(Vector3 from, Vector3 target)
    {
        pathfind = AstarPathFinding.Instance.GetPathThread(from, target, SetPath);
    }
    public Thread pathfind;
    public void SetPath(Queue<Vector3> path)
    {
        this.path = path;
        pathfind.Abort();
    }    

    void Update()
    {
        if(Vector3.Distance(transform.position,targetPosition) < 0.05f)
        {
            if(path.Count <= 0)
            {
                isMoving = false;
                FindClosestReward();
                return;
            }
            targetPosition = path.Dequeue();
        }
        isMoving = true;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
    public void FindClosestReward()
    {
        GameObject reward = GameField.Instance.FindClosestReward(transform.position);
        if (reward != null)
        {
            PathFind(transform.position, reward.transform.position);
        }
        else
        {
            UIController.DisplayWinText();
        }
    }
}
