using System.ComponentModel.DataAnnotations;

namespace FMS.Models
{
    public class County
    {
        #region Constructor
        // Constructor here
        #endregion

        #region Properties
        // This list will not change, so no need for "BaseActiveModel"
        public int Id { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        #endregion

        #region Methods
        //Methods here
   
        #endregion
    }
}
