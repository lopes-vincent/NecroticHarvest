using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingAction : StationAction
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
                    if (death.ReadyToGo())
                    {
                        _scoreCounter.score += death.embalmingSteps.Count * 2;
                        death.BeDropped(_station.gameObject);
                        death.End();
                        _player.Drop();
                    }
                }

                _player.pendingAction = false;
                Destroy(gameObject);
            }
            
            _player.Move(_station.transform.position);
        }
    }
}
