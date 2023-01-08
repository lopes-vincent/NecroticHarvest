using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour, ActionInterface
{
    private bool _active = false;
    private Player _player;

    void Update()
    {
        if (_active)
        {
            _player.pendingAction = true;
            if (_player.transform.position == transform.position)
            {
                _player.pendingAction = false;
                Destroy(gameObject);
            }
            
            _player.Move(transform.position);
        }
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    public void SetActive(bool active)
    {
        _active = active;
    }
}
