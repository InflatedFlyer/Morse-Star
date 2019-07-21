using UnityEngine;

public class Lattice : MonoBehaviour
{
    public static Lattice instance;
    public static float[] positionX;
    public static float[] positionY;

    public Vector2Int[] blockPositions;
    public Vector2Int[] successOffsets;

    private int a;
    private bool isSuccess;

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
        blockPositions[index] = new Vector2Int(x, y);
        isSuccess = true;
        for (a = 1; a < blockPositions.Length; ++a)
        {
            if (blockPositions[a].x - blockPositions[0].x != successOffsets[a].x || blockPositions[a].y - blockPositions[0].y !=
                successOffsets[a].y)
            {
                isSuccess = false;
                break;
            }
        }
        if (isSuccess)
        {
            //游戏完成时的动作
            GameObject.Find("Canvas/Text Success").GetComponent<UnityEngine.UI.Text>().text = "Success";
        }
    }
}
