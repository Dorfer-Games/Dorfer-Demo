using NaughtyAttributes;
using System.Linq;
using UnityEngine;
using UnityTools.Extentions;

public class TribuneComponent : MonoBehaviour
{
    [SerializeField, ReadOnly] private ViewerComponent[] _viewers;

    private void Awake()
    {
        _viewers = GetComponentsInChildren<ViewerComponent>()
            .Shuffle()
            .ToArray();

        ResetViewers();
    }

    public void ResetViewers()
    {
        foreach (var viewer in _viewers)
        {
            viewer.ResetName();
        }
    }

    public void AddViewer(string name)
    {
        if (NotExist(out var viewer, name))
        {
            viewer.SetName(name);
        }
    }

    private bool NotExist(out ViewerComponent viewer, string name)
    {
        for (int i = 0; i < _viewers.Length; i++)
        {
            if (_viewers[i].name == name)
            {
                viewer = null;
                return false;
            }
            if (string.IsNullOrEmpty(_viewers[i].name))
            {
                viewer = _viewers[i];
                return true;
            }
        }
        viewer = null;
        return false;
    }
}
