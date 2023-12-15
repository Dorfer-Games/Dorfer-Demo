using UnityEngine;

namespace Kuhpik
{
    public abstract class GameSystem : MonoBehaviour, IGameSystem
    {
        protected SaveData save;
        protected GameConfig config;
        protected GameData game;

        public virtual void OnCustomTick() { }

        public virtual void OnFixedUpdate() { }

        public virtual void OnGameEnd() { }

        public virtual void OnGameStart() { }

        public virtual void OnInit() { }

        public virtual void OnLateUpdate() { }

        public virtual void OnStateEnter() { }

        public virtual void OnStateExit() { }

        public virtual void OnUpdate() { }

        public void Save()
        {
            Bootstrap.Instance.SaveGame();
        }

        public T GetSystem<T>() where T : GameSystem
        {
            return Bootstrap.Instance.GetSystem<T>();
        }

        public T GetScreen<T>() where T : UIScreen
        {
            return UIManager.GetUIScreen<T>();
        }

        public void ChangeGameState(GameStateID state)
        {
            Bootstrap.Instance.ChangeGameState(state);
        }

        public void ReloadScene()
        {
            Bootstrap.Instance.GameRestart(0);
        }
    }
}