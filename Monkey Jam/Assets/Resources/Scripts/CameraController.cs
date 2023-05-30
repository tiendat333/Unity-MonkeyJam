using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //VARIABLES
    [SerializeField] private float mouseSensivity;
    private PlayerStats stats;

    //REFERENCES  
    private Transform parent;

    private void Start()
    {
        LockCursor();
        GetReferences();
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (!stats.IsDead())
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;

            parent.Rotate(Vector3.up, mouseX);
        }
        else if (Cursor.lockState == CursorLockMode.Locked)        
            UnlockCursor();        
        
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void GetReferences()
    {
        parent = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
        stats = GetComponentInParent<PlayerStats>();
    }
}
