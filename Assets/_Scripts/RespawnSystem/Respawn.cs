using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private RespawnPoint respawnPoint;
    [SerializeField] private RespawnPoint checkPoint;
    [SerializeField] private List<RespawnPoint> previousCheckPoints = new();
    [SerializeField] private List<RespawnPoint> previousRespawnPoint = new();

    public void SetRespawnPoint(RespawnPoint respawnPoint)
    {
        if (respawnPoint.IsCheckpoint)
        {
            AddCheckpoint(respawnPoint);
        }
        else
        {
            AddRespawnPoint(respawnPoint);
        }
    }

    public void RespawnFromRespawnPoint()
    {
        if (respawnPoint == null) return;

        this.transform.GetChild(0).position = respawnPoint.transform.position;
    }

    public void RespawnFromCheckPoint()
    {
        if (checkPoint == null) return;

        this.transform.GetChild(0).position = checkPoint.transform.position;
    }

    private void AddCheckpoint(RespawnPoint respawnPoint)
    {
        if (previousCheckPoints.Contains(respawnPoint)) 
            return;
        
        checkPoint = respawnPoint;
        previousCheckPoints.Add(checkPoint);
    }

    private void AddRespawnPoint(RespawnPoint respawnPoint)
    {
        bool alreadyActivated = false;

        if (!previousRespawnPoint.Contains(respawnPoint))
        {
            previousRespawnPoint.Add(respawnPoint);
            alreadyActivated = true;
        }

        if (respawnPoint.CanOnlyActivateOnce && alreadyActivated)
            return;

        this.respawnPoint = respawnPoint;
    }
}
