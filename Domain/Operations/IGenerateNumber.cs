
namespace Domain.Operations
{
    public interface IGenerateNumber
    {
        int Next(int maxValue);
        int Next(int minValue, int maxValue);
    }
}
