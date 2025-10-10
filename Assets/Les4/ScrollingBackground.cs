using UnityEngine;

public class scrollingBackground : MonoBehaviour
{
    [SerializeField] private Renderer bgRenderer;
    [Tooltip("Texture scroll speed in texture units per second")]
    public float speed = 2.0f;

    void Reset()
    {
        bgRenderer = GetComponent<Renderer>();
    }

    void Start()
    {
        if (bgRenderer == null)
        {
            Debug.LogWarning("[scrollingBackground] bgRenderer not assigned - attempting to get Renderer from this GameObject.");
            bgRenderer = GetComponent<Renderer>();
        }

        if (bgRenderer == null)
        {
            Debug.LogError("[scrollingBackground] No Renderer found. Please assign the Quad's Renderer in the inspector.");
            enabled = false;
            return;
        }

        if (bgRenderer.material != null && bgRenderer.material.mainTexture != null)
        {
            bgRenderer.material.mainTexture.wrapMode = TextureWrapMode.Repeat;
        }
    }

    void Update()
    {
        if (bgRenderer == null) return;
        bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0f);
    }
}
