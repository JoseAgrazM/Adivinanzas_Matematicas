using System.Threading.Channels;

namespace Adivinanzas_Matematicas
{
    internal class Program
    {

        static int numAciertos = 0;

        static int numErrores = 0;

        static int secIniciales = 3;

        static bool inGame = false;

        static TimeSpan tiempoRestante = TimeSpan.FromSeconds(secIniciales);
        static void Main(string[] args)
        {
            int x;
            int y;

            while (!inGame)
            {
                Contador_Tiempo();
                x = NumeroX_Random();
                y = NumeroY_Random(x);
                string op = Operacion_Random();

                int result = Calculo(x, y, op);

                Mensaje(x, y, op);

                int respuesta = Respuesta_Usuario(x, y);

                Logica(result, respuesta);

                inGame = Restart();

                Console.ReadLine();
            }
        }

        static int NumeroX_Random()
        {
            Random aleatorioX = new Random();
            int numX = 0;

            if (numAciertos <= 5)
            {
                numX = aleatorioX.Next(1, 10);
            }
            else if (numAciertos > 5)
            {
                numX = aleatorioX.Next(1, 100);
            }
            else if (numAciertos > 15)
            {
                numX = aleatorioX.Next(1, 1000);
            }
            return numX;
        }

        static int NumeroY_Random(int x)
        {
            Random aleatorioY = new Random();
            int numY = 0;

            do
            {
                if (numAciertos <= 10)
                {
                    numY = aleatorioY.Next(1, 10);
                }
                else if (numAciertos > 10)
                {
                    numY = aleatorioY.Next(1, 100);
                }
            } while (numY > x);

            return numY;
        }


        static string Operacion_Random()
        {
            string[] operacion = { "+", "-", "*", "/"};
            Random aleatorioOp = new Random();

            int opRandom = aleatorioOp.Next(0, 4);

            return operacion[opRandom];
        }

        static int Calculo(int x, int y, string operando)
        {
            switch (operando)
            {
                case "+":
                    return x + y;
                case "-":
                    return x - y;
                case "*":
                    return x * y;
                case "/":
                    return (int)x / y;
            }
            return 0;
        }

        static int Respuesta_Usuario(int x, int y)
        {
            
            int respNum;
            do
            {
                Console.Write("\nResultado: ");
                string respStr = Console.ReadLine();
                if (int.TryParse(respStr, out respNum) && respNum >= 0)
                {
                    return respNum;
                }
                else
                {
                    Console.WriteLine("\nTiene que ser un valor tipo entero.");
                }
            }
            while (true);
            
        }
        
        static void Mensaje(int x, int y, string operando)
        {
            Console.WriteLine($"\n{x} {operando} {y} = ");

        }

        static void Contador_Tiempo()
        {
            while (tiempoRestante.TotalSeconds > 0)
            {
                tiempoRestante = tiempoRestante.Subtract(TimeSpan.FromSeconds(1));

                inGame = true;
            }

            inGame = false;
        }

        static void Logica(int result, int respuesta)
        {

            if (result == respuesta)
            {
                Console.WriteLine("\nCorrecto!");
                numAciertos++;

                Console.WriteLine($"\nNumero de aciertos: {numAciertos}");
                Console.WriteLine($"\nNumero de fallos: {numErrores}");

            }
            else
            {
                Console.WriteLine($"\nNo es correcto el resultado es: {result}");
                numErrores++;

                Console.WriteLine($"\nNumero de aciertos: {numAciertos}");
                Console.WriteLine($"\nNumero de fallos: {numErrores}");

            }
        }

        static bool Restart()
        {
            Console.WriteLine("\nPulsa 'R' para intentarlo de nuevo");

            char respuesta = Console.ReadKey().KeyChar;

            if (respuesta == 'R' || respuesta == 'r')
            {
                return false;
            }
            else
            {
                return true;
            }

        }


    }
}