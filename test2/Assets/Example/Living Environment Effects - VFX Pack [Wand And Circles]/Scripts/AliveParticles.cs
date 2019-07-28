using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveParticles : MonoBehaviour 
{
    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;
    float[] randomHeights = new float[0];
    bool start = false;

    public List<Transform> targets;
    public ParticlesType typeOfParticles;
    [Header("Size of area")]
    [Range(1,1000)]
    public int sizeX;
    [Range(1, 1000)]
    public int sizeZ;
    [Header("Main Settings")]
    [Range(0.01f, 5f)]
    public float particlesScale = 1f;
    [Range(0, 100)]
    public float distanceForAction = 11f;
    [Range(0, 100)]
    public float actionSpeed = 1f;
    [Range(-25, 25)]
    public float actionHeight = 1f;
    [Header("Noise Settings")]
    public NoiseTypes noiseType;
    [Range(0, 100)]
    public float noiseSpeed = 0.5f;
    [Range(-25, 25)]
    public float noiseMaxHeight = 0.5f;

    public float waveSpeed;
    public float waveValue;
    [Header("Smoothness Settings")]
    public bool smoothness = false;
    [Range(0, 100)]
    public float smoothnessSpeed = 0.5f;
    [Range(1, 100)]
    public float smoothnessDistance = 1f;
    [Range(-25, 25)]
    public float smoothnessHeight = 1f;
    [Header("Coloring Settings")]
    public ColorCoatingTypes colorCoatingType;
    public Gradient gradient;

    private void FixedUpdate()
    {
        if (randomHeights.Length == sizeX*sizeZ)
        {
            int numParticlesAlive = m_System.GetParticles(m_Particles);

            for (int i = 0; i < numParticlesAlive; i++)
            {
                float minDistance = -1f;
                for (int a = 0; a < targets.Count; a++)
                {
                    if (targets[a] != null)
                    {
                        float distance = Vector3.Distance(new Vector3(m_Particles[i].position.x, targets[a].position.y, m_Particles[i].position.z), targets[a].position);
                        if (minDistance == -1 || distance < minDistance) minDistance = distance;
                    }
                }

                float myColorInGradient = 0f;

                if (colorCoatingType == ColorCoatingTypes.ByDistance)
                {
                    float fullRange = distanceForAction;
                    if (smoothness)
                    {
                        fullRange += smoothnessDistance;
                    }
                    myColorInGradient = (100f - (minDistance / (fullRange / 100f))) / 100f;
                }
                else
                {
                    float maxHeight = transform.position.y;
                    if(noiseType != NoiseTypes.None)
                    {
                        if(noiseMaxHeight > maxHeight)
                        {
                            maxHeight += noiseMaxHeight;
                        }
                        else
                        {
                            maxHeight += actionHeight;
                        }
                    }
                    else
                    {
                        maxHeight += actionHeight;
                    }
                    float minHeight = transform.position.y;
                    float onePercentOfFullDiapasone = (maxHeight - minHeight) / 100f;
                    float myHeight = m_Particles[i].position.y - minHeight;
                    float mypercentages = (100f - myHeight / onePercentOfFullDiapasone)/100f;
                    myColorInGradient = mypercentages;
                }

                if (minDistance != -1 && minDistance <= distanceForAction)
                {
                    if (colorCoatingType == ColorCoatingTypes.ByDistance || colorCoatingType == ColorCoatingTypes.ByHeight)
                    {
                        m_Particles[i].color = gradient.Evaluate(myColorInGradient);
                    }

                    m_Particles[i].position = Vector3.Lerp(m_Particles[i].position, new Vector3(m_Particles[i].position.x, transform.position.y+actionHeight, m_Particles[i].position.z), Time.deltaTime * actionSpeed);
                }
                else if (minDistance != -1 && smoothness && minDistance <= smoothnessDistance + distanceForAction)
                {
                    if (colorCoatingType == ColorCoatingTypes.ByDistance || colorCoatingType == ColorCoatingTypes.ByHeight)
                    {
                        m_Particles[i].color = gradient.Evaluate(myColorInGradient);
                    }
                    float onepercentOfHeight = (transform.position.y + smoothnessHeight) / 100f;
                    float distanceInSmooth = (minDistance - distanceForAction);
                    float onepercentofdistance = smoothnessDistance / 100f;
                    float need = 100f - (distanceInSmooth / onepercentofdistance);

                    m_Particles[i].position = Vector3.Lerp(m_Particles[i].position, new Vector3(m_Particles[i].position.x, transform.position.y + (need * onepercentOfHeight), m_Particles[i].position.z), Time.deltaTime * smoothnessSpeed);
                }
                else if(minDistance != -1)
                {
                    if (colorCoatingType == ColorCoatingTypes.ByDistance)
                    {
                        m_Particles[i].color = gradient.Evaluate(0);
                    }
                    else if (colorCoatingType == ColorCoatingTypes.ByHeight)
                    {
                        m_Particles[i].color = gradient.Evaluate(myColorInGradient);
                    }
                    
                    if (noiseType == NoiseTypes.None)
                    {
                        m_Particles[i].position = Vector3.Lerp(m_Particles[i].position, new Vector3(m_Particles[i].position.x,  transform.position.y, m_Particles[i].position.z), Time.deltaTime * actionSpeed);
                    }
                    else if (noiseType == NoiseTypes.NonStatic)
                    {
                        if (Vector3.Distance(m_Particles[i].position, new Vector3(m_Particles[i].position.x, randomHeights[i], m_Particles[i].position.z)) < 0.05f)
                        {
                            randomHeights[i] = Random.Range( transform.position.y, transform.position.y + noiseMaxHeight);
                        }
                        else
                        {
                            m_Particles[i].position = Vector3.Lerp(m_Particles[i].position, new Vector3(m_Particles[i].position.x, randomHeights[i], m_Particles[i].position.z), Time.deltaTime * noiseSpeed);
                        }
                    }
                    else if(noiseType == NoiseTypes.Static)
                    {
                        if(randomHeights[i] == transform.position.y)
                        {
                            randomHeights[i] = Random.Range( transform.position.y, transform.position.y + noiseMaxHeight);
                        }
                        else
                        {
                            m_Particles[i].position = Vector3.Lerp(m_Particles[i].position, new Vector3(m_Particles[i].position.x, randomHeights[i], m_Particles[i].position.z), Time.deltaTime * noiseSpeed);
                        }
                    }
                    else if (noiseType == NoiseTypes.WavesByValues)
                    {
                        m_Particles[i].position = Vector3.Lerp(m_Particles[i].position, new Vector3(m_Particles[i].position.x, transform.position.y + (Mathf.Sin(Time.time * waveSpeed+ i*waveValue) * noiseMaxHeight), m_Particles[i].position.z), Time.deltaTime * noiseSpeed);
                    }
                    else if (noiseType == NoiseTypes.Waves2)
                    {
                        m_Particles[i].position = Vector3.Lerp(m_Particles[i].position, new Vector3(m_Particles[i].position.x, transform.position.y + (Mathf.Sin(Time.time * (0.01f * i) + i) * noiseMaxHeight), m_Particles[i].position.z), Time.deltaTime * noiseSpeed);
                    }
                    else if (noiseType == NoiseTypes.Waves3)
                    {
                        m_Particles[i].position = Vector3.Lerp(m_Particles[i].position, new Vector3(m_Particles[i].position.x, transform.position.y + (Mathf.Sin(Time.time * (0.005f * i) + i) * noiseMaxHeight), m_Particles[i].position.z), Time.deltaTime * noiseSpeed);
                    }
                    else if (noiseType == NoiseTypes.Waves4)
                    {
                        m_Particles[i].position = Vector3.Lerp(m_Particles[i].position, new Vector3(m_Particles[i].position.x, transform.position.y + (Mathf.Sin(Time.time * 2f + i * 4.77f) * noiseMaxHeight), m_Particles[i].position.z), Time.deltaTime * noiseSpeed);
                    }
                    else if (noiseType == NoiseTypes.Waves5)
                    {
                        m_Particles[i].position = Vector3.Lerp(m_Particles[i].position, new Vector3(m_Particles[i].position.x, transform.position.y + (Mathf.Sin(Time.time * 2f + i * 5.09f) * noiseMaxHeight), m_Particles[i].position.z), Time.deltaTime * noiseSpeed);
                    }
                    else if (noiseType == NoiseTypes.Waves6)
                    {
                        m_Particles[i].position = Vector3.Lerp(m_Particles[i].position,new Vector3(m_Particles[i].position.x, transform.position.y + (Mathf.Sin(Time.time * 3.6f + i * 2.5f) * noiseMaxHeight), m_Particles[i].position.z),Time.deltaTime*noiseSpeed);
                    }
                    else if (noiseType == NoiseTypes.Waves7)
                    {
                        m_Particles[i].position = Vector3.Lerp(m_Particles[i].position, new Vector3(m_Particles[i].position.x, transform.position.y + (Mathf.Sin(Time.time * 3f + i * 6.29f) * noiseMaxHeight), m_Particles[i].position.z), Time.deltaTime * noiseSpeed);
                    }
                    else if (noiseType == NoiseTypes.Waves8)
                    {
                        m_Particles[i].position = Vector3.Lerp(m_Particles[i].position, new Vector3(m_Particles[i].position.x, transform.position.y + (Mathf.Sin(Time.time * 3f + i * 6.95f) * noiseMaxHeight), m_Particles[i].position.z), Time.deltaTime * noiseSpeed);
                    }
                    else if (noiseType == NoiseTypes.Waves9)
                    {
                        m_Particles[i].position = Vector3.Lerp(m_Particles[i].position, new Vector3(m_Particles[i].position.x, transform.position.y + (Mathf.Sin(Time.time * 3f + i * 42.24f) * noiseMaxHeight), m_Particles[i].position.z), Time.deltaTime * noiseSpeed);
                    }
                    else if (noiseType == NoiseTypes.Waves10)
                    {
                        m_Particles[i].position = Vector3.Lerp(m_Particles[i].position, new Vector3(m_Particles[i].position.x, transform.position.y + (Mathf.Sin(Time.time * 2f + i * 0.4f) * noiseMaxHeight), m_Particles[i].position.z), Time.deltaTime * noiseSpeed);
                    }
                    else if (noiseType == NoiseTypes.Waves11)
                    {
                        m_Particles[i].position = Vector3.Lerp(m_Particles[i].position, new Vector3(m_Particles[i].position.x, transform.position.y + (Mathf.Sin(Time.time * 2f + i * 0.13f) * noiseMaxHeight), m_Particles[i].position.z), Time.deltaTime * noiseSpeed);
                    }
                }
            }
            m_System.SetParticles(m_Particles, numParticlesAlive);
        }
        else
        {
            m_System.SetParticles(m_Particles, 0);

            if (typeOfParticles == ParticlesType.Hexagon)
            {
                for (int i = 0; i < sizeX; i++)
                {
                    for (int a = 0; a < sizeZ; a++)
                    {
                        float offset = 0f;
                        if (a % 2 != 0)
                        {
                            offset += 1.7321f / 2f;
                        }
                        m_System.Emit(new Vector3(transform.position.x + ((offset + i * 1.7321f) * transform.localScale.x), transform.position.y, transform.position.z + ((-(a * 1.5f)) * transform.localScale.z)), Vector3.zero, 100f * particlesScale, 99999f, Color.white);
                    }
                }
            }
            else if (typeOfParticles == ParticlesType.Cube)
            {
                for (int i = 0; i < sizeX * 2; i++)
                {
                    for (int a = 0; a < sizeZ / 2; a++)
                    {
                        float offset = 0f;
                        if (i % 2 != 0)
                        {
                            offset += 1f;
                        }
                        m_System.Emit(new Vector3(transform.position.x + ((i * 0.5f) * transform.localScale.x), transform.position.y, transform.position.z + ((offset + a * 2f) * transform.localScale.z)), Vector3.zero, 100f * particlesScale, 99999f, Color.white);
                    }
                }
            }
            else if (typeOfParticles == ParticlesType.Cube2)
            {
                for (int i = 0; i < sizeX ; i++)
                {
                    for (int a = 0; a < sizeZ; a++)
                    {
                        m_System.Emit(new Vector3(transform.position.x + ((i * 1f) * transform.localScale.x), transform.position.y, transform.position.z + ((a * 1f) * transform.localScale.z)), Vector3.zero, 100f * particlesScale, 99999f, Color.white);
                    }
                }
            }
            else if (typeOfParticles == ParticlesType.Form)
            {
                for (int i = 0; i < sizeX; i++)
                {
                    for (int a = 0; a < sizeZ; a++)
                    {
                        float offset = 0f;
                        if (a % 2 != 0)
                        {
                            offset += 1.7321f / 2f;
                        }
                        m_System.Emit(new Vector3(transform.position.x + ((i) * transform.localScale.x), transform.position.y, transform.position.z + ((a) * transform.localScale.z)), Vector3.zero, 50f * particlesScale, 99999f, Color.white);
                    }
                }
            }
            else if (typeOfParticles == ParticlesType.Triangle)
            {
                for (int i = 0; i < sizeX*2; i++)
                {
                    for (int a = 0; a < sizeZ/2; a++)
                    {
                        int offset = 0;

                        if (i % 2 == 0)
                        {
                            offset++;
                        }

                        float x = Mathf.FloorToInt(i/2f);
                        Vector3 forspawn = new Vector3(transform.position.x + ((x * 2f) * transform.localScale.x), transform.position.y, transform.position.z + ((offset + a * 2f) * transform.localScale.x));
                        m_System.Emit(forspawn, Vector3.zero, 100f * particlesScale, 99999f, Color.white);
                    }
                }

                int numParticlesAlive = m_System.GetParticles(m_Particles);
                int particleNumber = 0;
                for (int i = 0; i < sizeX*2; i++)
                {
                    for (int a = 0; a < sizeZ/2; a++)
                    {
                        if (i % 2 == 0)
                        {
                            m_Particles[particleNumber].rotation3D = new Vector3(0, 180, 0);
                        }
                        particleNumber++;
                    }
                }
                m_System.SetParticles(m_Particles, numParticlesAlive);
            }

            randomHeights = new float[sizeZ * sizeX];
            for(int i = 0; i<randomHeights.Length;i++)
            {
                randomHeights[i] = transform.position.y;
            }
        }
    }

    void OnEnable()
    {
        m_System = GetComponent<ParticleSystem>();
        m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];
        randomHeights = new float[0];
    }

    public enum NoiseTypes
    {
        None,
        Static,
        NonStatic,
        WavesByValues,
        Waves2,
        Waves3,
        Waves4,
        Waves5,
        Waves6,
        Waves7,
        Waves8,
        Waves9,
        Waves10,
        Waves11,
    }
    public enum ColorCoatingTypes
    {
        None,
        ByDistance,
        ByHeight
    }
    public enum ParticlesType
    {
        Hexagon,
        Cube,
        Cube2,
        Form,
        Triangle
    }
}
