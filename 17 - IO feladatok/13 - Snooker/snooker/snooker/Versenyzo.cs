namespace snooker;

public class Versenyzo
{
    public string Helyezes { get; set; }

    public string Nev { get; set; }

    public string Orszag {  get; set; }

    public int Nyeremeny { get; set; }

    public override string ToString()
    {
        return $"\tHelyezés: {Helyezes} \n\tNév: {Nev}\n\tOrszág: {Orszag}\n\tNyeremény összege: {Nyeremeny * 380} Ft";
    }
}
