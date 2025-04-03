using System;

// Nombre: Jorge Leonardo Cardenas Almanza

namespace TresEnRaya
{

    class Juego
    {
        private Tablero tablero;
        private Jugador jugador;
        private JugadorIA ia;
        private int turnos;

        public Juego()
        {
            tablero = new Tablero();
            jugador = new JugadorHumano('X');
            ia = new JugadorIA('O');
            turnos = 0;
        }

        public void Iniciar()
        {
            while (true)
            {
                tablero.MostrarTablero();
                if (turnos % 2 == 0)
                    jugador.Turno(tablero);
                else
                    ia.Turno(tablero);

                if (tablero.ComprobarVictoria(jugador.Simbolo))
                {
                    tablero.MostrarTablero();
                    Console.WriteLine("Ganaste bro");
                    break;
                }
                if (tablero.ComprobarVictoria(ia.Simbolo))
                {
                    tablero.MostrarTablero();
                    Console.WriteLine("Te gano la IA");
                    break;
                }
                if (++turnos == 9)
                {
                    tablero.MostrarTablero();
                    Console.WriteLine("Empate");
                    break;
                }
            }
        }
    }

    class Tablero
    {
        private char[,] tablero;

        public Tablero()
        {
            tablero = new char[3, 3]
            {
                { '1', '2', '3' },
                { '4', '5', '6' },
                { '7', '8', '9' }
            };
        }

        
        public void MostrarTablero()
        {
            Console.Clear();
            Console.WriteLine(" {0} | {1} | {2} ", tablero[0, 0], tablero[0, 1], tablero[0, 2]);
            Console.WriteLine("---+---+---");
            Console.WriteLine(" {0} | {1} | {2} ", tablero[1, 0], tablero[1, 1], tablero[1, 2]);
            Console.WriteLine("---+---+---");
            Console.WriteLine(" {0} | {1} | {2} ", tablero[2, 0], tablero[2, 1], tablero[2, 2]);
        }

        public bool MovimientoValido(int movimiento)
        {
            if (movimiento < 1 || movimiento > 9)
                return false;

            int fila = (movimiento - 1) / 3;
            int columna = (movimiento - 1) % 3;
            return tablero[fila, columna] != 'X' && tablero[fila, columna] != 'O';
        }

        
        public void ColocarMovimiento(int movimiento, char simbolo)
        {
            int fila = (movimiento - 1) / 3;
            int columna = (movimiento - 1) % 3;
            tablero[fila, columna] = simbolo;
        }

       
        public bool ComprobarVictoria(char simbolo)
        {
            for (int i = 0; i < 3; i++)
            {
                if ((tablero[i, 0] == simbolo && tablero[i, 1] == simbolo && tablero[i, 2] == simbolo) ||
                    (tablero[0, i] == simbolo && tablero[1, i] == simbolo && tablero[2, i] == simbolo))
                    return true;
            }
            if ((tablero[0, 0] == simbolo && tablero[1, 1] == simbolo && tablero[2, 2] == simbolo) ||
                (tablero[0, 2] == simbolo && tablero[1, 1] == simbolo && tablero[2, 0] == simbolo))
                return true;

            return false;
        }
    }

    abstract class Jugador
    {
        public char Simbolo { get; private set; }

        public Jugador(char simbolo)
        {
            Simbolo = simbolo;
        }

        public virtual void Turno(Tablero tablero)
        {
            // Este es mi metodo base por eso, no pongo nada y lo declaro Virtual, porque en el metodo turno de jugador e IA usare override
            
        }
    }

    
    class JugadorHumano : Jugador
    {
        public JugadorHumano(char simbolo) : base(simbolo) { }

        
        public override void Turno(Tablero tablero)
        {
            int eleccion;
            do
            {
                Console.Write($"Jugador {Simbolo}, ingresa un número de casilla (1-9): ");
            } while (!int.TryParse(Console.ReadLine(), out eleccion) || !tablero.MovimientoValido(eleccion));

            tablero.ColocarMovimiento(eleccion, Simbolo);
        }
    }

    /* 
     class JugadorIA : Jugador
    {
        // No sabia como hacerlo
    }
    */

    class Program
    {
        static void Main()
        {
            Juego juego = new Juego();
            juego.Iniciar();
        }
    }
}
