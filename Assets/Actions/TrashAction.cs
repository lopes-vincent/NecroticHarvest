using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashAction : StationAction
{
    void Update()
    {
        if (_active)
        {
            _player.pendingAction = true;
            if (_player.transform.position == _station.transform.position)
            {
                if (_player.carrying)
                {
                    Death death = _player.carry.GetComponent<Death>();
                    _player.Drop();
                    Destroy(death.gameObject);
                    _scoreCounter.score -= 1;
                }

                _player.pendingAction = false;
                Destroy(gameObject);
            }
            
            _player.Move(_station.transform.position);
        }
    }
}
