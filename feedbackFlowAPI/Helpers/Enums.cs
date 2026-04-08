using System.Text.Json.Serialization;

namespace feedbackFlowAPI.Helpers
{
    /// <summary>
    /// Niveau. A, B, C, D
    /// </summary>
    public enum ClassLevel
    {
        A,
        B,
        C,
        D
    }

    /// <summary>
    /// Delprøve. Digital eller Analog
    /// </summary>
    public enum ExamType
    {
        Digital,
        Analog
    }

    /// <summary>
    /// Is used for question answers. Private, Public
    /// </summary>
    public enum Visibility
    {
        Private,
        Public
    }

    public enum QuestionDifficulty
    {
        Hard,
        Medium,
        Easy
    }

    /// <summary>
    /// If a question needs something specific or not.
    /// ApplyFormula (Anvend formel), 
    /// SpecificMethod (Bestemt metode),
    /// NoRequirement (Ingen krav)
    /// </summary>
    public enum QuestionMethodRequirement
    {
        ApplyFormula,
        SpecificMethod,
        NoRequirement
    }

    /// <summary>
    /// Uddannelse.
    /// HF, HHX, HTX, STX
    /// </summary>

    public enum Education
    {
        HF,
        HHX,
        HTX,
        STX
    }

    /// <summary>
    /// Kontekst til et spørgsmål. F.eks bare løs opgaven eller der er tekst (stor/lille) tilhørende opgave.
    /// </summary>
    public enum QuestionContext
    {
        YesHeavy,
        YesLight,
        No
    }

    /// <summary>
    /// Om der er et tvist i opgaven eller ej.
    /// Ja, Delvist, Nej, Med et tvist.
    /// </summary>
    public enum StandardQuestion
    {
        Yes,
        Partially,
        No,
        WithATwist
    }

    /// <summary>
    /// Ny-gammel ordning. Det er ift. bekendtgørelser
    /// </summary>
    public enum NewOldSystem
    {
        New,
        Old
    }
}
