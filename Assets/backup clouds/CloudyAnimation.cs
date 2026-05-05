using UnityEngine;

public class CloudAnimator : MonoBehaviour
{
    [Header("Cloud Materials")]
    [Tooltip("Cần Material dùng Shader Unlit có Texture Base Map")]
    public Material cloudMaterial;

    [Header("Animation Settings")]
    [Tooltip("Tốc độ mây trôi theo trục X và Y")]
    public Vector2 scrollSpeed = new Vector2(0.01f, 0.0f);

    [Tooltip("Tốc độ mây xoay (thử để 1-2 độ để có hiệu ứng tự nhiên)")]
    public float rotationSpeed = 0.0f;

    // Cache property IDs for performance
    private int _baseMapPropertyID;

    private void Start()
    {
        if (cloudMaterial == null)
        {
            // Tự động tìm Material nếu chưa gán
            cloudMaterial = GetComponent<Renderer>()?.material;
        }

        if (cloudMaterial != null)
        {
            // Thử tìm ID của Universal Unlit
            _baseMapPropertyID = Shader.PropertyToID("_BaseMap");

            // Nếu không tìm thấy, thử tìm ID của Unlit cũ
            if (!cloudMaterial.HasProperty(_baseMapPropertyID))
            {
                _baseMapPropertyID = Shader.PropertyToID("_MainTex");
            }
        }
        else
        {
            Debug.LogError("CloudAnimator: Không tìm thấy Material để áp dụng hiệu ứng.");
            enabled = false; // Tắt script nếu không có Material
        }
    }

    private void Update()
    {
        if (cloudMaterial == null) return;

        // 1. Scrolling: Tạo offset dựa trên thời gian
        Vector2 offset = cloudMaterial.GetTextureOffset(_baseMapPropertyID);
        offset += scrollSpeed * Time.deltaTime;

        // Tối ưu hóa: Giữ offset trong khoảng 0-1 để tránh lỗi tích lũy giá trị lớn
        offset.x %= 1.0f;
        offset.y %= 1.0f;

        cloudMaterial.SetTextureOffset(_baseMapPropertyID, offset);

        // 2. Rotation: Xoay object
        if (rotationSpeed != 0.0f)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }
}