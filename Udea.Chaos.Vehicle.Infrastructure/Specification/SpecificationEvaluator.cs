using Ardalis.Specification;

namespace Udea.Chaos.Vehicle.Infrastructure.Specification
{
    public class SpecificationEvaluator : ISpecificationEvaluator
    {
        public static SpecificationEvaluator Default { get; } = new SpecificationEvaluator();

        private readonly List<IEvaluator> evaluators = new();

        public SpecificationEvaluator()
        {
            evaluators.AddRange(new IEvaluator[]
            {
                WhereEvaluator.Instance,
                OrderEvaluator.Instance,
                PaginationEvaluator.Instance,
            });
        }
        public SpecificationEvaluator(IEnumerable<IEvaluator> evaluators)
        {
            this.evaluators.AddRange(evaluators);
        }

        public virtual IQueryable<TResult> GetQuery<T, TResult>(IQueryable<T> inputQuery, ISpecification<T, TResult> specification) where T : class
        {
            inputQuery = GetQuery(inputQuery, (ISpecification<T>)specification);

#pragma warning disable CS8604 // Possible null reference argument. Null check is the caller responsibillity
            return inputQuery.Select(specification.Selector);
#pragma warning restore CS8604 // Possible null reference argument. Null check is the caller responsibillity
        }

        public virtual IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, ISpecification<T> specification, bool evaluateCriteriaOnly = false) where T : class
        {
            var selectedEvaluators = evaluateCriteriaOnly ? evaluators.Where(x => x.IsCriteriaEvaluator) : evaluators;

            foreach (var evaluator in selectedEvaluators)
            {
                inputQuery = evaluator.GetQuery(inputQuery, specification);
            }

            return inputQuery;
        }
    }
}