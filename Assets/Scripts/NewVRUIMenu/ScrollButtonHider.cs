using UnityEngine;

public class ScrollButtonHider : MonoBehaviour
{
    public VRUIScrollPanelBehaviour scrollPanelBehaviour;

    private void LateUpdate()
    {
        if (scrollPanelBehaviour.MaxDisplayedElements >= scrollPanelBehaviour.transform.childCount)
        {
            gameObject.SetActive(false);
        }
        enabled = false;
    }
}
