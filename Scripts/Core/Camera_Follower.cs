using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow2D : MonoBehaviour
{
    public Transform target;  // 玩家角色
    
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);
    

    [SerializeField] private bool enableBounds = true;
    [SerializeField] private Rect levelBounds;
    
    private Camera _camera;
    
    //get the level bounds from the scene
    private void CalculateLevelBounds()
    {
        // 找到所有标记为边界的对象
        GameObject[] boundsMarkers = GameObject.FindGameObjectsWithTag("LevelBounds");
        
        if (boundsMarkers.Length == 0)
        {
            Debug.LogWarning("the boundary is not found, using default value");
            levelBounds = new Rect(-10, -5, 20, 10);
            return;
        }
        
        float minX = float.MaxValue, maxX = float.MinValue;
        float minY = float.MaxValue, maxY = float.MinValue;
        
        foreach (GameObject marker in boundsMarkers)
        {
            Vector3 pos = marker.transform.position;
            minX = Mathf.Min(minX, pos.x);
            maxX = Mathf.Max(maxX, pos.x);
            minY = Mathf.Min(minY, pos.y);
            maxY = Mathf.Max(maxY, pos.y);
        }
        
        levelBounds = new Rect(minX, minY, maxX - minX, maxY - minY);
    }





    private void Awake()
    {
        _camera = GetComponent<Camera>();
        

        if (target == null)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) target = player.transform;
        }
        CalculateLevelBounds();
    }
    
    private void LateUpdate()
    {
        if (target == null) return;
        
        Vector3 desiredPosition = target.position + offset;
        
        // 应用边界限制
        if (enableBounds)
        {
            float camHeight = _camera.orthographicSize;
            float camWidth = camHeight * _camera.aspect;
            
            desiredPosition.x = Mathf.Clamp(
                desiredPosition.x, 
                levelBounds.xMin + camWidth, 
                levelBounds.xMax - camWidth);
                
            desiredPosition.y = Mathf.Clamp(
                desiredPosition.y, 
                levelBounds.yMin + camHeight, 
                levelBounds.yMax - camHeight);
        }
        
        // 平滑移动
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position, 
            desiredPosition, 
            smoothSpeed * Time.deltaTime * 60); // 乘以60使参数范围更直观
        
        transform.position = smoothedPosition;
    }
    
    // 在Scene视图中绘制边界(仅编辑器)
    private void OnDrawGizmosSelected()
    {
        if (!enableBounds) return;
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(levelBounds.center, levelBounds.size);
    }
}
