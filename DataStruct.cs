using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.DataStructure
{
    class Category : IComparable
    {
        public string Name { get; set; }
        public MessageType Type;
        public MessageTopic Topic;
        public Category(string name, MessageType type, MessageTopic topic)
        {
            Name = name;
            Type = type;
            Topic = topic;
        }

        public int CompareTo(object obj)
        {
            try
            {
                Category mess = obj as Category;
                if(String.Compare(Name, mess.Name) < 0)
                {
                    return -1;
                }
                else
                {
                    if(String.Compare(Name, mess.Name) == 0)
                    {
                        if(Type < mess.Type)
                        {
                            return -1;
                        }
                        else
                        {
                            if(Type == mess.Type)
                            {
                                if(Topic < mess.Topic)
                                {
                                    return -1;
                                }
                                else
                                {
                                    if(Topic == mess.Topic)
                                    {
                                        return 0;
                                    }
                                    else
                                    {
                                        return 1;
                                    }
                                }
                            }
                            else
                            {
                                return 1;
                            }
                        }
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            catch(NullReferenceException)
            {
                return -1;
            }
        }
        public override bool Equals(object obj)
        {
            try
            {
                Category mess = obj as Category;
                return (this == mess);
            }
            catch(NullReferenceException)
            {
                return false;
            }
        }
        public override string ToString()
        {
            return String.Format("{0}.{1}.{2}", Name, Type, Topic);
        }
        public static bool operator>(Category a, Category b)
        {
            return a.CompareTo(b) == 1;
        }
        public static bool operator<(Category a, Category b)
        {
            return a.CompareTo(b) == -1;
        }
        public static bool operator==(Category a, Category b)
        {
            return a.CompareTo(b) == 0;
        }
        public static bool operator!=(Category a, Category b)
        {
            return a.CompareTo(b) != 0;
        }
        public static bool operator>=(Category a, Category b)
        {
            return (a.CompareTo(b) == 1 || a.CompareTo(b) == 0); 
        }
        public static bool operator<=(Category a, Category b)
        {
            return (a.CompareTo(b) == -1 || a.CompareTo(b) == 0);
        }
    } 
}

//enums

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.DataStructure
{
    public enum MessageType
    {
        Incoming,
        Outgoing,
        Service
    }

    public enum MessageTopic
    {
        Subscribe,
        Error,
        Update
    }
}
