using UnityEngine;
using System.Collections.Generic;

public class ColorManager
{
    private List<Color> listaColores;

    public ColorManager()
    {
        this.listaColores = new List<Color>();
        this.listaColores.Add(Color.red);
        this.listaColores.Add(Color.black);
        this.listaColores.Add(Color.blue);
        this.listaColores.Add(Color.green);
        this.listaColores.Add(Color.grey);
        this.listaColores.Add(Color.magenta);
        this.listaColores.Add(Color.cyan);
    }

    public Color getRandomColor()
    {
        System.Random random = new System.Random(); //@TODO Numeros aleatorios
        Color aux = listaColores[random.Next(0, listaColores.Count)];
        return aux;
    }
}
