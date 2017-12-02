using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSpawnerController : MonoBehaviour
{
    public float spawnRate = 5.0f;

    public PackageController BoxPrefab;
    /*public GameObject CylinderPrefab;
    public GameObject FlatPrefab;
    public GameObject SpherePrefab;
    public GameObject PyramidPrefab;
    public GameObject RectanglePrefab;*/

    // Use this for initialization
    void Start () {
        m_timer = spawnRate;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (spawnRate == 0.0f)
            return;

        m_timer -= Time.deltaTime;
        while(m_timer <= 0.0f)
        {
            m_timer += spawnRate;
            SpawnRandomPackage();
        }
	}

    public PackageController SpawnRandomPackage()
    {
        PackageController package = null;
        int shapeEnumValue = Random.Range(0, System.Enum.GetNames(typeof(PackageShape)).Length);
        PackageShape shape = (PackageShape)shapeEnumValue;

        switch(shape)
        {
            case PackageShape.Box:
                {
                    package = Instantiate(BoxPrefab, transform.position, transform.rotation);
                }
                break;

            default:
                break;
        }

        int colorEnumValue = Random.Range(0, System.Enum.GetNames(typeof(PackageColor)).Length);
        PackageColor color = (PackageColor)colorEnumValue;
        package.SetColor(color);

        return package;
    }

    private float m_timer;
}
