using UnityEngine;

public class BlockDrag : MonoBehaviour
{
    public new static Camera camera;

    public int height;
    public int index;
    public int width;

    private int a;
    private float distance;
    private bool isMatched;
    private float minDistance;
    private Vector3 offset;
    private int x;
    private int y;

    public static float percent = 0.3f;    //单次插值从from到to的百分比
    public static float boundary = 0.1f;     //插值最小精度

    private Vector3 targetPos;  //插值移动的目标位置
    private bool flag; //是否归位flag

    private void Start()
    {
        if (camera == null)
        {
            camera = GameObject.Find("Camera").GetComponent<Camera>();
        }

        targetPos = transform.position;
    }

    private void OnMouseDown()
    {
        offset = transform.position - camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        transform.position = camera.ScreenToWorldPoint(Input.mousePosition) + offset;
    }

    private void OnMouseUp()
    {
        minDistance = Mathf.Abs(Lattice.positionX[0] - transform.position.x);
        x = 0;
        for (a = 1; a < 21 - width; ++a)
        {
            distance = Mathf.Abs(Lattice.positionX[a] - transform.position.x);
            if (distance < minDistance)
            {
                minDistance = distance;
                x = a;
            }
        }
        minDistance = Mathf.Abs(Lattice.positionY[0] - transform.position.y);
        y = 0;
        for (a = 1; a < 21 - height; ++a)
        {
            distance = Mathf.Abs(Lattice.positionY[a] - transform.position.y);
            if (distance < minDistance)
            {
                minDistance = distance;
                y = a;
            }
        }
        //transform.position = new Vector3(Lattice.positionX[x], Lattice.positionY[y], transform.position.z);

        targetPos = new Vector3(Lattice.positionX[x], Lattice.positionY[y], transform.position.z);
        flag = true;
        Lattice.instance.IsSuccess(index, x, y);
    }

    private void Update()
    {
        if (targetPos != transform.position&&flag)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, percent);
            if (Mathf.Abs(transform.position.x - targetPos.x) + Mathf.Abs(transform.position.y - targetPos.y) < boundary)
            {
                transform.position = targetPos;
                flag = false;
            }
        }
    }
}
