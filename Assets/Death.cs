using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Death : MonoBehaviour
{
   public ActionQueue actionQueue;
   public EmbalmingStepViewer overEmbalmingStepViewer;
   public CarryAction carryActionPrefab;
   public SortedDictionary<EmbalmingStep, bool> embalmingSteps = new SortedDictionary<EmbalmingStep, bool>();

   [SerializeField] private SpriteRenderer togaSprite;
   [SerializeField] private Color togaColor;
   
   [SerializeField] private List<SpriteRenderer> baseSprites;
   [SerializeField] private List<SpriteRenderer> evisceratedSprites;
   [SerializeField] private List<SpriteRenderer> saltedSprites;
   [SerializeField] private List<SpriteRenderer> strippedSprites;
   [SerializeField] public Color hoverColor;

   public bool Carried = false;
   public bool Dropped = false;
   public Station currentStation;

   private void Start()
   {
       togaColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
       if (Random.Range(0, 100) < 65)
       {
           embalmingSteps.Add(EmbalmingStep.Evisceration, false);
       }

       if (Random.Range(0, 100) < 65)
       {
           embalmingSteps.Add(EmbalmingStep.Salt, false);
       }

       embalmingSteps.Add(EmbalmingStep.Strip, false);
   }

   private void Update()
   {
       togaSprite.color = togaColor;
   }

   public void BeCarried(GameObject player)
   {
       transform.parent = player.transform;
       transform.localPosition = Vector3.zero;
       Carried = true;
       Dropped = false;
       foreach (SpriteRenderer spriteRenderer in  GetComponentsInChildren<SpriteRenderer>())
       {
           spriteRenderer.sortingOrder += 30;
       }
   }

   public void BeDropped(GameObject target)
   {
       transform.parent = target.transform;
       Carried = false;
       Dropped = true;
       currentStation = target.GetComponent<Station>();
       foreach (SpriteRenderer spriteRenderer in  GetComponentsInChildren<SpriteRenderer>())
       {
           spriteRenderer.sortingOrder -= 30;
       }
   }

   public void End()
   {
       GetComponent<Animator>().SetTrigger("End");
   }

   public void Destroy()
   {
       Destroy(gameObject);
   }

   public void OnMouseOver()
   {
       overEmbalmingStepViewer.DisplaySteps(this);
       if (!Carried && !Dropped)
       {
          foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
          {
              spriteRenderer.color = hoverColor;
          }
       }
       else if (Dropped)
       {
           currentStation.Over();
       }

   }

   public void OnMouseExit()
   {
       overEmbalmingStepViewer.ResetIcons();
       foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
       {
           spriteRenderer.color = Color.white;
       }
       
       if (Dropped)
       {
           currentStation.Exit();
       }
   }

   public void OnMouseDown()
   {
       if (Dropped)
       {
           currentStation.AddStationActionToQueue();
       } else if (!Carried)
       {
           CarryAction carryAction = Instantiate(carryActionPrefab, actionQueue.transform);
           Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           carryAction.transform.position = position;
           carryAction.SetTarget(gameObject);
     
           actionQueue.AddAction(carryAction);
       }
   }

   private bool CanBeEviscerated()
   {
       if (!embalmingSteps.ContainsKey(EmbalmingStep.Evisceration))
       {
           return false;
       }

       if (embalmingSteps[EmbalmingStep.Evisceration])
       {
           return false;
       }

       return true;
   }

   private bool CanBeSalted()
   {
       if (embalmingSteps.ContainsKey(EmbalmingStep.Evisceration) && !embalmingSteps[EmbalmingStep.Evisceration])
       {
           return false;
       }
       
       return true;
   }

   private bool CanBeStripped()
   {
       if (embalmingSteps.ContainsKey(EmbalmingStep.Evisceration) && !embalmingSteps[EmbalmingStep.Evisceration])
       {
           return false;
       }
       if (embalmingSteps.ContainsKey(EmbalmingStep.Salt) && !embalmingSteps[EmbalmingStep.Salt])
       {
           return false;
       }
       
       return true;
   }

   public bool CanDoEmbalmingStep(EmbalmingStep embalmingStep)
   {
       switch (embalmingStep)
       {
           case EmbalmingStep.Evisceration:
               return CanBeEviscerated();
           case EmbalmingStep.Salt:
               return CanBeSalted();
           case EmbalmingStep.Strip:
               return CanBeStripped();
       }

       return false;
   }   
   
   public List<SpriteRenderer> SpritesToEnable(EmbalmingStep embalmingStep)
   {
       switch (embalmingStep)
       {
           case EmbalmingStep.Evisceration:
               return evisceratedSprites;
           case EmbalmingStep.Salt:
               return saltedSprites;
           case EmbalmingStep.Strip:
               return strippedSprites;
       }

       return baseSprites;
   }

   public void DoEmbalmingStep(EmbalmingStep embalmingStep)
   {
       embalmingSteps[embalmingStep] = true;
       foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
       {
           spriteRenderer.gameObject.SetActive(false);
       }
       foreach (SpriteRenderer spriteRenderer in SpritesToEnable(embalmingStep))
       {
           spriteRenderer.gameObject.SetActive(true);
       }
   }

   public bool ReadyToGo()
   {
       foreach (bool done in embalmingSteps.Values)
       {
           if (!done)
           {
               return false;
           }
       }

       return true;
   }
}

public enum EmbalmingStep
{
    Evisceration,
    Salt,
    Strip,
}
