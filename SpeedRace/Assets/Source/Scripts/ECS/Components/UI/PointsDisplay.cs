using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityTools.Extentions;

public class PointsDisplay : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(ShowRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        transform.localScale = TweenHelper.zeroSize;
        yield return transform.DOScale(Vector3.one, 0.25f).WaitForCompletion();
        yield return transform.DOMove(transform.position + (Vector3.up * 0.5f), 0.5f).WaitForCompletion();
        yield return transform.DOScale(TweenHelper.zeroSize, 0.15f).WaitForCompletion();
        Destroy(gameObject);
    }
}
