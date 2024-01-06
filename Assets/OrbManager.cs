using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrbManager : MonoBehaviour
{
    private Queue<GameObject> orbs = new Queue<GameObject>();
    private bool waypointsActive = false;
    private GameObject currentOrb;
    [SerializeField] private GameObject waypointPrefab;
    private GameObject waypoint;
    [SerializeField] private Camera playerCamera;

    private void Start() {
        foreach (Transform t in transform) {
            if (t.CompareTag("Orb")) {
                orbs.Enqueue(t.gameObject);
            }
        }
    }

    private void Update() {
        if (waypointsActive) {
            if (currentOrb == null || !currentOrb.activeInHierarchy) {
                if (orbs.Count == 0) {
                    waypointsActive = false;
                    Destroy(waypoint);
                }
                currentOrb = orbs.Dequeue();
            }
            else {
                if (currentOrb.GetComponent<SpriteRenderer>().isVisible) {
                    waypoint.SetActive(false);
                }
                else {
                    Vector3 orbScreenPoint = playerCamera.WorldToScreenPoint(currentOrb.transform.position);
                    Vector3 newOrbScreenPoint = new Vector3(
                        Mathf.Clamp(orbScreenPoint.x, 40, Screen.width - 40),
                        Mathf.Clamp(orbScreenPoint.y, 40, Screen.height - 40),
                        9
                    );
                    waypoint.SetActive(true);
                    waypoint.transform.position = playerCamera.ScreenToWorldPoint(newOrbScreenPoint);
                }
            }

        }
    }


    public void StartWaypoints() {
        waypointsActive = true;
        waypoint = Instantiate(waypointPrefab);
    }
}
