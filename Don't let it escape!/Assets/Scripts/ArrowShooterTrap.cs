using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooterTrap : MonoBehaviour
{

    public GameObject ArrowPrefab;
    public LayerMask PlayerLayerMask;
    public float flip_angle;

    private GameObject _arrow;
    private float _timer;
    private bool _arrowFired = false;
    private Vector2 direction;

    void Start() {
        if (flip_angle == 0) {
            direction = new Vector2(0, 1);
        }
        else if (flip_angle == 90) {
            direction = new Vector2(-1, 0);
        }
        else if (flip_angle == 270) {
            direction = new Vector2(1, 0);
        }
        else {
            direction = new Vector2(0, -1);
        }
    }

    void Update()
    {

        RaycastHit2D raycastHit = Physics2D.Raycast(this.GetComponent<Transform>().position, direction, 5f, PlayerLayerMask);
        Debug.DrawRay(this.GetComponent<Transform>().position, direction, Color.green);
        Debug.Log(raycastHit.collider);

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
            _arrow = Instantiate(ArrowPrefab, this.GetComponent<Transform>().position, Quaternion.Euler(0, 0, flip_angle+90f));
        }

        if (_arrowFired) {
            Vector3 pos = _arrow.GetComponent<Transform>().position;

            _arrow.GetComponent<Transform>().position = pos + new Vector3((direction * 0.02f).x, (direction * 0.02f).y, 0);

        }
    }

}
