using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Death deathPrefab;
    [SerializeField] private ActionQueue actionQueue;
    [SerializeField] private EmbalmingStepViewer overEmbalmingStepViewer;

    private float _spanwDelay = 0.2f;
    private float _timeSpentSinceLastSpawn = 0f;

    void Update()
    {
        _timeSpentSinceLastSpawn += 1 * Time.deltaTime;

        if (_timeSpentSinceLastSpawn >= _spanwDelay)
        {
            _timeSpentSinceLastSpawn = 0f;
            SpawnDeath();
            _spanwDelay = Random.Range(1.5f, 2f);
        }

    }

    private void SpawnDeath()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("SpawnTable") && null == child.GetComponentInChildren<Death>())
            {
                Death death = Instantiate(deathPrefab, child.transform);
                death.actionQueue = actionQueue;
                death.overEmbalmingStepViewer = overEmbalmingStepViewer;
                death.transform.localPosition = Vector3.zero;
                return;
            }
        }
    }
}
