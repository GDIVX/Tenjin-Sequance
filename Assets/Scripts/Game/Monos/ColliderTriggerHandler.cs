using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTriggerHandler : MonoBehaviour
{
    private List<GameObject> gameObjects = new List<GameObject>();

    public List<GameObject> GameObjects => gameObjects;
    

    private void OnTriggerEnter(Collider other)
    {
        if(!gameObjects.Contains(other.gameObject)) gameObjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if(gameObjects.Contains(other.gameObject)) gameObjects.Remove(other.gameObject);
    }

}
