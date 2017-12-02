using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float timeBeforeGatesActivation = 5.0f;
    public float gateActivationInterval = 5.0f;

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
            gate.OnValidPackage += OnInvalidPackage;
            gate.SetObjectiveEnabled(false);
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
        if (!m_gatesActivated && Time.time > timeBeforeGatesActivation)
        {
            m_gatesActivated = true;

            PackageController randomPackage = PickRandomPackage();
            if (randomPackage != null)
            {
                m_gates[0].SetObjectiveEnabled(true);
                m_gates[0].SetObjective(randomPackage.GetShape(), randomPackage.GetColor(), randomPackage.GetLayout());
            }
            m_gateActivationTimer = gateActivationInterval;
        }

        if (m_gatesActivated)
        {
            m_gateActivationTimer -= Time.deltaTime;
            while (m_gateActivationTimer < 0.0f)
            {
                m_gateActivationTimer += gateActivationInterval;

                foreach(PackageGateController gate in m_gates)
                {
                    if (!gate.IsObjectiveEnabled())
                    {
                        PackageController randomPackage = PickRandomPackage();
                        if (randomPackage)
                        {
                            gate.SetObjectiveEnabled(true);
                            gate.SetObjective(randomPackage.GetShape(), randomPackage.GetColor(), randomPackage.GetLayout());
                        }

                        break;
                    }
                }
            }
        }
	}

    PackageController PickRandomPackage()
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
        PackageController randomPackage = PickRandomPackage();
        if (randomPackage != null)
        {
           _gate.SetObjective(randomPackage.GetShape(), randomPackage.GetColor(), randomPackage.GetLayout());
        }
        else
        {
            _gate.SetObjectiveEnabled(false);
        }
    }

    void OnInvalidPackage(PackageGateController _gate, PackageController _package)
    {
        m_packages.Remove(_package);
    }

    bool m_gatesActivated = false;
    float m_gateActivationTimer = 0.0f;
    List<PackageGateController> m_gates;
    List<PackageSpawnerController> m_spawners;
    List<PackageController> m_packages;
}
