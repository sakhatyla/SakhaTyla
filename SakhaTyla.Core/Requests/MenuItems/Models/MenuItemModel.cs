using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SakhaTyla.Core.Requests.MenuItems.Models
{
    public class MenuItemModel
    {
        public MenuItemModel(string name)
        {
            Name = name;
        }

        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Creation Date")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Modification Date")]
        public DateTime ModificationDate { get; set; }

        [DisplayName("Creation User")]
        public int? CreationUserId { get; set; }
        public Users.Models.UserShortModel? CreationUser { get; set; }

        [DisplayName("Modification User")]
        public int? ModificationUserId { get; set; }
        public Users.Models.UserShortModel? ModificationUser { get; set; }

        [DisplayName("TreePath")]
        public string? TreePath { get; set; }

        [DisplayName("TreeOrder")]
        public string? TreeOrder { get; set; }

        [DisplayName("Menu")]
        public int MenuId { get; set; }
        public Menus.Models.MenuShortModel Menu { get; set; } = null!;

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Url")]
        public string? Url { get; set; }

        [DisplayName("Weight")]
        public int Weight { get; set; }

        [DisplayName("Parent")]
        public int? ParentId { get; set; }
        public MenuItems.Models.MenuItemShortModel? Parent { get; set; }

        public ICollection<MenuItemModel> Children { get; set; }
    }
}
