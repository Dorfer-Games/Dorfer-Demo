using UnityEngine;

[CreateAssetMenu(menuName = "Config/BoostSystemConfig")]
public sealed class BoostSystemConfig : ScriptableObject
{
    [field: SerializeField] public float commentBoostSpeed { get; private set; }
    [field: SerializeField] public float commentBoostDuration { get; private set; }

    [field: SerializeField] public float giftBoostSpeed { get; private set; }
    [field: SerializeField] public float giftBoostDuration { get; private set; }
}
