using System.Collections.Generic;

namespace Japyx.RotationHelper.Models
{
    public class RotationDetail
    {
        public string SectionName { get; set; }
        public List<RotationAction> Actions { get; set; }
    }
}
