using System.Diagnostics;
using System.Linq;
public static class Part01
{
    public static void Execute()
    {
        string[] inputFile = File.ReadAllLines("input.txt");
        Stack<string> currentFolder = new Stack<string>();
        folder currentFolderZ = new folder();
        folder baseFolder = new folder();
        currentFolderZ = baseFolder;

        for (int i = 0; i < inputFile.Length; i++)
        {
            string line = inputFile[i];

            if (line.StartsWith("$ cd ..")) {
                currentFolder.Pop();
                currentFolderZ = currentFolderZ.parent ?? new folder();

            } else if (line.StartsWith("$ cd "))
            {
                currentFolder.Push(line.Replace("$ cd ", ""));
                folder newFolder = new folder() { name = currentFolder.Peek(), parent = currentFolderZ };
                currentFolderZ.children.Add(newFolder);
                currentFolderZ = newFolder;
            } else if (line.StartsWith("$ ls"))
            {
                i++;
                line = inputFile[i];
                while(!line.StartsWith("$")){
                    if (line.StartsWith("$"))
                    {
                        //i--;
                        break;
                    }
                    else if (line.StartsWith("dir "))
                    {
//                        string newChild = line.Replace("dir ", "");
//                        folder newFolder = new folder() { name = newChild };
//                        currentFolderZ.children.Add(newFolder);
                    }
                    else if (int.TryParse(line[0].ToString(), out int resultsInt2))
                    {
                        string[] splitFile = line.Split(' ');
                        //dict[currentFolder.Peek()].size += int.Parse(splitFile[0]);
                        currentFolderZ.size += int.Parse(splitFile[0]);
                    }
                    i++;
                    if (i >= inputFile.Length) break;
                    line = inputFile[i];
                }
                //update parent
                folder tmpFolder = currentFolderZ;
                double tmpSize = currentFolderZ.size;
                while(tmpFolder.parent != null) {
                    tmpFolder.parent.size += tmpSize;
                    tmpFolder = tmpFolder.parent;
                }
                i--;
            }
        }

        Debug.WriteLine($"Part 01 - {CalcFiles(baseFolder)}");
        Debug.WriteLine($"Part 02 - {CalcFilesFull(baseFolder)}");
    }

    public static double CalcFiles(folder baseFolder) {
        double size = 0;
        if (baseFolder.size <= 100000)
            size += baseFolder.size;

        foreach (var item in baseFolder.children)
        {
            size += CalcFiles(item);
        }
        return size;
    }

    public static double CalcFilesFull(folder baseFolder, double size = 0) {
        if (baseFolder.size >= 3598596 && (size > baseFolder.size || size == 0)){
            size = baseFolder.size;
        }

        foreach (var item in baseFolder.children)
        {
            size = CalcFilesFull(item, size);
        }
        return size;
    }

    public class folder
    {
        public string name { get; set; } = "";
        public double size { get; set; }

        public folder? parent { get; set; } = null;

        public List<folder> children { get; set; } = new List<folder>();
        // public double calcSize(Dictionary<string, folder> dict) {
        //     double size2 = 0;
        //     foreach (var item in children)
        //     {
        //         System.Console.WriteLine(item);
        //         size2 += dict[item].calcSize(dict);
        //     }
        //     return size + size2;
        // }
    }
}