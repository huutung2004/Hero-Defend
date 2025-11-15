using UnityEngine;

public class ScaleCamera : MonoBehaviour
{
    public static ScaleCamera _instance{ get; private set; }
    private Camera cam;
    private float lastAspect;
    [SerializeField] private float baseOrthoSize = 5f; // kích cỡ camera khi ngang

    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        cam = Camera.main;
        lastAspect = (float)Screen.width / Screen.height;
        UpdateCamera();
    }

    void Update()
    {
        float aspect = (float)Screen.width / Screen.height;
        if (Mathf.Abs(aspect - lastAspect) > 0.01f)
        {
            UpdateCamera();
            lastAspect = aspect;
        }
    }

    void UpdateCamera()
    {
        float aspect = (float)Screen.width / Screen.height;

        // Giữ chiều cao hiển thị cố định, thay đổi zoom theo aspect
        if (aspect < 1f) // portrait
        {
            cam.orthographicSize = (baseOrthoSize / aspect)*2;
            Debug.Log(cam.orthographicSize);
        }
        else // landscape
        {
            cam.orthographicSize = baseOrthoSize;
        }

        Debug.Log("Orientation changed → new orthoSize: " + cam.orthographicSize);
    }
}
