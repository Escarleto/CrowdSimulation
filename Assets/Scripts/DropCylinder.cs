using UnityEngine;
using UnityEngine.InputSystem;

public class DropCylinder : MonoBehaviour
{
    [SerializeField] GameObject obstacle;
    private GameObject[] agents;

    private void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("agent");
    }

    private void Update()
    {
        if (Mouse.current == null)
            return;
        
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit hitInfo;

            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out hitInfo))
            {
                Instantiate(obstacle, hitInfo.point, obstacle.transform.rotation);

                foreach (GameObject agent in agents)
                {
                    agent.GetComponent<AIControl>().DetectNewObstacle(hitInfo.point);
                }
            }
        }
    }
}
