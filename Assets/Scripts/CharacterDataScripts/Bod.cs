using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bod
{
    // Identity
    protected string m_name;
    public string name
    {
        get {return m_name;}
    }
    protected string m_houseName;
    public string houseName 
    {
        get {return m_houseName;}
    }
    public string fullName
    {
        get
        {
            string n = name;
            if (houseName != null && houseName.Trim() != "")
            {
                n += " " + houseName;
            }
            return n;
        }
    }

    // Personality Type
    protected PersonalityType m_personalityType;
    public PersonalityType personalityType
    {
        get {return m_personalityType;}
    }

    // Bod Class
    protected BodClass m_bodClass;
    public BodClass bodClass 
    {
        get {return m_bodClass;}
    }

    // Attributes
    protected int m_strength;
    public int strength
    {
        get {return m_strength;}
    }
    protected int m_agility;
    public int agility 
    {
        get {return m_agility;}
    }
    public int body
    {
        get {return strength + agility;}
    }
    protected int m_injury;
    public int injury 
    {
        get {return m_injury;}
    }
    public int health 
    {
        get {return Mathf.Max(body - injury, 0);}
    }
    public bool isDead
    {
        get
        {
            return health == 0;
        }
    }
    protected int m_mind;
    public int mind 
    {
        get {return m_mind;}
    }

    // TODO: Skills
    protected int m_fighting;
    public int fighting 
    {
        get {return m_fighting;}
    }
    protected int m_stealth;
    protected int m_diplomacy;

    public Bod(string n, string hn, PersonalityType pt, BodClass bc)
    {
        m_name = n;
        m_houseName = hn;
        m_personalityType = pt;
        m_bodClass = bc;

        SetInjury(0);
    }

    // Attribute Setters
    public void SetStrength(int val)
    {
        m_strength = val;
    }
    public void SetAgility(int val)
    {
        m_agility = val;
    }
    public void SetMind(int val)
    {
        m_mind = val;
    }
    public void SetInjury(int val)
    {
        m_injury = val;
    }

    // Skill Setters
    public void SetFightingSkill(int val)
    {
        m_fighting = val;
    }
}
