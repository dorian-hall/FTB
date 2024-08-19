using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    Controls _Controls;
    public GameObject HoveredObject;

    public GameObject SelectedPrefab;
    [SerializeField] LayerMask _LayerMask;
    

    private void Awake()
    {
        _Controls = new Controls();
        _Controls.actionmap.MouseLeft.performed += ctx => PlaceObject();
        _Controls.actionmap.MouseRight.performed += ctx => RemoveObject();

        _Controls.actionmap.E.performed += ctx => Rotate(90);
        _Controls.actionmap.Q.performed += ctx => Rotate(-90);
    }

    private void Rotate(float offset)
    {
        if (CoursorManager.instance.IsHovering) { return; }
        if (HoveredObject == null) { return; }
        Vector2Int pos = TileManager.Instance.GetTilePos[HoveredObject];
        
        TileManager.Instance.TileMap[pos.x, pos.y].Rotate(offset);
    }
    public void RemoveSelectedPrefab()
    {
        SelectedPrefab = null;
    }

    public void ChangeSelectedPrefab(GameObject Prefab)
    {
        SelectedPrefab = Prefab;
    }
    void RemoveObject()
    {
        if (CoursorManager.instance.IsHovering) { return; }
        if (HoveredObject == null) { return; }
        Vector2Int pos = TileManager.Instance.GetTilePos[HoveredObject];
        TileManager.Instance.TileMap[pos.x, pos.y].Remove();
    }
    void PlaceObject()
    {
        if (CoursorManager.instance.IsHovering) { return; }
        if (HoveredObject == null) { return; }
        if(SelectedPrefab == null) { return; }

        Vector2Int pos = TileManager.Instance.GetTilePos[HoveredObject];
        TileManager.Instance.TileMap[pos.x, pos.y].Place(SelectedPrefab);
    }
    private void FixedUpdate()
    {
        CastRay();
    }
    void CastRay()
    {
        if (CoursorManager.instance.IsHovering) { return; }

        Vector2 mouseinput = _Controls.actionmap.Mouse.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mouseinput);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, _LayerMask))
        {
            if(HoveredObject != null) HoveredObject.transform.position += Vector3.down/2;
            HoveredObject = hit.collider.gameObject;
            HoveredObject.transform.position += Vector3.up/2;
        }
        else
        {
            if (HoveredObject != null) HoveredObject.transform.position += Vector3.down / 2;
            HoveredObject = null;
        }
    }


    private void OnEnable()
    {
        _Controls.Enable();
    }

    private void OnDisable()
    {
        _Controls.Disable();
    }
}
