using UnityEngine;
using System.Collections.Generic;

public class ProceduralMeteorite : MonoBehaviour
{
    private List<Vector3> vertices = new List<Vector3>();
    public Texture2D texture;
    private int width = 256;
    private int height = 256;
    public float scale = 10f;
    public float offsetX = 100f;
    public float offsetY = 100f;
    private float rayDistance = 1f;

    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Renderer renderer = GetComponent<Renderer>();
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            Vector3 vertexInWorldSpace = mesh.vertices[i] + gameObject.transform.position;
            Vector3 worldPoint = gameObject.transform.TransformPoint(mesh.vertices[i]);
                       
            Ray ray = new Ray(worldPoint + mesh.vertices[i], -mesh.vertices[i] * 2);
            rayDistance = Vector3.Distance(worldPoint, gameObject.transform.position);
           

        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit)) { Debug.Log("No hit");  }
                
            Renderer rend = hit.transform.GetComponent<Renderer>();
            MeshCollider meshCollider = hit.collider.GetComponent<MeshCollider>();

            if (rend == null) { Debug.Log("rend == null"); return; }
            if (rend.sharedMaterial == null) { Debug.Log("rend.sharedMaterial == null"); return; }
            if (rend.sharedMaterial.mainTexture == null) { Debug.Log("rend.sharedMaterial.Texture == null"); return; }
            if (meshCollider == null) { Debug.Log("meshCollider == null"); return; }

            Texture2D tex = (Texture2D)renderer.material.mainTexture;
            Vector2 pixelUV = hit.textureCoord;
            pixelUV.x *= tex.width;
            pixelUV.y *= tex.height;
            
            Debug.Log("pixel value:"+tex.GetPixel((int)pixelUV.x, (int)pixelUV.y).grayscale);
            vertices.Add((ray.GetPoint(rayDistance - tex.GetPixelBilinear(hit.textureCoord.x,hit.textureCoord.y).grayscale) - gameObject.transform.position));
            
        }
        mesh.SetVertices(vertices);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.01f);
    }

    public Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }

    Color CalculateColor(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample);
    }

}