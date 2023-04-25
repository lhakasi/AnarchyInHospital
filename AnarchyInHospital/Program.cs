using System;
using System.Collections.Generic;
using System.Linq;

namespace AnarchyInHospital
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Terminal terminal = new Terminal();

            terminal.Work();
        }
    }

    class Patient
    {
        private string _name;       

        public Patient(string name, string surname, int age, string sickness)
        {
            _name = name;
            Surname = surname;
            Age = age;
            Sickness = sickness;
        }

        public string Surname { get; private set; }
        public string Sickness { get; private set; }
        public int Age { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"{Surname} {_name} - Возраст:{Age} Диагноз:{Sickness}");
        }
    }

    class Hospital
    {
        private List<Patient> _patients;

        public Hospital()
        {
            _patients = new List<Patient>()
            {
                new Patient("Дурик", "Дуриков", 38, "деменция"),
                new Patient("Кретин", "Кретинов", 32, "деменция"),
                new Patient("Шизик", "Шизиков", 29, "шизофрения"),
                new Patient("Шизоид", "Шизоидов", 18, "шизофрения"),
                new Patient("Псих", "Психов", 45, "психопатия"),
                new Patient("Психопат", "Психопатов", 32, "психопатия"),
                new Patient("Депресняк", "Депресняков", 38, "депрессия"),
                new Patient("Уныл", "Унылов", 18, "депрессия"),
                new Patient("Параноик", "Параноиков", 45, "паранойя"),
                new Patient("Нормал", "Нормалов", 29, "паранойя")
            };
        }

        public List<Patient> GetPatients() => new List<Patient>(_patients);        
    }

    class Terminal
    {
        private Hospital _hospital = new Hospital();

        public void Work()
        {
            const string CommandSortBySurname = "1";
            const string CommandSortByAge = "2";
            const string CommandSortBySickness = "3";
            const string CommandExit = "4";

            bool isWorking = true;

            while (isWorking)
            {
                List<Patient> filteredPatiens = new List<Patient>();

                Console.Clear();
                Console.WriteLine($"{CommandSortBySurname})Отсортировать больных по фамилии");
                Console.WriteLine($"{CommandSortByAge})Отсортировать больных по возрасту");
                Console.WriteLine($"{CommandSortBySickness})Вывести больных с определенным заболеванием");
                Console.WriteLine($"{CommandExit})Закрыть программу");
                Console.Write($"\nВведите номер команды: ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandSortBySurname:
                        filteredPatiens = SortBySurname();
                        break;

                    case CommandSortByAge:
                        filteredPatiens = SortByAge();
                        break;

                    case CommandSortBySickness:
                        filteredPatiens = SortBySickness();
                        break;

                    case CommandExit:
                        isWorking = false;
                        break;

                    default:
                        StorageOfErrors.ShowError(Errors.InvalidCommand);
                        break;
                }

                ShowPatients(filteredPatiens);
            }
        }

        private List<Patient> SortBySurname()
        {
            Console.Write("Введите фамилию больных: ");

            string userInput = Console.ReadLine();

            List<Patient> filteredPatients = _hospital.GetPatients().Where(patient => patient.Surname == userInput).ToList();

            return filteredPatients;
        }

        private List<Patient> SortByAge()
        {
            List<Patient> filteredPatients = new List<Patient>();

            Console.Write("Введите возраст больных: ");

            if (int.TryParse(Console.ReadLine(), out int userInput) == false || userInput <= 0)
                StorageOfErrors.ShowError(Errors.IncorrectInput);
            else
                filteredPatients = _hospital.GetPatients().Where(patient => patient.Age == userInput).ToList();            

            return filteredPatients;
        }

        private List<Patient> SortBySickness()
        {
            Console.Write("Введите болезнь больных: ");

            string userInput = Console.ReadLine().ToLower();

            List<Patient> filteredPatients = _hospital.GetPatients().Where(patient => patient.Sickness == userInput).ToList();

            return filteredPatients;
        }

        private void ShowPatients(List<Patient> filteredPatients)
        {
            Console.WriteLine();

            foreach (Patient patient in filteredPatients)            
                patient.ShowInfo();
            
            Console.ReadKey();
        }
    }

    class StorageOfErrors
    {
        public static void ShowError(Errors error)
        {
            Dictionary<Errors, string> errors = new Dictionary<Errors, string>
            {
                { Errors.InvalidCommand, "Недопустимая команда" },
                { Errors.IncorrectInput, "Некорректный ввод" }
            };

            if (errors.ContainsKey(error))
                Console.WriteLine(errors[error]);
        }
    }

    enum Errors
    {
        InvalidCommand,
        IncorrectInput
    }
}
