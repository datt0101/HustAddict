using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.AI;

public class CombineMeshesByMaterial : MonoBehaviour
{
    void Start()
    {
        // Dictionary để lưu trữ các vật liệu và các mesh tương ứng
        Dictionary<Material, List<CombineInstance>> combinesByMaterial = new Dictionary<Material, List<CombineInstance>>();
        List<Mesh> combinedMeshes = new List<Mesh>();

        // Lấy tất cả MeshRenderer và MeshFilter từ các đối tượng con
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();

        // Duyệt qua từng MeshRenderer và MeshFilter
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            Material material = meshRenderers[i].sharedMaterial;
           
            CombineInstance ci = new CombineInstance
            {
                mesh = meshFilters[i].sharedMesh,
                //mesh = meshFilters[i].sharedMesh,
                transform = meshFilters[i].transform.localToWorldMatrix
            };
            
            if (!combinesByMaterial.ContainsKey(material))
            {
                combinesByMaterial[material] = new List<CombineInstance>();
            }
            combinesByMaterial[material].Add(ci);

            meshRenderers[i].gameObject.SetActive(false); // Vô hiệu hóa đối tượng con sau khi hợp nhất
        }

        // Duyệt qua từng vật liệu và tạo mesh hợp nhất cho mỗi vật liệu
        foreach (Material material in combinesByMaterial.Keys)
        {
            List<CombineInstance> combines = combinesByMaterial[material];

            Mesh combinedMesh = new Mesh();
            combinedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

            foreach( var combine in combines)
            {
                Debug.Log(combine.mesh.name+ ": " + combine.mesh.isReadable);
            }
            combinedMesh.RecalculateNormals();
            combinedMesh.CombineMeshes(combines.ToArray(),true,true);
            
            combinedMeshes.Add(combinedMesh);

            GameObject combinedObject = new GameObject("Combined Mesh - " + material.name);
            combinedObject.transform.SetParent(transform);
            MeshFilter mf = combinedObject.AddComponent<MeshFilter>();
            mf.mesh = combinedMesh;
            MeshRenderer mr = combinedObject.AddComponent<MeshRenderer>();
            mr.material = material;

            // Thêm MeshCollider cho đối tượng đã hợp nhất
            MeshCollider mc = combinedObject.AddComponent<MeshCollider>();
            mc.sharedMesh = combinedMesh;
        }
    }

    //void Start()
    //{
    //    MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
    //    CombineInstance[] combine = new CombineInstance[meshFilters.Length];

    //    for (int i = 0; i < meshFilters.Length; i++)
    //    {
    //        combine[i].mesh.MarkDynamic();
    //        combine[i].mesh = meshFilters[i].sharedMesh;
    //        combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
    //        meshFilters[i].gameObject.SetActive(false);
    //        Debug.Log("before: " + combine[i].mesh.isReadable);

    //        Debug.Log("after: " + combine[i].mesh.isReadable);
    //    }

    //    Mesh combinedMesh = new Mesh();
    //    combinedMesh.CombineMeshes(combine, true, true);

    //    gameObject.AddComponent<MeshFilter>().sharedMesh = combinedMesh;
    //    gameObject.AddComponent<MeshRenderer>().material = meshFilters[0].GetComponent<MeshRenderer>().sharedMaterial;

    //    gameObject.SetActive(true);
    //}

}