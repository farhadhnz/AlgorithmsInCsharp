
namespace Algorithms
{
    public class Tree
    {
        private class Node
        {
            private string Label;

            public string GetLabel()
            {
                return Label;
            }

            public Node(string label)
            {
                Label = label;
            }
        }


        private Dictionary<string, Node> nodes = new Dictionary<string, Node>();
        private Dictionary<Node, List<Node>> adjacencyList = new Dictionary<Node, List<Node>>();

        private Node InsertNode(string label)
        {
            var node = new Node(label);

            nodes[label] = node;

            if (!adjacencyList.ContainsKey(node))
                adjacencyList[node] = new List<Node>();

            return node;
        }
        public void InsertEdge(string from, string to)
        {
            var fromNode = new Node(from);
            if (!nodes.ContainsKey(from))
                fromNode = InsertNode(from);
            else
                fromNode = nodes[from];

            var toNode = new Node(to);
            if (!nodes.ContainsKey(to))
                toNode = InsertNode(to);
            else
                toNode = nodes[to];

            adjacencyList[fromNode].Add(toNode);
            adjacencyList[toNode].Add(fromNode);
        }

        private Node GetNodeParent(Node node)
        {
            return adjacencyList.FirstOrDefault(x => x.Value.Contains(node)).Key;
        }


        private void SearchBFSVirus(string target, string exit, List<List<string>> dict)
        {
            var frontier = new Queue<Node>();
            var visited = new HashSet<Node>();
            var parents = new Dictionary<Node, Node>();

            frontier.Enqueue(nodes[exit]);
            visited.Add(nodes[exit]);

            while (frontier.Any())
            {
                var current = frontier.Dequeue();

                if (current.GetLabel() == target)
                {
                    var path = new List<Node>();
                    path.Add(nodes[target]);

                    while (path.Last().GetLabel() != exit)
                        path.Add(parents[path.Last()]);

                    path.Reverse();

                    dict.Add(path.Select(x => x.GetLabel()).ToList());
                    return;
                }

                foreach (var child in adjacencyList[current])
                {
                    if (!visited.Contains(child))
                    {
                        frontier.Enqueue(child);
                        visited.Add(child);
                        parents[child] = current;
                    }
                }
            }
        }


        public string GetBestLink(string target, List<string> exits)
        {
            var dict = new List<List<string>>();

            for (int i = 0; i < exits.Count; i++)
            {
                SearchBFSVirus(target, exits[i], dict);
            }

            var orderedDict = dict.OrderBy(x => x.Count);
            var maxVal = orderedDict.FirstOrDefault();

            var linkStart = nodes[maxVal[0]];
            var linkEnd = nodes[maxVal[1]];

            adjacencyList[linkStart].Remove(linkEnd);
            adjacencyList[linkEnd].Remove(linkStart);

            return $"{linkStart.GetLabel()} {linkEnd.GetLabel()}";
        }
    }
}