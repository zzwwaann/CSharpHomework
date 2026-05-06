static void DrawRhombus(int N)
{
    int center = N / 2;
    for (int i = 0; i <= center; i++)
    {
        Console.Write(new string(' ', center - i));
        if (i == 0)
        {
            Console.Write("X");
        }
        else
        {
            Console.Write("X");
            Console.Write(new string(' ', i * 2 - 1));
            Console.Write("X");
        }
        Console.WriteLine();
    }
    for (int i = center - 1; i >= 0; i--)
    {
        Console.Write(new string(' ', center - i));
        if (i == 0)
        {
            Console.Write("X");
        }
        else
        {
            Console.Write("X");
            Console.Write(new string(' ', i * 2 - 1));
            Console.Write("X");
        }
        Console.WriteLine();
    }
}

DrawRhombus(5);
