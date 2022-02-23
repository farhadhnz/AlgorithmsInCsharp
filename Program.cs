
using System.Text;
using Algorithms;
{
    // TreeNode<string> root = new TreeNode<string>("root");

    // TreeNode<string> node0 = root.AddChild("node0");
    // TreeNode<string> node1 = root.AddChild("node1");
    // TreeNode<string> node2 = root.AddChild("node2");
    // {
    //     TreeNode<string> node20 = node2.AddChild(null);
    //     TreeNode<string> node21 = node2.AddChild("node21");
    //     {
    //         TreeNode<string> node210 = node21.AddChild("node210");
    //         TreeNode<string> node211 = node21.AddChild("node211");
    //     }
    // }
    // TreeNode<string> node3 = root.AddChild("node3");
    // {
    //     TreeNode<string> node30 = node3.AddChild("node30");
    // }

    // foreach (TreeNode<string> node in root)
    // {
    //     string indent = CreateIndent(node.Level);
    //     Console.WriteLine(indent + (node.Data ?? "null"));
    // }

    // // TreeNode<string> found = root.FindTreeNode(node => node.Data != null && node.Data.Contains("210"));

    // // Console.WriteLine("Found: " + found);

    // Console.WriteLine(SearchBFS(root, "node211"));


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




    // string[] inputs;
    // // inputs = Console.ReadLine().Split(' ');
    // int n = 12; // the total number of nodes in the level, including the gateways
    // int l = 79; // the number of links
    // int e = 3; // the number of exit gateways

    // var links = new string[] { "11 6", "0 9", "1 2", "0 1", "10 1", "11 5", "2 3", "4 5", "8 9", "6 7", "7 8", "0 6", "3 4", "0 2", "11 7", "0 8", "0 4", "9 10",
    // "0 5", "0 7", "0 3", "0 10","5 6"};
    //     var links = new string[] {
    // "28 36",
    // "0 2",
    // "3 34",
    // "29 21",
    // "37 35",
    // "28 32",
    // "0 10",
    // "37 2",
    // "4 5",
    // "13 14",
    // "34 35",
    // "27 19",
    // "28 34",
    // "30 31",
    // "18 26",
    // "0 9",
    // "7 8",
    // "18 24",
    // "18 23",
    // "0 5",
    // "16 17",
    // "29 30",
    // "10 11",
    // "0 12",
    // "15 16",
    // "0 11",
    // "0 17",
    // "18 22",
    // "23 24",
    // "0 7",
    // "35 23",
    // "22 23",
    // "1 2",
    // "0 13",
    // "18 27",
    // "25 26",
    // "32 33",
    // "28 31",
    // "24 25",
    // "28 35",
    // "21 22",
    // "4 33",
    // "28 29",
    // "36 22",
    // "18 25",
    // "37 23",
    // "18 21",
    // "5 6",
    // "19 20",
    // "0 14",
    // "35 36",
    // "9 10",
    // "0 6",
    // "20 21",
    // "0 3",
    // "33 34",
    // "14 15",
    // "28 33",
    // "11 12",
    // "12 13",
    // "17 1",
    // "18 19",
    // "36 29",
    // "0 4",
    // "0 15",
    // "0 1",
    // "18 20",
    // "2 3",
    // "0 16",
    // "8 9",
    // "0 8",
    // "26 27",
    // "28 30",
    // "3 4",
    // "31 32",
    // "6 7",
    // "37 1",
    // "37 24",
    // "35 2"
    // };

    // var tree = new Tree();
    // for (int i = 0; i < l; i++)
    // {
    //     // inputs = Console.ReadLine().Split(' ');
    //     tree.InsertEdge(links[i].Split(' ')[0], links[i].Split(' ')[1]);
    // }
    // var gates = new List<string>();
    // gates.Add("0");
    // gates.Add("18");
    // gates.Add("28");
    // // for (int i = 0; i < e; i++)
    // // {
    // //     gates.Add(Console.ReadLine()); // the index of a gateway node
    // // }

    // var positions = new string[] { "37", "35" };
    // // game loop
    // foreach (var item in positions)
    // {

    //     string si = item; // The index of the node on which the Bobnet agent is positioned this turn

    //     // Write an action using Console.WriteLine()
    //     // To debug: Console.Error.WriteLine("Debug messages...");


    //     Console.WriteLine(tree.GetBestLink(si, gates));


    // }



    string[] inputs;
    // inputs = Console.ReadLine().Split(' ');
    int R = 15; // number of rows.
    int C = 30; // number of columns.
                // int A = int.Parse(inputs[2]); // number of rounds between the time the alarm countdown is activated and the time the alarm goes off.

    var alarmed = false;
    var graph = new LabyrinthGraph();
    graph.CreateGraphBasedOnInputs(R, C);
    // game loop
    while (true)
    {
        var matrix = new string[R, C];
        // inputs = Console.ReadLine().Split(' ');
        int KR = 6; // row where Rick is located.
        int KC = 9; // column where Rick is located.

        var ROWS = new string[]{
            "??????????????????????????????",
            "??????????????????????????????",
            "??????????????????????????????",
            "??????????????????????????????",
            "???############???????????????",
            "???############???????????????",
            "???##T......C##???????????????",
            "???############???????????????",
            "???############???????????????",
            "??????????????????????????????",
            "??????????????????????????????",
            "??????????????????????????????",
            "??????????????????????????????",
            "??????????????????????????????",
            "??????????????????????????????"
        };

        for (int i = 0; i < R; i++)
        {
            string ROW = ROWS[i]; // C of the characters in '#.TC?' (i.e. one line of the ASCII maze).
            var rowInputs = ROW.ToArray();
            for (int j = 0; j < C; j++)
            {
                var xx = rowInputs[j];
                matrix[i, j] = xx.ToString();
            }
        }

        alarmed = graph.DefineMove(KR, KC, matrix, true);

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        // Console.WriteLine("RIGHT"); // Rick's next move (UP DOWN LEFT or RIGHT).

    }


}

