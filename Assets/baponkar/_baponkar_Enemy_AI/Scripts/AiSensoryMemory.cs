using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThirdPersonShooter.Ai
{
public class AiMemory
{
    public GameObject gameObject;
    public Vector3 position;
    public Vector3 direction;
    public float distance;
    public float angle;
    public float lastSeen;
    public float score;
    
    public float age
    {
        get { return Time.time - lastSeen; }
    }
}
public class AiSensoryMemory 
{
    public List<AiMemory> memories = new List<AiMemory>();

    GameObject [] characters;

    public AiSensoryMemory(int maxPlayers)
    {
        characters = new GameObject[maxPlayers];
    }

    public void UpdateSenses(AiVisonSensor sensor)
    {
        int targets = sensor.Filter(characters, "Player");
        for(int i =0; i< targets; ++i)
        {
            GameObject target = characters[i];
            RefreshMemory(sensor.gameObject, target);
        }
    }

    public void RefreshMemory(GameObject agent, GameObject target)
    {
        AiMemory memory = FetchMemory(target);
        memory.gameObject = target;
        memory.position = target.transform.position;
        memory.direction = target.transform.position - agent.transform.position;
        memory.distance = memory.direction.magnitude;
        memory.angle = Vector3.Angle(agent.transform.forward, memory.direction);
        memory.lastSeen = Time.time;
    }

    public AiMemory FetchMemory(GameObject gameObject)
    {
        AiMemory memory = memories.Find(x => x.gameObject == gameObject);
        if(memory == null)
        {
            memory = new AiMemory();
            memories.Add(memory);
        }
        return memory;
    }


    public void ForgetMenories(float olderThan)
    {
        memories.RemoveAll(m => m.age > olderThan); // Remove all memories older than olderThan
        memories.RemoveAll(m => !m.gameObject); // Remove all memories that have no gameObject
        memories.RemoveAll(m => !m.gameObject.GetComponent<Health>()); // Remove all memories that have no Health component
        memories.RemoveAll(m => m.gameObject.GetComponent<Health>().isDead); // Remove all memories that is dead
        //memories.RemoveAll(memory => !memory.gameObject.GetComponent<HitBox>().health.isDead); // Remove all memories that already dead
        //memories.RemoveAll(m => m.gameObject.GetComponent<HitBox>().health.isDead); // Remove all memories that already dead
        // var toRemove = memories.Find(m => m.gameObject.GetComponent<Health>().isDead); // Remove all memories that already dead
        // if(toRemove != null) memories.Remove(toRemove); // Remove all memories that already dead
    }
    
}
}