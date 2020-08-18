using System.Runtime.Serialization;

namespace RestWithAPS_NETUdemy.Model.Base
{
    [DataContract]
    public class BaseEntity
    {
        public long? Id { get; set; }
    }
}
