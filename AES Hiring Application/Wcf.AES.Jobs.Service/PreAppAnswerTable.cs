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
    
    public partial class PreAppAnswerTable
    {
        public int Id { get; set; }
        public int ApplicationID { get; set; }
        public int QuestionID { get; set; }
        public string Answer { get; set; }
    
        public virtual Application Application { get; set; }
        public virtual PreAppQuestion PreAppQuestion { get; set; }
    }
}
