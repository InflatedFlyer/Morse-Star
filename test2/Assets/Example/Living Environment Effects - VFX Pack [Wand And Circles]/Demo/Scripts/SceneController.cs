using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour 
{
    public GameObject obj;
    public Transform character;
    public GameObject[] effectsObjects;
    public GameObject[] cameras;
    public Transform[] spawnDots;
    public GameObject ballPrefab;
    public Text particleName;
    public GameObject canvas;
    public int selectedEffect = 0;
    public int selectedCamera = 0;
    List<GameObject> spawnedBalls = new List<GameObject>();

   

    public void Update()
    {
        obj = effectsObjects[selectedEffect];
        if (Input.GetKey(KeyCode.Q))
        {
            cameras[selectedCamera].transform.eulerAngles = new Vector3(0, cameras[selectedCamera].transform.eulerAngles.y - 0.5f, 0);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            cameras[selectedCamera].transform.eulerAngles = new Vector3(0, cameras[selectedCamera].transform.eulerAngles.y + 0.5f, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            character.eulerAngles = new Vector3(0, character.eulerAngles.y - 1f, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            character.eulerAngles = new Vector3(0, character.eulerAngles.y + 1f, 0);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnBall();
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            for(int i = spawnedBalls.Count-1;i >= 0; i--)
            {
                for (int a = 0; a < effectsObjects.Length; a++)
                {
                    if (effectsObjects[a].GetComponentsInChildren<AliveParticles>() != null)
                    {
                        AliveParticles[] prtcls = effectsObjects[a].GetComponentsInChildren<AliveParticles>();
                        foreach (AliveParticles alpr in prtcls)
                        {
                            alpr.targets.Remove(spawnedBalls[i].transform);
                        }
                    }
                    if (effectsObjects[a].GetComponentInChildren<FieldParticles>() != null)
                    {
                        FieldParticles[] prtcls = effectsObjects[a].GetComponentsInChildren<FieldParticles>();
                        foreach (FieldParticles flpr in prtcls)
                        {
                            flpr.targets.Remove(spawnedBalls[i].transform);
                        }
                    }
                }
                    Destroy(spawnedBalls[i]);
            }
            spawnedBalls.Clear();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            for (int i = 0; i < canvas.transform.childCount; i++)
            {
                canvas.transform.GetChild(i).gameObject.SetActive(!canvas.transform.GetChild(i).gameObject.activeSelf);
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PreviousEffect();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            NextEffect();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            PreviousCamera();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            NextCamera();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            character.position = new Vector3(-11f, 14.988f, 0f);
            character.eulerAngles = new Vector3(0f, 90f, 0f);
        }
    }
    public void RefreshBallsColor()
    {
        foreach(GameObject go in spawnedBalls)
        {
            Color clr = GameObject.FindObjectOfType<Light>().color;
            go.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", clr);
        }
    }
    public void SpawnBall()
    {
        GameObject newBall = Instantiate(ballPrefab, spawnDots[Random.Range(0, spawnDots.Length)].position,new Quaternion());
        newBall.GetComponent<Rigidbody>().AddForce((new Vector3(0, newBall.transform.position.y, 0) - newBall.transform.position) / Random.Range(1.9f,2.9f), ForceMode.Impulse);
        if(effectsObjects[selectedEffect].GetComponentsInChildren<AliveParticles>() != null)
        {
            AliveParticles[] prtcls = effectsObjects[selectedEffect].GetComponentsInChildren<AliveParticles>();
            foreach(AliveParticles alpr in prtcls)
            {
                alpr.targets.Add(newBall.transform);
            }
        }
        if (effectsObjects[selectedEffect].GetComponentInChildren<FieldParticles>() != null)
        {
            FieldParticles[] prtcls = effectsObjects[selectedEffect].GetComponentsInChildren<FieldParticles>();
            foreach (FieldParticles flpr in prtcls)
            {
                flpr.targets.Add(newBall.transform);
            }
        }
        spawnedBalls.Add(newBall);
        RefreshBallsColor();
    }
    public void NextEffect()
    {
        if (selectedEffect == effectsObjects.Length - 1)
        {
            selectedEffect = 0;
            for (int i = 0; i < effectsObjects.Length; i++)
            {
                effectsObjects[i].SetActive(false);
            }
            effectsObjects[selectedEffect].SetActive(true);
        }
        else
        {
            selectedEffect++;
            for (int i = 0; i < effectsObjects.Length; i++)
            {
                effectsObjects[i].SetActive(false);
            }
            effectsObjects[selectedEffect].SetActive(true);
        }
        particleName.text = effectsObjects[selectedEffect].name;
        RefreshBallsColor();
    }
    public void PreviousEffect()
    {
        if (selectedEffect == 0)
        {
            selectedEffect = effectsObjects.Length - 1;
            for (int i = 0; i < effectsObjects.Length; i++)
            {
                effectsObjects[i].SetActive(false);
            }
            effectsObjects[selectedEffect].SetActive(true);
        }
        else
        {
            selectedEffect--;
            for (int i = 0; i < effectsObjects.Length; i++)
            {
                effectsObjects[i].SetActive(false);
            }
            effectsObjects[selectedEffect].SetActive(true);
        }
        RefreshBallsColor();
        particleName.text = effectsObjects[selectedEffect].name;
    }
    public void NextCamera()
    {
        if (selectedCamera == cameras.Length - 1)
        {
            selectedCamera = 0;
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].SetActive(false);
            }
            cameras[selectedCamera].SetActive(true);
        }
        else
        {
            selectedCamera++;
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].SetActive(false);
            }
            cameras[selectedCamera].SetActive(true);
        }
    }
    public void PreviousCamera()
    {
        if (selectedCamera == 0)
        {
            selectedCamera = cameras.Length - 1;
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].SetActive(false);
            }
            cameras[selectedCamera].SetActive(true);
        }
        else
        {
            selectedCamera--;
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].SetActive(false);
            }
            cameras[selectedCamera].SetActive(true);
        }
    }
    void Start()
    {
        for (int i = 0; i < effectsObjects.Length; i++)
        {
            effectsObjects[i].SetActive(false);
        }
        effectsObjects[selectedEffect].SetActive(true);
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].SetActive(false);
        }
        cameras[selectedCamera].SetActive(true);
        particleName.text = effectsObjects[selectedEffect].name;
    }
}
