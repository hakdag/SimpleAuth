namespace SimpleAuth.Contracts.Business.Strategies
{
    public interface IStrategyFactory<T>
    {
        T Create();
    }
}
