﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Diagnostics;

namespace zad_1
{
    class Program
    {
        static void Main(string[] args)
        {
            IRIS iris = new IRIS(@"~\..\..\..\data\iris.data.csv");
            iris.start(iris.Dane);
            Process proc = new Process();

    //        proc.StartInfo.FileName = "C:/Program Files (x86)/gnuplot/bin/wgnuplot.exe";

   //         proc.Start();

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

    public void start(List<VectorClassification> dane)
    {
        List<List<double>> irissetosa = new List<List<double>>();
        List<List<double>> irisversicolor = new List<List<double>>();
        List<List<double>> irisvirginica = new List<List<double>>();

        irissetosa = eksportMoreFromVector(dane, "Iris-setosa");
        irisversicolor = eksportMoreFromVector(dane, "Iris-versicolor");
        irisvirginica = eksportMoreFromVector(dane, "Iris-virginica");

        Console.WriteLine("Iris-setosa");
        calculate(irissetosa);
        Console.WriteLine("Iris-versicolor");
        calculate(irisversicolor);
        Console.WriteLine("Iris-virginica");
        calculate(irisvirginica);

        makeHistogram(irissetosa[0], irisversicolor[0], irisvirginica[0]);
    }
      
    public void calculate(List<List<double>> data)
    {
        foreach (var i in data)
        {
            Console.WriteLine("Kolumna nr: " + (data.IndexOf(i)+1));
            Console.WriteLine("Mediana: " + Math.Round( mediana(i), 2));
            Console.WriteLine("Dominanta: " + Math.Round(dominanta(i),2));
            Console.WriteLine("Średnia arytmetyczna: " + Math.Round(srednia(i),2));
            Console.WriteLine("Średnia geometryczna: " + Math.Round(srednia_geometryczna(i),2));
            Console.WriteLine("Średnia harmoniczna: " + Math.Round(srednia_harmoniczna(i),2));
            Console.WriteLine("Kwartyl 1: " + Math.Round(kwartyl1(i),2));
            Console.WriteLine("Kwartyl 3: " + Math.Round(kwartyl3(i),2));
            Console.WriteLine("Wariancja: " + Math.Round(wariancja(i), 2));
            Console.WriteLine("Odchylenie standardowe: " + Math.Round(odchylenie_standardowe(i), 2));
            Console.WriteLine("Odchylenie ćwiartkowe: " + Math.Round(odchylenie_cwiartkowe(i), 2));
            Console.WriteLine("Trzeci moment centralny: " + Math.Round(momentCtr(i, 3), 2));
            Console.WriteLine("Współczynnik skośności na podstawie mediany: " + Math.Round(skosnosc_p1(i), 2));
            Console.WriteLine("Współczynnik skośności na podstawie dominanty: " + Math.Round(skosnosc_p2(i), 2));
            Console.WriteLine("Współczynnik skośności na podstawie odchylenia ćwiartkowego: " + Math.Round(skosnosc_p3(i), 2));

            Console.WriteLine();
        }
    }

    public List<double> eksportFromVector(List<VectorClassification> dane, string etykieta, int column)
    {
        List<double> tempList = new List<double>();

        foreach(var i in dane)
        {
                if (i.etykieta == etykieta)
                {
                    tempList.Add(i.Vector[column]);
                }
        }

        return tempList;
    }

    public void makeHistogram(List<double> setosa, List<double> versicolor, List<double> virginica)
    {
        IdentyficationList setosaa = new IdentyficationList(setosa, "Iris-setosa");
        IdentyficationList versicolorr = new IdentyficationList(versicolor, "Iris-versicolor");
        IdentyficationList virginicaa = new IdentyficationList(virginica, "Iris-virginica");
        List<double> dGranice = new List<double>();
        List<double> gGranice = new List<double>();
        List<double> liczSetosa = new List<double>();
        List<double> liczVersicolor = new List<double>();
        List<double> liczVirginica = new List<double>();

        List<double> max = new List<double>() ;

        max.Add(setosaa.data.Max());
        max.Add(versicolorr.data.Max());
        max.Add(virginicaa.data.Max());

        double gGranica = max.Max();

        List<double> min = new List<double>();

        min.Add(setosaa.data.Min());
        min.Add(versicolorr.data.Min());
        min.Add(virginicaa.data.Min());

        double dGranica = min.Min();

        //  Console.WriteLine(dGranica + " " + gGranica);

        double szerokosc = (gGranica - dGranica) / 25;

        //  Console.WriteLine(szerokosc);

        for(var i=0; i<25; i++)
        {
           // Console.WriteLine(i+"  " + Math.Round(dGranica, 2));
            dGranice.Add(dGranica);
            dGranica += szerokosc;
            
        }

        dGranica = min.Min();
        for (var i = 0; i < 25; i++)
        {
            
            dGranica += szerokosc;
            gGranice.Add(dGranica);
         //   Console.WriteLine(i + "  " + Math.Round(dGranica, 2));

        }


        for(var i = 0; i<25; i++)
        {
            int counter = 0;
            for(var j=0; j<setosaa.data.Count(); j++)
            if (setosaa.data[j]>=dGranice[i] && setosaa.data[j]<gGranice[i])
            {
                counter++;            
            }
            liczSetosa.Add(counter);
       }

        for (var i = 0; i < 25; i++)
        {
            int counter = 0;
            for (var j = 0; j < versicolorr.data.Count(); j++)
                if (versicolorr.data[j] >= dGranice[i] && versicolorr.data[j] < gGranice[i])
                {
                    counter++;
                }
            liczVersicolor.Add(counter);
        }

        for (var i = 0; i < 25; i++)
        {
            int counter = 0;
            for (var j = 0; j < virginicaa.data.Count(); j++)
                if (virginicaa.data[j] >= dGranice[i] && virginicaa.data[j] < gGranice[i])
                {
                    counter++;
                }
            liczVirginica.Add(counter);
        }

        List<string> fileOut = new List<string>();

        for(var i=0; i<25; i++)
        {
            fileOut.Add(Math.Round(dGranice[i],2)+"\t"+Math.Round(gGranice[i],2)+"\t"+liczSetosa[i]+"\t"+liczVersicolor[i]+"\t"+liczVirginica[i]);
        }

            System.IO.File.WriteAllLines(@"~\..\..\..\data\first.dat", fileOut);
        
    }

    public List<List<double>> eksportMoreFromVector(List<VectorClassification> dane, string etykieta)
    {
        List<List<double>> listWithData = new List<List<double>>();

            for (int i = 0; i < 4; i++)
            {
                listWithData.Add(eksportFromVector(dane, etykieta, i));
            }

        return listWithData;
    }

    public void printData(List<List<double>> listWithData)
    {
        foreach (var k in listWithData)
        {
            foreach (var j in k)
            {
                Console.Write(j + "  ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
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

        foreach (var i in liczbaNajwiekszychWartosci.Distinct())
        {
            dominanta += i;
        }

        dominanta = dominanta / liczbaNajwiekszychWartosci.Distinct().Count();
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

        sumaKwadratowRoznic = sumaKwadratowRoznic / (counter - 1);
        return sumaKwadratowRoznic;
    }

    public double odchylenie_standardowe(List<double> dane)
    {
        double odchylenie = Math.Sqrt(wariancja(dane));
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

        return median;
    }

    public double skosnosc_p1(List<double> dane)
    {
        double srednia1 = srednia(dane);
        double moda = dominanta(dane);
        double odchylenie_stand = odchylenie_standardowe(dane);
        double wynik = (srednia1 - moda) / odchylenie_stand;

        return wynik;
    }

    public double skosnosc_p2(List<double> dane)
    {
        double srednia1 = srednia(dane);
        double media = mediana(dane);
        double odchylenie_stand = odchylenie_standardowe(dane);

        double wynik = 3*(srednia1 - media) / odchylenie_stand;
       
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
        return moment;
    }

    public double kurtoza(List<double> dane)
    {
        double partOne = 0;
        double partTwo = 0;
        double partThree = 0;
        double kurtoza = 0;
        double n = dane.Count();
        double s = odchylenie_standardowe(dane);
        double x = srednia(dane);

        partOne = (n * (n + 1)) / ((n - 1) * (n - 2) * (n - 3));

        foreach (var i in dane)
        {
            partTwo += Math.Pow(((i - x) / s), 4);
        }

        partThree = (3 * ((n - 1) * (n - 1))) / ((n - 2) * (n - 3));
        kurtoza = partOne * partTwo - partThree;
        return kurtoza;
    }

    public double odchylenie_cwiartkowe(List<double> dane)
    {
        double q3 = kwartyl3(dane);
        double q1 = kwartyl1(dane);
        double odchylenie = (q3 - q1) / 2;
        return odchylenie;
    }

    public double skosnosc_p3(List<double> dane)
    {
        double q1 = kwartyl1(dane);
        double q3 = kwartyl3(dane);
        double q = odchylenie_cwiartkowe(dane);
        double m = mediana(dane);
        double p3 = (q1 + q3 - 2 * m) / (2 * q);
        return p3;
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

class IdentyficationList
{
    public List<double> data;
    public string etykieta;

    public IdentyficationList(List<double> data, string etykieta)
    {
        this.data = data;
        this.etykieta = etykieta;
    }
}