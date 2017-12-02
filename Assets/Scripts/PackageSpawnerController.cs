using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSpawnerController : MonoBehaviour
{
    public float spawnRate = 5.0f;

    public PackageController packagePrefab;

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
        PackageController package = Instantiate(packagePrefab, transform.position, transform.rotation);

        int shapeEnumValue = Random.Range(0, System.Enum.GetNames(typeof(PackageShape)).Length);
        PackageShape shape = (PackageShape)shapeEnumValue;
        package.SetShape(shape);

        int colorEnumValue = Random.Range(0, System.Enum.GetNames(typeof(PackageColor)).Length);
        PackageColor color = (PackageColor)colorEnumValue;
        package.SetColor(color);

        int layoutEnumValue = Random.Range(0, System.Enum.GetNames(typeof(PackageLayout)).Length);
        PackageLayout layout = (PackageLayout)layoutEnumValue;
        package.SetLayout(layout);

        return package;
    }

    private float m_timer;
}
