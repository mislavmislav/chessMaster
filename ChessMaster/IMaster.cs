using ChessMaster.DataModel;

namespace ChessMaster
{
    public interface IMaster
    {
        DataStatus GenerateDataModel(string username);
        DataStatus GetStatus(string username);
    }
}