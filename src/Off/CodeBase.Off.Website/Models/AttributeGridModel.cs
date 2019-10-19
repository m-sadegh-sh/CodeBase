namespace CodeBase.Off.Website.Models {
    using System.ComponentModel;

    public sealed class AttributeGridModel {
        [DisplayName(@"شناسه مالک")]
        public object Owner { get; set; }

        [DisplayName(@"کلید")]
        public string Key { get; set; }

        [DisplayName(@"مقدار")]
        public object Value { get; set; }
    }
}