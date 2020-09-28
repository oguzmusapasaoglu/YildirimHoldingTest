using System;

namespace YildirimHoldingTest.Core.Common.Base
{
    public class BaseModel
    {
        public int Id { get; set; }
        public int ActivationStatus { get; set; }
    }
    public class ExtendBaseModel : BaseModel
    {
        public DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }
    }
}
