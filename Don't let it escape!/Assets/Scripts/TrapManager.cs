using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrapManager : MonoBehaviour
{
    public GameObject TrapPreviewPrefabSpike;
    public GameObject TrapPreviewPrefabArrowShooter;
    public GameObject TrapPrefabObstalce;
    public GameObject TrapPreviewPrefabObstacle;
    public GameObject TrapPrefabSpike;
    public GameObject TrapPrefabArrowShooter;
    public GameObject TrapPanel;
    public TMP_Text TrapText;
    [SerializeField] private float StartPositionRange = 5f;

    private Vector3 _mousePos;
    private GameObject _trapPreview;
    private GameObject _currentTrapPrefab;
    private GameObject _trap;
    private GameObject _currentTrapPrefabPreview;
    private Quaternion _currentTrapPrefabRotation;
    private bool _placingTrap = false;
    private float _flip_angle = 270;
    private int _traps_placed_counter = 0;
    
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
            if (Input.GetKeyDown(KeyCode.F)) {
                _flip_angle += 90f;
                if (_flip_angle >= 360f) {
                    _flip_angle = 0f;
                }
                Debug.Log(_flip_angle);
                Destroy(_trapPreview);
                _trapPreview = Instantiate(_currentTrapPrefabPreview, _mousePos, Quaternion.Euler(0f, 0f, _flip_angle));
              //  _currentTrapPrefab.GetComponent<Transform>().localRotation = Quaternion.Euler(0f, 0f, (_flip? 90f : -90f));
            }
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

    public void TrapButtonSpikeClicked()
    {
        _placingTrap = true;
        TrapPanel.SetActive(false);
        UpdateMousePosition();
        _currentTrapPrefabPreview = TrapPreviewPrefabSpike;
        _currentTrapPrefab = TrapPrefabSpike;
        _trapPreview = Instantiate(TrapPreviewPrefabSpike, _mousePos, Quaternion.identity);
    }

    public void TrapButtonArrowClicked()
    {
        _placingTrap = true;
        TrapPanel.SetActive(false);
        UpdateMousePosition();
        _currentTrapPrefabPreview = TrapPreviewPrefabArrowShooter;
        _currentTrapPrefab = TrapPrefabArrowShooter;
        _trapPreview = Instantiate(TrapPreviewPrefabArrowShooter, _mousePos, Quaternion.Euler(0f, 0f, -90f));
    }

    public void TrapButtonObstacleClicked()
    {
        _placingTrap = true;
        TrapPanel.SetActive(false);
        UpdateMousePosition();
        _currentTrapPrefabPreview = TrapPreviewPrefabObstacle;
        _currentTrapPrefab = TrapPrefabObstalce;
        _trapPreview = Instantiate(TrapPreviewPrefabObstacle, _mousePos, Quaternion.identity);
    }

    public void TrapPlaced()
    {
        _trap = Instantiate(_currentTrapPrefab, _mousePos, _trapPreview.GetComponent<Transform>().localRotation);
        Destroy(_trapPreview);
        if (_currentTrapPrefab == TrapPrefabArrowShooter) {
            _trap.GetComponent<ArrowShooterTrap>().flip_angle = _flip_angle;
        }
        _placingTrap = false;
        _traps_placed_counter += 1;
        if (_traps_placed_counter >= 3) {
            _traps_placed_counter = 0;
            GameObject.Find("GameManager").GetComponent<GameManager>().EndTrapPlacementPhase();
        }
        else {
            TrapPanel.SetActive(true);
            _placingTrap = false;
        }
    }

    public void UpdateTrapText(string text)
    {
        TrapText.text = text;
    }
}
