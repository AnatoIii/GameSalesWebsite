using Infrastructure.CommandBase;
using Infrastructure.HandlerBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.InfrastructureCommandDecorators
{
    /// <summary>
    /// Command decorator for saving changes in <see cref="GameSalesContext"/>
    /// </summary>
    /// <typeparam name="TIn">TIn</typeparam>
    /// <typeparam name="TOut">TOut, can be used like additional output type in <see cref="CommandHandlerDecoratorBase{TIn, TOut}.Handle(TIn)"</typeparam>
    public class SaveChangesCommandDecorator<TIn, TOut> 
        : CommandHandlerDecoratorBase<TIn, TOut>
        where TIn : ICommand<TOut>
    {
        private readonly DbContext _rDBContext;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="decorated"><see cref="ICommandHandler{TIn, TOut}"/> inner decorator</param>
        public SaveChangesCommandDecorator(ICommandHandler<TIn, TOut> decorated,
            DbContext dbContext)
            : base(decorated)
        {
            _rDBContext = dbContext;
        }

        /// <summary>
        /// Perform saving changes for inner decorators
        /// </summary>
        /// <param name="command">TIn command</param>
        public override void Execute(TIn command)
        {
            _rDecorated.Execute(command);

            if (_rDBContext.SaveChanges() == 0)
                throw new DbUpdateException("Save to DB");
        }

        /// <summary>
        /// Perform saving changes for inner decorators
        /// </summary>
        /// <param name="input">TIn command</param>
        /// <returns>TOut</returns>
        public override TOut Handle(TIn input)
        {
            var res = _rDecorated.Handle(input);

            _rDBContext.SaveChanges();
            //if (_rDBContext.SaveChanges() == 0)
            //    throw new DbUpdateException("Save to DB");


            return res;
        }
    }
}
