using UnityEngine;

public class ParticleDisplay2D : MonoBehaviour
{
    [SerializeField] public Mesh mesh;
    [SerializeField] public Shader shader;
    [SerializeField] public float scale;
    [SerializeField] public Gradient colorMap;
    [SerializeField] public int gradientResolution;
    [SerializeField] public float velocityDisplayMax;

    private Material material;
    private ComputeBuffer argsBuffer;
    private Bounds bounds;
    private Texture2D gradientTexture;
    private bool needsUpdate;

    public void Init(FluidSimulation2D sim) {
        material = new Material(shader);
        material.SetBuffer("Positions2D", sim.positionBuffer);
        material.SetBuffer("Velocities", sim.velocityBuffer);
        material.SetBuffer("DensityData", sim.densityBuffer);

        argsBuffer = ComputeHelper.CreateArgsBuffer(mesh, sim.positionBuffer.count);
        bounds = new Bounds(Vector3.zero, Vector3.one * 10000);
    }

    private void LateUpdate() {
        if (shader != null) {
            UpdateSettings();
            Graphics.DrawMeshInstancedIndirect(mesh, 0, material, bounds, argsBuffer);
        }
    }

    private void UpdateSettings() {
        if (needsUpdate) {
            needsUpdate = false;
            TextureFromGradient(ref gradientTexture, gradientResolution, colorMap);
            material.SetTexture("ColorMap", gradientTexture);

            material.SetFloat("scale", scale);
            material.SetFloat("velocityMax", velocityDisplayMax);
        }
    }

    public static void TextureFromGradient(ref Texture2D texture, int width, Gradient gradient, FilterMode filterMode = FilterMode.Bilinear) {
        if (texture == null)
		{
			texture = new Texture2D(width, 1);
		}
		else if (texture.width != width)
		{
			texture.Reinitialize(width, 1);
		}
		if (gradient == null)
		{
			gradient = new Gradient();
			gradient.SetKeys(
				new GradientColorKey[] { new GradientColorKey(Color.black, 0), new GradientColorKey(Color.black, 1) },
				new GradientAlphaKey[] { new GradientAlphaKey(1, 0), new GradientAlphaKey(1, 1) }
			);
		}
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.filterMode = filterMode;

		Color[] cols = new Color[width];
		for (int i = 0; i < cols.Length; i++)
		{
			float t = i / (cols.Length - 1f);
			cols[i] = gradient.Evaluate(t);
		}
		texture.SetPixels(cols);
		texture.Apply();
    }

    private void OnValidate() {
        needsUpdate = true;
    }

    private void OnDestroy() {
        ComputeHelper.Release(argsBuffer);
    }
}
