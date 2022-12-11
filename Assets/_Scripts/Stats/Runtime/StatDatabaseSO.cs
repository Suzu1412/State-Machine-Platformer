using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatSystem/StatDatabase", fileName = "StatDatabase")]
public class StatDatabaseSO : ScriptableObject
{
    public List<StatDefinitionSO> stats;
    public List<StatDefinitionSO> attributes;
}
