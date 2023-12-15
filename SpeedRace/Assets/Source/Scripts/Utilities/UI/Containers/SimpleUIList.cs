using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class SimpleUIList<UIElement, ModelElement>
    where UIElement : MonoBehaviour, ISimpleUIListElement<ModelElement>
{
    [SerializeField] private UIElement _elementPrefab;
    [SerializeField] private Transform _elementsContainer;

    private readonly List<UIElement> _uiElements = new List<UIElement>();
    private IEnumerable<ModelElement> _modelElementsSource;

    public event Action<UIElement> Destroyed;
    public event Action<UIElement, ModelElement> Created;

    public List<UIElement> uiElements => _uiElements;

    public virtual void Init(IEnumerable<ModelElement> modelElementsSource)
    {
        _modelElementsSource = modelElementsSource;
        Redraw();
    }

    private protected virtual Transform GetContainer(ModelElement model)
    {
        return _elementsContainer;
    }

    public void Redraw()
    {
        Clear();
        Spawn();
    }

    private void Clear()
    {
        foreach (var element in _uiElements)
        {
            OnElementDestroy(element);
            Destroyed?.Invoke(element);
            Object.Destroy(element.gameObject);
        }
        _uiElements.Clear();
    }

    protected virtual void OnElementDestroy(UIElement element)
    {

    }

    private protected virtual bool Filter(ModelElement model)
    {
        return true;
    }

    private void Spawn()
    {
        foreach (var modelElement in _modelElementsSource)
        {
            if (Filter(modelElement))
            {
                var uiElemnet = Object.Instantiate(_elementPrefab, GetContainer(modelElement));
                uiElemnet.Init(modelElement);
                OnElementCreate(uiElemnet, modelElement);
                Created?.Invoke(uiElemnet, modelElement);
                _uiElements.Add(uiElemnet);
            }
        }
    }

    protected virtual void OnElementCreate(UIElement element, ModelElement model)
    {

    }
}