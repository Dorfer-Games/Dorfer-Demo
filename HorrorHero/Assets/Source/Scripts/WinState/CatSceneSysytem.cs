using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Kuhpik;
using UnityEngine;

public class CatSceneSysytem : GameSystem, IIniting, IUpdating
{
     private Camera cam;
     private Vector3 pos;
     private Light light;

     [SerializeField] private Transform gameCoordinats;
     
     /// <summary>
     /// прямые уровни
     /// </summary>
     
     void IIniting.OnInit()
     {
          Time.timeScale = 1;
          game.murder.GetComponent<Collider>().enabled = false;
          game.victim.GetComponent<Collider>().enabled = false;
          
          Rigidbody rbMurder = game.murder.GetComponent<Rigidbody>();
          Rigidbody rbVictim = game.victim.GetComponent<Rigidbody>();

          
          rbMurder.constraints = RigidbodyConstraints.FreezeAll;
          rbVictim.constraints = RigidbodyConstraints.FreezeAll;
          
          cam = Camera.main;
          
          Vector3 posVictim = game.victim.transform.position;
          posVictim.z -= 2f;
          pos = new Vector3(-0.9f, 6.1f, posVictim.z -2f);
          Vector3 rot = new Vector3(9f, 0,0);
          cam.transform.DOMove(pos, 3f);
          cam.transform.DORotate(rot, 3f);
          gameCoordinats.DOMoveX(13, 6f).OnComplete(EndRotate);
          game.light.transform.DOMoveY(0, 3f);
          light = game.light.GetComponent<Light>();
          
          Vector3 newRotate = new Vector3(0, 180, 0);
          game.victim.transform.DORotate(-newRotate, 0.25f);
          game.masking = true;
          float distance = game.murder.transform.position.z - game.victim.transform.position.z;
          game.murder.transform.DOMove(posVictim, distance).OnComplete(EndMurderPosition); 
          
          Animator anim = game.victim.transform.GetChild(0).GetComponent<Animator>();
          Animator animMurder = game.murder.transform.GetChild(0).GetComponent<Animator>();
          anim.SetBool("Win", true);
          animMurder.SetBool("Win", true);

          player.level += 1;
     }

     void EndRotate()
     {
          if (player.vibration)
          {
               Vibration.VibratePeek(); 
          }
          Bootstrap.ChangeGameState(EGamestate.WinScreen);
     }
     
     void IUpdating.OnUpdate()
     {
          light.intensity = gameCoordinats.position.x;
          
     }

     void EndMurderPosition()
     {
          game.murder.transform.GetChild(1).gameObject.SetActive(false);
          game.murder.transform.GetChild(0).DOScale(Vector3.one, 0.1f).SetEase(Ease.OutBounce);
     }
     
     
     /////////
     ///////// Уровни крестом
     ///
      
     /*void IIniting.OnInit()
     {
          cam = Camera.main;
          pos = new Vector3(-0.9f, 6.1f, -4.8f);
          Vector3 posVictim = game.victim.transform.position;
          GameObject g = new GameObject();
          g.transform.position = posVictim;
          g.transform.SetParent(game.murder.transform.parent);
         

          posVictim = g.transform.localPosition;
          posVictim.z -= 2f;
          
          Vector3 rot = new Vector3(9f, 0 ,0);
          cam.transform.DOLocalMove(pos, 3f);
          cam.transform.DOLocalRotate(rot, 3f);
          gameCoordinats.DOMoveX(13, 6f).OnComplete(EndRotate);
          game.light.transform.DOMoveY(0, 3f);
          light = game.light.GetComponent<Light>();
          
          Vector3 newRotate = new Vector3(0, 180, 0);
          game.victim.transform.DORotate(-newRotate, 0.25f);
          game.masking = true;
          float distance = game.murder.transform.position.z - game.victim.transform.position.z;
          game.murder.transform.DOLocalMove(posVictim, distance).OnComplete(EndMurderPosition); 
          
          Animator anim = game.victim.transform.GetChild(0).GetComponent<Animator>();
          Animator animMurder = game.murder.transform.GetChild(0).GetComponent<Animator>();
          anim.SetBool("Win", true);
          animMurder.SetBool("Win", true);
     }

     void EndRotate()
     {
          if (player.vibration)
          {
              Vibration.VibratePeek(); 
          }
          Bootstrap.ChangeGameState(EGamestate.WinScreen);
     }
     
     void IUpdating.OnUpdate()
     {
          light.intensity = gameCoordinats.position.x;
          
     }

     void EndMurderPosition()
     {
          game.murder.transform.GetChild(1).gameObject.SetActive(false);
          game.murder.transform.GetChild(0).DOScale(Vector3.one, 0.1f).SetEase(Ease.OutBounce);
     }*/
}
