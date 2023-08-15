namespace Recurs_tree
{
    internal class RecursTree
    {
        int width, height;
        ConsoleBuffer buff;

        public RecursTree(int width, int height, ConsoleBuffer buff)
        {
            this.width = width;
            this.height = height;
            this.buff = buff;
        }
        public void DrawTree(int depth, float sizeDecr, float angle, (float x, float y) curVector, short color, int startX, int startY, char symbol = '0')
        {
            if (depth == 0){return;}

            (float x, float y) newVector = (curVector.x * sizeDecr, curVector.y * sizeDecr);
            (float x, float y) newPos1 = rotate(newVector.x, newVector.y, angle);
            (float x, float y) newPos2 = rotate(newVector.x, newVector.y, -angle);

            DrawLine(startX, startY, (int)(startX + newPos1.x), (int)(startY + newPos1.y), symbol, color);
            DrawLine(startX, startY, (int)(startX + newPos2.x), (int)(startY + newPos2.y), symbol, color);

            DrawTree(depth - 1, sizeDecr, angle, newPos1, (short)((color + 1) % 16), (int)(startX + newPos1.x), (int)(startY + newPos1.y), symbol);
            DrawTree(depth - 1, sizeDecr, angle, newPos2, (short)((color + 1) % 16), (int)(startX + newPos2.x), (int)(startY + newPos2.y), symbol);
        }

        void DrawLine(int x0, int y0, int x1, int y1, char symbol, short color)
        {
            if (Math.Abs(y1 - y0) < Math.Abs(x1 - x0))
            {
                if (x0 > x1)
                {
                    drawLineLow(x1, y1, x0, y0, symbol, color);
                }
                else
                {
                    drawLineLow(x0, y0, x1, y1, symbol, color);
                }
            }
            else
            {
                if (y0 > y1)
                {
                    drawLineHigh(x1, y1, x0, y0, symbol, color);
                }
                else
                {
                    drawLineHigh(x0, y0, x1, y1, symbol, color);
                }
            }
        }
        void drawLineLow(int x0, int y0, int x1, int y1, char symbol, short color)
        {
            float dx = x1 - x0;
            float dy = y1 - y0;
            int yi = 1;
            if (dy < 0)
            {
                yi = -1;
                dy = -dy;
            }
            float D = (2 * dy) - dx;
            int y = y0;

            for (int x = x0; x < x1; x++)
            {
                if (!outOfDisplay(x, y))
                {
                    buff.WriteToBuff(x, y, symbol, color);
                }
                if (D > 0)
                {
                    y = y + yi;
                    D = D + (2 * (dy - dx));
                }
                else
                {
                    D = D + 2 * dy;
                }
            }
        }
        void drawLineHigh(int x0, int y0, int x1, int y1, char symbol, short color)
        {
            float dx = x1 - x0;
            float dy = y1 - y0;
            int xi = 1;
            if (dx < 0)
            {
                xi = -1;
                dx = -dx;
            }
            float D = (2 * dx) - dy;
            int x = x0;

            for (int y = y0; y < y1; y++)
            {
                if (!outOfDisplay(x, y))
                {
                    buff.WriteToBuff(x, y, symbol, color);
                }
                if (D > 0)
                {
                    x = x + xi;
                    D = D + (2 * (dx - dy));
                }
                else
                {
                    D = D + 2 * dx;
                }
            }
        }
        bool outOfDisplay(int x, int y)
        {
            return x >= width || x < 0 || y < 0 || y >= height;
        }

        (float x, float y) rotate(float x, float y, float angle)
        {
            float retX = ((float)(x * Math.Cos(angle * Math.PI / 180) - y * Math.Sin(angle * Math.PI / 180)));
            float retY = ((float)(x * Math.Sin(angle * Math.PI / 180) + y * Math.Cos(angle * Math.PI / 180)));
            return (retX, retY);
        }

    }
}
