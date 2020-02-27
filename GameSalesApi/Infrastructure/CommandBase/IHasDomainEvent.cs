namespace Infrastructure.CommandBase
{
    // [DN] add basic methods
    public interface IHasDomainEvents<TOut>
    {
        ICommand<TOut> GetDomainEvents();
    }
}
