internal class Node
{
    public Node? Next { get; init; }
    public string Value { get; }

    public Node(string value, Node? next = null)
    {
        Value = value;
        Next = next;
    }
}
