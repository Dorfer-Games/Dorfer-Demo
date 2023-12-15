
using DG.Tweening;
using Kuhpik;
using TMPro;
using UnityEngine;

public class GASController : GameSystem, IIniting, IUpdating
{
    [SerializeField] private GameObject gas;

    public float angle;

    private float newAngle;
    private bool t;

    private Vector3 murderPos;
    private Vector3 victimPos;

    void IIniting.OnInit()
    {
        Time.timeScale = 1;
        murderPos = game.murder.transform.position;
        victimPos = game.victim.transform.position;
        game.murder.transform.GetChild(1).gameObject.SetActive(false);
        game.murder.transform.GetChild(0).DOScale(Vector3.one, 0.45f);
        gas.SetActive(true);
        game.masking = true;
        Animator anim = game.victim.transform.GetChild(0).GetComponent<Animator>();
        Animator animMurder = game.murder.transform.GetChild(0).GetComponent<Animator>();
        anim.SetBool("Lose", true);
        animMurder.SetBool("Lose", true);
        EndRotate();
        t = false;

        Vector3 targetDir = game.murder.transform.position - game.victim.transform.position;
        Vector3 forw = game.victim.transform.forward;

        game.victim.transform.rotation = Quaternion.Euler(Vector3.zero);
        angle = Vector3.SignedAngle(targetDir, forw, Vector3.up);
        
        newAngle = -180 - angle;
        
        Vector3 newRotate = new Vector3(0, newAngle, 0);
        game.victim.transform.DORotate(newRotate, 0.45f).OnComplete(EndRotate);
        // //game.victim.transform.DORotate(-newRotate, 0.25f).OnComplete(EndRotate);
        //float angle = Vector3.Angle(game.victim.transform.position, game.murder.transform.position);
        // game.victim.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }

    void EndRotate()
    {
        t = true;
        if (player.vibration)
        {
           Vibration.VibrateNope(); 
        }
    }

    void IUpdating.OnUpdate()
    {
        if (t)
        {
            game.victim.transform.rotation = Quaternion.Euler(new Vector3(0, newAngle, 0));
            game.victim.transform.position = victimPos;
            game.murder.transform.position = murderPos;
        }
    }
    
    
}
