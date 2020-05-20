
namespace ValidationService
{
    public class UnknownObjectValidator : IAValidator
    {
        public bool IsValid()
        {
            return true;
        }

        public string Errors()
        {
            return "Unknown object type";
        }
    }
}
