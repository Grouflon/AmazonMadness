﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageGateAudio : MonoBehaviour {

    public PackageGateController packageGateController;

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
        AudioManager.Instance.Play("Gate_WrongPackage", transform.position);
    }

}