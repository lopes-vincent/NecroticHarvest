using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryAction : MonoBehaviour, ActionInterface
{
    private bool _active = false;
    private Player _player;
    private GameObject _target;

    void Update()
    {
        if (_active)
        {
            _player.pendingAction = true;
            if (_player.transform.position == _target.transform.position)
            {
                if (!_player.carrying)
                {
                    _target.GetComponent<Death>().BeCarried(_player.gameObject);
                    _player.Carry(_target);
                }

                _player.pendingAction = false;
                Destroy(gameObject);
            }
            
            _player.Move(_target.transform.position);
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

    public void SetTarget(GameObject gameObject)
    {
        _target = gameObject;
    }
}
