using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace Kuhpik
{
    public class UIScreen : MonoBehaviour, IUIScreen
    {
        [SerializeField][BoxGroup("Settings")] private bool shouldOpenWithState;
        [SerializeField][BoxGroup("Settings")][ShowIf("shouldOpenWithState")] private GameStateID[] statesToOpenWith;

        //You will get the idea once you use it
        [SerializeField][BoxGroup("Elements")] private bool hideElementsOnOpen;
        [SerializeField][BoxGroup("Elements")] private bool showElementsOnHide;

        [SerializeField][BoxGroup("Elements")][ShowIf("hideElementsOnOpen")] private GameObject[] elementsToHideOnOpen;
        [SerializeField][BoxGroup("Elements")][ShowIf("showElementsOnHide")] private GameObject[] elementsToShowOnHide;
        private HashSet<GameStateID> statesMap;

        private void Awake()
        {
            statesMap = new HashSet<GameStateID>(statesToOpenWith);
        }

        public virtual void Open()
        {
            foreach (var element in elementsToHideOnOpen)
            {
                element.SetActive(false);
            }

            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            foreach (var element in elementsToShowOnHide)
            {
                element.SetActive(true);
            }

            gameObject.SetActive(false);
        }

        /// <summary>
        /// Subscribe is called on Awake()
        /// </summary>
        public virtual void Subscribe()
        {
        }

        internal void TryOpenWithState(GameStateID id)
        {
            if (shouldOpenWithState && statesMap.Contains(id))
            {
                Open();
            }
        }
    }
}