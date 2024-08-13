using BmiProject.Service.Models;

namespace BmiProject.Service;

public interface IBmiComputationService
{
    BmiResult Compute(double heightCm, double weightKg);
}

public class BmiComputationService : IBmiComputationService
{
    private readonly IBmiValidator _validator;
    public BmiComputationService(IBmiValidator validator)
    {
        _validator = validator;
    }

    public virtual BmiResult Compute(double heightCm, double weightKg)
    {
        var heightMeter = heightCm / 100;

        _validator.Validate(heightMeter, weightKg);

        double bmi = Math.Round(weightKg / (heightMeter * heightMeter), 2);
     
        string category = bmi switch
        {
            < 18.5 => "Underweight",
            <= 24.9 => "Normal weight",
            <= 29.9 => "Overweight",
            _ => "Obesity",
        };

        return new BmiResult
        {
            Bmi = bmi,
            Category = category
        };
    }
}


