using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooterTrap : MonoBehaviour
{

    public GameObject ArrowPrefab;
    public LayerMask PlayerLayerMask;

    private GameObject _arrow;
    private float _timer;
    private bool _arrowFired = false;

    void Update()
    {

        RaycastHit2D raycastHit = Physics2D.Raycast(this.GetComponent<Transform>().position, new Vector2(1, 0), 5f, PlayerLayerMask);

        if (_arrowFired) {
            _timer += Time.deltaTime;
            if (_timer > 1.5f) {
                Destroy(_arrow);
                _arrowFired = false;
            }
        }

        if (raycastHit.collider != null && !_arrowFired) {
            _timer = 0f;
            _arrowFired = true;
            _arrow = Instantiate(ArrowPrefab, this.GetComponent<Transform>().position, Quaternion.identity);
        }

        if (_arrowFired) {
            Vector3 pos = _arrow.GetComponent<Transform>().position;
            Quaternion rotation = _arrow.GetComponent<Transform>().localRotation;
            Debug.Log(rotation);
            _arrow.GetComponent<Transform>().position = new Vector3(pos.x + 0.02f, pos.y, pos.z);
        }
    }

}
