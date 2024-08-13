namespace BmiProject.Service;

public interface IBmiValidator
{
    bool Validate(double heightMeter, double weightKg);
}

public class BmiValidator : IBmiValidator
{
    public bool Validate(double heightMeter, double weightKg)
    {
        if (heightMeter >= 0 && weightKg >= 0) return true;

        throw new ValidationException("Invalid height or weight");
    }
}   
