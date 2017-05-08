namespace JessicasAquariumMonitor.Helpers.General
{
    public interface IConverter
    {
    }

    public interface IConverter<in TFrom, out TTo> : IConverter
    {
        TTo Convert(TFrom from);
    }
}