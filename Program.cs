using System.Diagnostics;

// See tests below

static bool FindNeedle(Node head, string niddle)
{
    // Skipping arguments validation

    var currentNode = head;
    int matchingIndex = 0;

    while (currentNode != null)
    {
        var nodeValue = currentNode.Value;

        for (int i = 0; i < nodeValue.Length; i++)
        {
            if (nodeValue[i] != niddle[matchingIndex])
            {
                if (matchingIndex > 0) // no backtracking required if matching index is 0
                {
                    // handling pointer invalidation here
                    // example: aab compared to ab -> when nodeValue[1]|a != niddle[1]|b -> backtracking the comparison
                    // so that nodeValue[1] is compared again
                    matchingIndex -= 1;
                    i -= 1;
                }

                continue;
            }

            matchingIndex += 1;

            if (matchingIndex == niddle.Length)
            {
                return true;
            }
        }

        currentNode = currentNode.Next;
    }

    return false;
}

#region Tests

void WhenHeadContainsNeedle_ReturnsTrue()
{
    // abcd -> null | abcd
    var head = new Node("abcd");

    bool found = FindNeedle(head, "abcd");

    Debug.Assert(found);

    // abcd -> null | bc
    head = new Node("abcd");

    found = FindNeedle(head, "bc");

    Debug.Assert(found);
}

void WhenAnyNodeContainsNeedle_ReturnsTrue()
{
    // add -> abcd -> bb -> null | bc
    var node2 = new Node("bb");
    var node1 = new Node("abcd", node2);
    var head = new Node("add", node1);

    bool found = FindNeedle(head, "bc");

    Debug.Assert(found);

    // add -> abcd -> bb -> null | da
    node2 = new Node("bb");
    node1 = new Node("abcd", node2);
    head = new Node("add", node1);

    found = FindNeedle(head, "da");

    Debug.Assert(found);
}

void WhenNeedleSpreadsAcrossNodes_ReturnsTrue()
{
    // a -> b -> c -> null | abc
    var node2 = new Node("c");
    var node1 = new Node("b", node2);
    var head = new Node("a", node1);

    bool found = FindNeedle(head, "abc");

    Debug.Assert(found);

    // aa -> b -> c -> null | abc
    node2 = new Node("c");
    node1 = new Node("b", node2);
    head = new Node("aa", node1);

    found = FindNeedle(head, "abc");

    Debug.Assert(found);
}

void WhenNeedleNotPartOfAnyNode_ReturnsFalse()
{
    // aabh -> b -> c -> null| abc
    var node2 = new Node("c");
    var node1 = new Node("b", node2);
    var head = new Node("aabh", node1);

    bool found = FindNeedle(head, "abc");

    Debug.Assert(!found);

    // abacab -> ab -> bc -> null | abc
    node2 = new Node("c");
    node1 = new Node("b", node2);
    head = new Node("aabh", node1);

    found = FindNeedle(head, "abc");

    Debug.Assert(!found);
}

#endregion

WhenHeadContainsNeedle_ReturnsTrue();
WhenAnyNodeContainsNeedle_ReturnsTrue();
WhenNeedleSpreadsAcrossNodes_ReturnsTrue();
WhenNeedleNotPartOfAnyNode_ReturnsFalse();