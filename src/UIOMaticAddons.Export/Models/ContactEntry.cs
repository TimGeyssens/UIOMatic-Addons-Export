using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UIOMatic.Attributes;
using UIOMatic.Enums;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace UIOMaticAddons.Export.Models
{
    [UIOMaticAction("export","Export", "~/App_Plugins/UIOMaticAddons/export.html", Icon = "icon-document-dashed-line")]
    public class ExportAction { }

    [TableName("ContactEntries")]
    [PrimaryKey("Id", autoIncrement = true)]
    [UIOMatic("contactentries", "Contact Entries", "Contact Entry", 
        FolderIcon = "icon-users",
        SortColumn = "Created", SortOrder = "desc",
        RenderType = UIOMaticRenderType.List,
        ListViewActions = new[]{ typeof(ExportAction)})]
    public class ContactEntry
    {
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Required]
        [UIOMaticListViewField]
        [UIOMaticField]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [UIOMaticListViewField]
        [UIOMaticField]
        public string Email { get; set; }

        [SpecialDbType(SpecialDbTypes.NTEXT)]
        [Required]
        [UIOMaticListViewField]
        [UIOMaticField(View = UIOMatic.Constants.FieldEditors.Rte)]
        public string Message { get; set; }

        [UIOMaticListViewField(Config = "{'format' : '{{value|relativeDate}}'}")]
        [UIOMaticField(View = UIOMatic.Constants.FieldEditors.DateTime)]
        public DateTime Created { get; set; }
    }
}