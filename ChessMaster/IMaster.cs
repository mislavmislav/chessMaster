namespace ChessMaster
{
    public interface IMaster
    {
        void CheckDataModel(string username);
        object GetStatus(string username);
    }
}