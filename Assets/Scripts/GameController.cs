using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float startFadeInTime = 2.0f;

    public int validPackageScore = 5;
    public int invalidPackageScore = -10;
    public int expiredPackageScore = -20;
    public int loseScoreLimit = -50;
    public int oneStarScore = 10;
    public int twoStarScore = 20;
    public int threeStarScore = 30;
    public float levelDuration = 30.0f;
    public float levelWarmUpDuration = 5.0f;

    public int GetScore()
    {
        return m_score;
    }

    public float GetRemainingTime()
    {
        return m_isGameOver ? 0.0f : Mathf.Max(0.0f, levelDuration - m_timer);
    }

    public bool IsGameStarted()
    {
        return m_isGameStarted;
    }

    // Use this for initialization
    void Start()
    {
        Blackout blackout = FindObjectOfType<Blackout>();
        if (blackout)
        {
            blackout.FadeTo(Color.black, 0.0f);

            Color c = Color.black;
            c.a = 0.0f;
            blackout.FadeTo(c, 1.0f);
        }

        m_gates = new List<PackageGateController>();
        m_spawners = new List<PackageSpawnerController>();
        m_packages = new List<PackageController>();

        PackageGateController[] gates = FindObjectsOfType<PackageGateController>();
        foreach(PackageGateController gate in gates)
        {
            m_gates.Add(gate);
            gate.OnValidPackage += OnValidPackage;
            gate.OnInvalidPackage += OnInvalidPackage;
            gate.OnObjectiveExpired += OnObjectiveExpired;
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
        if (!m_isGameStarted)
        {
            if (Time.time < levelWarmUpDuration)
            {
                return;
            }
            m_isGameStarted = true;
        }

        if (m_isGameOver)
            return;

        m_timer += Time.deltaTime;

        if (m_timer > levelDuration)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        m_isGameOver = true;

        foreach (PackageGateController gate in m_gates)
        {
            gate.autoGetObjective = false;
            gate.SetObjective(null);
        }

        foreach (PackageSpawnerController spawner in m_spawners)
        {
            spawner.enabled = false;
        }
    }

    void AddToScore(int _value)
    {
        m_score += _value;
        if (m_score <= loseScoreLimit)
        {
            GameOver();
        }
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

        AddToScore((int)_gate.GetObjectiveRemainingTime() + validPackageScore);
    }

    void OnInvalidPackage(PackageGateController _gate, PackageController _package)
    {
        m_packages.Remove(_package);

        foreach (PackageGateController gate in m_gates)
        {
            if (gate.GetObjective() == _package)
            {
                gate.SetObjective(gate.FindValidObjective());
            }
        }

        AddToScore(invalidPackageScore);
    }

    void OnObjectiveExpired(PackageGateController _gate, PackageController _package)
    {
        AddToScore(expiredPackageScore);
    }

    public void GetPackages(ref List<PackageController> _packages)
    {
        _packages.AddRange(m_packages);
    }

    public void GetGates(ref List<PackageGateController> _gates)
    {
        _gates.AddRange(m_gates);
    }

    int m_score = 0;
    bool m_isGameStarted = false;
    bool m_isGameOver;
    float m_timer = 0.0f;

    List<PackageGateController> m_gates;
    List<PackageSpawnerController> m_spawners;
    List<PackageController> m_packages;
}
