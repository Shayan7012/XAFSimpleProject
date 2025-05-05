using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;

namespace Solution3.Module.BusinessObjects.Marketing
{
    [NavigationItem("Marketing")]
    public class Customer : BaseObject
    {
        public Customer(Session session) : base(session) { }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Occupation { get; set; }
        [ImageEditor(ListViewImageEditorCustomHeight = 75, DetailViewImageEditorFixedHeight = 150)]
        public byte[] Photo { get; set; }
        [Association, Aggregated]
        public XPCollection<Testimonial> Testimonials
        {
            get { return GetCollection<Testimonial>(nameof(Testimonials)); }
        }
        [PersistentAlias("Concat(FirstName, ' ', LastName, Iif(Company != null, Concat(' (', Company, ')'), ''))")]
        public string FullName
        {
            get { return (string)EvaluateAlias(nameof(FullName)); }
        }
    }

    [NavigationItem("Marketing")]
    public class Testimonial : BaseObject
    {
        public Testimonial(Session session) : base(session)
        {
            CreatedOn = DateTime.Now;
        }
        [Size(SizeAttribute.Unlimited)]
        public string Quote { get; set; }
        [Size(512)]
        public string Highlight { get; set; }
        [VisibleInListView(false)]
        public DateTime CreatedOn { get; set; }
        public string Tags { get; set; }
        [Association]
        public Customer Customer { get; set; }
    }
}