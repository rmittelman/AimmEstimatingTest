//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AimmEstimatingTest
{
    using System;
    using System.Collections.Generic;
    
    public partial class EstimateSubCalculator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EstimateSubCalculator()
        {
            this.EstimateCalculators = new HashSet<EstimateCalculator>();
        }
    
        public int EstimateSubCalculatorId { get; set; }
        public string Description { get; set; }
        public Nullable<int> Sequence { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstimateCalculator> EstimateCalculators { get; set; }
    }
}