using UnityEngine;

public class LineAttacher : MonoBehaviour
{
    [SerializeField] private Transform attachedObj;
    private LineRenderer line;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    private void LateUpdate()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, attachedObj.position);
    }
}
