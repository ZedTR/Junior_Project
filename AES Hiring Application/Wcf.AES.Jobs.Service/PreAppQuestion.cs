//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wcf.AES.Jobs.Service
{
    using System;
    using System.Collections.Generic;
    
    public partial class PreAppQuestion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PreAppQuestion()
        {
            this.PreAppAnswerTables = new HashSet<PreAppAnswerTable>();
        }
    
        public int Id { get; set; }
        public string Question_Title { get; set; }
        public string Question { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PreAppAnswerTable> PreAppAnswerTables { get; set; }
    }
}
