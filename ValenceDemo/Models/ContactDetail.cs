
namespace ValenceDemo.Models
{
    using DAL;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ContactDetail
    {


        /// <summary>
        /// Singleton pattern
        /// </summary>
        private static ContactDetail instancecontactdetail = new ContactDetail();

        public static ContactDetail Instance
        {
            get
            {
                return instancecontactdetail;
            }
        }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string First_Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string Last_Name { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone No.")]
        public decimal Phone_No { get; set; }

        public bool Status
        {
            get; set;
        } = true;
        public System.DateTime CreateDate { get; set; } = DateTime.Now;


    }
}
