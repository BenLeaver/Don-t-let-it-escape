using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public GameObject TrapPreviewPrefab;
    public GameObject Trap1Prefab;
    public GameObject TrapPanel;


    private Vector3 _mousePos;
    private GameObject _trapPreview;
    private bool _placingTrap = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_placingTrap)
        {
            UpdateMousePosition();
            _trapPreview.transform.position = _mousePos;
            if(Input.GetMouseButtonDown(0))
            {
                TrapPlaced();
            }
        }
    }

    public void UpdateMousePosition()
    {
        _mousePos = Input.mousePosition;
        _mousePos = Camera.main.ScreenToWorldPoint(_mousePos);
    }

    public void TrapButtonClicked()
    {
        _placingTrap = true;
        Debug.Log("Trap 1 Clicked!");
        TrapPanel.SetActive(false);
        UpdateMousePosition();
        _trapPreview = Instantiate(TrapPreviewPrefab, _mousePos, Quaternion.identity);
    }

    public void TrapPlaced()
    {
        Instantiate(Trap1Prefab, _mousePos, Quaternion.identity);
        Destroy(_trapPreview);
        TrapPanel.SetActive(true);
        _placingTrap = false;
    }
}
