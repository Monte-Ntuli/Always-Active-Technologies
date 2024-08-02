using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleApp1
{
    class Program
    {
        static List<int> globalList = new List<int>();
        static object lockObject = new object();
        static Random random = new Random();

        static void Main(string[] args)
        {
            Thread oddThread = new Thread(AddOddNumbers);
            Thread primeThread = new Thread(AddNegativePrimes);

            oddThread.Start();
            primeThread.Start();

            while (true)
            {
                lock (lockObject)
                {
                    if (globalList.Count >= 250000)
                    {
                        Thread evenThread = new Thread(AddEvenNumbers);
                        evenThread.Start();
                        break;
                    }
                }
            }

            oddThread.Join();
            primeThread.Join();

            lock (lockObject)
            {
                if (globalList.Count < 1000000)
                {
                    Thread evenThread = new Thread(AddEvenNumbers);
                    evenThread.Start();
                    evenThread.Join();
                }
            }

            globalList.Sort();

            int oddCount = globalList.Count(x => x % 2 != 0);
            int evenCount = globalList.Count - oddCount;

            Console.WriteLine($"Odd numbers: {oddCount}");
            Console.WriteLine($"Even numbers: {evenCount}");

            SerializeToBinary(globalList, "globalList.bin");
            SerializeToXml(globalList, "globalList.xml");
        }

        static void AddOddNumbers()
        {
            while (true)
            {
                lock (lockObject)
                {
                    if (globalList.Count >= 1000000)
                        break;

                    int num = random.Next(1, 1000000);
                    if (num % 2 != 0)
                        globalList.Add(num);
                }
            }
        }

        static void AddNegativePrimes()
        {
            int num = 2;
            while (true)
            {
                lock (lockObject)
                {
                    if (globalList.Count >= 1000000)
                        break;

                    if (IsPrime(num))
                        globalList.Add(-num);

                    num++;
                }
            }
        }

        static void AddEvenNumbers()
        {
            while (true)
            {
                lock (lockObject)
                {
                    if (globalList.Count >= 1000000)
                        break;

                    int num = random.Next(1, 1000000);
                    if (num % 2 == 0)
                        globalList.Add(num);
                }
            }
        }

        static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            for (int i = 3; i <= Math.Sqrt(number); i += 2)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }

        static void SerializeToBinary(List<int> list, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, list);
            }
        }

        static void SerializeToXml(List<int> list, string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<int>));
            using (TextWriter writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, list);
            }
        }
    }
}