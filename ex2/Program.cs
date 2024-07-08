using System;
using System.Collections.Generic;

public class Persoana
{
    public string ID { get; set; }
    public string Nume { get; set; }

    public Persoana(string id, string nume)
    {
        ID = id;
        Nume = nume;
    }
}

public class Tranzactie
{
    public DateTime Data { get; set; }
    public decimal Suma { get; set; }
    public string Tip { get; set; }

    public Tranzactie(DateTime data, decimal suma, string tip)
    {
        Data = data;
        Suma = suma;
        Tip = tip;
    }
}

public class ContBancar
{
    public DateTime DataCrearii { get; private set; }
    public Persoana Proprietar { get; private set; }
    public string NumarCont { get; private set; }
    public string Moneda { get; private set; }
    private string PIN { get; set; }
    private List<Tranzactie> Tranzactii { get; set; }

    public ContBancar(Persoana proprietar, string numarCont, string moneda, string pin)
    {
        DataCrearii = DateTime.Now;
        Proprietar = proprietar;
        NumarCont = numarCont;
        Moneda = moneda;
        PIN = pin;
        Tranzactii = new List<Tranzactie>();
    }

    public decimal DeterminaSold()
    {
        decimal sold = 0;
        foreach (var tranzactie in Tranzactii)
        {
            if (tranzactie.Tip == "Depozit")
            {
                sold += tranzactie.Suma;
            }
            else if (tranzactie.Tip == "Retragere")
            {
                sold -= tranzactie.Suma;
            }
        }
        return sold;
    }

    public void AlimentareCont(decimal suma)
    {
        Tranzactii.Add(new Tranzactie(DateTime.Now, suma, "Depozit"));
    }

    public void ExtrageFond(decimal suma)
    {
        Tranzactii.Add(new Tranzactie(DateTime.Now, suma, "Retragere"));
    }
}

class Program
{
    static void Main(string[] args)
    {
        Persoana persoana = new Persoana("123456789", "Jalba Oleg");
        ContBancar contBancar = new ContBancar(persoana, "123456789", "MDL", "1234");

        contBancar.AlimentareCont(1000);
        contBancar.ExtrageFond(200);
        contBancar.AlimentareCont(500);

        // Afișarea soldului curent
        Console.WriteLine($"Soldul curent: {contBancar.DeterminaSold()} {contBancar.Moneda}");
    }
}
