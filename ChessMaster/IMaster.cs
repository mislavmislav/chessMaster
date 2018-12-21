using ChessMaster.DataModel;

namespace ChessMaster
{
    public interface IMaster
    {
        void CheckDataModel(string username);
        DataStatus GetStatus(string username);
    }
}