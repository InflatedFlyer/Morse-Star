using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldParticles : MonoBehaviour 
{
    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;
    [Header("Main Settings")]
    public List<Transform> targets;
    public bool ignoreY = true;
    [Range(1, 5000)]
    public int particlesToSpawn = 150;
    [Range(1, 100)]
    public float distanceForAction = 11f;
    [Range(1, 100)]
    public float actionSpeed = 1f;
    Vector3[] startPositions = new Vector3[0];
    [Header("Coloring Settings")]
    public Gradient gradient;
    [Range(1, 100)]
    public float distanceForColoring = 11f;
	void OnEnable ()
    {
        m_System = GetComponent<ParticleSystem>();
        m_System.maxParticles = particlesToSpawn;
        m_System.Simulate(0.1f);
        m_System.Play();

        startPositions = new Vector3[particlesToSpawn];
        m_Particles = new ParticleSystem.Particle[particlesToSpawn];
        int numParticlesAlive = m_System.GetParticles(m_Particles);
        for(int i = 0; i<numParticlesAlive;i++)
        {
            startPositions[i] = m_Particles[i].position;
        }
	}

    private void LateUpdate()
    {
        int numParticlesAlive = m_System.GetParticles(m_Particles);
        for (int i = 0; i < numParticlesAlive; i++)
        {
            float minDistance = -1f;
            Vector3 targetPosition = new Vector3();
            for (int a = 0; a < targets.Count; a++)
            {
                if (targets[a] != null)
                {
                    float distance = 0;
                    if (ignoreY)
                    {
                        distance = Vector3.Distance(new Vector3(startPositions[i].x, targets[a].position.y, startPositions[i].z), targets[a].position);
                    }
                    else
                    {
                        distance = Vector3.Distance(startPositions[i], targets[a].position);
                    }
                    if (minDistance == -1 || distance < minDistance)
                    {
                        minDistance = distance;
                        targetPosition = targets[a].position;
                    }
                }
            }

            float distanceFromParticle = Vector3.Distance(m_Particles[i].position, targetPosition);
            float myColorInGradient = 0f;
            myColorInGradient = (100f - (distanceFromParticle / ((distanceForColoring) / 100f))) / 100f;
            Color32 colorToSet = gradient.Evaluate(myColorInGradient);

            float particleToStart = Vector3.Distance(m_Particles[i].position, startPositions[i]);
            float onePercentOfFullPath = distanceForAction / 100f;
            float distanceInPercent = particleToStart/onePercentOfFullPath;
            float alpha = (255 - (distanceInPercent * 5f));
            if (alpha < 0) alpha = 0;
            colorToSet.a = (byte)alpha;
            m_Particles[i].color = colorToSet;

            if (minDistance != -1 && minDistance <= distanceForAction)
            {
                Vector3 shootplace = targetPosition;
                Vector3 shootTarget = m_Particles[i].position;
                Vector3 shootDirection = shootTarget - shootplace;
                float distancePerOneDirection = Vector3.Distance(shootplace, shootplace + shootDirection);
                Vector3 pointAtEnd = Vector3.zero;
                if (distancePerOneDirection < distanceForAction)
                {
                    float multiplier = (distanceForAction - distancePerOneDirection) / distancePerOneDirection;

                    pointAtEnd = shootplace + shootDirection + (shootDirection * multiplier);
                }
                else
                {
                    pointAtEnd = shootplace + (shootDirection / (distancePerOneDirection / (distancePerOneDirection / (distancePerOneDirection / distanceForAction))));
                }

                m_Particles[i].position = Vector3.MoveTowards(m_Particles[i].position, pointAtEnd, Time.deltaTime * actionSpeed);
            }
            else
            {
                m_Particles[i].position = Vector3.MoveTowards(m_Particles[i].position, startPositions[i], Time.deltaTime * (actionSpeed/4f));
            }
        }
        m_System.SetParticles(m_Particles, numParticlesAlive);
    }
}
