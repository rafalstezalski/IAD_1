﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Collections;

namespace zad_1
{
    class Program
    {
        static void Main(string[] args)
        {
            IRIS iris = new IRIS(@"~\..\..\..\data\iris.data.csv");
            List<double> listWithData = new List<double>();
            listWithData = iris.eksportFromVector(iris.Dane, "Iris-versicolor", 0);
            
            iris.srednia(listWithData);
            Console.WriteLine();
            iris.srednia_geometryczna(listWithData);
            Console.WriteLine();
            iris.srednia_harmoniczna(listWithData);
            Console.WriteLine();
            iris.dominanta(listWithData);
            Console.WriteLine();
            iris.wariancja(listWithData);
            Console.WriteLine();
            iris.odchylenie_standardowe(listWithData);
            Console.WriteLine();
            iris.mediana(listWithData);
            Console.WriteLine();
            iris.skosnosc_p1(listWithData);
            Console.WriteLine();
            iris.skosnosc_p2(listWithData);
            Console.WriteLine();
            iris.kwartyl1(listWithData);
            Console.WriteLine();
            iris.kwartyl3(listWithData);
            Console.WriteLine();
            iris.momentCtr(listWithData, 4);

            Console.ReadKey();
        }
    }
}

class IRIS
{
    public List<VectorClassification> Dane;
    public IRIS(string Filename)
    {
        Dane = new List<VectorClassification>();
        using (FileStream fs = File.Open(Filename, FileMode.Open))
        {
            using (StreamReader sr = new StreamReader(fs))
            {
                int i = 0;
                while (!sr.EndOfStream)
                {
                    string Line = sr.ReadLine();
                    if (Line.Length < 3) continue;
                    string[] Parts = Line.Split(',');
                    List<double> Vector = new List<double>(); // 
                    for (int p = 0; p < 4; p++) Vector.Add(double.Parse(Parts[p], System.Globalization.CultureInfo.InvariantCulture));
                    string etykieta = null;
                    etykieta = Parts[4];
                    Dane.Add(new VectorClassification(Vector.ToArray(), etykieta));

                    i++;
                }
                foreach (var j in Dane)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        double temp = 0;
                        temp = +j.Vector[k];
                    }
                }
            }
        }
    }

    public List<double> eksportFromVector(List<VectorClassification> dane, string etykieta, int column)
    {
        List<double> tempList = new List<double>();

        foreach(var i in dane)
        {
            if(i.etykieta==etykieta)
            {
                tempList.Add(i.Vector[0]);
            }
        }

        return tempList;
    }

    public double srednia(List<double> dane)
    {
        double suma = 0;
        int licznik = 0;
        
        foreach (var o in dane)
        {
                licznik++;
                suma += o;
        }

        suma = suma / licznik;
        Console.WriteLine(suma + " to jest arytmetyczna");  
        return suma;        
    }

    public double srednia_geometryczna (List<double> dane)
    { 
        double iloczyn = 1;
        double licznik = 0;

        foreach (var o in dane)
        {
                licznik++;
                iloczyn *= o;
        }
       
       iloczyn = Math.Pow(iloczyn,1/licznik);
       Console.Write("to jest srednia geometryczna" + iloczyn + "\n");
       return iloczyn; 
    }

    public double srednia_harmoniczna (List<double> dane)
    {
        double licznik = 0;
        double mianownik = 0;
        double wynik = 0;

        foreach (var o in dane)
        {
                licznik++;
                mianownik += 1 / o;
        }

        wynik = licznik / mianownik;
        Console.Write(" to jest srednia harmoniczna" + "\n" + wynik);
        return wynik;
    }
 
    public double dominanta(List<double> listaWartosci)
    {
        List<int> liczList = new List<int>();
        List<double> liczbaNajwiekszychWartosci = new List<double>();
          
        double maks = 0;
        int licz = 0;
        double liczba = 0;
        double dominanta = 0;

        listaWartosci.Sort();

        for (int i = 0; i < listaWartosci.Count; i++)
        {
            licz = 0;
            for (int j = 0; j < listaWartosci.Count; j++)
            {
                if (listaWartosci[i] == listaWartosci[j])
                {
                    licz++;

                    liczList.Add(licz);
                }     
            }  
        }

        for (int i = 0; i < listaWartosci.Count; i++)
        {
            licz = 0;
            for (int j = 0; j < listaWartosci.Count; j++)
            {
                if (listaWartosci[i] == listaWartosci[j])
                {
                    licz++;
                    if (licz == liczList.Max())
                    {
                        liczbaNajwiekszychWartosci.Add(listaWartosci[i]);
                    }
                }
            }
        }

        foreach (var i in liczbaNajwiekszychWartosci.Distinct()) /// dobre !!!
        {
            dominanta += i;
        }

        dominanta = dominanta / liczbaNajwiekszychWartosci.Distinct().Count();
        Console.Write("dominanta: " + dominanta);
        return dominanta;
    }

	public double wariancja(List<double> dane)
    {
        double sumaKwadratowRoznic = 0;
        double roznica;
        double counter = 0;
        double sred = srednia(dane);

        foreach(var i in dane)
        {              
                    roznica = 0;
                    roznica = i - sred;
                    roznica = roznica * roznica;
                    sumaKwadratowRoznic += roznica;
                    counter++;
        }

        sumaKwadratowRoznic = sumaKwadratowRoznic / counter;
        Console.WriteLine(sumaKwadratowRoznic);
        return sumaKwadratowRoznic;
    }

    public double odchylenie_standardowe(List<double> dane)
    {
        double odchylenie = Math.Sqrt(wariancja(dane));
        Console.WriteLine(odchylenie);
        return odchylenie;
    }

    public double mediana(List<double> tempListWithValuesFromOneColumn)
    {
        double quantityOfValues = 0;
        double median = 0;
        int index = 0;

        tempListWithValuesFromOneColumn.Sort();
        quantityOfValues = tempListWithValuesFromOneColumn.Count();

        if (quantityOfValues % 2 == 0)
        {
            quantityOfValues = quantityOfValues / 2;
            index = Convert.ToInt32(quantityOfValues);
            median = (tempListWithValuesFromOneColumn[index - 1] + tempListWithValuesFromOneColumn[index]) / 2;
        }
        else
        {
            quantityOfValues = quantityOfValues / 2;
            index = Convert.ToInt32(quantityOfValues);
            median = tempListWithValuesFromOneColumn[Convert.ToInt32(quantityOfValues)];
        }

        Console.WriteLine("to jest mediana" + median);
        return median;
    }

    public double skosnosc_p1(List<double> dane)
    {
        double srednia1 = srednia(dane);
        double moda = dominanta(dane);
        double odchylenie_stand = odchylenie_standardowe(dane);
        double wynik = (srednia1 - moda) / odchylenie_stand;

        Console.WriteLine(wynik + " to jest p1");
        return wynik;
    }

    public double skosnosc_p2(List<double> dane)
    {
        double srednia1 = srednia(dane);
        double media = mediana(dane);
        double odchylenie_stand = odchylenie_standardowe(dane);

        double wynik = 3*(srednia1 - media) / odchylenie_stand;
       
        Console.WriteLine(wynik + " to jest p2");
        return wynik;
    }

    public double kwartyl1(List<double> dane)
    {
        double quantityOfValues = 0;
        double median = 0;
        int index = 0;

        dane.Sort();
        quantityOfValues = dane.Count();

        if (quantityOfValues % 2 == 0)
        {

            quantityOfValues = quantityOfValues / 4;
            index = Convert.ToInt32(quantityOfValues);
            median = (dane[index - 1] + dane[index]) / 2;
        }
        else
        {
            quantityOfValues = quantityOfValues / 4;
            index = Convert.ToInt32(quantityOfValues);
            median = dane[Convert.ToInt32(quantityOfValues)];
        }

        Console.WriteLine("to jest kwartyl  -  " + median);
        return median;
    }

    public double kwartyl3(List<double> dane)
    {
        double quantityOfValues = 0;
        double median = 0;
        int index1,index2 = 0;

        dane.Sort();
        quantityOfValues = dane.Count();
        quantityOfValues = quantityOfValues / 2;
        index1 = Convert.ToInt32(quantityOfValues);
        quantityOfValues = quantityOfValues / 2;
        index2 = Convert.ToInt32(quantityOfValues);
        if (quantityOfValues % 2 == 0)
        {
            median = (dane[index1 + index2 - 1] + dane[index1+index2]) / 2;
        }
        else
        {
            median = dane[index1 + index2];
        }

        Console.WriteLine("to jest kwartyl 2  -  " + median);
        return median;
    }

    public double momentCtr(List<double> dane, int degree)
    {
        double avg = srednia(dane);
        double moment = 0;
        double noOfObservations = dane.Count();
        noOfObservations = 1 / noOfObservations;

        foreach(var i in dane)
        {
            moment += Math.Pow((i - avg), degree);
        }
        moment = moment * noOfObservations;
        Console.WriteLine("moment centralny " + degree + " rzedu wynosi: " + moment);
        return moment;
    }
}


class VectorClassification
{
    public double[] Vector;
    public string etykieta;
    public VectorClassification(double[] Vector, string etykieta)
    {
        this.Vector = new double[Vector.Length];
        Set(Vector, etykieta);
    }
    public void Set(double[] Vector, string etykieta)
    {
        Vector.CopyTo(this.Vector, 0);
        this.etykieta = etykieta;
    }

    public override string ToString()
    {
        return etykieta.ToString();
    }
}