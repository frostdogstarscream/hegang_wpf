using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hegang.APP
{
    class Node : INotifyPropertyChanged
    {
        private string nodeName;
        private Node parentNode;
        private ObservableCollection<Node> childList;
        private bool isChecked;
        private bool isExpanded;

        public Node(string nodeName)
        {
            this.nodeName = nodeName;
        }

        public Node(string nodeName, Node parentNode)
        {
            this.nodeName = nodeName;
            this.parentNode = parentNode;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public string NodeName
        {
            get { 
                return nodeName; 
            }
            set { 
                nodeName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("NodeName"));
            }
        }

        public Node ParentNode
        {
            get
            {
                return parentNode;
            }
            set
            {
                parentNode = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ParentNode"));
            }
        }

        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                
                if (this.childList != null)
                {
                    foreach (Node n in childList)
                        n.IsChecked = value;
                }

                if (isChecked == true)
                {
                    if (this.ParentNode != null)
                        this.ParentNode.setIsChecked(true);
                    this.IsExpanded = true;
                }

                OnPropertyChanged(new PropertyChangedEventArgs("IsChecked"));
            }
        }

        public bool IsExpanded
        {
            get
            {
                return isExpanded;
            }
            set
            {
                isExpanded = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsExpanded"));
            }
        }

        public ObservableCollection<Node> ChildList
        {
            get
            {
                return childList;
            }
            set
            {
                childList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ChildList"));
            }
        }

        public void setIsChecked(bool value)
        {
            this.isChecked = value;
            OnPropertyChanged(new PropertyChangedEventArgs("IsChecked"));
        }
        

        public static ObservableCollection<Node> test()
        {
            ObservableCollection<Node> result = new ObservableCollection<Node>();
            
            Node node_1 = new Node("主井");
            Node node_2 = new Node("副井");

            ObservableCollection<Node> childList_1 = new ObservableCollection<Node>();
            childList_1.Add(new Node("提升机电控", node_1));
            childList_1.Add(new Node("温度", node_1));
            childList_1.Add(new Node("液压站", node_1));

            ObservableCollection<Node> childList_2 = new ObservableCollection<Node>();
            childList_2.Add(new Node("提升机电控", node_2));
            childList_2.Add(new Node("温度", node_2));
            childList_2.Add(new Node("液压站", node_2));

            node_1.ChildList = childList_1;
            node_2.ChildList = childList_2;

            result.Add(node_1);
            result.Add(node_2);

            return result;
        }
    }
}
