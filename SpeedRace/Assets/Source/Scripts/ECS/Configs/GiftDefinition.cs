using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/GiftDefinition")]
public sealed class GiftDefinition : ScriptableObject
{
    [field: SerializeField, ShowAssetPreview] public Sprite sprite { get; private set; }
    [field: SerializeField] public string displayName { get; private set; }
    [field: SerializeField] public int id { get; private set; }
}
