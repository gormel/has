using UnityEngine;
using System.Collections;
using Assets.Scripts.Core;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public RenderTexture Source;
    public Camera Camera;
    public Root Root;

    void Start()
    {
        var map = Root.MapView.Model<Map>();
        Camera.transform.localPosition = new Vector3(map.Width / 2f, map.Height / 2f, -1);
        Camera.orthographicSize = map.Width / 2f;
    }

    void Update()
    {
    }
}
