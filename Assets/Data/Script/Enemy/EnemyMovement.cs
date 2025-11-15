using UnityEngine;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    // gán parent chứa tất cả waypoint
    [SerializeField] private Transform waypointsParent;
    public float speed = 2f;

    private List<Transform> waypoints = new List<Transform>();
    private int currentIndex = 0;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (waypointsParent == null)
        {
            GameObject wpParentObj = GameObject.FindWithTag("Waypoints");
            if (wpParentObj != null)
                waypointsParent = wpParentObj.transform;
        }
    }
    public void InitWaypoints(Transform wpParent)
    {
        waypointsParent = wpParent;
        waypoints.Clear();
        currentIndex = 0;

        if (waypointsParent == null) return;

        foreach (Transform child in waypointsParent)
        {
            waypoints.Add(child);
        }
    }
    void Update()
    {
        if (waypointsParent == null || waypoints.Count == 0 || currentIndex >= waypoints.Count) return;

        Vector3 target = waypoints[currentIndex].position;
        Vector3 dir = (target - transform.position).normalized;

        // Di chuyển
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Cập nhật animation theo hướng
        UpdateAnimation(dir);

        // Kiểm tra tới waypoint
        if (Vector3.Distance(transform.position, target) < 0.05f)
            currentIndex++;
        if (currentIndex >= waypoints.Count)
        {
            ReachDestination(); // gọi khi tới waypoint cuối
        }
    }

    void UpdateAnimation(Vector3 dir)
    {
        animator.SetFloat("dirX", dir.x);
        animator.SetFloat("dirY", dir.y);
    }
    void ReachDestination()
    {
        Debug.Log("Enemy has reached the destination!");
        EnemyPool.Instance.ReturnEnemyToPool(gameObject);
    }
    // -------- Vẽ đường đi bằng Gizmos --------
    void OnDrawGizmos()
    {
        if (waypointsParent == null) return;

        Gizmos.color = Color.red;
        Transform previous = null;

        foreach (Transform child in waypointsParent)
        {
            if (previous != null)
            {
                Gizmos.DrawLine(previous.position, child.position);
            }
            previous = child;

            // Vẽ sphere ở mỗi waypoint
            Gizmos.DrawSphere(child.position, 0.1f);
        }
    }
}
