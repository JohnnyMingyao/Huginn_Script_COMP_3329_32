
using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    public float floatSpeed = 1f;        // How fast it moves
    public float floatHeight = 0.25f;    // Max height above and below
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = startPos + new Vector3(0, yOffset, 0);
    }
}

