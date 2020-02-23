using DataAccess;
using Infrastructure.HandlerBase;
using Infrastructure.Result;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Linq;

namespace GameSalesApi.Features.AccountManagement
{
    /// <summary>
    /// Command handler for update <see cref="User.Email"/> in <see cref="GameSalesContext"/>
    /// </summary>
    public class UpdateEmailCommandHandler
        : CommandHandlerDecoratorBase<UpdateUserEmail, Result>
    {
        private readonly DbSet<User> _rUserSet;
        private readonly GameSalesContext _rDbContext;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="userSet">User <see cref="DbSet{User}"/></param>
        public UpdateEmailCommandHandler(DbSet<User> userSet)
            : base(null)
        {
            _rUserSet = userSet;
        }

        /// <summary>
        /// Update <see cref="User.Email"/> in <see cref="GameSalesContext"/>
        /// </summary>
        /// <param name="command"><see cref="UpdateUserEmail"/></param>
        public override void Execute(UpdateUserEmail command)
        {
            if (command.UserId != null)
                throw new ArgumentNullException(nameof(command.UserId));

            if (string.IsNullOrEmpty(command.Email))
                throw new ArgumentNullException(nameof(command.Email));

            var user = _rUserSet.FindAsync(command.UserId).Result;
            user.Email = command.Email;

            _rDbContext.ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified &&
                            !typeof(User).IsAssignableFrom(x.Entity.GetType()))
                .ToList()
                .ForEach(entry => {
                    entry.CurrentValues.SetValues(entry.OriginalValues);
                });

            _rDbContext.SaveChanges();
        }

        /// <summary>
        /// Update <see cref="User.Email"/> in <see cref="GameSalesContext"/>
        /// </summary>
        /// <param name="input"><see cref="UpdateUserEmail"/></param>
        /// <returns><see cref="Result"/></returns>
        public override Result Handle(UpdateUserEmail input)
        {
            try
            {
                Execute(input);
            }
            catch(ArgumentNullException e)
            {
                return Result.Fail(e.Message);
            }

            return Result.Ok();
        }
    }
}
