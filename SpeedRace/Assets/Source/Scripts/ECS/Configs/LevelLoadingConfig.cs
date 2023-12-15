using UnityEngine;

[CreateAssetMenu(menuName = "Config/LevelLoadingConfig")]
public sealed class LevelLoadingConfig : ScriptableObject
{
    [field: SerializeField] public string path { get; private set; }
}
