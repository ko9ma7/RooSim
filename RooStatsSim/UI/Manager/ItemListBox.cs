﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RooStatsSim.DB;
using System.Collections.Generic;

using RooStatsSim.DB.Table;
using System.Windows.Controls;
using System.ComponentModel.Design;

namespace RooStatsSim.UI.Manager
{
    
    class ItemDB_Binding : ItemDB, INotifyPropertyChanged
    {
        public ItemDB_Binding() { }
        public ItemDB_Binding(ItemDB db)
            : base(db)
        { }

        public new int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged("Id"); }
        }
        public new string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }
        public string Itype_name
        {
           get { return Enum.GetName(typeof(ITYPE), i_option.Keys); }
        }
        public int Count
        {
            get { return i_option.Count + d_option.Count + if_option.Count + se_option.Count; }
        }

        public Dictionary<ITYPE, int> ITYPE_OPTION { get; set; }
        public string Itype_value
        {
            get { return Convert.ToString(i_option.Values); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString() => _name;

        protected void OnPropertyChanged(string info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public void ChangeValue(ItemDB_Binding param)
        {
            Id = param.Id;
            Name = param.Name;
            i_option = param.i_option;
            d_option = param.d_option;
            se_option = param.se_option;
            if_option = param.if_option;
        }
    }

    class ItemListBox : ObservableCollection<ItemDB_Binding>
    {
        public ItemListBox()
        { }
        public ItemListBox(ref Dictionary<int, ItemDB> DB)
        {
            foreach (KeyValuePair<int, ItemDB> items in DB)
            {
                ItemDB db = items.Value;
                Add(new ItemDB_Binding(db));
            }

            //if ( Count == 0)
            //{
            //    ItemDB db = new ItemDB();
            //    db.Id = 0;
            //    db.Name = "Null";
            //    Add(new ItemDB_Binding(db));
            //}
        }
        
        public void AddList(ItemDB db)
        {
            if (Count == db.Id)
                Add(new ItemDB_Binding(db));
            else
                SetItem(db.Id, new ItemDB_Binding(db));
            
        }
    }

    class ItemOption_Binding<TYPE, D_TYPE> : INotifyPropertyChanged
    {
        public ItemOption_Binding() { }
        public ItemOption_Binding(KeyValuePair<TYPE, D_TYPE> db)
        {
            _type_name = Enum.GetName(typeof(TYPE), db.Key);
            _type_value = db.Value;
        }

        string _type_name;
        D_TYPE _type_value;
        public string Type_name
        { 
            get { return _type_name; } 
            set { _type_name = value; OnPropertyChanged("Type_name"); } 
        }
        
        public D_TYPE Type_value
        {
            get { return _type_value; }
            set { _type_value = value; OnPropertyChanged("Type_value"); }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString() => _type_name;

        protected void OnPropertyChanged(string info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }

    class ItemOption_Binding : INotifyPropertyChanged
    {
        public ItemOption_Binding() { }
        public ItemOption_Binding(KeyValuePair<IFTYPE, AbilityPerStatus> db)
        {
            _type_name = Enum.GetName(typeof(IFTYPE), db.Key);
            _type_value = db.Value;
        }

        string _type_name;
        AbilityPerStatus _type_value;
        public string Type_name
        {
            get { return _type_name; }
            set { _type_name = value; OnPropertyChanged("Type_name"); }
        }

        public string Type_value
        {
            get {
                string temp = Convert.ToString(Type_per_value) + "당 +" + Convert.ToString(Type_add_value);
                return temp;
            }
        }
        public int Type_add_value
        {
            get { return _type_value.AddValue; }
            set { _type_value.AddValue = value; OnPropertyChanged("Type_value"); }
        }
        public int Type_per_value
        {
            get { return _type_value.PerValue; }
            set { _type_value.PerValue = value; OnPropertyChanged("Type_value"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString() => _type_name;

        protected void OnPropertyChanged(string info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }


    class ItemOptionListBox<TYPE,D_TYPE> : ObservableCollection<ItemOption_Binding<TYPE,D_TYPE>>
    {
        public ItemOptionListBox()
        { }
        public ItemOptionListBox(ref Dictionary<TYPE, D_TYPE> DB)
        {
            foreach (KeyValuePair<TYPE, D_TYPE> items in DB)
            {
                Add(new ItemOption_Binding<TYPE,D_TYPE>(items)) ;
            }
        }
    }
    class ItemOptionListBox : ObservableCollection<ItemOption_Binding>
    {
        public ItemOptionListBox()
        { }
        public ItemOptionListBox(ref Dictionary<IFTYPE, AbilityPerStatus> DB)
        {
            foreach (KeyValuePair<IFTYPE, AbilityPerStatus> items in DB)
            {
                Add(new ItemOption_Binding(items));
            }
        }
    }

    class Job_Limite_List : ObservableCollection<AbilityBinding<bool>>
    {
        public Job_Limite_List()
        {
            foreach(JOB_SELECT_LIST job in Enum.GetValues(typeof(JOB_SELECT_LIST)))
            {
                Add(new AbilityBinding<bool>(EnumProperty_Kor.JOB_SELECT_LIST_KOR_3WORD[job], false, false, Enum.GetName(typeof(JOB_SELECT_LIST), job)));
            }
        }

        public void SelectClass(JOB_SELECT_LIST select_job, bool value)
        {
            int inx = 0;
            int ClassRoot = 0;
            if ( (int)select_job % 100 == 0)
                ClassRoot = 100;
            else if ((int)select_job % 10 == 0)
                ClassRoot = 10;
            
            foreach(JOB_SELECT_LIST job in Enum.GetValues(typeof(JOB_SELECT_LIST)))
            {
                if ( ( (int)job >= (int)select_job) && ((int)job < (int)select_job+ ClassRoot))
                {
                    this[inx].Point = value;
                }
                inx++;
            }
        }
    }
}

