using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageGateAudio : MonoBehaviour {

    public PackageGateController packageGateController;

    public Alarm alarm;

	void Start ()
    {
        packageGateController.OnValidPackage += GoodPackageSfx;
        packageGateController.OnInvalidPackage += WrongPackageSfx;
        packageGateController.OnObjectiveExpired += ObjectiveExpiredSfx;	
	}

    private void GoodPackageSfx(PackageGateController _gate, PackageController _package)
    {
        AudioManager.Instance.Play("Gate_GoodPackage", transform.position);
    }

    private void WrongPackageSfx(PackageGateController _gate, PackageController _package)
    {
        AudioManager.Instance.Play("Gate_WrongPackage", transform.position);
    }

    private void ObjectiveExpiredSfx(PackageGateController _gate, PackageController _package)
    {
        AudioManager.Instance.Play("Gate_TimeOut", transform.position);
    }

    void Update()
    {
        if (packageGateController.GetObjective() != null && packageGateController.GetObjectiveRemainingTime() < 5f)
        {
            alarm.On();
        }
        else
        {
            alarm.Off();
        }
    }

}
