using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent (typeof(MeshFilter))]
[RequireComponent (typeof(MeshRenderer))]
public class Sea : MonoBehaviour {
	static int WIDTH = 10;
	static int HEIGHT = 10;

	Light dirLight = null;
	public int width = 60;
	public int height = 60;
	public float unit = 40.0f;

	MeshFilter _meshFilter = null;
	MeshRenderer _renderer = null;

	int getWidth() {
		if (width < 0)
			width = WIDTH;
		return width;
	}

	int getHeight() {
		if (height < 0) 
			height = HEIGHT;
		return height;
	}

	int getRow() {
		return getHeight() + 1;
	}

	int getCol() {
		return getWidth() + 1;
	}

	void setMaterialProps() {
		if (dirLight == null) {
			//Debug.Log("no light");
			//return;
		}

		//Mesh_renderer _renderer = GetComponent<Mesh_renderer>();
		if (_renderer == null) {
			//Debug.Log("no _renderer");
			//return;
		}

		Material material = _renderer.sharedMaterial;
		if (material == null) {
			//Debug.Log("no material");
			return;
		}

		Vector3 dirForward;

		//dirForward = Camera.main.transform.forward;
		//material.SetVector("_eyeWorldForward", new Vector4(dirForward.x, dirForward.y, dirForward.z, 0));


		dirForward = -1 * dirLight.transform.forward;
		//pos = dirLight.transform.position;
		//material.SetVector("_dirLightWorldPos", new Vector4(pos.x, pos.y, pos.z, 1));
		material.SetColor("_dirLightColor", dirLight.color);
		Debug.Log("light color:" + dirLight.color.ToString());
		material.SetVector("_dirLightWorldForward", new Vector4(dirForward.x, dirForward.y, dirForward.z, 0));
		material.SetFloat("_dirLightIntensity", dirLight.intensity);
		Debug.Log("light intensity:" + dirLight.intensity.ToString());

		Texture tex = material.GetTexture ("_MainTex");
		if (tex != null) 
			tex.wrapMode = TextureWrapMode.Repeat;

		tex = material.GetTexture ("_SecondTex");
		if (tex != null)
			tex.wrapMode = TextureWrapMode.Repeat;

		/*
		dirForward = -1 * spotLight.transform.forward;
		pos = spotLight.transform.position;
		material.SetVector("_spotLightWorldPos", new Vector4(pos.x, pos.y, pos.z, 1));
		material.SetColor("_spotLightColor", spotLight.color);
		material.SetVector("_spotLightWorldForward", new Vector4(dirForward.x, dirForward.y, dirForward.z, 0));
		material.SetFloat("_spotLightLen", spotLight.range);
		material.SetFloat("_spotLightAngle", spotLight.spotAngle);
		*/


		                  
	}

	void setMaterial() {
		Material material = _renderer.sharedMaterial;
		int width = getWidth();
		int height = getHeight();
		float [] offset = getOffset();

		material.SetFloat("_1_width", 1.0f/(width * unit));
		material.SetFloat("_1_height", 1.0f/(height * unit));
		material.SetFloat("_offsetX", offset[0]);
		material.SetFloat("_offsetZ", offset[1]);

		Texture tex;
		tex = material.GetTexture ("_MainTex");
		if (tex != null)
			tex.wrapMode = TextureWrapMode.Repeat;

		tex = material.GetTexture ("_NormalTex");
		if (tex != null) 
			tex.wrapMode = TextureWrapMode.Repeat;

		tex = material.GetTexture ("_LightMapTex");
		if (tex != null)
			tex.wrapMode = TextureWrapMode.Repeat;

		tex = material.GetTexture ("_BackgroundTex");
		if (tex != null)
			tex.wrapMode = TextureWrapMode.Repeat;


	}

	// Use this for initialization
	void Start () {
		//spotLight = GameObject.Find("") as Light;
		_meshFilter = GetComponent<MeshFilter>();
		_renderer = GetComponent<MeshRenderer>();
		//setMaterialProps();
		setMaterial();
		buildMesh();
	}

	int vertexIndex(int col, int row) {
		return row * getCol() + col;
	}

	int retangleIndex(int w, int h) {
		return h * getWidth() + w;
	}

	int[] triangleIndex(int w, int h) {
		int lb = vertexIndex(w, h);
		int rb = vertexIndex(w + 1, h);
		int lt = vertexIndex(w, h + 1);
		int rt = vertexIndex(w + 1, h + 1);
		return new int[] {lb, rb, lt, rt};
	}

	float [] getOffset() {
		int width = getWidth();
		int height = getHeight();
		return new float [] {unit * width/2f,  unit * height/2f};
	}


	void buildMesh() {
		int row = getRow();
		int col = getCol();

		int width = getWidth();
		int height = getHeight();


		int vertexCount = row * col;
		Vector3 [] vertices = new Vector3[vertexCount];
		Color [] colors = new Color[vertexCount];
		int [] triangles = new int[height * width * 2 * 3];

		float [] offset = getOffset();

		for (int r = 0; r < row; r++) {
			for(int c = 0; c < col; c++) {
				int i = vertexIndex(c, r);
				Vector3 v = new Vector3();
				float x = (float)c;
				float z = (float)r;
				v.x = x * unit - offset[0];
				v.z = z * unit - offset[1];
				v.y = 0 * unit;

				//Debug.Log("vertex:" + vertexCount.ToString() + " index:" + i.ToString() + " : " + v.ToString());
				vertices[i] = v;


				Color co = new Color();
				co.r = Random.value;
				co.g = Random.value;
				co.b = Random.value;
				co.a = Random.value;
				colors[i] = co;
			}
		}


		for (int h = 0; h < height; h++) {
			for(int w = 0; w < width; ++w) {
				int [] tis = triangleIndex(w, h);
				int ri = retangleIndex(w, h);
				int i = ri * 2 * 3;
				triangles[i + 0] = tis[0];
				triangles[i + 1] = tis[1];
				triangles[i + 2] = tis[2];
				triangles[i + 3] = tis[1];
				triangles[i + 4] = tis[3];
				triangles[i + 5] = tis[2];
				//Debug.Log("retangles:" + i.ToString() + " : " + triangles.ToString());

			}
		}
	
		Mesh mesh = _meshFilter.sharedMesh;
		if (mesh == null) {
			mesh = new Mesh();
			_meshFilter.sharedMesh = mesh;
		}
		//Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
		mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.colors = colors;


	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
		//setMaterialProps();
		setMaterial();
		buildMesh();
#endif
	}
}
