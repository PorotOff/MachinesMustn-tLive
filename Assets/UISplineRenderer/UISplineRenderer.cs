using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Splines;

[AddComponentMenu("UI/UI Spline Renderer")]
[RequireComponent(typeof(CanvasRenderer))]
public class UISplineRenderer : Graphic {
    [Tooltip("The set of spline to extrude.")]
    public SplineContainer container;

    [Tooltip("Which spline from the set to render. -1 means render all splines")]
    public int splineIndex = -1;

    [Tooltip("Enable to regenerate the extruded mesh when the target Spline is modified.")]
    public bool rebuildOnSplineChange = true;

    [Tooltip("Thickness of the rendered spline.")]
    public float thickness = 1;

    [Tooltip("The number of edge loops that comprise the length of one unit of the mesh. The " +
    "total number of sections per spline is equal to \"Spline.GetLength() * segmentsPerUnit\".")]
    public float segmentsPerUnit = 1;

    [Tooltip("The section of the Spline to extrude.")]
    public Vector2 range = new Vector2(0f, 1f);

    public void Rebuild() =>
        SetVerticesDirty();

    protected override void OnEnable() {
        base.OnEnable();
        Spline.Changed += _OnSplineChanged;
    }

    protected override void OnDisable() {
        base.OnDisable();
        Spline.Changed -= _OnSplineChanged;
    }

    protected override void OnValidate() =>
        Rebuild();

    private void _OnSplineChanged(Spline spline, int _, SplineModification __) {
        if (container != null && container.Splines.Contains(spline) && rebuildOnSplineChange) {
            Rebuild();
        }
    }

    // Faster than spline.Evaluate because we don't need up vector
    private bool _Evaluate(Spline spline, float t, out float3 position, out float3 tangent) {
        if (spline.Count < 1) {
            position = float3.zero;
            tangent = new float3(0, 0, 1);
            return false;
        }

        var curveIndex = SplineUtility.SplineToCurveT(spline, t, out var curveT);
        var curve = spline.GetCurve(curveIndex);

        position = CurveUtility.EvaluatePosition(curve, curveT);
        tangent = CurveUtility.EvaluateTangent(curve, curveT);

        return true;
    }

    private void _PopulateMesh(VertexHelper vh, Spline spline) {
        var span = Mathf.Abs(range.x - range.y);
        var numSegments = Mathf.RoundToInt(spline.GetLength() * span * segmentsPerUnit);
        if (numSegments < 2) {
            return;
        }
        float norm = 1.0f / (numSegments - 1);

        var vert = UIVertex.simpleVert;
        vert.color = color;

        for (int p = 0; p < numSegments; p++) {
            var t = p * norm;
            _Evaluate(spline, t, out var position, out var tangent);
            position.z = 0; // Flatten					
            tangent.z = 0;	// Flatten
            
            var tangentLengthSqr = math.lengthsq(tangent);
            if (tangentLengthSqr < 1e-9) {
                if (t + norm <= 1.0f) {
                    tangent = spline.EvaluatePosition(t + norm) - position;
                }
                else if (t - norm >= 0.0f) {
                    tangent = position - spline.EvaluatePosition(t - norm);
                }
                tangent.z = 0; // Flatten
                tangentLengthSqr = math.lengthsq(tangent);
                
                // If still 0, use the x axis. Should be rare.
                if (tangentLengthSqr < 1e-9) {
                    tangent = new float3(1, 0, 0);
                    tangentLengthSqr = 1;
                }
            }
            
            // Normalize tangent
            var tangentLength = math.sqrt(tangentLengthSqr);
            tangent /= tangentLength;
            
            // Normal is tangent cross (0, 0, 1)
            var normal = new float3(tangent.y, -tangent.x, 0);
            var offset = normal * thickness * 0.5f;

            vert.position = position + offset;
            vert.uv0 = new Vector2(0, t);
            vh.AddVert(vert);

            vert.position = position - offset;
            vert.uv0 = new Vector2(1, t);
            vh.AddVert(vert);
        }

        for (int i = 0; i < numSegments - 1; i++) {
            int j = i * 2;
            vh.AddTriangle(j, j + 3, j + 2);
            vh.AddTriangle(j, j + 1, j + 3);
        }
    }

    protected override void OnPopulateMesh(VertexHelper vh) {
        vh.Clear();
        
        if (container == null) {
            return;
        }

        if (splineIndex >= 0 && splineIndex < container.Splines.Count) {
            _PopulateMesh(vh, container.Splines[splineIndex]);
        }
        else {
            foreach (var spline in container.Splines) {
                _PopulateMesh(vh, spline);
            }
        }
    }
}