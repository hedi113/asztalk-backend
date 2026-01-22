namespace LabdarugoVB;

public class VB
{
    public string Varos {  get; set; }
    public string Nev1 { get; set; }
    public string Nev2 { get; set; }
    public int FeroHely { get; set; }

    public override string ToString()
    {
        return $"{Varos} - {Nev1}, {Nev2}, {FeroHely}";
    }
}
