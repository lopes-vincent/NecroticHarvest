using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionQueue : MonoBehaviour
{
    [SerializeField] private Queue<ActionInterface> queue = new Queue<ActionInterface>();
    [SerializeField] private Player player;

    public void AddAction(ActionInterface action)
    {
        action.SetPlayer(player);
        queue.Enqueue(action);
    }

    public void DoNextAction()
    {
        if (queue.Count > 0)
        {
            ActionInterface nextAction = queue.Dequeue();
            nextAction.SetActive(true);
        }
    }

    public void Purge()
    {
        queue.Clear();
        foreach (Transform action in transform) {
            Destroy(action.gameObject);
        }
        player.pendingAction = false;
    }
}
