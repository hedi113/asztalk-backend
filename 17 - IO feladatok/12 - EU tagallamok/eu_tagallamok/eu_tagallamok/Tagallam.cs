namespace eu_tagallamok;

public class Tagallam
{
    public string Nev {  get; set; }

    public string CsatlakozasDatuma { get; set; }

    public override string ToString()
    {
        return $"{Nev} - {CsatlakozasDatuma}";
    }
}
