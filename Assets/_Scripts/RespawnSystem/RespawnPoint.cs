using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] private bool isCheckpoint;
    [SerializeField] private bool canOnlyActivateOnce;

    public bool IsCheckpoint => isCheckpoint;
    public bool CanOnlyActivateOnce => canOnlyActivateOnce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out Respawn respawn))
        {
            respawn.SetRespawnPoint(this);
        }
    }

    /*
    private GameObject respawnTarget;

    [field: SerializeField]
    private UnityEvent OnSpawnPointActivated { get; set; }

    public GameObject RespawnTarget => respawnTarget;

    private void Start()
    {
        OnSpawnPointActivated.AddListener(() =>
            GetComponentInParent<RespawnPointManager>().UpdateRespawnPoint(this)
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent<RespawnAgent>(out var agent))
            {

            }

            //this.respawnTarget = collision.transform.parent.gameObject;
            //OnSpawnPointActivated?.Invoke();
        }
    }

    public void RespawnPlayer()
    {
        respawnTarget.transform.position = transform.position;
    }

    public void SetPlayerGO(GameObject player)
    {
        respawnTarget = player;
        GetComponent<Collider2D>().enabled = false;
    }

    public void DisableRespawnPoint()
    {
        gameObject.SetActive(false);
    }

    public void ResetRespawnPoint()
    {
        respawnTarget = null;
        GetComponent<Collider2D>().enabled = true;
    }
    */
}
