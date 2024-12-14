public class PhysicsRun
{

    public static void Main()
    {
        int delay = 100;
        int[] CanvasSize = { 18, 18 };
        float[] ParticlePosition = { 10, 10 };
        float[] ParticleVelocity = { -100, -50 };
        float[] ParticleAcceleration = new float[] { };
        float[] NextPosition = new float[] { };
        for (int i = 0; i < ParticlePosition.Length; i += 1)
        {
            NextPosition = NextPosition.Append<float>(0).ToArray();
            ParticleAcceleration = ParticleAcceleration.Append<float>(0).ToArray();
        }
        ParticleAcceleration[1] = -100;
        float TimeStep = 10;
        bool running = true;
        float EnergyLoss = 1;
        string input = "";
        int typeDelay = 0;

        Console.Clear();

        Print("INPUI CANVAS SIZE (DEFAULT 18:18) > ", delay);
        input = Console.ReadLine();
        if (input != "") CanvasSize = Array.ConvertAll(input.Split(':'), s => int.Parse(s));
        Console.Clear();

        Print("INPUI PARTICLE POSITION (DEFAULT 10:10) > ", delay);
        input = Console.ReadLine();
        if (input != "") ParticlePosition = Array.ConvertAll(input.Split(':'), s => float.Parse(s));
        Console.Clear();

        Print("INPUI PARTICLE VELOCITY (DEFAULT -100:-50) > ", delay);
        input = Console.ReadLine();
        if (input != "") ParticleVelocity = Array.ConvertAll(input.Split(':'), s => float.Parse(s));
        Console.Clear();

        Print("INPUI PARTICLE ACCELERATION (DEFAULT 0:-100) > ", delay);
        input = Console.ReadLine();
        if (input != "") ParticleAcceleration = Array.ConvertAll(input.Split(':'), s => float.Parse(s));
        Console.Clear();

        Print("INPUI TIMESTEP (DEFAULT 10 > ", delay);
        input = Console.ReadLine();
        if (input != "") TimeStep = float.Parse(input);
        Console.Clear();

        Print("INPUT ENERGY LOSS (DEFAULT 1) > ", delay);
        input = Console.ReadLine();
        if (input != "") EnergyLoss = float.Parse(input);
        Console.Clear();

        Print("INPUT RENDERDELAY (DEFAULT 100) > ", delay);
        input = Console.ReadLine();
        if (input != "") delay = int.Parse(input);
        Console.Clear();





        if (!(ParticlePosition.Length == ParticleVelocity.Length && ParticleVelocity.Length == CanvasSize.Length && ParticlePosition.Length == CanvasSize.Length))
        {
            Console.WriteLine("ERR 1.0: INVALID CONFIG: Length of starting arrays do not match");
            Environment.Exit(1);
        }
        for (int d = 0; d < CanvasSize.Length; d += 1)
        {
            if (CanvasSize[d] <= 0)
            {
                Console.WriteLine("ERR 1.1: INVALID CONFIG: Size of canvas cannot be negative");
                Environment.Exit(1);
            }
        }
        if (CanvasSize.Length == 0 || ParticlePosition.Length == 0 || ParticleVelocity.Length == 0)
        {
            Console.WriteLine("ERR 1.2: INVALID CONFIG: Non-defined crutial variable");
            Environment.Exit(1);
        }

        int dimentions = ParticlePosition.Length;
        Console.ForegroundColor = ConsoleColor.Yellow;
        while (running)
        {
            Console.Clear();
            for (int d = 0; d <= (dimentions - 1); d += 1)
            {

                ParticleVelocity[d] += ParticleAcceleration[d] * (TimeStep / 1000);


                NextPosition[d] = ParticlePosition[d] + (ParticleVelocity[d] * TimeStep / 1000);

                if (NextPosition[d] >= CanvasSize[d] || NextPosition[d] <= 0)
                {
                    ParticleVelocity[d] = -ParticleVelocity[d] * ((100 - EnergyLoss) / 100);
                }

                ParticlePosition[d] += ParticleVelocity[d] * TimeStep / 1000;
            }



            //Console.WriteLine("POSITION");

            Console.ForegroundColor = ConsoleColor.Green;
            Print("POSITION:", delay);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Print(" [ ", delay);
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < ParticlePosition.Length; i += 1)
            {
                Print(ParticlePosition[i].ToString(), delay);
                if (i != ParticleAcceleration.Length - 1) { Print(", ", delay); }


            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Print(" ]", delay);
            Console.Write("\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Print("VELOCITY:", delay);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Print(" [ ", delay);
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < ParticlePosition.Length; i += 1)
            {
                Print(ParticleVelocity[i].ToString(), delay);
                if (i != ParticleAcceleration.Length - 1) { Print(", ", delay); }


            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Print(" ]", delay);
            Console.Write("\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Print("ACCELERATION:", delay);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Print(" [ ", delay);
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < ParticleAcceleration.Length; i += 1)
            {
                Print(ParticleAcceleration[i].ToString(), delay);
                if (i != ParticleAcceleration.Length - 1) { Print(", ", delay); }

            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Print(" ]", delay);
            Console.Write("\n\n");

            Render(ParticlePosition, CanvasSize, delay);
            Thread.Sleep((int)(TimeStep));
            Thread.Sleep(delay * 4);
        }

    }

    public static string Render(float[] particlePosition, int[] CanvasSize, int delay)
    {
        string Canvas = "";
        for (int j = CanvasSize[1]; j >= 0; j -= 1)
        {

            if (j < 10)
            {
                //Canvas += j.ToString() + " ";

                Console.ForegroundColor = ConsoleColor.Green;
                PrintF(j.ToString() + " ", delay);
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (j < 100)
            {
                //Canvas += j.ToString();

                Console.ForegroundColor = ConsoleColor.Green;
                PrintF(j.ToString(), delay);
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            for (int i = 0; i <= CanvasSize[0]; i += 1)
            {
                if (i == Math.Round(particlePosition[0]) && j == Math.Round(particlePosition[1]))
                {
                    //Canvas += " X ";

                    Console.ForegroundColor = ConsoleColor.Red;
                    PrintF(" X ", delay);
                    Console.ForegroundColor = ConsoleColor.Yellow;

                }
                else
                {
                    //Canvas += "[ ]";
                    PrintF("[ ]", delay);


                }
            }
            //Canvas += "\n";
            PrintF("\n", delay);

        }
        //Canvas += "  ";
        PrintF("  ", delay);

        for (int i = 0; i <= CanvasSize[0]; i += 1)
        {
            if (i < 10)
            {
                //Canvas += " " + i.ToString() + " ";

                Console.ForegroundColor = ConsoleColor.Green;
                Print(" " + i.ToString() + " ", delay);
                Console.ForegroundColor = ConsoleColor.Yellow;

            }
            else if (i < 100)
            {
                //Canvas += " " + i.ToString();

                Console.ForegroundColor = ConsoleColor.Green;
                Print(" " + i.ToString(), delay);
                Console.ForegroundColor = ConsoleColor.Yellow;

            }

        }
        Print("\n", delay);
        return Canvas;
    }
    public static void PrintN(string input, int delay)
    {
        foreach (char c in input)
        {
            Console.Write(c);
            SmallSleep(delay);
        }
        Console.Write("\n");
    }
    public static void Print(string input, int delay)
    {
        foreach (char c in input)
        {
            Console.Write(c);
            Thread.Sleep(delay / 5);
        }
    }
    public static void PrintF(string input, int delay)
    {
        foreach (char c in input)
        {
            Console.Write(c);
            SmallSleep(delay);
        }
    }
    public static void PrintNF(string input, int delay)
    {
        foreach (char c in input)
        {
            Console.Write(c);
            SmallSleep(delay);
        }
    }
    public static void SmallSleep(int delay)
    {
        for (int i = 0; i <= delay * 100000; i += 0)
        {
            i += 1;
        }
    }


}

