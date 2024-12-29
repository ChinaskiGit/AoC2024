using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Aoc2024.Problemas;
public static class ProblemaDia5
{
    public static void ResolverParte1(string datos)
    {
        string[] lineas = File.ReadAllLines(datos);
        List<(int, int)> valores = new();
        List<List<int>> ordenes = new();
        ParsearDatos(lineas, valores, ordenes);
        int sumaPaginasCentrales = 0;
        foreach (var orden in ordenes)
        {
            if (EsOrdenValido(orden, valores))
            {
                int paginaCentral = ObtenerPaginaCentral(orden);
                sumaPaginasCentrales += paginaCentral;
            }
        }
        Debug.WriteLine(sumaPaginasCentrales);
    }

    public static void ResolverParte2(string datos)
    {
        string[] lineas = File.ReadAllLines(datos);
        List<(int, int)> reglas = [];
        List<List<int>> actualizaciones = [];
        ParsearDatos(lineas, reglas, actualizaciones);
        List<List<int>> actualizacionesInvalidas = [];
        foreach (var actualizacion in actualizaciones)
        {
            if (!EsOrdenValido(actualizacion, reglas))
            {
                actualizacionesInvalidas.Add(actualizacion);
            }
        }
        int sumaInvalidas = 0;
        foreach (var actualizacion in actualizacionesInvalidas)
        {
            var reordenada = ReordenarActualizacion(actualizacion, reglas);
            sumaInvalidas += reordenada[reordenada.Count / 2];
        }
        Debug.WriteLine(sumaInvalidas);
    }

    private static List<int> ReordenarActualizacion(List<int> actualizacion, List<(int, int)> reglas)
    {
        List<int> ordenada = [];
        foreach (int pagina in actualizacion)
        {
            int posicion = ordenada.Count;
            for (int i = 0; i < ordenada.Count; i++)
            {
                int actual = ordenada[i];
                if (reglas.Contains((pagina, actual)))
                {
                    posicion = i;
                    break;
                }
            }
            ordenada.Insert(posicion, pagina);
        }
        return ordenada;
    }

    private static int ObtenerPaginaCentral(List<int> orden)
    {
        int indiceCentral = orden.Count / 2;
        return orden[indiceCentral];
    }

    private static bool EsOrdenValido(List<int> orden, List<(int, int)> valores)
    {
        Dictionary<int, int> posicionPaginas = orden.Select((pagina, indice) => (page: pagina, indice))
                                                  .ToDictionary(x => x.page, x => x.indice);
        foreach (var (x, y) in valores)
        {
            if (posicionPaginas.ContainsKey(x) && posicionPaginas.ContainsKey(y))
            {
                if (posicionPaginas[x] >= posicionPaginas[y])
                {
                    return false;
                }
            }
        }
        return true;
    }

    private static void ParsearDatos(string[] lineas, List<(int, int)> valores, List<List<int>> ordenes)
    {
        bool esSeccionPares = true;
        foreach (string linea in lineas)
        {
            if (string.IsNullOrWhiteSpace(linea))
            {
                esSeccionPares = false;
                continue;
            }

            if (esSeccionPares)
            {
                string[] partes = linea.Split('|', StringSplitOptions.RemoveEmptyEntries);
                if (partes.Length == 2)
                {
                    int primero = int.Parse(partes[0]);
                    int segundo = int.Parse(partes[1]);
                    valores.Add((primero, segundo));
                }
            }
            else
            {
                string[] numeros = linea.Split(',', StringSplitOptions.RemoveEmptyEntries);
                List<int> grupo = [];
                foreach (string numero in numeros)
                {
                    grupo.Add(int.Parse(numero));
                }
                ordenes.Add(grupo);
            }
        }
    }


}