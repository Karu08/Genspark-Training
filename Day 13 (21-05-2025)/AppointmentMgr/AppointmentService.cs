using System;
using System.Collections.Generic;
using System.Linq;

public class AppointmentService
{
    private List<Appointment> appointments = new List<Appointment>();
    private int nextId = 1;

    public int AddAppointment(string name, int age, DateTime date, string reason)
    {
        var appointment = new Appointment
        {
            Id = nextId++,
            PatientName = name,
            PatientAge = age,
            AppointmentDate = date,
            Reason = reason
        };

        appointments.Add(appointment);
        return appointment.Id;
    }

    public List<Appointment> Search(AppointmentSearchModel model)
    {
        var result = appointments;

        result = SearchByName(result, model.PatientName);
        result = SearchByDate(result, model.AppointmentDate);
        result = SearchByAge(result, model.AgeRange);

        return result;
    }

    private List<Appointment> SearchByName(List<Appointment> list, string? name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            list = list.Where(a => a.PatientName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        return list;
    }

    private List<Appointment> SearchByDate(List<Appointment> list, DateTime? date)
    {
        if (date.HasValue)
        {
            list = list.Where(a => a.AppointmentDate.Date == date.Value.Date).ToList();
        }
        return list;
    }

    private List<Appointment> SearchByAge(List<Appointment> list, IntRange? range)
    {
        if (range != null)
        {
            list = list.Where(a => range.Contains(a.PatientAge)).ToList();
        }
        return list;
    }

    public void PrintAppointments(List<Appointment> list)
    {
        if (!list.Any())
        {
            Console.WriteLine("No matching appointments found.");
            return;
        }

        foreach (var a in list)
        {
            Console.WriteLine($"\nID: {a.Id}\nName: {a.PatientName}\nAge: {a.PatientAge}\nDate: {a.AppointmentDate}\nReason: {a.Reason}");
        }
    }
}
