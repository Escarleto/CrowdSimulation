using UnityEngine;

public class Flock : MonoBehaviour
{
    private float speed;
    private bool turning = false;

    private void Start()
    {
        speed = Random.Range(FlockingManager.FM.minSpeed, FlockingManager.FM.maxSpeed);
    }

    private void Update()
    {
        Bounds b = new Bounds(FlockingManager.FM.transform.position, FlockingManager.FM.swimLimits * 2);

        if (!b.Contains(transform.position))
            turning = true;
        else
            turning = false;
        
        if (turning)
        {
            Vector3 direction = FlockingManager.FM.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), FlockingManager.FM.rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (Random.Range(0f, 100f) < 10f)
                speed = Random.Range(FlockingManager.FM.minSpeed, FlockingManager.FM.maxSpeed);

            if (Random.Range(0f, 100f) < 10f)
                ApplyBehaviour();
        }

        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void ApplyBehaviour()
    {
        GameObject[] gos;
        gos = FlockingManager.FM.allFish;

        Vector3 vCentre = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;
        float gSpeed = 0.01f;
        float nDistance;
        int groupSize = 0;

        foreach (GameObject go in gos)
        {
            if (go != gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, transform.position);
            
                if (nDistance <= FlockingManager.FM.neighbourDistance)
                {
                    vCentre += go.transform.position;
                    groupSize++;
                    if (nDistance < 1f)
                        vAvoid += transform.position - go.transform.position;
                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed += anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0)
        {
            vCentre = vCentre / groupSize + (FlockingManager.FM.goalPos - transform.position);
            speed = gSpeed / groupSize;
            if (speed > FlockingManager.FM.maxSpeed)
                speed = FlockingManager.FM.maxSpeed;    

            Vector3 direction = vCentre + vAvoid - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), FlockingManager.FM.rotationSpeed * Time.deltaTime);
        }
    }
}
