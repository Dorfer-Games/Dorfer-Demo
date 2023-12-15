using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class TutorialLevel : GameSystem, IIniting, IUpdating
{
   [SerializeField] private float timeDurable;
   [SerializeField] private GameObject tap;
   [SerializeField] private GameObject unTap;
   [SerializeField] private GameObject slide;
   
   private bool firstLevel;
   private bool seeBack;
   private float time;
   
   void IIniting.OnInit()
   {
      tap.SetActive(false);
      unTap.SetActive(false);
      slide.SetActive(false);

      time = Time.fixedDeltaTime;
      
      seeBack = true;
      if (player.level == 0)
      {
         firstLevel = true;
      }
      else
      {
         firstLevel = false;
      }
   }

   void IUpdating.OnUpdate()
   {
      if (firstLevel && seeBack)
      {
         if (game.fear)
         {
            if (Input.GetMouseButtonUp(0))
            {
               unTap.SetActive(false);
               Time.timeScale = 1;
               Time.fixedDeltaTime = time;
               seeBack = false;
            }
            else
            {
               slide.SetActive(false);
               tap.SetActive(false);
               unTap.SetActive(true);
               Time.timeScale = timeDurable;
               Time.fixedDeltaTime = Time.timeScale * 0.02f;
            }
         }
         else
         {
            if (Input.GetMouseButtonUp(0))
            {
               slide.SetActive(true);
               //tap.SetActive(false);
               Time.timeScale = timeDurable;
               Time.fixedDeltaTime = Time.timeScale * 0.02f;
               
            }

            if (Input.GetMouseButtonDown(0))
            {
               slide.SetActive(false);
               //tap.SetActive(true);
               Time.timeScale = 1;
               Time.fixedDeltaTime = time;
            }
         }
      }
   }
}
