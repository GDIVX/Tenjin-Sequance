using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent OnSpaceBarPressed;
    public UnityEvent OnLeftMousePressed;
    public UnityEvent OnRightMousePressed;
    public UnityEvent OnECSPressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) OnSpaceBarPressed.Invoke();

        if (Input.GetKeyDown(KeyCode.Mouse0)) OnLeftMousePressed.Invoke();

        if (Input.GetKeyDown(KeyCode.Mouse1)) OnRightMousePressed.Invoke();

        if (Input.GetKeyDown(KeyCode.Escape)) OnECSPressed.Invoke();
    }
}