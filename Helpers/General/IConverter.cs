namespace JessicasAquariumMonitor.Helpers.General
{
    public interface IConverter<in TFrom, out TTo>
    {
        TTo Convert(TFrom from);
    }
}