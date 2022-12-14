using System.Collections.Generic;

public class Node
{
    public NodeType Type { get; set; }

    public string Name { get; set; }

    public int Size { get; set; }

    public Node Parent { get; set; }

    public List<Node> Children = new List<Node>();
}
