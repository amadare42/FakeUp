namespace FakeUp.ValueEvaluation
{
    public class EvaluationResult
    {
        public bool IsSuccessful { get; }
        public object Value { get; }

        public EvaluationResult()
        {
            this.IsSuccessful = false;
        }

        public EvaluationResult(object value)
        {
            this.IsSuccessful = true;
            this.Value = value;
        }

        public static EvaluationResult Empty => new EvaluationResult();
    }
}