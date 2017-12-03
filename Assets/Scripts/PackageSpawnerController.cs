using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSpawnerController : MonoBehaviour
{
    public float spawnRate = 5.0f;
    public float randomSpawnRateAmplitude = 1.0f;
    public float randomImpulseStrength = 10.0f;

    public PackageController packagePrefab;

    public delegate void PackageSpawnedDelegate(PackageSpawnerController _spawner, PackageController _package);
    public event PackageSpawnedDelegate OnPackageSpawned;

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
            m_timer += spawnRate + Mathf.Sin(Random.Range(0.0f, Mathf.PI * 2.0f)) * randomSpawnRateAmplitude;
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

        Rigidbody rb = package.GetComponent<Rigidbody>();
        float randomAngle = Random.Range(0.0f, 360.0f);
        float randomStrength = Random.Range(0.0f, randomImpulseStrength);
        Vector3 force = transform.rotation * Quaternion.AngleAxis(randomAngle, Vector3.up) * (Vector3.forward * randomStrength);
        Vector3 randomOffset = new Vector3(Mathf.Sin(Random.Range(0.0f, Mathf.PI * 2.0f)), Mathf.Sin(Random.Range(0.0f, Mathf.PI * 2.0f)), Mathf.Sin(Random.Range(0.0f, Mathf.PI * 2.0f))) * 0.5f;
        rb.AddForceAtPosition(force, package.transform.position + randomOffset);

        if (OnPackageSpawned != null) OnPackageSpawned(this, package);

        return package;
    }

    private float m_timer;
}
