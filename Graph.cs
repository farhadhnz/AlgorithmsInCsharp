using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Graph<T>
    {
        protected class Node<T>
        {
            private string Label;
            private T Content;

            public string GetLabel()
            {
                return Label;
            }

            public T GetContent()
            {
                return Content;
            }

            public Node(string label, T content)
            {
                Label = label;
                Content = content;
            }
        }


        protected Dictionary<string, Node<T>> nodes = new Dictionary<string, Node<T>>();
        protected Dictionary<Node<T>, List<Node<T>>> adjacencyList = new Dictionary<Node<T>, List<Node<T>>>();

        private Node<T> InsertNode(string label, T content)
        {
            var node = new Node<T>(label, content);

            nodes[label] = node;

            if (!adjacencyList.ContainsKey(node))
                adjacencyList[node] = new List<Node<T>>();

            return node;
        }
        public void InsertEdge(string from, string to, T contentFrom, T contentTo)
        {
            var fromNode = new Node<T>(from, contentFrom);
            if (!nodes.ContainsKey(from))
                fromNode = InsertNode(from, contentFrom);
            else
                fromNode = nodes[from];

            var toNode = new Node<T>(to, contentTo);
            if (!nodes.ContainsKey(to))
                toNode = InsertNode(to, contentTo);
            else
                toNode = nodes[to];

            adjacencyList[fromNode].Add(toNode);
            adjacencyList[toNode].Add(fromNode);
        }

        private Node<T> GetNodeParent(Node<T> node)
        {
            return adjacencyList.FirstOrDefault(x => x.Value.Contains(node)).Key;
        }

        // public string GetBestLink(string target, List<string> exits)
        // {
        //     var dict = new List<List<string>>();

        //     for (int i = 0; i < exits.Count; i++)
        //     {
        //         SearchBFSVirus(target, exits[i], dict);
        //     }

        //     var orderedDict = dict.OrderBy(x => x.Count);
        //     var maxVal = orderedDict.FirstOrDefault();

        //     var linkStart = nodes[maxVal[0]];
        //     var linkEnd = nodes[maxVal[1]];

        //     adjacencyList[linkStart].Remove(linkEnd);
        //     adjacencyList[linkEnd].Remove(linkStart);

        //     return $"{linkStart.GetLabel()} {linkEnd.GetLabel()}";
        // }


    }
}