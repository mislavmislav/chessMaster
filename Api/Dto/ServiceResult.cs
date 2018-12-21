using System.Runtime.Serialization;

namespace Api.Dto
{
    [DataContract]
    public class ServiceResult<T>
    {
        public ServiceResult(T result)
        {
            Result = result;
            ErrorCode = (int)0;
        }

        [DataMember]
        public T Result { get; set; }

        [DataMember]
        public int ErrorCode { get; set; }
    }
}
