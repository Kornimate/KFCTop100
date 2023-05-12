namespace KFCSharedData.Service
{
    public class KFCService : IKFCService
    {
        private KFCDbContext context;

        public KFCService(KFCDbContext context)
        {
            this.context = context;
        }

        public Record LoadById(int id)
        {
            return context.Records.Single(x => x.Id == id);
        }

        public List<Record> LoadTop100()
        {
            return context.Records.Take(100).ToList();
        }

        public byte[]? LoadDefaultPicture()
        {
            return File.ReadAllBytes("Pictures/warning.png");
        }

        public void SaveRecordToDatabase(Record rec)
        {
            Record data = context.Records.FirstOrDefault(x => x.Address == rec.Address && x.Name == rec.Name)!;
            if(data != null)
            {
                context.Records.Remove(data);
            }
            context.Records.Add(rec);
            context.SaveChanges();
        }
    }
}
