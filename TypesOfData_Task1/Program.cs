double initialDeposit = 1000;
int years = 3;
double interestRate = 10;

string result = CalculateCompoundInterest(initialDeposit, years, interestRate);
Console.WriteLine(result);

static string CalculateCompoundInterest(double initialDeposit, int years, double interestRate)
{
    string result = "";
    double currentAmount = initialDeposit;

    for (int i = 0; i < years; i++)
    {
        currentAmount = currentAmount * (1 + interestRate / 100);
        result = result + $"Год {i + 1}: {currentAmount:F2} руб.\n";
    }

    return result;
}