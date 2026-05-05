using UnityEngine;
using UnityEngine.EventSystems;

public class HiddenLinks : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouseOver = false;
    [SerializeField] GameObject hiddenLink;
    [SerializeField] GameObject shownLink;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        //Debug.Log("OnPointerEnter");
        shownLink.SetActive(false);
        hiddenLink.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        //Debug.Log("OnPointerExit");
        shownLink.SetActive(true);
        hiddenLink.SetActive(false);
    }
}
