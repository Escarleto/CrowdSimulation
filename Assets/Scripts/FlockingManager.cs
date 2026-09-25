using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    public static FlockingManager FM;
    [SerializeField] private GameObject[] fishPrefab;
    [SerializeField] private int numFish = 20;
    public GameObject[] allFish;
    public Vector3 swimLimits = new Vector3(5, 5, 5);
    public Vector3 goalPos;

    [Header ("Fish settings")]
    [Range(0f, 5f)] public float minSpeed;
    [Range(0f, 5f)] public float maxSpeed;
    [Range(1f, 5f)] public float rotationSpeed;
    [Range(1f, 10f)] public float neighbourDistance;
    
    private void Start()
    {
        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; i++)
        {
            Vector3 pos = transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x),
                                                           Random.Range(-swimLimits.y, swimLimits.y),
                                                           Random.Range(-swimLimits.z, swimLimits.z));
            allFish[i] = Instantiate(fishPrefab[Random.Range(0, fishPrefab.Length)], pos, Quaternion.identity);
        }

        FM = this;
        goalPos = transform.position;
    }

    private void Update()
    {
        if (Random.Range(0f, 100f) < 10f)
        {
            goalPos = transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x),
                                                       Random.Range(-swimLimits.y, swimLimits.y),
                                                       Random.Range(-swimLimits.z, swimLimits.z));
        }
    }
}
