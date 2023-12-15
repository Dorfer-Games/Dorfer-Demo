using DG.Tweening;
using UnityEngine;

namespace UnityTools.Extentions
{
    public static class TweenExtensions
    {
        public static Tween DoJumpNormalized(this Transform transform, Vector3 target, float jumpPower, float speed)
        {
            var distance = Vector3.Distance(transform.position, target);
            var duration = distance / speed;
            return transform.DOJump(target, jumpPower, 1, duration);
        }

        public static Tween DoMoveNormalized(this Transform transform, Vector3 target, float speed)
        {
            var distance = Vector3.Distance(transform.position, target);
            var duration = distance / speed;
            return transform.DOMove(target, duration);
        }

        public static Tween DoLocalMoveNormalized(this Transform transform, Vector3 target, float speed)
        {
            var distance = Vector3.Distance(transform.localPosition, target);
            var duration = distance / speed;
            return transform.DOLocalMove(target, duration);
        }

        public static Tween DoMoveXZ(this Transform transfom, Vector3 target, float duration)
        {
            transfom.DOMoveX(target.x, duration);
            return transfom.DOMoveZ(target.z, duration);
        }

        public static Tweener DoLocalTransform(this Component transform, Vector3 position, Vector3 rotation, float duration)
        {
            transform.transform.DOLocalMove(position, duration);
            return transform.transform.DOLocalRotate(rotation, duration);
        }

        public static Tweener DoLocalTransform(this Component transform, Vector3 position, Quaternion rotation, float duration)
        {
            transform.transform.DOLocalMove(position, duration);
            return transform.transform.DOLocalRotate(rotation.eulerAngles, duration);
        }

        public static Tweener DoTransform(this Component transform, Vector3 targetPositin, Quaternion targetRotation, float duration)
        {
            transform.transform.DOMove(targetPositin, duration);
            return transform.transform.DORotate(targetRotation.eulerAngles, duration);
        }

        public static Tweener DoTransform(this Transform transform, Transform target, float duration)
        {
            transform.DOMove(target.position, duration);
            return transform.DORotate(target.eulerAngles, duration);
        }

        public static Tweener DoTransform(this Component monoBehaviour, Component target, float duration)
        {
            return monoBehaviour.transform.DoTransform(target.transform, duration);
        }

        public static Tweener DoInsert(this Transform transform, Transform target, float duration)
        {
            transform.DOMove(target.position, duration);
            return transform.DoDisapear(duration);
        }

        public static Tweener DoInsert(this Transform transform, Vector3 target, float duration)
        {
            transform.DOMove(target, duration);
            return transform.DoDisapear(duration);
        }

        public static Tweener DoDisapear(this Transform transform, float duration)
        {
            return transform.DOScale(TweenHelper.zeroSize, duration);
        }

        public static Tweener DoAppears(this Transform transform, float duration)
        {
            var temp = transform.localScale;
            transform.localScale = TweenHelper.zeroSize;
            return transform.DOScale(temp, duration);
        }
    }
}