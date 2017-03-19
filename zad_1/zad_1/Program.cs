using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

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

    public void srednia(List<VectorClassification> dane, string etykieta)
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