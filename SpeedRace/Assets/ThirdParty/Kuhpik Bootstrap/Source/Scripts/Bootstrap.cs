﻿using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = System.Object;

namespace Kuhpik
{
    [DefaultExecutionOrder(500)]
    public class Bootstrap : Singleton<Bootstrap>
    {
        [SerializeField] private GameConfig config;

        [field: SerializeField] public SaveData PlayerData { get; private set; }
        [field: SerializeField] public GameData GameData { get; private set; }

        internal GameStateID[] launchStates;
        internal Dictionary<Type, GameSystem> systems;
        internal List<Object> itemsToInject = new List<Object>();

        internal event Action GamePreStartEvent;
        internal event Action GameStartEvent;
        internal event Action GameEndEvent;
        internal event Action EventSave;

        internal event Action<GameStateID> StateEnterEvent;
        internal event Action<GameStateID> StateExitEvent;

        internal FSMProcessor<GameStateID, GameState> fsm;
        private GameState currentState;
        private GameState lastState;

        private void Start()
        {
            itemsToInject.Add(config);
            itemsToInject.Add(new GameData());

            systems = FindObjectsOfType<GameSystem>().ToDictionary(x => x.GetType(), x => x);

            ProcessInstallers();

            GameData = itemsToInject.First(x => x.GetType() == typeof(GameData)) as GameData;
            PlayerData = itemsToInject.First(x => x.GetType() == typeof(SaveData)) as SaveData;

            GamePreStartEvent?.Invoke();
            GameStartEvent?.Invoke();

            LaunchStates();
        }

        private void Update()
        {
            currentState.Update();
        }

        private void LateUpdate()
        {
            currentState.LateUpdate();
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdate();
        }

        public void GameRestart(int sceneIndex)
        {
            GameEndEvent?.Invoke();
            SaveGame();
            SceneManager.LoadScene(sceneIndex);
        }

        /// <summary>
        /// Saves all changes in Player Data to PlayerPrefs.
        /// On level complete\fail use GameRestart() instead.
        /// </summary>
        [Button]
        public void SaveGame()
        {
            EventSave?.Invoke();
        }

        public void ChangeGameState(GameStateID id)
        {
            if (currentState != null)
            {
                StateExitEvent?.Invoke(currentState.ID);
                lastState = currentState;
            }

            fsm.ChangeState(id);
            StateEnterEvent?.Invoke(id);
            currentState = fsm.CurrentState;
        }

        public T GetSystem<T>() where T : class
        {
            return systems[typeof(T)] as T;
        }

        public GameStateID GetCurrentGamestateID()
        {
            return currentState.ID;
        }

        public GameStateID GetLastGamestateID()
        {
            return lastState.ID;
        }

        private void LaunchStates()
        {
            for (int i = 0; i < launchStates.Length; i++)
            {
                ChangeGameState(launchStates[i]);
            }
        }

        public T GetScreen<T>() where T : UIScreen
        {
            return UIManager.GetUIScreen<T>();
        }

        private void ProcessInstallers()
        {
            var installers = FindObjectsOfType<Installer>().OrderBy(x => x.Order).ToArray();

            for (int i = 0; i < installers.Length; i++)
            {
                installers[i].Process();
            }
        }
    }
}