using System;
using System.Collections.Generic;

using System.Text;

namespace SmartPortal.Model
{
    public class NewsModel
    {
        private int _newsID;

        public int NewsID
        {
            get { return _newsID; }
            set { _newsID = value; }
        }
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _content;

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
        private string _dateCreated;

        public string DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }
        private string _author;

        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        private int _catID;

        public int CatID
        {
            get { return _catID; }
            set { _catID = value; }
        }

        private string _summary;

        public string Summary
        {
            get { return _summary; }
            set { _summary = value; }
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
