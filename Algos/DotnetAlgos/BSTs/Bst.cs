namespace DotnetAlgos.Bsts
{
    public class Bst
    {
        public BstNode root;
        public Bst()
        {
            this.root = null;
        }
        public void add(int value)
        {
            if(this.root == null)
            {
                this.root = new BstNode(value);
                return;
            }
            this.add(value, this.root);
        }
        private void add(int value, BstNode node)
        {
            // check right
            if(value >= node.value)
            {
                if(node.right == null)
                {
                    node.right = new BstNode(value);
                    return;
                }
                this.add(value, node.right);
            }
            // check left
            else
            {
                if(node.left == null)
                {
                    node.left = new BstNode(value);
                    return;
                }
                this.add(value, node.left);
            }
        }
    }
}