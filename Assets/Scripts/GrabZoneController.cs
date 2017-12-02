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
            PackageController package = other.GetComponent<PackageController>();
            if (package != null)
            {
                package.Destroyed += OnPackageDestroyed;
            }
        }
        ++m_grabbableObjects[other.gameObject];
    }

    private void OnTriggerExit(Collider other)
    {
        --m_grabbableObjects[other.gameObject];
        if (m_grabbableObjects[other.gameObject] == 0)
        {
            m_grabbableObjects.Remove(other.gameObject);
            PackageController package = other.GetComponent<PackageController>();
            if (package != null)
            {
                package.Destroyed -= OnPackageDestroyed;
            }
        }
    }

    public void GetGrabbableObjects(ref List<GameObject> _list)
    {
        foreach (KeyValuePair<GameObject, int> pair in m_grabbableObjects)
        {
            _list.Add(pair.Key);
        }
    }

    void OnPackageDestroyed(PackageController _package)
    {
        m_grabbableObjects.Remove(_package.gameObject);
        _package.Destroyed -= OnPackageDestroyed;
    }

    Dictionary<GameObject, int> m_grabbableObjects;
}
