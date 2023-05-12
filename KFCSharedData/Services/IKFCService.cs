namespace KFCSharedData.Service
{
    public interface IKFCService
    {
        public List<Record> LoadTop100();

        public Record LoadById(int id);

        public byte[]? LoadDefaultPicture();

        public void SaveRecordToDatabase(Record rec);
    }
}
