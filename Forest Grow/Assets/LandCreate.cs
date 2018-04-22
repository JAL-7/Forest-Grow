using UnityEngine;
using System.Collections.Generic;

public class LandCreate : MonoBehaviour
{

    public MeshFilter meshFilter;
    Mesh mesh;

    public Gradient coloring;
    public Gradient coloringB;

    public GameObject[] trees = new GameObject[5];
    public Transform treePar;

    public float minX;
    public float minZ;
    public float maxX;
    public float maxZ;

    List<Vector3> verts = new List<Vector3>();
    List<bool> bools = new List<bool>();
    List<int> tris = new List<int>();

    void Start()
    {

        mesh = meshFilter.mesh = new Mesh();

        float start = 24;

        for (int i = (int)transform.position.x; i < (int)transform.position.x+100; i++) {
            for (int j = (int)transform.position.z; j < (int)transform.position.z+100; j++) {

                verts.Add(new Vector3(i,0,j));
                verts.Add(new Vector3(i+1,0,j+1));
                verts.Add(new Vector3(i+1,0,j));
                verts.Add(new Vector3(i,0,j));
                verts.Add(new Vector3(i,0,j+1));
                verts.Add(new Vector3(i+1,0,j+1));

                if (i >= minX && i <= maxX && j >=minZ && j <= maxZ) {
                    bools.Add(true);
                    bools.Add(true);
                    bools.Add(true);
                    bools.Add(true);
                    bools.Add(true);
                    bools.Add(true);
                }
                else {
                    bools.Add(false);
                    bools.Add(false);
                    bools.Add(false);
                    bools.Add(false);
                    bools.Add(false);
                    bools.Add(false);
                }

            }
        }

        for (int i = 0; i < 60000; i++) {
            verts[i] = new Vector3(verts[i].x, 6*Mathf.PerlinNoise(verts[i].x/start,verts[i].z/start), verts[i].z);
        }

        for (int i = 0; i < 60000; i++) {
            tris.Add(i);
        }

        mesh.SetVertices(verts);
        mesh.SetTriangles(tris,0);;


        Color[] colors = new Color[verts.Count];

        for (int i = 0; i < 60000; i+=3)
        {

            if (!bools[i])
            {
                colors[i] = coloring.Evaluate(verts[i].y/6+Random.Range(-0.1f,0.1f));
                colors[i + 1] = coloring.Evaluate(verts[i].y/6+Random.Range(-0.1f,0.1f));
                colors[i + 2] = coloring.Evaluate(verts[i].y/6+Random.Range(-0.1f,0.1f));
            }
            else {
                colors[i] =coloringB.Evaluate(verts[i].y/6+Random.Range(-0.1f,0.1f));
                colors[i + 1] = coloringB.Evaluate(verts[i].y/6+Random.Range(-0.1f,0.1f));
                colors[i + 2] = coloringB.Evaluate(verts[i].y/6+Random.Range(-0.1f,0.1f));
            }
        }

        mesh.colors = colors;

        for (int i = (int)transform.position.x; i < (int)transform.position.x+100; i+=10) {
            for (int j = (int)transform.position.z; j < (int)transform.position.z+100; j+=10) {

                if (Random.Range(0.0f,5.0f) < 1.5) {
                    continue;
                }

                float x = Random.Range(2.2f,7.8f);
                float z = Random.Range(2.2f,7.8f);

                int tree = Random.Range(0,5);

                int path = 600 * (int)(i+x-transform.position.x) + (int)(j+z-transform.position.z) * 6;

                Instantiate(trees[tree], new Vector3(i+x, trees[tree].transform.position.y + verts[path].y + 2.1f, j+z), new Quaternion(0, Random.Range(0,360), 0, 1), treePar);

            }
        }

        transform.position = Vector3.zero;

    }

}
