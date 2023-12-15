using DG.Tweening;
using Kuhpik;
using UnityEngine;

public class ViewerMovementController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] private float moveCooldown;

    private float currentCooldown = 0;
    private MovementZone movementZone;

    private void OnEnable()
    {
        currentCooldown = 0;
    }


    private void Update()
    {
        currentCooldown += Time.deltaTime;
        if (currentCooldown >= moveCooldown)
        {
            Move();
            currentCooldown = 0;
        }
    }

    private void Move()
    {
        animator.SetBool("Walk", true);
        movementZone ??= Bootstrap.Instance.GameData.MovementZone;
        var position = movementZone.GetRandomPosition(transform.position.y);
        transform.DOMove(position, speed).OnComplete(() => animator.SetBool("Walk", false));
        transform.DOLookAt(position, 1f);
    }
}