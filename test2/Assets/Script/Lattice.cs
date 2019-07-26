using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;
[System.Serializable]
public class blockgroup
{
    public Vector2Int[] blockPositions;
    public int[] group;
}
[System.Serializable]
public class successgroup
{
    public Vector2Int[] successOffsets;
    public int[] group;
}
public class Lattice : MonoBehaviour
{
    public static Lattice instance;
    public static float[] positionX;
    public static float[] positionY;
    public int scenes;
    private SceneFadeInOut fadescene;
    // public Vector2Int[] blockPositions;

    //public Vector2Int[] successOffsets;

    public blockgroup bg;

    public successgroup sg;

    private int a;
    private int b;
    private bool isSuccess;
    private bool allSuccess;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        positionX = new float[21];
        positionY = new float[21];
        float offsetX = transform.position.x/* - transform.localScale.x * 10f*/;
        float offsetY = transform.position.y/* - transform.localScale.x * 10f*/;
        for (int a = 0; a < 21; ++a)
        {
            positionX[a] = a * transform.localScale.x + offsetX;
            positionY[a] = a * transform.localScale.x + offsetY;
        }
    }

    public void IsSuccess(int index, int x, int y)
    {
        isSuccess = true;
        bg.blockPositions[index] = new Vector2Int(x, y);
        bool[] isuse = new bool[bg.blockPositions.Length];
        for (a = 0; a < bg.blockPositions.Length; ++a)
        {
            for (b = 0; b < bg.blockPositions.Length; ++b)
            {
                if (bg.group[a] == sg.group[b] && !isuse[b])
                {
                    if (bg.blockPositions[a].x - bg.blockPositions[0].x == sg.successOffsets[b].x && bg.blockPositions[a].y - bg.blockPositions[0].y ==
                    sg.successOffsets[b].y)
                    {
                        isuse[b] = true;
                    }
                }
            }
        }
        for (a = 0; a < bg.blockPositions.Length; ++a)
        {
            if (!isuse[a])
            {
                isSuccess = false;
                break;
            }
        }
        if (isSuccess)
        {
            //SceneManager.LoadScene(scenes);
            fadescene = GameObject.Find("RawImage").GetComponent<SceneFadeInOut>();
            fadescene.scenenum = scenes;
            fadescene.sceneEnding = true;
        }
    }
}
