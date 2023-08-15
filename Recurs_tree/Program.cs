using Recurs_tree;

const int width = 280, height = 150;

ConsoleBuffer buff = new ConsoleBuffer(width, height);
RecursTree tree = new RecursTree(width, height, buff);

int curDegree = 0;
while (true)
{
    curDegree = (curDegree+1) %360;

    tree.DrawTree(7, 0.66f, curDegree, (0, height/2), 1, width/2, height/8);
    buff.Swap();
    buff.Clear();
}
