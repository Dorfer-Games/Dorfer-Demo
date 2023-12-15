using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class LevelLoadingSystem : GameSystem, IIniting
{
    [SerializeField] private string levelsPath;
    [SerializeField] private int maxLevels;
    void IIniting.OnInit()
    {
        
        if (player.level >= maxLevels)
        {
            player.level = 0;
        }
        var level = Mathf.Clamp(player.level, 0, maxLevels - 1);
        var levelGo = Resources.Load<GameObject>(string.Format(levelsPath, level + 1));
       
       Vibration.Init();
       game.level = Instantiate(levelGo);/**/
        
        game.victim = GameObject.FindWithTag("Victim");
        game.murder = GameObject.FindWithTag("Murder");
        game.victimGhost = GameObject.FindWithTag("VictimGhost");
        game.victimGhost.GetComponent<NavMeshAgent>().enabled = false;
        game.masking = true;
        game.fearBar = GameObject.FindWithTag("Fear").GetComponent<Scrollbar>();
        game.finish = GameObject.FindWithTag("EndLevel");
        game.light = GameObject.FindWithTag("LightCatScene");
        
        Vibration.Init();
    }
}
