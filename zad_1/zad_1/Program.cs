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
            IRIS iris = new IRIS("C:/Users/emalgpa/Desktop/dane.csv");
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
                    double[] Classification = new double[3];
                    switch (Parts[4])
                    {
                        case "Iris-setosa":
                            Classification[0] = 1;
                            break;
                        case "Iris-versicolor":
                            Classification[1] = 1;
                            break;
                        case "Iris-virginica":
                            Classification[2] = 1;
                            break;
                        default: throw new Exception("bad label");
                           
                    }


                   Dane.Add(new VectorClassification(Vector.ToArray(), Classification));
                   
                    
                    
                    i++;
                    
                  
                  //  Console.Write(Validation[i].ToString());

                }
                foreach(var j in Dane)
                {
                    for (int k = 0; k < 4; k++) {
                        //Console.Write(j.Vector[k]);
                        double temp = 0;
                        temp = +j.Vector[k];
                        Console.Write(temp);
                    }
                        

                    
                    Console.Write("\n");
                    
                }
                //Console.Write(i);
            }
        }
        
    }
}

class VectorClassification
{
    public double[] Vector;
    public double[] Classification;
    public VectorClassification(double[] Vector, double[] Classification)
    {
        this.Vector = new double[Vector.Length];
        Set(Vector, Classification);
    }
    public void Set(double[] Vector, double[] Classification)
    {
        Vector.CopyTo(this.Vector, 0);
        this.Classification = Classification;
    }

    public override string ToString()
    {
        return Classification.ToString();
    }
}