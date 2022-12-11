using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

public class StatTests
{
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        EditorSceneManager.LoadSceneInPlayMode("Assets/_Scripts/Tests/Scenes/Test.unity", new UnityEngine.SceneManagement.LoadSceneParameters(UnityEngine.SceneManagement.LoadSceneMode.Single));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Stat_WhenModifierApplied_ChangesValue()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

        StatController statController = GameObject.FindObjectOfType<StatController>();

        Stat physicalAttack = statController.Stats["PhysicalAttack"];

        Assert.AreEqual(0, physicalAttack.Value);

        physicalAttack.AddModifier(new StatModifier
        {
            magnitude = 5,
            type = ModifierOperationType.Additive 
        });
        Assert.AreEqual(5, physicalAttack.Value);
    }

    [UnityTest]
    public IEnumerator Stat_WhenModifierApplied_DoesNotExceedCap()
    {
        yield return null;

        StatController statController = GameObject.FindObjectOfType<StatController>();
        Stat attackSpeed = statController.Stats["AttackSpeed"];
        Assert.AreEqual(1, attackSpeed.Value);
        attackSpeed.AddModifier(new StatModifier
        {
            magnitude = 5,
            type = ModifierOperationType.Additive
        });
        Assert.AreEqual(3, attackSpeed.Value);
    }
}
