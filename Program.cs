
using System.Text;
using Algorithms;
{
    TreeNode<string> root = new TreeNode<string>("root");

    TreeNode<string> node0 = root.AddChild("node0");
    TreeNode<string> node1 = root.AddChild("node1");
    TreeNode<string> node2 = root.AddChild("node2");
    {
        TreeNode<string> node20 = node2.AddChild(null);
        TreeNode<string> node21 = node2.AddChild("node21");
        {
            TreeNode<string> node210 = node21.AddChild("node210");
            TreeNode<string> node211 = node21.AddChild("node211");
        }
    }
    TreeNode<string> node3 = root.AddChild("node3");
    {
        TreeNode<string> node30 = node3.AddChild("node30");
    }

    foreach (TreeNode<string> node in root)
    {
        string indent = CreateIndent(node.Level);
        Console.WriteLine(indent + (node.Data ?? "null"));
    }

    // TreeNode<string> found = root.FindTreeNode(node => node.Data != null && node.Data.Contains("210"));

    // Console.WriteLine("Found: " + found);

    Console.WriteLine(SearchBFS(root, "node211"));


    static String CreateIndent(int depth)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < depth; i++)
        {
            sb.Append(' ');
        }
        return sb.ToString();
    }


    static string SearchBFS(TreeNode<string> root, string target)
    {
        var frontier = new Queue<TreeNode<string>>();

        frontier.Enqueue(root);

        while (frontier.Any())
        {
            var current = frontier.Dequeue();

            if (current.Data == target)
            {
                var list = new List<string>();
                list.Add(current.Data);

                var parent = current.Parent;
                while (parent != null)
                {
                    list.Add(parent.Data);
                    parent = parent.Parent;
                }

                list.Reverse();

                return $"target found: {String.Join(' ', list.ToArray())}";
            }


            foreach (var child in current.Children)
            {
                frontier.Enqueue(child);
            }
        }

        return "Not Found";
    }

}

