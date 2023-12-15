using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityTools.Extentions
{
    public static class EventSystemExtentions
    {
        private static RaycastHit[] _hits = new RaycastHit[1];

        public static bool TryRaycast<T>(this EventSystem eventSystem, Vector2 position, out T component)
        {
            var pointerEventData = new PointerEventData(eventSystem) { position = position };
            var raycastResults = new List<RaycastResult>();
            eventSystem.RaycastAll(pointerEventData, raycastResults);
            component = raycastResults
                .Select(x => x.gameObject.GetComponent<T>())
                .FirstOrDefault(x => x != null);
            return component != null;
        }

        public static bool TryRaycastNonAlloc<T>(this EventSystem eventSystem, Vector2 position, LayerMask target, out T result) where T : class
        {
            result = null;
            _hits[0] = new RaycastHit();
            var ray = new Ray((Vector3)position - (Vector3.forward * 10), Vector3.forward);
            var hitCount = Physics.RaycastNonAlloc(ray, _hits);
            if (hitCount > 0)
            {
                result = _hits[0].collider.gameObject.GetComponent<T>();
            }
            return result != null;
        }
    }
}