using ProductFinder.Domain;

namespace ProductFinder.Services
{
    public interface IInputParser
    {
        FinderInputs ParseInputs(string input);
    }
}