using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace KFCSharedData
{
    public class DbInitializer
    {
        public static void Initialize(KFCDbContext context)
        {
            context.Database.Migrate();

            if (context.Records.Any())
            {
                return;
            }
            string imagePath = "Pictures/warning.png";
            List<Record> newData = new List<Record>()
            {
                new Record {Name="Test",Date="1/1/1",Picture = File.ReadAllBytes(imagePath), Address="nod data", Population=0}
            };

            context.AddRange(newData);
            context.SaveChanges();
        }
    }
}
