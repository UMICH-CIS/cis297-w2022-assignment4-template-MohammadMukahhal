using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientRecordApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            FileHandler handler = new FileHandler();
            handler.FileWriter();
            ArrayList patientArr = handler.Reader();
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("What would you like to do: ");
                Console.WriteLine("1: Display all patients");
                Console.WriteLine("2: Find by ID");
                Console.WriteLine("3: Display by balance");
                Console.WriteLine("4: Exit");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": PrintAll(patientArr);
                        break;
                    case "2": PrintByID(patientArr);
                        break;
                    case "3": PrintByBalance(patientArr);
                        break;
                    case "4": loop = false;
                        break;
                }
            }
        }

        static void PrintAll(ArrayList arr)
        {
            foreach (Patient p in arr)
            {
                Console.WriteLine("#Patient#");
                Console.WriteLine("ID:" + p.id);
                Console.WriteLine("Name: " + p.name);
                Console.WriteLine("Balance $" + p.balance);
            }
        }

        static void PrintByID(ArrayList arr)
        {
            Console.WriteLine("Enter ID to find: ");
            int id = Int32.Parse(Console.ReadLine());
            foreach (Patient p in arr)
            {
                if (id == p.id)
                {
                    Console.WriteLine("#Patient#");
                    Console.WriteLine("ID:" + p.id);
                    Console.WriteLine("Name: " + p.name);
                    Console.WriteLine("Balance $" + p.balance);
                }
            }

        }

        static void PrintByBalance(ArrayList arr)
        {
            Console.WriteLine("Enter min balance to search by: ");
            decimal balance = Decimal.Parse(Console.ReadLine());
            foreach (Patient p in arr)
            {
                if (balance <= p.balance)
                {
                    Console.WriteLine("#Patient#");
                    Console.WriteLine("ID:" + p.id);
                    Console.WriteLine("Name: " + p.name);
                    Console.WriteLine("Balance $" + p.balance);
                }
            }
        }
    }

    class Patient
    {
        public int id;
        public string name;
        public decimal balance;

        public Patient(int id, string name, decimal balance)
        {
            this.id = id;
            this.name = name;
            this.balance = balance;
        }
    }

    class FileHandler
    {
        public ArrayList Reader()
        {
            int count = 0;
            ArrayList patientArr = new ArrayList();
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader("data.txt"))
                {
                    string line;
                    int id = 0;
                    string name = "";
                    decimal balance = 0;

                    // Read and display lines from the file until 
                    // the end of the file is reached. 
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line == "#")
                        {
                            count = 0;
                        }
                        switch (count)
                        {
                            case 1: id = Int32.Parse(line);
                                break;
                            case 2: name = line;
                                break;
                            case 3: balance = Decimal.Parse(line);
                                break;
                        }
                        if(count == 3)
                        {
                            Patient patient = new Patient(id, name, balance);
                            patientArr.Add(patient);
                        }
                        count++;
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return patientArr;
        }

        public void FileWriter()
        {
            int id;
            string name;
            decimal balance;
            bool loop = true;
            ArrayList patientArr = new ArrayList();
            try
            {
                while (loop) {
                    Console.WriteLine("Enter numerical patient ID: ");
                    id = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Enter patient name: ");
                    name = Console.ReadLine();
                    Console.WriteLine("Enter patient balance: ");
                    balance = Decimal.Parse(Console.ReadLine());
                    Patient patient = new Patient(id,name,balance);
                    patientArr.Add(patient);
                    Console.WriteLine("Would you like to add another? (y or n): ");
                    string input = Console.ReadLine();
                    if(input == "n")
                    {
                        loop = false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
                Environment.Exit(1);
            }

            using (StreamWriter sw = new StreamWriter("data.txt"))
            {

                foreach (Patient p in patientArr)
                {
                    sw.WriteLine("#");
                    sw.WriteLine(p.id);
                    sw.WriteLine(p.name);
                    sw.WriteLine(p.balance);
                }
            }
        }
    }

}
