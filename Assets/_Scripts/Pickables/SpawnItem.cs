using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnItem : MonoBehaviour
{
    [SerializeField] private DropTableSO dropTable;

    public void SpawnItemPickUp()
    {
        List<PickableItem> dropItems = dropTable.GetDrop();

        foreach (PickableItem item in dropItems)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }

}
