using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace school18.Models
{
     
    
        public class Student  : Person
        {
        //    public int PersonID { get; set; }

        //[StringLength(50, MinimumLength = 1, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        //[Column("LastName")]
        //public string LastName { get; set; }
        //[Column("FirstName")]
        //[StringLength(50, MinimumLength = 1, ErrorMessage = "First name cannot be longer than 50 characters.")]
        //public string FirstMidName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        
        //public string FullName
        //{
        //    get { return LastName + ", " + FirstMidName; }
        //}

        public virtual ICollection<Enrollment> Enrollments { get; set; }
        }
    
}