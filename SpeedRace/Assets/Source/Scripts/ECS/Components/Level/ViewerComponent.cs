using System;
using TMPro;
using UnityEngine;

public class ViewerComponent : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;

    private string _name;

    public string name => _name;

    public void ResetName()
    {
        SetName(null);
        gameObject.SetActive(false);
    }

    public void SetName(string name)
    {
        _name = name;
        _nameText.text = _name;
        gameObject.SetActive(true);
    }
}