using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public TMP_Text textComponent;  // Drag your TextMeshPro Text here
    public Vector3 offset = new Vector3(0, 1.5f, 0);  // Text position offset
    public float floatSpeed = 1f;
    public float floatHeight = 0.1f;

    private Vector3 startLocalPos;

    void Start()
    {
        startLocalPos = transform.localPosition;
        ApplyOffset();
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.localPosition = startLocalPos + new Vector3(0, yOffset, 0);
    }

    private void ApplyOffset()
    {
        transform.localPosition = startLocalPos + offset;
    }
}

