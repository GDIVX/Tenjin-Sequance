using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEditor : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] Mesh mesh;
    private BoxCollider boxCollider;

    [Button("Add collider")]
    public void AddCollider()
    {
        Transform child = transform.GetChild(0);
        if (!child.gameObject.TryGetComponent<BoxCollider>(out var col))
        {
            child.gameObject.AddComponent<BoxCollider>();
        }
    }

    private void OnValidate()
    {
        MeshFilter meshFilter = GetComponentInChildren<MeshFilter>();
        meshFilter.mesh = mesh;
    }
#endif

}
