using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrapManager : MonoBehaviour
{
    public GameObject TrapPreviewPrefab;
    public GameObject Trap1Prefab;
    public GameObject TrapPanel;
    public TMP_Text TrapText;
    [SerializeField] private float StartPositionRange = 5f;

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
            if(Input.GetMouseButtonDown(0) && PositionValid())
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

    private bool PositionValid()
    {
        Vector3 startPos = GameObject.Find("Start").GetComponent<Transform>().position;
        float distance = (_mousePos - startPos).magnitude;
        if(distance < StartPositionRange)
        {
            return false;
        }
        return true;
    }

    public void TrapButtonClicked()
    {
        _placingTrap = true;
        TrapPanel.SetActive(false);
        UpdateMousePosition();
        _trapPreview = Instantiate(TrapPreviewPrefab, _mousePos, Quaternion.identity);
    }

    public void TrapPlaced()
    {
        Instantiate(Trap1Prefab, _mousePos, Quaternion.identity);
        Destroy(_trapPreview);
        _placingTrap = false;
        GameObject.Find("GameManager").GetComponent<GameManager>().EndTrapPlacementPhase();
    }

    public void UpdateTrapText(string text)
    {
        TrapText.text = text;
    }
}
