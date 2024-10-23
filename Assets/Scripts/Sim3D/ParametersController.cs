using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParametersController : MonoBehaviour
{
    [SerializeField] private FluidSimulation3D sim;
    [SerializeField] private ParticleSpawner3D spawner;

    [Header("UI")]
    [SerializeField] private TMP_InputField numParticlesInput;
    [SerializeField] private TMP_Text gravityText;
    [SerializeField] private TMP_Text densityText;
    [SerializeField] private TMP_Text viscosityText;

    private void Awake() {
        SetGravity(10);
        SetDensity(700);
        SetViscosity(0.0008f);
    }

    public void SetNumParticlesPerAxis() {
        int numParticles = int.Parse(numParticlesInput.text);
        spawner.numParticlesPerAxis = numParticles;
    }

    public void SetGravity(float value) {
        sim.gravity = -value;

        string gravityString = value.ToString();
        if (gravityString.Length > 4) {
            gravityString = gravityString.Substring(0, 4);
        }

        gravityText.text = gravityString;
    }

    public void SetDensity(float value) {
        sim.targetDensity = value;

        float falseValue = value * 1.42f;
        string densityString = falseValue.ToString();
        if (densityString.Length > 5) {
            densityString = densityString.Substring(0, 5);
        }

        densityText.text = densityString;
    }

    public void SetViscosity(float value) {
        sim.viscosityStrength = value;

        float falseValue = value * 10;
        string viscosityString = falseValue.ToString();
        if (viscosityString.Length > 6) {
            viscosityString = viscosityString.Substring(0, 6);
        }

        viscosityText.text = viscosityString;
    }
}
