using System;

class Program
{
    static void Main()
    {
        var service = new AppointmentService();

        while (true)
        {
            Console.WriteLine("\n---- Cardiologist Appointment Manager ----");
            Console.WriteLine("1. Add Appointment");
            Console.WriteLine("2. Search Appointments");
            Console.WriteLine("3. Exit");
            Console.Write("Enter choice: ");
            string? choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        AddAppointment(service);
                        break;
                    case "2":
                        SearchAppointments(service);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error]: {ex.Message}");
            }
        }
    }

    static void AddAppointment(AppointmentService service)
    {
        Console.Write("Enter patient name: ");
        string name = Console.ReadLine()!;
        Console.Write("Enter patient age: ");
        int age = int.Parse(Console.ReadLine()!);
        Console.Write("Enter appointment date and time (yyyy-MM-dd HH:mm): ");
        DateTime date = DateTime.Parse(Console.ReadLine()!);
        Console.Write("Enter reason for visit: ");
        string reason = Console.ReadLine()!;

        int id = service.AddAppointment(name, age, date, reason);
        Console.WriteLine($"Appointment added successfully. ID = {id}");
    }

    static void SearchAppointments(AppointmentService service)
    {
        var model = new AppointmentSearchModel();

        Console.Write("Search by patient name (optional): ");
        string name = Console.ReadLine()!;
        model.PatientName = string.IsNullOrWhiteSpace(name) ? null : name;

        Console.Write("Search by date (yyyy-MM-dd, optional): ");
        string dateInput = Console.ReadLine()!;
        if (!string.IsNullOrWhiteSpace(dateInput))
        {
            if (DateTime.TryParse(dateInput, out DateTime parsedDate))
                model.AppointmentDate = parsedDate;
        }

        Console.Write("Search by age range (e.g., 30-50, optional): ");
        string rangeInput = Console.ReadLine()!;
        if (!string.IsNullOrWhiteSpace(rangeInput))
        {
            var parts = rangeInput.Split('-');
            if (parts.Length == 2 &&
                int.TryParse(parts[0], out int start) &&
                int.TryParse(parts[1], out int end))
            {
                model.AgeRange = new IntRange { Start = start, End = end };
            }
        }

        var results = service.Search(model);
        service.PrintAppointments(results);
    }
}
