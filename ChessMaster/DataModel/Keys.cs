namespace ChessMaster.DataModel
{
    public class Keys
    {

        #region Control
        const string DataPullStatus = "datapullstatus";
        public static string CustomerDataPullStatus(string username) => $"{DataPullStatus}_{username}";
        #endregion

        #region Games



        #endregion
    }

    public enum DataPullStatusValue
    {
        Idle,
        Ready,
        InProgress
    }
}
