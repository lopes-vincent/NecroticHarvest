using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationAction : MonoBehaviour, ActionInterface
{
    public ScoreCounter _scoreCounter;

    protected bool _active = false;
    protected Player _player;
    protected Station _station;
    
    void Update()
    {
        if (_active)
        {
            _player.pendingAction = true;
            if (_player.transform.position == _station.transform.position)
            {
                GameObject stationDeath =  _station.death;
                GameObject playerDeath =  _player.carry;
                bool stationWasFull = _station.full;
                bool playerWasCarrying = _player.carrying;
                
                if (_player.carrying)
                {
                    playerDeath.GetComponent<Death>().BeDropped(_station.gameObject);
                    _player.Drop();
                    _station.full = true;
                    _station.death = playerDeath;
                    if (playerDeath.GetComponent<Death>().CanDoEmbalmingStep(_station.embalmingStep))
                    {
                        _station.LaunchActionTimer();
                    }
                    else
                    {
                        _station.impossibleActionVisual.SetActive(true);
                    }
                }
                
                if (stationWasFull)
                {
                    stationDeath.GetComponent<Death>().BeCarried(_player.gameObject);
                    _player.Carry(stationDeath);
                    _station.full = false;
                    _station.ResetActionTimer();
                    _station.impossibleActionVisual.SetActive(false);
                    _station.finishedActionVisual.SetActive(false);
                    if (playerWasCarrying)
                    {
                        _station.full = true;
                        if (playerDeath.GetComponent<Death>().CanDoEmbalmingStep(_station.embalmingStep))
                        {
                            _station.LaunchActionTimer();
                        }
                        else
                        {
                            _station.impossibleActionVisual.SetActive(true);
                        }
                    }
                }
                
                _player.pendingAction = false;
                Destroy(gameObject);
            }
            
            _player.Move(_station.transform.position);
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
        _station = gameObject.GetComponent<Station>();
    }
}
