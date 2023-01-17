using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DropItem/Drop table", fileName = "New Drop Table")]
public class DropTableSO : ScriptableObject
{
    public List<DropDefinition> drops;

    public List<PickableItem> GetDrop()
    {
        List<PickableItem> dropItems = new List<PickableItem>();

        foreach (DropDefinition drop in drops)
        {
            bool shouldDrop = Random.value < drop.DropChance;

            if (shouldDrop)
            {
                dropItems.Add(drop.Item);
            }
        }

        return dropItems;
    }
}
