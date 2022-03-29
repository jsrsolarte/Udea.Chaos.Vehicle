namespace Udea.Chaos.Vehicle.Domain.Entities
{
    public interface IEntityBase<out T>
    {
        T Id { get; }
    }
}