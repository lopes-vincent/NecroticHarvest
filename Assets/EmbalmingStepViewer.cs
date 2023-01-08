using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using ColorUtility = UnityEngine.ColorUtility;

public class EmbalmingStepViewer : MonoBehaviour
{
    [SerializeField] GameObject eviscerationIcon;
    [SerializeField] GameObject saltIcon;
    [SerializeField] GameObject stripIcon;
    [SerializeField] bool over = false;
    [SerializeField] Color overColor;
    [SerializeField] Color notOverColor;
    
    

    private void Update()
    {
        if (over)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void ResetIcons()
    {
        eviscerationIcon.SetActive(false);
        if (over)
        {
            GetComponent<Image>().color = notOverColor;
        }
        eviscerationIcon.GetComponent<Image>().color = Color.white;
        saltIcon.SetActive(false);
        saltIcon.GetComponent<Image>().color = Color.white;
        stripIcon.SetActive(false);
        stripIcon.GetComponent<Image>().color = Color.white;
    }
    
    public void DisplaySteps(Death death)
    {
        if (over)
        {
            GetComponent<Image>().color = overColor;
        }
        
        if (death.embalmingSteps.ContainsKey(EmbalmingStep.Evisceration))
        {
            eviscerationIcon.SetActive(true);
            if (death.embalmingSteps[EmbalmingStep.Evisceration])
            {
                eviscerationIcon.GetComponent<Image>().color = Color.green;
            }
        }
        
        if (death.embalmingSteps.ContainsKey(EmbalmingStep.Salt))
        {
            saltIcon.SetActive(true);
            if (death.embalmingSteps[EmbalmingStep.Salt])
            {
                saltIcon.GetComponent<Image>().color = Color.green;
            }
        }
        if (death.embalmingSteps.ContainsKey(EmbalmingStep.Strip))
        {
            stripIcon.SetActive(true);
            if (death.embalmingSteps[EmbalmingStep.Strip])
            {
                stripIcon.GetComponent<Image>().color = Color.green;
            }
        }
    }
}
