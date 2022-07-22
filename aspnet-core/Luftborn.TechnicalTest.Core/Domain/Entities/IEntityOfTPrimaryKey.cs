namespace Luftborn.TechnicalTest.Domain.Entities
{

    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
        bool IsTransient();
    }
}
