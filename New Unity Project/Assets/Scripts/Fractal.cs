using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour {

    public float RndChild;
    public Mesh mymesh;
    public Material mymat;
    public int Maxi;
    private int Nowi;
    public float myscale;
    private Material[] materials;
    private static Vector3[] childDirections = {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };

    private static Quaternion[] childOrientations = {
        Quaternion.identity,
        Quaternion.Euler(0f, 0f, -90f),
        Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(90f,0f,0f),
        Quaternion.Euler(-90f,0f,0f)
    };

    // Use this for initialization
    void Start () {
        if (materials == null)
        {
            InitializeMaterials();
        }
        gameObject.AddComponent<MeshFilter>().mesh = mymesh;
        gameObject.AddComponent<MeshRenderer>().material = materials[Nowi];
        if (Nowi < Maxi)
        {
            StartCoroutine(CreateAll());            
        }        
	}
	

	// Update is called once per frame
	void Update () {
		
	}

    private void InitializeMaterials()
    {
        materials = new Material[Maxi + 1];
        for (int i = 0; i <= Maxi; i++)
        {
            materials[i] = new Material(mymat);
            materials[i].color = Color.Lerp(Color.white, Color.yellow, (float)i / Maxi);
        }
        materials[Maxi].color = Color.red;
    }

    private IEnumerator CreateAll()
    {
        for(int i=0;i<childDirections.Length; i++)
        {
            if(Random.value < RndChild)
            {
                yield return new WaitForSeconds(1);
                new GameObject("FChild").AddComponent<Fractal>().Init(this, i);
            }            
        }
    }
    private void Init( Fractal Parent, int Childindex)
    {
        RndChild = Parent.RndChild;
        mymesh = Parent.mymesh;
        mymat = Parent.mymat;
        Maxi = Parent.Maxi;
        Nowi = Parent.Nowi + 1;
        myscale = Parent.myscale;
        transform.parent = Parent.transform;
        transform.localScale = Vector3.one * myscale;
        transform.localPosition = childDirections[Childindex] * (0.5f + 0.5f * myscale);
        transform.localRotation = childOrientations[Childindex];
    }
}
