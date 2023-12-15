using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/CarDefinition")]
public sealed class CarDefinition : ScriptableObject
{
    [field: SerializeField, ShowAssetPreview] public CarComponent prefab { get; private set; }
    [field: SerializeField] public Color color { get; private set; }
    [field: SerializeField] public string displayName { get; private set; }
    [field: SerializeField, Expandable] public GiftDefinition gift { get; private set; }

    [field: SerializeField, ShowAssetPreview] public Sprite leaderSprite { get; private set; }
    [field: SerializeField, ShowAssetPreview] public Sprite memeberSprite { get; private set; }
}
