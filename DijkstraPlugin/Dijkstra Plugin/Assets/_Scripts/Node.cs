using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(MeshRenderer))]
public class Node : MonoBehaviour
{
    public Vector3 Position;

    [SerializeField]
    private List<Node> adjecentNodes = new List<Node>();

    [SerializeField]
    private Material Default;
    [SerializeField]
    private Material Highlight;
    private bool selected = false;

    private Ray ray;
    private RaycastHit raycastHitInfo;
    private Node hit;

    private MeshRenderer mesh;

    public void AddAdjecentNode(Node node)
    {
        if (node != null)
        {
            adjecentNodes.Add(node);
            return;
        }
        Debug.LogError("Node " + node.name + " was null while adding it to adjecent waypoints");
    }

    private void Awake()
    {
        Position = transform.position;
        mesh = GetComponent<MeshRenderer>();
    }
    private void OnMouseDown()
    {
        if (NodeManager.Instance.StartNode != null)
        {
            NodeManager.Instance.EndNode = this;
        }
        else
        {
            NodeManager.Instance.StartNode = this;
        }
        if (selected)
        {
            DefaultMaterial();
        }
        else
        {
            HighlightMaterial();
        }
        selected = !selected;
    }
    public void DefaultMaterial()
    {
        mesh.material = Default;
    }
    public void HighlightMaterial()
    {
        mesh.material = Highlight;
    }
}
