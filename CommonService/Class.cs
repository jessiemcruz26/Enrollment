//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CommonService
{
    using System;
    using System.Collections.Generic;
    
    public partial class Class
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Class()
        {
            this.StudentClasses = new HashSet<StudentClass>();
        }
    
        public int ClassID { get; set; }
        public Nullable<int> CourseID { get; set; }
        public Nullable<int> InstructorID { get; set; }
        public string ClassTime { get; set; }
        public string ClassDate { get; set; }
        public string RoomNumber { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual Instructor Instructor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentClass> StudentClasses { get; set; }
    }
}