using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveMusicFriend.Models
{
    public class TestBST
    {
        BinaryTree BST { get; set; }

        public TestBST()
        {
            BST = new BinaryTree();
        }

        public void Test()
        {
            BST.Insert("first node");
            BST.Insert("second node");
            BST.Insert("third node");
            BST.Insert("fourth node");
            BST.Insert("fifth node");
            BST.Insert("sixth node");
            BST.Insert("seventh node");
            BST.Insert("eighth node");

            string results = BST.DrawTree();
        }
    }

    public class TreeNode
    {
        public TreeNode Left, Right;
        public string Name {get; set;}

        public TreeNode(string name)
        {
            Left = null;
            Right = null;
            Name = name;
        }
    }

    public class BinaryTree
    {
        private TreeNode _root;
        private int _count = 0;
        public int Count { get { return _count; }}

        public BinaryTree()
        {
            _root = null;
            _count = 0;
        }

        public void Clear()
        {
            this.KillTree(ref _root);
            _count = 0;
        }

        public TreeNode FindSymbol(string name)
        {
            TreeNode np = _root;
            int cmp;

            while (np != null)
            {
                cmp = String.CompareOrdinal(name, np.Name);

                if (cmp == 0)   // found !
                {
                    return np;
                }

                if (cmp < 0)
                {
                    np = np.Left;
                }
                else
                {
                    np = np.Right;
                }
            }
            return null;  // Return null to indicate failure to find name
        }

        public TreeNode Insert(string name)
        {
            TreeNode node = new TreeNode(name);
            try
            {
                if (_root == null)
                    _root = node;
                else
                    Add(node, ref _root);

                _count++;
                return node;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void Add(TreeNode node, ref TreeNode tree)
        {
            if (tree == null)
            {
                tree = node;
            }
            else
            {
                int comparison = String.CompareOrdinal(node.Name, tree.Name);

                if (comparison == 0)
                    throw new Exception();
                if (comparison < 0)
                    Add(node, ref tree.Left);
                else
                    Add(node, ref tree.Right);
            }
        }

        private TreeNode FindParent(string name, ref TreeNode parent)
        {
            TreeNode np = _root;
            parent = null;
            int cmp;

            while (np != null)
            {
                cmp = String.Compare(name, np.Name);
                if (cmp == 0)   // found !
                    return np;

                if (cmp < 0)
                {
                    parent = np;
                    np = np.Left;
                }
                else
                {
                    parent = np;
                    np = np.Right;
                }
            }
            return null;  // Return null to indicate failure to find name
        }

        public TreeNode FindSuccessor(TreeNode startNode, ref TreeNode parent)
        {
            parent = startNode;

            // Look for the left-most node on the right side
            startNode = startNode.Right;

            while (startNode.Left != null)
            {
                parent = startNode;
                startNode = startNode.Left;
            }
            return startNode;
        }

        public void Delete(string key)
        {
            TreeNode parent = null;

            // First find the node to delete and its parent
            TreeNode nodeToDelete = FindParent(key, ref parent);

            if (nodeToDelete == null)
            {
                throw new Exception("Unable to delete node: " + key.ToString());  // can't find node, then say so 
            }

            // Three cases to consider, leaf, one child, two children

            // If it is a simple leaf then just null what the parent is pointing to
            if ((nodeToDelete.Left == null) && (nodeToDelete.Right == null))
            {
                if (parent == null)
                {
                    _root = null;
                    return;
                }

                // find out whether left or right is associated 
                // with the parent and null as appropriate
                if (parent.Left == nodeToDelete)
                {
                    parent.Left = null;
                }
                else
                {
                    parent.Right = null;
                }

                _count--;
                return;
            }

            // One of the children is null, in this case
            // delete the node and move child up
            if (nodeToDelete.Left == null)
            {
                // Special case if we're at the _root
                if (parent == null)
                {
                    _root = nodeToDelete.Right;
                    return;
                }

                // Identify the child and point the parent at the child
                if (parent.Left == nodeToDelete)
                {
                    parent.Right = nodeToDelete.Right;
                }
                else
                {
                    parent.Left = nodeToDelete.Right;
                }

                // Clean up the deleted node
                nodeToDelete = null;
                _count--;
                return;

            }

            // One of the children is null, in this case
            // delete the node and move child up
            if (nodeToDelete.Right == null)
            {
                // Special case if we're at the _root            
                if (parent == null)
                {
                    _root = nodeToDelete.Left;
                    return;
                }

                // Identify the child and point the parent at the child
                if (parent.Left == nodeToDelete)
                {
                    parent.Left = nodeToDelete.Left;
                }
                else
                {
                    parent.Right = nodeToDelete.Left;
                }

                // Clean up the deleted node
                nodeToDelete = null;
                _count--;
                return;

            }

            // Both children have nodes, therefore find the successor, 
            // replace deleted node with successor and remove successor
            // The parent argument becomes the parent of the successor
            TreeNode successor = FindSuccessor(nodeToDelete, ref parent);

            // Make a copy of the successor node
            TreeNode tmp = new TreeNode(successor.Name);

            // Find out which side the successor parent is pointing to the
            // successor and remove the successor
            if (parent.Left == successor)
            {
                parent.Left = null;
            }
            else
            {
                parent.Right = null;
            }

            // Copy over the successor values to the deleted node position
            nodeToDelete.Name = tmp.Name;
            _count--;
        }

        public string DrawTree()
        {
            return DrawNode(_root);
        }

        public override string ToString()
        {
            return this.DrawTree();
        }

        private void KillTree(ref TreeNode p)
        {
            if (p != null)
            {
                KillTree(ref p.Left);

                KillTree(ref p.Right);

                p = null;

            }
        }

        private string DrawNode(TreeNode node)
        {
            if (node == null)
            {
                return "empty";
            }

            if (node.Left == null &&
                node.Right == null)
            {
                return node.Name;
            }
            if (node.Left != null &&
                node.Right == null)
            {
                return String.Format("{0}({1}, )", node.Name, DrawNode(node.Left));
            }
            if (node.Right != null &&
                node.Left == null)
            {
                return String.Format("{0}(_,{1})", node.Name, DrawNode(node.Right));

            }

            return String.Format("{0}({1},{2})", node.Name, DrawNode(node.Left), DrawNode(node.Right));

        }
    }
}