using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario
{
    protected string m_scenarioId;
    public string scenarioId 
    {
        get {return m_scenarioId;}
    }
    protected ScenarioType m_scenarioType;
    public ScenarioType scenarioType 
    {
        get {return m_scenarioType;}
    }

    protected Party m_mainParty;
    public Party mainParty 
    {
        get {return m_mainParty;}
    }

    protected virtual void Initialize()
    {
        m_scenarioType = ScenarioType.NONE;
    }
}

public class Scenario1v1 : Scenario
{
    protected Party m_alternateParty;
    public Party alternateParty 
    {
        get {return m_alternateParty;}
    }

    protected override void Initialize()
    {
        m_scenarioType = ScenarioType.S1V1;
    }
}
