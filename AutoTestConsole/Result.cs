namespace AutoTestConsole;

public class Result
{
    public int Id { get; set; }
    public int TotalQuestionCount = 20;
    public int CorrectAnswersCount { get; set; } 
    public int InCorrectAnswersCount => TotalQuestionCount - CorrectAnswersCount;
    public string Username { get; set; }
}