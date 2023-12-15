using UnityEngine;
using UnityTools.Collections;

public class SimpleObservableUIList<UIElement, ModelElement> : SimpleUIList<UIElement, ModelElement>
        where UIElement : MonoBehaviour, ISimpleUIListElement<ModelElement>
{
    public void Init(SimpleObservableList<ModelElement> model)
    {
        model.Changed += Redraw;
        base.Init(model);
    }
}
