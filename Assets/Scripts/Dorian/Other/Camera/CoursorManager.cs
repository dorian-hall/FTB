using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoursorManager : MonoBehaviour
{
    int UILayer;
    bool ishovering;
    public bool IsHovering => ishovering;

    [SerializeField]
    Texture2D UICursor,InGameCursor;

    public static CoursorManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


    private void Start()
    {
        UILayer = LayerMask.NameToLayer("UI");
        Cursor.SetCursor(InGameCursor, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        if(IsPointerOverUIElement() != ishovering)
        {
            ishovering = !ishovering;
            if (ishovering) { Cursor.SetCursor(UICursor, Vector2.zero, CursorMode.Auto); }
            else { Cursor.SetCursor(InGameCursor, Vector2.zero, CursorMode.Auto); }
        }
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UILayer)
                return true;
        }
        return false;
    }


    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}
