using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ActionQueue actionQueue;
    
    [SerializeField] private MoveAction moveActionPrefab;
    
    [SerializeField] private Player player;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(pos, new Vector2(0, 0), 0.01f);
            if (hits.Length == 0)
            {
                MoveAction moveAction = Instantiate(moveActionPrefab, actionQueue.transform);
                Vector3 movePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                movePosition.z = 0;
                moveAction.transform.position = movePosition;

                actionQueue.AddAction(moveAction);
            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            actionQueue.Purge();
        }

        if (!player.pendingAction)
        {
            actionQueue.DoNextAction();
        }
    }
}
