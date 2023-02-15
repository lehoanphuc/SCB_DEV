using System;
using System.Collections.Generic;

using System.Text;

namespace SmartPortal.Model
{
    public class CategoryModel
    {
        private int _catID;

        public int CatID
        {
            get { return _catID; }
            set { _catID = value; }
        }
        private string _catName;

        public string CatName
        {
            get { return _catName; }
            set { _catName = value; }
        }
        private string _catDescription;

        public string CatDescription
        {
            get { return _catDescription; }
            set { _catDescription = value; }
        }
        private int _parentID;

        public int ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }
        private string _langID;

        public string LangID
        {
            get { return _langID; }
            set { _langID = value; }
        }
        private int _isPublished;

        public int IsPublished
        {
            get { return _isPublished; }
            set { _isPublished = value; }
        }

        private string _link;

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        private int _order;

        public int Order
        {
            get { return _order; }
            set { _order = value; }
        }

        private string _tag;

        public string Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        private string _userModified;

        public string UserModified
        {
            get { return _userModified; }
            set { _userModified = value; }
        }

        private string _dateModified;

        public string DateModified
        {
            get { return _dateModified; }
            set { _dateModified = value; }
        }
    }
}
