using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScenarioType
{
    NONE,
    S1V1
}

public class ScenarioManager : MonoBehaviour
{
    public static ScenarioManager instance;

    protected Dictionary<string, Scenario> m_scenarioDict;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Initialize();
    }

    void Initialize()
    {
        m_scenarioDict = new Dictionary<string, Scenario>();
    }
}
