using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageGateController : MonoBehaviour
{
    public Transform objectiveDisplayTransform;
    public PackageController packagePrefab;

    [Header("Gameplay")]
    public float objectiveExpirationTime = 20.0f;
    public float activationTime = 0.0f;
    public bool autoGetObjective = true;

    [Header("Objective Display")]
    public float spinSpeed = 20.0f;
    public float viewAngleAmplitude = 20.0f;
    public float viewAnglePhase = 3.0f;
    public float wiggleAmplitude = 1.0f;
    public float wigglePhase = 1.0f;
    public Text timerText;

    public delegate void PackageGateDelegate(PackageGateController _gate, PackageController _package);

    public event PackageGateDelegate OnValidPackage;
    public event PackageGateDelegate OnInvalidPackage;
    public event PackageGateDelegate OnObjectiveExpired;

    public PackageController GetObjective()
    {
        return m_objective;
    }

    public void SetObjective(PackageController _package)
    {
        m_objective = _package;

        if (m_objectiveProxy == null)
            return;

        if (m_objective == null)
        {
            m_objectiveProxy.gameObject.SetActive(false);
            timerText.enabled = false;
        }
        else
        {
            timerText.enabled = true;
            m_objectiveProxy.gameObject.SetActive(true);
            m_objectiveProxy.SetShape(m_objective.GetShape());
            m_objectiveProxy.SetColor(m_objective.GetColor());
            m_objectiveProxy.SetLayout(m_objective.GetLayout());

            m_objectiveExpirationTimer = objectiveExpirationTime;
        }
    }

	// Use this for initialization
	void Start ()
    {
        m_game = FindObjectOfType<GameController>();

        m_objectiveProxy = Instantiate(packagePrefab, objectiveDisplayTransform);
        m_objectiveProxy.gameObject.layer = 0;
        Rigidbody rb = m_objectiveProxy.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        m_objectiveProxy.gameObject.GetComponent<Collider>().enabled = false;
        m_objectiveProxy.transform.localPosition = Vector3.zero;
        m_objectiveProxy.outline.enabled = false;

        SetObjective(m_objective);
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_lifeTimer += Time.deltaTime;
        if (autoGetObjective && GetObjective() == null && m_lifeTimer > activationTime)
        {
            PackageController randomPackage = FindValidObjective();
            SetObjective(randomPackage);
        }

		if (GetObjective() != null)
        {
            m_objectiveExpirationTimer -= Time.deltaTime;

            m_objectiveProxyAnimationTimer += Time.deltaTime;
            m_objectiveProxyAnimationTimer = m_objectiveProxyAnimationTimer % (viewAnglePhase * wigglePhase);

            m_spinAngle += spinSpeed * Time.deltaTime;
            m_spinAngle = m_spinAngle % 360.0f;
            float viewAngle = ((Mathf.Sin(Mathf.PI * 2.0f * m_objectiveProxyAnimationTimer / viewAnglePhase) * 2.0f) - 1.0f) * viewAngleAmplitude;
            m_objectiveProxy.transform.rotation = Quaternion.AngleAxis(m_spinAngle, Vector3.up) * Quaternion.AngleAxis(viewAngle, Vector3.right);

            float wiggle = 1.0f + ((Mathf.Sin(Mathf.PI * 2.0f * m_objectiveProxyAnimationTimer / wigglePhase) * 2.0f) - 1.0f) * wiggleAmplitude;
            m_objectiveProxy.transform.localScale = Vector3.one * wiggle;

            int minutes = (int)(m_objectiveExpirationTimer / 60.0f);
            int seconds = (int)(m_objectiveExpirationTimer % 60.0f);
            timerText.text = "" + minutes.ToString("00") + ":" + seconds.ToString("00");

            if (m_objectiveExpirationTimer < 0.0f)
            {
                if (OnObjectiveExpired != null) OnObjectiveExpired(this, m_objective);

                PackageController newObjective = FindValidObjective();
                SetObjective(newObjective);
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (m_objective == null)
            return;

        PackageController package = other.GetComponent<PackageController>();
        if (package && package.canValidateWinCondition)
        {
            if(package.GetShape() == m_objectiveProxy.GetShape() && package.GetColor() == m_objectiveProxy.GetColor() && package.GetLayout() == m_objectiveProxy.GetLayout())
            {
                if (OnValidPackage != null) OnValidPackage(this, package);

                PackageController newPackage = FindValidObjective();
                SetObjective(newPackage);
            }
            else
            {
                if (OnInvalidPackage != null) OnInvalidPackage(this, package);
            }

            package.PrettyDestroy();
            package.canValidateWinCondition = false;
        }
    }

    public PackageController FindValidObjective()
    {
        List<PackageController> packages = new List<PackageController>();
        m_game.GetPackages(ref packages);

        List<PackageGateController> gates = new List<PackageGateController>();
        m_game.GetGates(ref gates);

        foreach(PackageGateController gate in gates)
        {
            if (gate.GetObjective())
            {
                packages.Remove(gate.GetObjective());
            }
        }

        if (packages.Count == 0)
            return null; 

        return packages[Random.Range(0, packages.Count)];
    }

    public float GetObjectiveRemainingTime()
    {
        return m_objectiveExpirationTimer;
    }

    float m_lifeTimer = 0.0f;
    float m_objectiveExpirationTimer = 0.0f;

    GameController m_game;
    PackageController m_objectiveProxy;
    PackageController m_objective;
    float m_objectiveProxyAnimationTimer = 0.0f;
    float m_spinAngle = 0.0f;
}
