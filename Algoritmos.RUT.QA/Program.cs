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

        //Función para validar RUT usando el número y el Dígito Verificador proporcionados.
        static bool RUTVálido(int rut, char dv)
        {
            //Comente y descomente aquellos returns para probar los algoritmos 1 o 2.
            return dv == DV1(rut); //Retorna si el Dígito Verificador es igual al que devuelve el algoritmo 1 según el RUT.
            //return dv == DV2(rut); //Retorna si el Dígito Verificador es igual al que devuelve el algoritmo 2 según el RUT.
        }

        //Función para determinar si el formato del RUT ingresado está correcto.
        static bool FormatoVálido(string rut)
        {
            if (rut.Split('-').Length == 2) //Primero comprobar que el RUT esté escrito en el formato XXXXXXXX-X, de haber más de un guión, o de no haber uno, es inválido.
                if (rut.Split('-')[1].Length == 1) //Luego comprobar que el Dígito Verificador sea de verdad UN solo dígito.
                    return true; //Si se cumple todo, el formato del RUT es válido.
            return false; //Si ninguna de las condiciones anteriores se cumple, el RUT no es válido
            //y se retorna false por defecto.
        }

        //Función especial para imprimir mensajes de error en el programa cuando se requiera.
        static void ImprimirMensajeDeError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red; //Se cambiará el color del texto para resaltar el siguiente mensaje.
            Console.WriteLine(mensaje); //Se mostrará el mensaje de error.
            Console.ForegroundColor = ConsoleColor.Gray; //Se vuelve a cambiar el color del texto al normal.
        }

        //Función para confirmar que el número de RUT es un número válido.
        static bool SoloNúmero(string rut)
        {
            int auxiliar = 0; //Se crea una variable auxiliar para almacenar el valor.
            try
            {
                auxiliar = int.Parse(rut); //Se intenta convertir el string en un entero.
                return true; //Si no hay problemas en la conversión, se devuelve true.
            }
            catch (Exception)
            {
                return false; //Si existen problemas en la conversión, se atrapa la excepción, y se devuelve false.
            }
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
            if (!FormatoVálido(strrut)) //Si el formato del RUT no es válido.
            {
                //Se imprime el mensaje de error.
                ImprimirMensajeDeError("El RUT ingresado no es válido porque no cumple con el formato \"12345678-K\". Por favor, ingréselo de nuevo.");
                goto A; //Se devuelve al ingreso del RUT.
            }
            if (!SoloNúmero(strrut.Split('-')[0])) //Si el número del RUT no es un número válido.
            {
                ImprimirMensajeDeError("Ingrese un número de RUT válido."); //Se imprime el mensaje de error correspondiente.
                goto A; //Se devuelve directamente al ingreso del RUT.
            }
            rut = int.Parse(strrut.Split('-')[0]); //Se almacena el número de RUT.
            dv = (char)strrut.Split('-')[1][0]; //Si el número del RUT y el formato es válido, se almacenará el Dígito Verificador.
            if (!DVVálido(dv)) //Si el dígito ingresado no es válido.
            {
                //Se notifica al usuario que el digito ingresado no es válido, muestra los dígitos que sí son válidos, y finalmente se le pide que ingrese el RUT otra vez.
                ImprimirMensajeDeError("El dígito verificador ingresado no es válido, sólo puede ser un dígito (entre 0 y 9) o \"K\". Por favor, escriba el RUT nuevamente.");
                goto A; //Se devuelve al ingreso del RUT.
            }
            if(RUTVálido(rut, dv)) //Si el RUT y el Dígito Verificador son válidos...
                Console.WriteLine("El RUT {0}-{1} es un RUT válido.", rut, dv); //Se mostrará un mensjae diciendo que el RUT ingresado es válido.
            else //Sino...
                Console.WriteLine("El RUT {0}-{1} no es un RUT válido.", rut, dv); //Se mostrará un mensaje diciendo que el RUT ingresado no es válido.
            Console.ReadLine(); //El programa se "pausará" hasta que el usuario aprete ENTER.
        }
    }
}
