using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] public ActionQueue actionQueue;
    [SerializeField] public StationAction stationActionPrefab;
    [SerializeField] public EmbalmingStep embalmingStep;
    [SerializeField] public float actionTime = 1f;
    [SerializeField] public GameObject impossibleActionVisual;
    [SerializeField] public GameObject finishedActionVisual;
    [SerializeField] public ScoreCounter scoreCounter;
    [SerializeField] public SpriteRenderer visual;
    [SerializeField] public Color hoverColor;
    
   
    public bool full = false;
    public GameObject death;

    private bool _timerEnabled;
    private float _timeElapsed;
    
    void Update()
    {
        if (_timerEnabled)
        {
            _timeElapsed += 1 * Time.deltaTime;
    
            if (_timeElapsed >= actionTime)
            {
                finishedActionVisual.SetActive(true);
                ApplyAction();
            }
        }
    }

    public void Over()
    {
        visual.color = hoverColor;
    }
    

    public void Exit()
    {
        visual.color = Color.white;

    }
    
    public void OnMouseOver()
    {
        Over();
    }

    public void OnMouseExit()
    {
        Exit();
    }
    
    public void OnMouseDown()
    {
        AddStationActionToQueue();
    }

    public void AddStationActionToQueue()
    {
        StationAction stationAction = Instantiate(stationActionPrefab, actionQueue.transform);
        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        stationAction.transform.position = position;
        stationAction.SetTarget(gameObject);
        stationAction._scoreCounter = scoreCounter;

        actionQueue.AddAction(stationAction);
    }

    public void LaunchActionTimer()
    {
        _timeElapsed = 0;
        _timerEnabled = true;
    }

    public void ResetActionTimer()
    {
        _timeElapsed = 0;
        _timerEnabled = false;
    }

    public void ApplyAction()
    {
        Death deathComponent = death.GetComponent<Death>();
        deathComponent.DoEmbalmingStep(embalmingStep);
    }
}
