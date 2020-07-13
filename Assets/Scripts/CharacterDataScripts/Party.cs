using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party
{
    protected string m_partyName;
    public string partyName 
    {
        get 
        {
            if (m_partyName != null && m_partyName != "")
            {
                return m_partyName;
            }
            else
            {
                return (leader == null) ? "Party" : leader.fullName + "'s Party";
            }
        }
    }
    protected Bod m_leader;
    public Bod leader
    {
        get {return m_leader;}
    }
    protected List<Bod> m_retainers;
    public List<Bod> retainers 
    {
        get {return m_retainers;}
    }
    protected List<Bod> m_members;
    public List<Bod> members 
    {
        get {return m_members;}
    }

    public Party(Bod lead, string nameOverride = "")
    {
        m_leader = lead;
        m_partyName = nameOverride;
        Initialize();
    }
    void Initialize()
    {
        m_retainers = new List<Bod>();
        m_members = new List<Bod>();
    }

    public void SetLeader(Bod bod)
    {
        if (!IsInParty(bod))
        {
            m_leader = bod;
        }
    }
    public void AddRetainer(Bod bod)
    {
        if (!IsInParty(bod))
        {
            m_retainers.Add(bod);
        }
    }
    public void AddMember(Bod bod)
    {
        if (!IsInParty(bod))
        {
            m_members.Add(bod);
        }
    }
    public bool IsInParty(Bod bod)
    {
        return m_members.Contains(bod) || m_retainers.Contains(bod) || m_leader == bod;
    }

    public List<Bod> GetCombatants(bool includeLeader = true)
    {
        List<Bod> combatants = new List<Bod>();

        foreach (Bod bod in members)
        {
            if (BodManager.instance.IsCombatant(bod) && !bod.isDead)
            {
                combatants.Add(bod);
            }
        }
        foreach (Bod bod in retainers)
        {
            if (BodManager.instance.IsCombatant(bod) && !bod.isDead)
            {
                combatants.Add(bod);
            }
        }
        if (includeLeader && BodManager.instance.IsCombatant(leader) && !leader.isDead)
        {
            combatants.Add(leader);
        }

        return combatants;
    }
}
