using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeetController : MonoBehaviour
{
    private void Start()
    {
        m_touchedObjects = new Dictionary<GameObject, int>();
    }

    public bool IsTouching()
    {
        return m_touchedObjects.Count != 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!m_touchedObjects.ContainsKey(other.gameObject))
        {
            m_touchedObjects.Add(other.gameObject, 0);
        }
        ++m_touchedObjects[other.gameObject];
    }

    private void OnTriggerExit(Collider other)
    {
        --m_touchedObjects[other.gameObject];
        if (m_touchedObjects[other.gameObject] == 0)
        {
            m_touchedObjects.Remove(other.gameObject);
        }
    }

    Dictionary<GameObject, int> m_touchedObjects;
}
