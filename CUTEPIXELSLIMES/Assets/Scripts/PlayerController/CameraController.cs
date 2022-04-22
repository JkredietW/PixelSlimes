using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControllerNameSpace;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    private GameObject focus;
    private bool zoomIn;

    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Vector3 cameraZoom;
    [SerializeField] private float cameraLerpSpeed;

    private GameObject mainFocus;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            mainFocus = FindObjectOfType<PlayerController>().gameObject;
            SetFocus(mainFocus);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void LateUpdate()
    {
        FocusOnObject();
    }

    public void SetFocus(GameObject newFoxus = null, bool needToZoom = false)
    {
        focus = newFoxus == null ? mainFocus : newFoxus;
        zoomIn = needToZoom;
    }

    void FocusOnObject()
    {
        Vector3 newFocus = focus.transform.position + (zoomIn ? cameraZoom : cameraOffset);
        transform.position = Vector3.Lerp(newFocus, transform.position, cameraLerpSpeed);
    }
}
