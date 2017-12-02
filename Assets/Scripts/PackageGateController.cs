using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageGateController : MonoBehaviour
{
    public Transform objectiveDisplayTransform;
    public PackageController packagePrefab;

    public delegate void PackageGateDelegate(PackageGateController _gate, PackageController _package);

    public event PackageGateDelegate OnValidPackage;
    public event PackageGateDelegate OnInvalidPackage;

    public void SetObjectiveEnabled(bool _enabled)
    {
        m_objectiveEnabled = _enabled;

        m_objective.enabled = m_objectiveEnabled;
    }

    public bool IsObjectiveEnabled()
    {
        return m_objectiveEnabled;
    }

    public void SetObjective(PackageShape _shape, PackageColor _color, PackageLayout _layout)
    {
        m_objective.SetShape(_shape);
        m_objective.SetColor(_color);
        m_objective.SetLayout(_layout);
    }

	// Use this for initialization
	void Start ()
    {
        m_objective = Instantiate(packagePrefab, objectiveDisplayTransform);
        m_objective.gameObject.layer = 0;
        Rigidbody rb = m_objective.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        m_objective.gameObject.GetComponent<Collider>().enabled = false;
        m_objective.enabled = m_objectiveEnabled;
        m_objective.transform.localPosition = Vector3.zero;

        SetObjective(PackageShape.Box, PackageColor.Red, PackageLayout.Layout2);
        SetObjectiveEnabled(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!m_objectiveEnabled)
            return;

        PackageController package = other.GetComponent<PackageController>();
        if (package && package.canValidateWinCondition)
        {
            if(package.GetShape() == m_objective.GetShape() && package.GetColor() == m_objective.GetColor() && package.GetLayout() == m_objective.GetLayout())
            {
                if (OnValidPackage != null) OnValidPackage(this, package); 
            }
            else
            {
                if (OnInvalidPackage != null) OnInvalidPackage(this, package);
            }

            package.PrettyDestroy();
            package.canValidateWinCondition = false;
        }
    }

    PackageController m_objective;
    bool m_objectiveEnabled = false;
}
