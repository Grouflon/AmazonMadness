using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        m_gates = new List<PackageGateController>();
        m_spawners = new List<PackageSpawnerController>();
        m_packages = new List<PackageController>();

        PackageGateController[] gates = FindObjectsOfType<PackageGateController>();
        foreach(PackageGateController gate in gates)
        {
            m_gates.Add(gate);
            gate.OnValidPackage += OnValidPackage;
            gate.OnInvalidPackage += OnInvalidPackage;
        }

        PackageSpawnerController[] spawners = FindObjectsOfType<PackageSpawnerController>();
        foreach (PackageSpawnerController spawner in spawners)
        {
            m_spawners.Add(spawner);
            spawner.OnPackageSpawned += OnPackageSpawned;
        }

        PackageController[] packages = FindObjectsOfType<PackageController>();
        foreach (PackageController package in packages)
        {
            m_packages.Add(package);
        }
    }
	
	// Update is called once per frame
	void Update()
    {
        
	}

    public PackageController PickRandomPackage()
    {
        return m_packages[Random.Range(0, m_packages.Count)];
    }

    void OnPackageSpawned(PackageSpawnerController _spawner, PackageController _package)
    {
        m_packages.Add(_package);
    } 

    void OnValidPackage(PackageGateController _gate, PackageController _package)
    {
        m_packages.Remove(_package);
    }

    void OnInvalidPackage(PackageGateController _gate, PackageController _package)
    {
        m_packages.Remove(_package);
    }

    public void GetPackages(ref List<PackageController> _packages)
    {
        _packages.AddRange(m_packages);
    }

    public void GetGates(ref List<PackageGateController> _gates)
    {
        _gates.AddRange(m_gates);
    }

    List<PackageGateController> m_gates;
    List<PackageSpawnerController> m_spawners;
    List<PackageController> m_packages;
}
