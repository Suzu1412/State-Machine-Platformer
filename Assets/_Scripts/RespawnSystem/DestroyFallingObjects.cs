using UnityEngine;

public class DestroyFallingObjects : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out Respawn respawn))
        {
            if (collision.transform.parent.TryGetComponent(out HealthSystem health))
            {
                health.GetHit(1);
            }

            respawn.RespawnFromRespawnPoint();
        }
    }


    /*
    public LayerMask objectsToDestoryLayerMask;
    public Vector2 size;

    [Header("Gizmo parameters")]
    public Color gizmoColor = Color.red;
    public bool showGizmo = true;

    private void FixedUpdate()
    {
        /*
        Collider2D collider = Physics2D.OverlapBox(transform.position, size, 0, objectsToDestoryLayerMask);
        if (collider != null)
        {
            Agent agent = collider.GetComponentInParent<Agent>();
            if (agent == null)
            {
                Destroy(collider.transform.parent.gameObject);
                return;
            }
            //var damagable = agent.GetComponent<Damagable>();
            //if (damagable != null)
            //{
            //    damagable.GetHit(1);
            //    if (damagable.CurrentHealth == 0 && agent.CompareTag("Player"))
            //    {
                    agent.GetComponent<RespawnHelper>().RespawnPlayer();
            //    }
            //}


            //agent.AgentDied();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.parent.TryGetComponent<Agent>(out var agent))
        {
            Destroy(collision.gameObject);
            return;
        }

        agent.Respawn.RespawnPlayer();

    }


    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawCube(transform.position, size);
     
    }
    */
}
