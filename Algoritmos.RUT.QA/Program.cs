using System;

namespace Algoritmos.RUT.QA
{
    class Program
    {
        static char DV1(int rut)
        {
            //Primer algoritmo de obtención de Dígito Verificador de RUT.
            //Para más información acerca del proceso, visitar http://cristobaldiaz.cl/blog/validacion-del-rut/
            char dv = '\0';
            int factor = 2;
            int suma = 0;
            int idV;
            int digito;
            
            while (rut > 0)
            {
                digito = rut % 10;
                suma += digito * factor;
                if (++factor > 7)
                    factor = 2;
                rut /= 10;
            }
            idV = 11 - suma % 11;
            switch (idV)
            {
                case 11:
                    dv = '0';
                    break;
                case 10:
                    dv = 'K';
                    break;
                default:
                    dv = (char)(48 + idV);
                    break;
            }
            return dv;
        }

        static char DV2(int rut)
        {
            //Segundo algoritmo de obtención de Dígito Verificador de RUT.
            //Para más información acerca del proceso, visitar http://cristobaldiaz.cl/blog/validacion-del-rut/
            char dv = '\0';
            int factor = 9;
            int suma = 0;
            int idV;
            int digito;

            while (rut > 0)
            {
                digito = rut % 10;
                suma += digito * factor;
                if (--factor < 4)
                    factor = 9;
                rut /= 10;
            }
            idV = suma % 11;
            switch (idV)
            {
                case 11:
                    dv = '0';
                    break;
                case 10:
                    dv = 'K';
                    break;
                default:
                    dv = (char)(48 + idV);
                    break;
            }
            return dv;
        }

        //El tercer algoritmo no ha sido implementado en este proyecto, ya que, al parecer más corto en papel
        //en código de habría hecho más largo y, por lo tanto, menos eficiente.

        //Función para determinar si el Dígito Verificador que se ingresa es válido.
        static bool DVVálido(char dv)
        {
            char[] válidos = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'K' }; //Se crea un arreglo de caracteres que incluyan todos los dígitos del sistema decimal y la letra K mayúscula.
            //Se itera por el arreglo de caracteres para compararlo con el Dígito Verificador que se ingresó a la función.
            for(int i = 0; i < válidos.Length; i++)
            {
                if (dv == válidos[i]) //Si el dígito ingresado es válido.
                    return true; //Se retorna true o verdadero.
            }
            return false; //Si no es válido, se retornará false o false.
        }

        //Función o método principal.
        static void Main(string[] args)
        {
            string strrut = string.Empty; //Se crea un string que recibirá el RUT que el usuario ingresará en pantalla.
            int rut = 0; //Se crea un espacio para almacenar el número del RUT.
            char dv = '\0'; //Se crea un espacio para almacenar el Dígito Verificador que el usuario ingrese.
            Console.WriteLine("Escriba un RUT en el siguiente formato: 12345678-K"); //Se dan las instrucciones en pantalla.
            A: strrut = Console.ReadLine().ToUpper(); //Se recibe el RUT que el usuario ingrese, y se fuerza su ingreso en mayúsculas,
            //de esa forma, ingresar un RUT que termine en "k" será tan válido como un RUT que termine en "K".
            if (!int.TryParse(strrut.Split('-')[0], out rut)) //Si el número del RUT no es un número válido.
            {
                Console.ForegroundColor = ConsoleColor.Red; //Se cambiará el color del texto para resaltar el siguiente mensaje.
                Console.WriteLine("Ingrese un número de RUT válido."); //Se le notificará al usuario que debe ingresar un número de RUT válido.
                Console.ForegroundColor = ConsoleColor.Gray; //Se vuelve a cambiar el color del texto al normal.
                goto A; //Se devuelve directamente al ingreso del RUT.
            }
            dv = (char)strrut.Split('-')[1][0]; //Si el número del RUT es válido, se almacenará el Dígito Verificador.
            if (!DVVálido(dv)) //Si el dígito ingresado no es válido.
            {
                Console.ForegroundColor = ConsoleColor.Red; //Se cambia el color del texto para resaltar el mensaje.
                //Se notifica al usuario que el digito ingresado no es válido, muestra los dígitos que sí son válidos, y finalmente se le pide que ingrese el RUT otra vez.
                Console.WriteLine("El dígito verificador ingresado no es válido, sólo puede ser un dígito (entre 0 y 9) o \"K\". Por favor, escriba el RUT nuevamente.");
                Console.ForegroundColor = ConsoleColor.Gray; //Se vuelve al color original.
                goto A; //Se devuelve al ingreso del RUT.
            }
            //Las siguientes dos líneas de código hacen uso de uno de los dos algoritmos de obtención de Dígito Verificador que existen en el programa.
            //Para probar uno, comente el "if" que no usará y quite el comentario del que sí usará.
            if(dv == DV1(rut)) //Si el Dígito Verificador ingresado corresponde al Dígito Verificador del RUT (según algoritmo 1).
            //if(dv == DV2(rut)) //Si el Dígito Verificador ingresado corresponde al Dígito Verificador del RUT (según algoritmo 2).
                Console.WriteLine("El RUT {0}-{1} es un RUT válido.", rut, dv); //Se mostrará un mensjae diciendo que el RUT ingresado es válido.
            else //Sino...
                Console.WriteLine("El RUT {0}-{1} no es un RUT válido.", rut, dv); //Se mostrará un mensaje diciendo que el RUT ingresado no es válido.
            Console.ReadLine(); //El programa se "pausará" hasta que el usuario aprete ENTER.
        }
    }
}
