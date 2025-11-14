using UnityEngine;

public class CanRandomMaterial : MonoBehaviour
{
    [SerializeField] private Material[] canMaterials;

    private MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (canMaterials.Length>0)
        {
            int rand = Random.Range(0, canMaterials.Length);
            meshRenderer.material = canMaterials[rand];
        }
    }

}
