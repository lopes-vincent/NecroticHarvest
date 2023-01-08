using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingAction : StationAction
{
    void Update()
    {
        if (_active)
        {
            _player.pendingAction = true;
            if (_player.transform.position == _station.transform.position)
            {
                if (_player.carrying && !_station.full)
                {
                    GameObject death = _player.carry;
                    death.GetComponent<Death>().BeDropped(_station.gameObject);
                    _player.Drop();
                    _station.full = true;
                    _station.death = death;
                }
                else if (!_player.carrying && _station.full)
                {
                    GameObject death = _station.death;
                    death.GetComponent<Death>().BeCarried(_player.gameObject);
                    _player.Carry(death);
                    _station.full = false;
                }

                _player.pendingAction = false;
                Destroy(gameObject);
            }
            
            _player.Move(_station.transform.position);
        }
    }
}
