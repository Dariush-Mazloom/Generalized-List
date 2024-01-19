using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure2
{
    class GNode
    {
        public object item;
        public bool tag;
        public GNode nextlink;
        public GNode()
        {
            tag = false;
            this.item = null;
            nextlink = null;
        }
        public GNode(bool tag, object item)
        {
            if (tag == true)
            {
                this.item = new GList().first;
                this.tag = tag;
            }
            else
            {
                this.item = item;
                this.tag = tag;
            }

            nextlink = null;
        }
    }
    class TwoWayNode
    {
        public TwoWayNode Rlink;
        public TwoWayNode Llink;
        public object item;
        public TwoWayNode()
        {
            Rlink = null;
            Llink = null;
            item = null;
        }
        public TwoWayNode(object item)
        {
            this.item = item;
        }

    }
    class TwoWayLinkList
    {
        public TwoWayNode first;
        public TwoWayLinkList()
        {
            first = new TwoWayNode();
        }
        // print 
        public void Print(TwoWayNode p)
        {
            while (p.Rlink != first)
            {
                Console.WriteLine(p.item);
                p = p.Rlink;
            }
            Console.WriteLine(p.item);
        }
    }
    class GList
    {
        public GNode first;
        public GList()
        {
            first = new GNode(false, null);
        }
        // Add an tomic node
        public void AddInListGnode(GNode n)
        {
            GNode p = first;
            while (p.nextlink != null)
            {
                p = p.nextlink;
            }
            p.nextlink = n;
        }
        // Add a sublist at the end
        public void AddInListGlist(GList l)
        {
            GNode p = first;
            while (p.nextlink != null)
            {
                p = p.nextlink;
            }
            GNode sub = new GNode(true, null);
            p.nextlink = sub;
            sub.item = l.first;
        }
        // Print the List
        public void Print(GNode p)
        {
            //for the input p must be the first of the list for the first time => p=first;
            Console.Write("<");
            while (p != null)
            {
                if (!p.tag)
                {
                    if (p.item != null)
                        Console.Write(p.item);
                }
                else
                {
                    Print((GNode)p.item);
                }
                p = p.nextlink;
            }
            Console.Write(">");
        }
        // Depth of List
        public int Depth(GNode s)
        {
            if (s == null)
                return 0;
            GNode p = s;
            int m = 0;
            while (p != null)
            {
                if (p.tag)
                {
                    int n = Depth((GNode)p.item);
                    if (n > m)
                        m = n;

                }
                p = p.nextlink;
            }
            return m + 1;
        }
        // sum of items 
        public int SumOfItem(GNode p)
        {
            int sum = 0;
            while (p != null)
            {
                if (p.tag)
                {
                    sum += SumOfItem((GNode)p.item);
                }
                else
                {
                    if (p.item != null)
                        sum += Convert.ToInt32(p.item);
                }
                p = p.nextlink;
            }
            return sum;
        }
        //Delete data X of List
        public void DeleteOfList(GNode s, object x)
        {
            GNode p = s;
            GNode q = null;
            while (p != null)
            {
                if (!p.tag)
                {
                    if (Convert.ToChar(p.item) == Convert.ToChar(x))
                    {
                        q.nextlink = p.nextlink;

                    }
                    else
                        q = p;

                }
                else
                {
                    DeleteOfList((GNode)p.item, x);

                }
                p = p.nextlink;
            }
        }
        // Creating a TWowayList of all item in GList
        public TwoWayLinkList ConvertToTwoWay(GNode p)
        {
            TwoWayLinkList l1 = new TwoWayLinkList();
            TwoWayNode q = l1.first;
            TwoWayNode r = l1.first;
            while (p != null)
            {
                if (p.item != null)
                {
                    if (!p.tag)
                    {
                        TwoWayNode temp = new TwoWayNode(p.item);
                        q.Rlink = temp;
                        q = temp;
                        q.Llink = r;
                        q.Rlink = l1.first;
                        l1.first.Llink = q;
                        r = q;
                    }
                    else
                    {
                        TwoWayNode temp = ConvertToTwoWay((GNode)p.item).first.Rlink;
                        q.Rlink = temp;
                        q = temp;
                        q.Llink = r;
                        r = q;
                        while (q.Rlink.item != null)
                        {
                            q = q.Rlink;
                            r = q;
                        }
                        q.Rlink = l1.first;
                    }
                }
                p = p.nextlink;
            }


            return l1;
        }
        //Check if two List are eaual
        public bool IsEqual(GNode p, GNode q)
        {
            while (p != null || q != null)
            {
                if (p != null && q != null)
                {
                    if (p.tag && q.tag)
                    {
                        if (IsEqual((GNode)q.item, (GNode)p.item))
                        {
                            p = p.nextlink;
                            q = q.nextlink;
                        }
                        else
                            return false;
                    }
                    else if (!p.tag && !q.tag)
                    {
                        if (Convert.ToChar(p.item) == Convert.ToChar(q.item) || (p == null && q == null))
                        {
                            p = p.nextlink;
                            q = q.nextlink;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            return true;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //(a(ab(xy)c)(e(f(gh))))
            Console.ForegroundColor = ConsoleColor.Magenta;
            string wellcome = "Welcome to our program";
            Console.SetCursorPosition((Console.WindowWidth - wellcome.Length) / 2, Console.CursorTop);
            Console.WriteLine(wellcome);
            Console.ResetColor();
            GList l = null;
            TwoWayLinkList l1 = null;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("please choose your command: ");
                Console.WriteLine("1: Create a new GeneralizeList: ");
                Console.WriteLine("2: print the currunt GeneralizeList ");
                Console.WriteLine("3: Depth of the currunt GeneralizeList ");
                Console.WriteLine("4: The sum of all item ");
                Console.WriteLine("5: Delete an item from the currunt GeneralizeList");
                Console.WriteLine("6: Convert the currunt GeneralizeList to a TwoWayLinkList and print");
                Console.WriteLine("7: Add a Node or a sublist ");
                Console.WriteLine("8: Check if another List is equal to the current one ");
                Console.WriteLine("9: Quit");
                Console.ForegroundColor = ConsoleColor.Cyan;
                int key = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                if (key == 9)
                    break;

                if (key == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Please Enter your expression:");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    string s = Console.ReadLine();
                    int counter = 1;
                    l = CreateList(s, ref counter);

                }
                else if (key == 2)
                {
                    GNode p = l.first;
                    l.Print(p);
                }

                else if (key == 3)
                {
                    GNode p = l.first;
                    Console.WriteLine("The Depth is : " + l.Depth(p));
                }
                else if (key == 4)
                {
                    GNode p = l.first;
                    Console.WriteLine("the sum of item is : " + l.SumOfItem(p));
                }
                else if (key == 5)
                {
                    GNode p = l.first;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Please enter the item you want to delete:");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    object item = Console.ReadLine();
                    l.DeleteOfList(p, item);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("The list after delete: ");
                    l.Print(p);
                }
                else if (key == 6)
                {
                    GNode p = l.first;
                    l1 = l.ConvertToTwoWay(p);
                    TwoWayNode p1 = l1.first.Rlink;
                    l1.Print(p1);
                }
                else if (key == 7)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("1.Wanna add a node");
                    Console.WriteLine("2.Wanna add a sublist");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    int key1 = Convert.ToInt32(Console.ReadLine());
                    if (key1 == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Enter value ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        object item = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Enter tag 0:false , 1:True");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        bool toggle = Convert.ToBoolean(Console.ReadLine());
                        GNode p = new GNode(toggle, item);
                        l.AddInListGnode(p);
                    }
                    else if (key1 == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Please Enter your expression:");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        string s = Console.ReadLine();
                        int counter = 1;
                        GList temp = CreateList(s, ref counter);
                        l.AddInListGlist(temp);
                    }
                    else
                    {
                        Console.WriteLine("We dont have that option right now");
                    }
                }
                else if (key == 8)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Please Enter your expression for the list you want to check:");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    string s = Console.ReadLine();
                    int counter = 1;
                    GList l2 = CreateList(s, ref counter);
                    GNode p = l.first;
                    GNode q = l2.first;
                    if (l.IsEqual(q, p))
                    {
                        Console.WriteLine("The list is equal to the current one");
                    }
                    else
                        Console.WriteLine("The list is not equal to the current one");
                }


            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Do not visit us again thank you for your visit :) ");
            Console.ResetColor();


        }
        // Creating Glist from expression
        public static GList CreateList(string exp, ref int count)
        {
            GList l = new GList();
            GNode p = l.first;
            char[] s = exp.ToCharArray();
            for (ref int i = ref count; i < s.Length; i++)
            {
                char c = s[i];
                if (c == ')')
                    break;
                else if (c != '(')
                {
                    GNode h = new GNode(false, c);
                    p.nextlink = h;
                    p = h;
                }
                else if (c == '(')
                {
                    count++;
                    GNode h = new GNode(true, null);
                    p.nextlink = h;
                    p = h;
                    h.item = CreateList(exp, ref count).first;

                }

            }
            return l;
        }
    }
}
