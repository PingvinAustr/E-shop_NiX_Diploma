namespace Catalog.Host.Models.Responses
{
    public class AddCarResponse<T>
    {
        public T Id { get; set; } = default(T)!;
    }
}
