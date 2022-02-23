using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithms
{

    public class LabyrinthGraph
    {
        private class Node
        {
            private string Label;
            private NodeContent Content;

            public string GetLabel()
            {
                return Label;
            }

            public NodeContent GetContent()
            {
                return Content;
            }

            public Node(string label, NodeContent content = null)
            {
                Label = label;
                Content = content;
            }
        }

        private class NodeContent
        {
            public int X { get; set; }
            public int Y { get; set; }
            public string StrContent { get; set; }

            public NodeContent(int x, int y, string strContent)
            {
                X = x;
                Y = y;
                StrContent = strContent;
            }
        }

        private Dictionary<string, Node> nodes = new Dictionary<string, Node>();
        private Dictionary<Node, List<Node>> adjacencyList = new Dictionary<Node, List<Node>>();

        private Node InsertNode(string label, NodeContent content)
        {
            var node = new Node(label, content);

            nodes[label] = node;

            if (!adjacencyList.ContainsKey(node))
                adjacencyList[node] = new List<Node>();

            return node;
        }
        private void InsertEdge(string from, string to)
        {
            var fromNode = new Node(from);
            if (!nodes.ContainsKey(from))
                return;
            else
                fromNode = nodes[from];

            var toNode = new Node(to);
            if (!nodes.ContainsKey(to))
                return;
            else
                toNode = nodes[to];

            adjacencyList[fromNode].Add(toNode);
            // adjacencyList[toNode].Add(fromNode);
        }

        private Node GetNodeParent(Node node)
        {
            return adjacencyList.FirstOrDefault(x => x.Value.Contains(node)).Key;
        }

        public void CreateGraphBasedOnInputs(int rows, int cols)
        {
            // Adding Nodes
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < cols; i++)
                {
                    InsertNode($"Node{i} {j}", new NodeContent(i, j, "?"));
                }
            }

            // Adding Edges
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < cols; i++)
                {
                    if (j != 0)
                        InsertEdge($"Node{i} {j}", $"Node{i} {j - 1}");
                    if (j != rows - 1)
                        InsertEdge($"Node{i} {j}", $"Node{i} {j + 1}");
                    if (i != 0)
                        InsertEdge($"Node{i} {j}", $"Node{i - 1} {j}");
                    if (i != cols - 1)
                        InsertEdge($"Node{i} {j}", $"Node{i + 1} {j}");
                }
            }
        }

        public bool DefineMove(int startY, int startX, string[,] matrix, bool alarmed)
        {
            var start = nodes.First(x => x.Value.GetContent().X == startX &&
                                         x.Value.GetContent().Y == startY);
            var targets = new string[] { "?", "C" };
            var alarmTargets = new string[] { "T" };

            var forbidens = new string[] { "#" };

            UpdateNodes(matrix);


            // If position is at control room, try to get back
            if (start.Value.GetContent().StrContent == "C")
                alarmed = true;

            if (alarmed)
                GetBestRoute(alarmTargets, new List<string>() { " " }, forbidens, start.Value);

            // Find all available movements
            var availableMovements = FindAvailableMovements(start.Value, forbidens);

            // If only one available => do movement
            if (availableMovements.Count == 1)
                DoMovement(availableMovements[0]);
            else
                // Find best way
                GetBestRoute(targets, availableMovements, forbidens, start.Value);

            return alarmed;
        }

        private void DoMovement(string direction)
        {
            // Move in Direction
            Console.WriteLine(direction.ToUpper());
        }

        private List<string> FindAvailableMovements(Node start, string[] forbidens)
        {
            var availableChildren = new List<Node>();
            foreach (var child in adjacencyList[start])
            {
                if (!forbidens.Any(x => x == child.GetContent().StrContent))
                {
                    availableChildren.Add(child);
                }
            }

            var availableMovements = new List<string>();
            foreach (var child in availableChildren)
            {
                var direction = GetDirection(start, child);
                availableMovements.Add(direction);
            }
            return availableMovements;
        }

        private void UpdateNodes(string[,] matrix)
        {
            for (int j = 0; j < matrix.GetLength(0); j++)
            {
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    var node = nodes.First(x => x.Value.GetContent().X == i &&
                                         x.Value.GetContent().Y == j);

                    if (matrix[j, i] == ".")
                    {

                    }
                    node.Value.GetContent().StrContent = matrix[j, i];
                }
            }
        }

        private void GetBestRoute(string[] targets, List<string> availableMoves, string[] forbidens, Node start)
        {
            var dict = new List<List<Node>>();

            for (int i = 0; i < availableMoves.Count; i++)
            {
                SearchBFSLab(targets, start, forbidens, dict);
            }

            var orderedDict = dict.OrderBy(x => x.Count);
            var maxVal = orderedDict.FirstOrDefault();

            var direction = GetDirection(maxVal[0], maxVal[1]);

            DoMovement(direction);
        }

        private void SearchBFSLab(string[] targets, Node start, string[] forbidens, List<List<Node>> dict)
        {
            var frontier = new Queue<Node>();
            var visited = new HashSet<Node>();
            // var dict = new List<List<string>>();
            var parents = new Dictionary<Node, Node>();

            frontier.Enqueue(start);
            visited.Add(start);

            while (frontier.Any())
            {
                var current = frontier.Dequeue();

                if (targets.Any(x => x == current.GetContent().StrContent))
                {
                    var path = new List<Node>();
                    path.Add(current);

                    while (path.Last() != start)
                        path.Add(parents[path.Last()]);

                    path.Reverse();

                    dict.Add(path);
                    break;
                }

                foreach (var child in adjacencyList[current])
                {
                    if (!visited.Contains(child) && !forbidens.Any(x => x == child.GetContent().StrContent))
                    {
                        frontier.Enqueue(child);
                        visited.Add(child);
                        parents[child] = current;
                    }
                }
            }
        }

        private string GetDirection(Node current, Node next)
        {
            if (next.GetContent().X > current.GetContent().X)
                return "right";
            else if (next.GetContent().X < current.GetContent().X)
                return "left";
            else if (next.GetContent().Y < current.GetContent().Y)
                return "up";
            else
                return "down";
        }

    }
}