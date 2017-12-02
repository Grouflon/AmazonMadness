using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabZoneController : MonoBehaviour
{
    private void Start()
    {
        m_grabbableObjects = new Dictionary<GameObject, int>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!m_grabbableObjects.ContainsKey(other.gameObject))
        {
            m_grabbableObjects.Add(other.gameObject, 0);
        }
        ++m_grabbableObjects[other.gameObject];
    }

    private void OnTriggerExit(Collider other)
    {
        --m_grabbableObjects[other.gameObject];
        if (m_grabbableObjects[other.gameObject] == 0)
        {
            m_grabbableObjects.Remove(other.gameObject);
        }
    }

    public void GetGrabbableObjects(ref List<GameObject> _list)
    {
        foreach (KeyValuePair<GameObject, int> pair in m_grabbableObjects)
        {
            _list.Add(pair.Key);
        }
    }

    Dictionary<GameObject, int> m_grabbableObjects;
}
