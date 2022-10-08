using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThirdPersonShooter.Ai
{
    [ExecuteInEditMode]
    public class AiTargetingSystem : MonoBehaviour
    {
        public float memorySpan = 3.0f;
        public float distanceWeight = 1.0f;
        public float angleWeight = 1.0f;
        public float ageWeight = 1.0f;

        public bool HasTarget { get { return bestMemory != null; }}
        AiMemory bestMemory;
        public GameObject Target { get { return bestMemory.gameObject; } }
        public Vector3 TargetPosition { get { return bestMemory.gameObject.transform.position; } }
        public bool TargetInSight { get { return bestMemory.age < 0.5f; } }
        public float TargetDistance { get { return bestMemory.distance; } }
        AiSensoryMemory memory = new AiSensoryMemory(10);
        AiVisonSensor sensor;
        
        
        void Start()
        {
            sensor = GetComponent<AiVisonSensor>();
        }

        void Update()
        {
            memory.UpdateSenses(sensor);
            memory.ForgetMenories(memorySpan);
            EvaluateScores();
        }

    
        void EvaluateScores()
        {
            bestMemory = null;
            foreach(AiMemory memory in memory.memories)
            {
                if(memory.gameObject != this.gameObject)
                {
                    memory.score = CalculateScore(memory);
                    if(bestMemory == null || memory.score > bestMemory.score)
                    {
                        bestMemory = memory;
                    }
                }
            }
        }

        float Normalize(float value, float maxValue)
        {
            return 1.0f - (value / maxValue);
        }

        float CalculateScore(AiMemory memory)
        {
            float distanceScore = Normalize(memory.distance, sensor.distance) * distanceWeight;
            float angleScore = Normalize(memory.angle, sensor.angle) * angleWeight;
            float ageScore = Normalize(memory.age, memorySpan) * ageWeight;
            float score = distanceScore + angleScore + ageScore;
            return score;
        }


        private void OnDrawGizmos()
        {
            float maxScore = float.MinValue;
            foreach(AiMemory memory in memory.memories)
            {
                maxScore = Mathf.Max(maxScore, memory.score);
            }
            foreach(var memory in memory.memories)
            {
                Color color = Color.red;
                if(memory == bestMemory)
                {
                    color = Color.yellow;
                }
                color.a = memory.score / maxScore;
                Gizmos.color = color;
                
                Gizmos.DrawSphere(memory.position, 0.4f);
            }
        }
    }
}