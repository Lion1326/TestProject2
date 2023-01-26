using Microsoft.EntityFrameworkCore;

namespace TestProject.Helpers
{
    public static class IdentityHelpers
    {
        public static void EnableIdentityInsert<T>(this DbContext context) => SetIdentityInsert<T>(context, enable: true);
        public static void DisableIdentityInsert<T>(this DbContext context) => SetIdentityInsert<T>(context, enable: false);
        private static void SetIdentityInsert<T>(DbContext context, bool enable)
        {
            var entityType = context.Model.FindEntityType(typeof(T));
            if (entityType == null) 
            {
                throw new InvalidOperationException("IdentityHelpers.SetIdentityInsert FindEntityType: can't fined FindEntityType");
            }
            var value = enable ? "ON" : "OFF";
            context.Database.ExecuteSqlRaw(
                $"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} {value}");
        }
    }
}
