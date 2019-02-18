using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabQuiz
{
    // T is inherited with IComparable class for comparison operations such as greater than or less than on numeric data
    class BinarySearchTree<T> where T : IComparable<T>
    {
        public class BNode //Binary Node class to represent nodes in a tree
        {
            public T data; 
            public BNode left; //left child of the node
            public BNode right; //right child of the node

            public BNode(T value)
            {
                data = value;
                left = null;
                right = null;
            }

            public BNode()
            {
                left = null;
                right = null;
            }
        }

        private BNode root;

        public void Add(T value) 
        {
            root = Add(value, root); //updates the root after adding value to the tree
        }
        private BNode Add(T value, BNode node)
        {
            if(node == null)//if root is null make a node and return it as root
            {
                BNode n = new BNode(value);
                return n;
            }
            else
            {
                if (LessThan(value, node.data, -1)) //if value to add is less than the node data than add value to the left of node
                {
                    node.left = Add(value, node.left);
                }
                else
                    node.right = Add(value, node.right); //if value to add is greater than the node data than add value to the right of node
                return node;
            }
        }

        public BNode Search(T value) //search a value passed in the tree
        {
            return Search(root, value);
        }
        private BNode Search(BNode node, T value) //returns the Node containing the value
        {
            if (node == null) //returns null if value is not in the tree
            {
                Console.WriteLine("Value not found");
                return null;
            }

            if (EqualTo(value, node.data, 0)) 
                return node;

            else if (LessThan(value, node.data, -1))
                return Search(node.left, value);

            else
                return Search(node.right, value);
        }

        public void InOrder()
        {
            Console.WriteLine("InOrder traversing the tree: ");
            InOrder(root); //to print the tree from the root 
        }
        private void InOrder(BNode node)
        {
            if (node == null)
                return;
            InOrder(node.left); //first traverse the left of tree
            Console.WriteLine("{0} ", node.data); //print the node data
            InOrder(node.right); //traverse the right of tree
        }
        public void PreOrder()
        {
            Console.WriteLine("PreOrder traversing of the tree: ");
            PreOrder(root);
        }

        private void PreOrder(BNode node)
        {
            if (node == null)
                return;
            Console.WriteLine(node.data + " ");
            PreOrder(node.left);
            PreOrder(node.right);
        }

        public void PostOrder()
        {
            Console.WriteLine("Traversing tree in PostOrder: ");
            PostOrder(root);
        }
        private void PostOrder(BNode node)
        {
            if (node == null) 
            PostOrder(node.left);
            PostOrder(node.right);
            Console.WriteLine(node.data + " ");

        }

        private bool LessThan(T value1, T value2, int checkType) //function to compare two values for < operation
        {
            if (value1.CompareTo(value2) == -1)
                return true;
            return false;
        }

        private bool GreaterThan(T value1, T value2, int checkType) //function to compare two values for > operation
        {
            if (value1.CompareTo(value2) > 0)
                return true;
            return false;
        }
        
        private bool EqualTo(T value1, T value2, int checkType) //function to check equality of two values
        {
            if (value1.CompareTo(value2) == 0)
                return true;
            return false;
        }

        public int Height()//to check the height of the tree from the root
        {
            Console.WriteLine("The Height of the root is: ");
            return Height(root);
        }

        public int Height(BNode node) // to check the height of the subtree from the passed node
        {
            if (node == null)
                return -1;
            int l = Height(node.left); //stores the height of the left subtree
            int r = Height(node.right); //stores the height of the right substree

            if (l > r)
                return l + 1; //if left subtree is longer than return the height of the left subtree
            else
                return r + 1;//if right subtree is longer than return the height of right substree
        }

        public int Size()//return the size of the tree from the root
        {
            Console.WriteLine("Size of the tree: ");
            return Size(root);
        }
        private int Size(BNode node)
        {
            if (node == null)
                return 0;
            else
                return 1 + Size(node.left) + Size(node.right); //returns the size of the tree by adding 1 for every node that exists
        }

        public BNode Maximum(BNode node)//returns the maximum value node of the node passed
        {
            if (node == null)
                return null;
            if (node.right == null)//if there is nothing to the right of node then it is the maximum 
                return node;
            else
                return Maximum(node.right);
        }

        public BNode Minimum(BNode node) //returns the minimum value node of the node passed
        {
            if (node == null)
                return null;
            if (node.left == null) //if there is nothing to the left of the node then it is the minimum 
                return node;
            else
                return Minimum(node.left);
        }

        public BNode Successor(BNode node)//finds the successor of the passed node
        {
            if (node == null)
                return null;
            else
                return Minimum(node.right);//the most immediate value is the right and then left most value
        }

        public BNode Predecessor(BNode node)//finds the predecessor of the passed node
        {
            if (node == null)
                return null;
            else
                return Maximum(node.left); //right most to the left of the node is the predecessor
        }

        public void Delete(T value)//update the tree after deleting the value
        {
            root = Delete(value, root);
        }

        private BNode Delete(T value, BNode node)
        {
            if (node == null)
                return null;
            if(EqualTo(value, node.data, 0))
            {
                //case 1: node being deleted has no children
                if (node.left == null && node.right == null)
                    return null;
                //case 2: node being deleted has only one children
                else if (node.left == null && node.right != null)
                    return node.right;
                else if (node.left != null && node.right == null)
                    return node.left;
                //case 3: if the node has 2 children then we find its successor and replace with it
                else if (node.right != null) 
                {
                    BNode succ_node = Successor(node);//find the successor node
                    node.data = succ_node.data;//replace current node data with the successor node
                    node.right = Delete(succ_node.data ,node.right); //delete the successor node
                }
                return node;
            }
            if (LessThan(value, node.data, -1))
            {
                node.left = Delete(value, node.left);
            }
            else
                node.right = Delete(value, node.right);
            return node;
        }
    }
}
