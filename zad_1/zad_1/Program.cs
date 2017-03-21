using System;
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
            

            iris.srednia(iris.Dane, "Iris-setosa");
            Console.WriteLine();
            iris.srednia(iris.Dane, "Iris-versicolor");
            Console.WriteLine();
            iris.srednia(iris.Dane, "Iris-virginica");
            Console.WriteLine();
            iris.srednia_geometryczna(iris.Dane, "Iris-setosa");
            Console.WriteLine();
            iris.srednia_harmoniczna(iris.Dane, "Iris-setosa");
            Console.WriteLine();
            iris.dominanta(iris.Dane, "Iris-setosa");
          //  iris.mediana(iris.Dane, "Iris-setosa");
			Console.WriteLine();
       		iris.wariancja(iris.Dane, "Iris-setosa");
            Console.WriteLine();
            iris.odchylenie_standardowe(iris.Dane, "Iris-setosa");
            Console.WriteLine();
            iris.mediana2(iris.Dane, "Iris-setosa");
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


                    //  Console.Write(Validation[i].ToString());

                }
                foreach (var j in Dane)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        //Console.Write(j.Vector[k]);
                        double temp = 0;
                        temp = +j.Vector[k];
                        //Console.Write(temp);
                    }



                    //Console.Write("\n");

                }
                //Console.Write(i);
            }
        }


}

    public double srednia(List<VectorClassification> dane, string etykieta)
    {
      //  List<VectorClassification> dane;
     //   dane = Dane; 
        double suma = 0;
        int licznik = 0;
        
        foreach (var o in dane)
        {
            if (o.etykieta == etykieta)
            {
                licznik++;
                suma += o.Vector[0];
            }

        }

        suma = suma / licznik;
        return suma;
        Console.Write(suma + "\n");
        Console.Write(licznik);
    }

    public void srednia_geometryczna (List<VectorClassification> dane, string etykieta)
    {
        //  List<VectorClassification> dane;
        //   dane = Dane; 
        double iloczyn = 1;
        double licznik = 0;

        foreach (var o in dane)
        {
            if (o.etykieta == etykieta)
            {
                licznik++;
                iloczyn *= o.Vector[0];
            }

        }
       
       iloczyn = Math.Pow(iloczyn,1/licznik);
        Console.Write(iloczyn + "\n");
        //Console.Write(licznik);
    }


    public void srednia_harmoniczna (List<VectorClassification> dane, string etykieta)
    {
        double licznik = 0;
        double mianownik = 0;

        foreach (var o in dane)
        {
            if (o.etykieta == etykieta)
            {
                licznik++;
                mianownik += 1 / o.Vector[0];
            }

        }
        double wynik = 0;

        wynik = licznik / mianownik;
        Console.Write("\n" + wynik);
    }

  
    public void dominanta(List<VectorClassification> dane, string etykieta)
    {
        List<double> listaWartosci = new List<double>();
        List<int> liczList = new List<int>();
        List<double> liczbaNajwiekszychWartosci = new List<double>();
        
        
        double maks = 0;
        int licz = 0;
        double liczba = 0;
        double dominanta = 0;

        foreach (var o in dane)
        {

            if (o.etykieta == etykieta)
            {
                listaWartosci.Add(o.Vector[0]);
            }
        };

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
                
                //if (licz >maks)
                //{
                //    maks = licz;
                //    liczba = listaWartosci[j];
                //}

                
               
            }
           
        }
       // Console.WriteLine("DOMINANTA WYNOSI " + liczba + "    "+liczbaNajwiekszychWartosci.Max());
        foreach (var i in liczbaNajwiekszychWartosci.Distinct()) /// dobre !!!
        {
            
            Console.WriteLine(i);

        }

        
    }


    public void mediana(List<VectorClassification> dane, string etykieta)
    {

        dane.Sort((a, b) => { return a.Vector[0].CompareTo(b.Vector[0]); });

        Console.Write("chuj\n");
        double licznik = 0;
        for (int i = 0; i<dane.Count; i++)
        {
            if (dane[i].etykieta == etykieta)
            {
                
                Console.Write(dane[i].Vector[0] + "\n");
                licznik++;
            }
            
        }
        Console.Write(licznik);

        double med = 0;
        int wielkosc = dane.Count;
        
        med = (dane[(wielkosc - 1) / 2].Vector[0] + dane[wielkosc/2].Vector[0])/2 ;
        Console.Write("mediana równa się" + med);
        Console.WriteLine();
        

    }

	public double wariancja(List<VectorClassification> dane, string etykieta)
    {
        double sumaKwadratowRoznic = 0;
        double roznica;
        double counter = 0;


        foreach(var i in dane)
        {
            if (i.etykieta == etykieta)
            {
                    
                    roznica = 0;
                    roznica = i.Vector[0] - srednia(dane, etykieta);
                    roznica = roznica * roznica;
                    sumaKwadratowRoznic += roznica;
                    counter++;
            }

        }

        sumaKwadratowRoznic = sumaKwadratowRoznic / counter;
        Console.WriteLine(sumaKwadratowRoznic);
        return sumaKwadratowRoznic;
    }

    public double odchylenie_standardowe(List<VectorClassification> dane, string etykieta)
    {
        double odchylenie = Math.Sqrt(wariancja(dane, etykieta));
        Console.WriteLine(odchylenie);
        return odchylenie;
    }

    public double mediana2(List<VectorClassification> dane, string etykieta)
    {
        List<double> tempListWithValuesFromOneColumn = new List<double>();
        double quantityOfValues = 0;
        double median = 0;
        int index = 0;

        foreach(var i in dane)
        {
            if(i.etykieta == etykieta)
            {
                tempListWithValuesFromOneColumn.Add(i.Vector[0]);
            }
            
        }

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

        Console.WriteLine(median);
        return median;
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