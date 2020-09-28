using Dapper.Contrib.Extensions;
using System;

namespace YildirimHoldingTest.Core.Common.Base
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int ActivationStatus { get; set; }
    }
    public class ExtendBaseEntity : BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }
    }
}
