using UnityEngine;

public class HookLine : MonoBehaviour
{
    public static HookLine Instance;

    public LineRenderer lineRenderer;
    public Transform rodTip;       // assign in Inspector

    private void Awake()
    {
        Instance = this;
        lineRenderer.positionCount = 0;
    }

    public void ShowLine(Vector3 fishPos)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, rodTip.position);
        lineRenderer.SetPosition(1, fishPos);
    }

    public void HideLine()
    {
        lineRenderer.positionCount = 0;
    }
}
