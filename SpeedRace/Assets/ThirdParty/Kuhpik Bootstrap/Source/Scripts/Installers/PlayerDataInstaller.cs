using NaughtyAttributes;
using UnityEngine;

namespace Kuhpik
{
    public class PlayerDataInstaller : Installer
    {
        [SerializeField] private bool isTesting;
        [SerializeField][ShowIf("isTesting")] private SaveData testData;

        public override int Order => 2;

        private const string saveKey = "saveKey";
        private SaveData data;

        public override void Process()
        {
            data = HandlePlayerData();

            Bootstrap.Instance.itemsToInject.Add(data);
            Bootstrap.Instance.EventSave += Save;
        }

        private SaveData HandlePlayerData()
        {
#if UNITY_EDITOR
            return isTesting ? testData : Load();
#else
            return Load();
#endif
        }

        private void Save()
        {
            SaveExtension.Save(data, saveKey);
        }

        private SaveData Load()
        {
            return SaveExtension.Load(saveKey, new SaveData());
        }
    }
}
