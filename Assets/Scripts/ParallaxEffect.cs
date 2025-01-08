using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public static ParallaxEffect parallaxEffect;

    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layer;
        public Vector2 parallaxSpeed;
    }

    [System.Serializable]
    public class ParallaxGroup
    {
        public string name;
        public List<ParallaxLayer> layers;
    }

    public List<ParallaxGroup> parallaxGroups;
    public Camera mainCamera;
    public float transitionHeight = 10f;

    private Vector3 previousCameraPosition;
    private int currentGroupIndex = 0;

    void Start()
    {
        if (parallaxEffect == null)
        {
            parallaxEffect = this;
            DontDestroyOnLoad(gameObject);
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
            previousCameraPosition = mainCamera.transform.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        Vector3 cameraDelta = mainCamera.transform.position - previousCameraPosition;

        // Apply parallax to the active group
        foreach (var layer in parallaxGroups[currentGroupIndex].layers)
        {
            if (layer.layer != null)
            {
                Vector3 parallax = new Vector3(cameraDelta.x * layer.parallaxSpeed.x, cameraDelta.y * layer.parallaxSpeed.y, 0);
                layer.layer.position += parallax;
            }
        }

        // Check for camera height and switch groups
        if (mainCamera.transform.position.y > transitionHeight && currentGroupIndex < parallaxGroups.Count - 1)
        {
            currentGroupIndex++;
            Debug.Log($"Switched to parallax group: {parallaxGroups[currentGroupIndex].name}");
        }
        else if (mainCamera.transform.position.y < -transitionHeight && currentGroupIndex > 0)
        {
            currentGroupIndex--;
            Debug.Log($"Switched to parallax group: {parallaxGroups[currentGroupIndex].name}");
        }

        previousCameraPosition = mainCamera.transform.position;
    }

    void OnDrawGizmos()
    {
        if (mainCamera != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(new Vector3(mainCamera.transform.position.x - 10, transitionHeight, 0), new Vector3(mainCamera.transform.position.x + 10, transitionHeight, 0));
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(mainCamera.transform.position.x - 10, -transitionHeight, 0), new Vector3(mainCamera.transform.position.x + 10, -transitionHeight, 0));
        }
    }
}
